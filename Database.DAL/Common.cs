using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Net;
using System.IO;
using System.Xml.Linq;
using Database.DAL;
namespace ChangeOfferingBusiness
{
    public class Common
    {
       
        public static bool Writelog = true;

        public  List<string> ParseSOAPResponse(string soapResult, string NodeNameSpace, string NodeName)
        {
            TextReader tr = new StringReader(soapResult);
            XDocument doc = XDocument.Load(tr);
            IEnumerable<XElement> xNames;
            XNamespace ns = NodeNameSpace;
            xNames = doc.Descendants(ns + NodeName);
            List<string> list = new List<string>();
            list= xNames.Select(item => item.ToString()).ToList();
            return list;
        }

        public  string BinaryToText(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }
        public  string getXMLTagValue(string TAG)
        {
            return (TAG.Substring(TAG.IndexOf(">") + 1, TAG.IndexOf("</") - TAG.IndexOf(">") - 1));
        }
        public  string getXMLTagValue(string TAG, string tagName)
        {
            string endtag = tagName.Replace("<", "</");
            return (TAG.Substring(TAG.IndexOf(tagName) + tagName.Length, TAG.IndexOf(endtag) - TAG.IndexOf(tagName) - endtag.Length + 1));
        }
        private  string GetValueFromXmlTag(string xml, string tag)
        {
            if (xml == null || tag == null || xml.Length == 0 || tag.Length == 0)
                return "";

            string
                startTag = "<" + tag + ">",
                endTag = "</" + tag + ">",
                value = null;

            int
                startTagIndex = xml.IndexOf(tag, StringComparison.OrdinalIgnoreCase),
                endTagIndex = xml.IndexOf(endTag, StringComparison.OrdinalIgnoreCase);


            if (startTagIndex < 0 || endTagIndex < 0)
                return "";

            int valueIndex = startTagIndex += startTag.Length - 1;

            try
            {
                value = xml.Substring(valueIndex, endTagIndex - valueIndex);
            }
            catch (ArgumentOutOfRangeException)
            {
                string err = string.Format("Error reading value for \"{0}\" tag from XXX XML", tag);
                //log.Error(err, responseXmlParserEx);
            }

            return (value ?? "");
        }
        public  void MyLogEvent(Exception ex)
        {
            try
            {
                if (Writelog)
                    if (!EventLog.SourceExists("ChangeOffering"))
                        EventLog.CreateEventSource("ChangeOffering", "ChangeOffering");
                LogException(ex, ex.Source);
                EventLog.WriteEntry("ChangeOffering", ex.Message);
                //SendSMS("ChangeOffering  -" + ex.Message, "577773380");
            }
            catch (Exception)
            {

            }
        }
        public  void LogException(Exception exc, string source)
        {
            // Include enterprise logic for logging exceptions 
            // Get the absolute path to the log file 
            string logFile = "ErrorLog" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            // Open the log file for append and write the log
            StreamWriter sw = new StreamWriter(logFile, true);
            sw.WriteLine("********** {0} **********", DateTime.Now);
            if (exc.InnerException != null)
            {
                sw.Write("Inner Exception Type: ");
                sw.WriteLine(exc.InnerException.GetType().ToString());
                sw.Write("Inner Exception: ");
                sw.WriteLine(exc.InnerException.Message);
                sw.Write("Inner Source: ");
                sw.WriteLine(exc.InnerException.Source);
                if (exc.InnerException.StackTrace != null)
                {
                    sw.WriteLine("Inner Stack Trace: ");
                    sw.WriteLine(exc.InnerException.StackTrace);
                }
            }
            sw.Write("Exception Type: ");
            sw.WriteLine(exc.GetType().ToString());
            sw.WriteLine("Exception: " + exc.Message);
            sw.WriteLine("Source: " + source);
            sw.WriteLine("Stack Trace: ");
            if (exc.StackTrace != null)
            {
                sw.WriteLine(exc.StackTrace);
                sw.WriteLine();
            }
            sw.Close();
        }
        public  void SendSMS(string content, string receiptno)
            {
                try
                {
                    string xmlPayload = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\""
                                                        + " xmlns:util=\"http://www.huawei.com/bss/soaif/interface/UtilityService/\" "
                                                        + " xmlns:com=\"http://www.huawei.com/bss/soaif/interface/common/\">"
                                               + "<soapenv:Header/>"
                                               + "<soapenv:Body>"
                                                  + "<util:SendSMSReqMsg>"
                                                     + "<com:ReqHeader>"
                                                        + ""
                                                        + "<com:Version>1</com:Version>"
                                                        + "<com:Channel>82</com:Channel>"
                                                        + ""
                                                        + "<com:BrandId>101</com:BrandId>"
                                                        + "<com:ReqTime>"+ DateTime.Now.ToString("yyzMMddhhmmssfffff") + "</com:ReqTime>"
                                                        + "<com:AccessUser>LebaraMobile</com:AccessUser>"
                                                        + "<com:AccessPassword>yWJLEODB1+RwJXKe8DFAXToB7wgYyFc6yxflNUz8TYI=</com:AccessPassword>"
                                                        + ""
                                                        + "<com:OperatorId>101</com:OperatorId>"
                                                     + "</com:ReqHeader>"
                                                     + "<!--1 to 100 repetitions:-->"
                                                     + "<util:SMSInfo>"
                                                        + "<util:BatchSeqId>1111</util:BatchSeqId>"
                                                        + "<util:Content>" + content + "</util:Content>"
                                                        + "<util:DestinationNum>" + receiptno + "</util:DestinationNum>"
                                                        + ""
                                                        + "<util:SourceNum>1755</util:SourceNum>"
                                                     + "</util:SMSInfo>"
                                                  + "</util:SendSMSReqMsg>"
                                               + "</soapenv:Body>"
                                            + "</soapenv:Envelope>";
                    string URL_ADDRESS = "http://10.200.102.83:7081/SELFCARE/HWBSS_Utility";
                    string action = "SendSMS";
                    HttpWebRequest request = WebRequest.Create(new Uri(URL_ADDRESS)) as HttpWebRequest;

                    // Set type to POST
                    request.Method = "POST";
                    request.Headers.Add("SOAPAction", action);
                    StringBuilder data = new StringBuilder();
                    data.Append(xmlPayload);
                    byte[] byteData = Encoding.UTF8.GetBytes(data.ToString());      // Create a byte array of the data we want to send
                    request.ContentLength = byteData.Length;
                    // Set the content length in the request headers

                    using (Stream postStream = request.GetRequestStream())
                    {
                        postStream.Write(byteData, 0, byteData.Length);
                    }

                    // Get response and return it
                    XmlDocument xmlResult = new XmlDocument();

                    IAsyncResult asyncResult = request.BeginGetResponse(null, null);
                    asyncResult.AsyncWaitHandle.WaitOne();
                    string soapResult;
                    using (WebResponse webResponse = request.EndGetResponse(asyncResult))
                    {
                        using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                        {
                            soapResult = rd.ReadToEnd();
                        }

                    }

                }
                catch (Exception e)
                {
                    MyLogEvent(e);
                }
            }
        public void Download()
        {
            try
            {
                DBHandlerMySQL DH = new DBHandlerMySQL();
                ChangeOfferingBusiness.Common CommonObj = new ChangeOfferingBusiness.Common();
                ChangePrimaryBusList ChangePrimaryBuslst = new ChangePrimaryBusList();
                string SQLinsert = string.Empty;
                string msg = "Choose offer by enter \nCO(space)number\n---------";
                string Offers = "";
                int i = 1;
                DataTable dtRequest = DH.ExecuteDataTable("select *  from SmsServer.Messages where CustomField1=0 and (lower(body)='co' or lower(body)like 'co %')");
                foreach (DataRow dr in dtRequest.Rows)
                {
                    ChangePrimaryBus ChangePrimaryBusObj = new ChangePrimaryBus();
                    ChangePrimaryBusObj.MSISDN = dr["FromAddress"].ToString().Substring(3);
                    ChangePrimaryBusObj.SMS_content = dr["Body"].ToString();
                    int Option = 0;
                    if (dr["Body"].ToString().ToLower().Equals("co"))
                    {
                        ChangePrimaryBusObj.status = 0;
                        ChangePrimaryBusObj.request_type = 0;
                        DataTable dt = CommonObj.GetMappingPrimaryOffer(ChangePrimaryBusObj.MSISDN);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow drr in dt.Rows)
                            {
                                msg += "\n" + i++.ToString() + "-" + drr["offer_name"];
                                Offers +=",["+ (i-1) + "]:" + drr["offer_id"];
                            }
                            CommonObj.SendSMS(msg, ChangePrimaryBusObj.MSISDN);
                        }
                        ChangePrimaryBusObj.SMS_Options = Offers==""?"":Offers.Substring(1);
                        ChangePrimaryBuslst.Add(ChangePrimaryBusObj);
                    }
                    else if (dr["Body"].ToString().Split(' ').Length == 2)
                    {
                        if (dr["Body"].ToString().Split(' ')[0].ToLower() == "co")
                        {
                             bool res =int.TryParse(dr["Body"].ToString().Split(' ')[1],out Option);
                            if (res == true)
                            {
                                ChangePrimaryBusObj=ChangePrimaryBuslst.Get("MSISDN='" + ChangePrimaryBusObj.MSISDN + "' and status=0 and request_type=0 order by receive_date desc limit 1");
                                if (ChangePrimaryBusObj.SMS_Options == null)
                                {
                                    CommonObj.SendSMS("send co to see the available offers\n then reply with co number", dr["FromAddress"].ToString().Substring(3));
                                }
                                else
                                {
                                    string offer_ID="";
                                    ChangePrimaryBusObj.Reply_Option = dr["Body"].ToString().Split(' ')[1].ToString();
                                    foreach (string s in ChangePrimaryBusObj.SMS_Options.Split(','))
                                    {
                                        if(s.IndexOf("["+ ChangePrimaryBusObj.Reply_Option + "]:")>=0)
                                        {
                                            offer_ID = s.Split(':')[1];
                                        }
                                    }
                                    if (offer_ID.Equals(""))
                                    {
                                        ChangePrimaryBusObj.status = -1;
                                        ChangePrimaryBusObj.request_type = -1;
                                    }
                                    else
                                    {
                                        ChangePrimaryBusObj.status = 1;
                                        ChangePrimaryBusObj.request_type = 1;
                                        string result = CommonObj.ChangeOffering(ChangePrimaryBusObj.MSISDN, offer_ID);
                                        ChangePrimaryBusObj.request_type=int.Parse(CommonObj.GetValueFromXmlTag(result, "com:ReturnCode"));
                                        ChangePrimaryBusObj.error_desc = CommonObj.GetValueFromXmlTag(result, "com:ReturnMsg");
                                        CommonObj.SendSMS("Your change offer  result is \n"+ChangePrimaryBusObj.error_desc, ChangePrimaryBusObj.MSISDN);
                                        ChangePrimaryBuslst.Update(ChangePrimaryBusObj);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        ChangePrimaryBusObj.status =-1;
                        ChangePrimaryBusObj.error_desc = "Invalid request";
                        CommonObj.SendSMS("Invalid reply", ChangePrimaryBusObj.MSISDN);
                    }
                    DH.ExecuteNonQuery("update SmsServer.Messages set CustomField1=2 where ID=" + dr["ID"].ToString());
                } //end for loop
            }
            catch (Exception e)
            {
                MyLogEvent(e);
            }
        }
        public void process()
        {
            // string x= CommonObj.ChangeOffering("578708948", "526878875", "100000001");
        }
        public string ChangeOffering(string MSISDN, string NewOffer)
        {
            string soapResult = "";
            try
            {
                string xmlPayload = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:off=\"http://www.huawei.com/bss/soaif/interface/OfferingService/\" xmlns:com=\"http://www.huawei.com/bss/soaif/interface/common/\">";
                xmlPayload += @"<soapenv:Header/>
                               <soapenv:Body>
                                  <off:ChangePrimaryOfferingReqMsg>
                                     <com:ReqHeader>
                                        <com:Version>version_1.1</com:Version>
                                        <com:BusinessCode>ChangePrimaryOffering</com:BusinessCode>
                                        <com:TransactionId>" + DateTime.Now.AddDays(-1).ToString("yyyyMMddhhmmssfffff") + @"</com:TransactionId>
                                        <com:Channel>82</com:Channel>
                                        <com:PartnerId>101</com:PartnerId>
                                        <com:BrandId>LEBARA</com:BrandId>
                                        <com:ReqTime>" + DateTime.Now.AddDays(-1).ToString("yyyyMMddhhmmss") + @"</com:ReqTime>
                                        <com:TimeFormat>
                                           <com:TimeType>2</com:TimeType>
                                           <com:TimeZoneID>100</com:TimeZoneID>
                                        </com:TimeFormat>
                                        <com:AccessUser>hwbss</com:AccessUser>
                                        <com:AccessPassword>ZLgBMiMcuOOINwLKq1XgUTG5DdleKozU0LHj58MqNxg=</com:AccessPassword>
                                        <com:OperatorId>101</com:OperatorId>
                                     </com:ReqHeader>
                                     <off:AccessInfo>
                                        <com:ObjectIdType>4</com:ObjectIdType>
                                        <com:ObjectId>" + MSISDN + @"</com:ObjectId>
                                     </off:AccessInfo>
                                     <off:OldPrimaryOffering>
                                        <com:OfferingId>" + GetPrimaryOfferID(MSISDN) + @"</com:OfferingId>
                                        <com:PurchaseSeq>1111</com:PurchaseSeq>
                                     </off:OldPrimaryOffering>
                                     <off:NewPrimaryOffering>
                                        <com:OfferingId>
                                           <com:OfferingId>" + NewOffer + @"</com:OfferingId>
                                           <com:PurchaseSeq>" + DateTime.Now.AddDays(-1).ToString("yyMMddhhmmss") + @" </com:PurchaseSeq>
                                        </com:OfferingId>
                                        <com:Contract>
                                           <com:AdditionalProperty>
                                              <com:Code>18</com:Code>
                                              <com:Value>19</com:Value>
                                           </com:AdditionalProperty>
                                        </com:Contract>
                                        <off:EffectiveMode>
                                           <com:Mode>I</com:Mode>
                                           <com:EffectiveDate>" + DateTime.Now.AddDays(-1).ToString("yyyyMMddhhmmss") + @"</com:EffectiveDate>
                                        </off:EffectiveMode>
                                     </off:NewPrimaryOffering>
                                  </off:ChangePrimaryOfferingReqMsg>
                               </soapenv:Body>
                            </soapenv:Envelope>";
                string URL_ADDRESS = "http://10.200.102.83:7081/SELFCARE/HWBSS_Offering";
                string action = "ChangePrimaryOffering";
                HttpWebRequest request = WebRequest.Create(new Uri(URL_ADDRESS)) as HttpWebRequest;

                // Set type to POST
                request.Method = "POST";
                request.Headers.Add("SOAPAction", action);
                StringBuilder data = new StringBuilder();
                data.Append(xmlPayload);
                byte[] byteData = Encoding.UTF8.GetBytes(data.ToString());      // Create a byte array of the data we want to send
                request.ContentLength = byteData.Length;
                // Set the content length in the request headers

                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(byteData, 0, byteData.Length);
                }

                // Get response and return it
                XmlDocument xmlResult = new XmlDocument();
                IAsyncResult asyncResult = request.BeginGetResponse(null, null);
                asyncResult.AsyncWaitHandle.WaitOne();

                using (WebResponse webResponse = request.EndGetResponse(asyncResult))
                {
                    using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                    }

                }
            }
            catch (Exception e)
            {
                MyLogEvent(e);
            }
            return soapResult;
        }
        public string GetPrimaryOfferID(string MSISDN)
        {
            string OfferID = "";
            try
            {

                string SQL = @"select b.offer_ID from CCARE.INF_SUBSCRIBER_ALL a ,CCARE.PDM_OFFER b,CCARE.INF_OFFERS c
                        where a.sub_id = c.sub_id and c.offer_ID = b.OFFER_ID AND A.msisdn = '" + MSISDN + @"' AND A.SUB_STATE = 'B01' and b.primary_flag = 1";
                DBHandler Dh = new DBHandler();
                DataTable dt = Dh.ExecuteDataTable(SQL);
                if (dt.Rows.Count > 0)
                    OfferID = dt.Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                MyLogEvent(e);
            }
            return OfferID;
        }
        public string GetPrimaryOfferName(string MSISDN)
        {
            string OfferName = "";
            try
            {

                string SQL = @"select b.offer_Name from CCARE.INF_SUBSCRIBER_ALL a ,CCARE.PDM_OFFER b,CCARE.INF_OFFERS c
                        where a.sub_id = c.sub_id and c.offer_ID = b.OFFER_ID AND A.msisdn = '" + MSISDN + @"' AND A.SUB_STATE = 'B01' and b.primary_flag = 1";
                DBHandler Dh = new DBHandler();
                DataTable dt = Dh.ExecuteDataTable(SQL);
                if (dt.Rows.Count > 0)
                    OfferName = dt.Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                MyLogEvent(e);
            }
            return OfferName;
        }
        public DataTable GetMappingPrimaryOffer(string MSISDN)
        {
            DataTable dt=null ;
            try
            {

                string SQL = @"SELECT offer_id,offer_name FROM CRMPUB.PDM_OFFER WHERE offer_id in (select O_offer_id from CRMPUB.PDM_OFFER_MIGRATE where offer_id='"+ GetPrimaryOfferID(MSISDN)+ "')";
                DBHandler Dh = new DBHandler();
                dt=Dh.ExecuteDataTable(SQL);
                           }
            catch (Exception e)
            {
                MyLogEvent(e);
            }
            return dt;
        }
    }
}
