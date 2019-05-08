using System;
using System.Data;
using System.Collections;

namespace Database.DAL
{
    public class ChangePrimaryBus
    {
        public ChangePrimaryBus()
        {
        }
        public int ID { get; set; }
        public string MSISDN { get; set; }
        public string receive_date { get; set; }
        public string SMS_content { get; set; }
        public string SMS_Options { get; set; }
        public string Reply_Option { get; set; }
        public int status { get; set; }
        public string error_desc { get; set; }
        public int request_type { get; set; }
    }

public class ChangePrimaryBusList
{
	/// <summary>
	/// Default Contructor
	/// <summary>
	public ChangePrimaryBusList()
	{
	}

	public  string strFieldsToSelect="*";
	public  string strTableName="ChangePrimaryBus";
	public DBHandlerMySQL DH = new DBHandlerMySQL();

	public  ArrayList GetList(string Condition)
	{
		ArrayList arrChangePrimaryBusList =  new ArrayList();
		ChangePrimaryBus ChangePrimaryBusObj;
		try
		{
			DataTable dtChangePrimaryBus = DH.ExecuteDataTable("select " + strFieldsToSelect + " from " + strTableName+" Where "+Condition);
			foreach(DataRow dr in dtChangePrimaryBus.Rows)
			{
				ChangePrimaryBusObj = new ChangePrimaryBus();
				if(dr["ID"] != DBNull.Value)
					{
						ChangePrimaryBusObj.ID=int.Parse(dr["ID"].ToString());
					}
				if(dr["MSISDN"] != DBNull.Value)
					{
						ChangePrimaryBusObj.MSISDN=dr["MSISDN"].ToString();
					}
				if(dr["receive_date"] != DBNull.Value)
					{
						ChangePrimaryBusObj.receive_date=dr["receive_date"].ToString();
					}
				if(dr["SMS_content"] != DBNull.Value)
					{
						ChangePrimaryBusObj.SMS_content=dr["SMS_content"].ToString();
					}
                    if (dr["SMS_Options"] != DBNull.Value)
                    {
                        ChangePrimaryBusObj.SMS_Options = dr["SMS_Options"].ToString();
                    }
                if (dr["Reply_Option"] != DBNull.Value)
                    {
                        ChangePrimaryBusObj.Reply_Option = dr["Reply_Option"].ToString();
                    }
                    if (dr["status"] != DBNull.Value)
					{
						ChangePrimaryBusObj.status=int.Parse(dr["status"].ToString());
					}
				if(dr["error_desc"] != DBNull.Value)
					{
						ChangePrimaryBusObj.error_desc=dr["error_desc"].ToString();
					}
				if(dr["request_type"] != DBNull.Value)
					{
						ChangePrimaryBusObj.request_type=int.Parse(dr["request_type"].ToString());
					}
				arrChangePrimaryBusList.Add(ChangePrimaryBusObj);
			}
		}
		catch(Exception ex)
		{
			throw ex;
		}
		return arrChangePrimaryBusList;
	}

	public  ChangePrimaryBus Get(string Condition)
	{
		ChangePrimaryBus ChangePrimaryBusObj = new ChangePrimaryBus();
		try
		{
			DataTable dtChangePrimaryBus = DH.ExecuteDataTable("select " + strFieldsToSelect + " from " + strTableName+" Where "+Condition);
			if(dtChangePrimaryBus.Rows.Count > 0)
			{
				DataRow dr = dtChangePrimaryBus.Rows[0];
				if(dr["ID"] != DBNull.Value)
					{
						ChangePrimaryBusObj.ID=int.Parse(dr["ID"].ToString());
					}
				if(dr["MSISDN"] != DBNull.Value)
					{
						ChangePrimaryBusObj.MSISDN=dr["MSISDN"].ToString();
					}
				if(dr["receive_date"] != DBNull.Value)
					{
						ChangePrimaryBusObj.receive_date=dr["receive_date"].ToString();
					}
				if(dr["SMS_content"] != DBNull.Value)
					{
						ChangePrimaryBusObj.SMS_content=dr["SMS_content"].ToString();
					}
                 if (dr["SMS_Options"] != DBNull.Value)
                    {
                        ChangePrimaryBusObj.SMS_Options = dr["SMS_Options"].ToString();
                    }
                    if (dr["Reply_Option"] != DBNull.Value)
                    {
                        ChangePrimaryBusObj.Reply_Option = dr["Reply_Option"].ToString();
                    }
                    if (dr["status"] != DBNull.Value)
					{
						ChangePrimaryBusObj.status=int.Parse(dr["status"].ToString());
					}
				if(dr["error_desc"] != DBNull.Value)
					{
						ChangePrimaryBusObj.error_desc=dr["error_desc"].ToString();
					}
				if(dr["request_type"] != DBNull.Value)
					{
						ChangePrimaryBusObj.request_type=int.Parse(dr["request_type"].ToString());
					}
			}
		}
		catch(Exception ex)
		{
			throw ex;
		}
		return ChangePrimaryBusObj;
	}

	/// <summary>
	///Return DataTable from Table View to bind it with any Databind control 
	/// <summary>
	public  DataTable GetDataTable(string Condition)
	{
		try
		{
			string Vw_Name="ChangePrimaryBus";
			return DH.ExecuteDataTable("Select * From "+Vw_Name+" Where "+Condition);
		}
		catch(Exception ex)
		{
			throw ex;
		}
	}

	public  int Update(ChangePrimaryBus ChangePrimaryBusObj)
	{
		try
		{
			string strUpdate = "update " + strTableName + " set ";
			string strSets = "";
			string strWhere = " where 1=1" +""+" and ID=" + ChangePrimaryBusObj.ID;
				if(ChangePrimaryBusObj.MSISDN!= null)
				{
					strSets += "MSISDN="+DH.ToDBString(ChangePrimaryBusObj.MSISDN);
				}
				else
				{
					strSets += "MSISDN=null";
				}
				
				if(ChangePrimaryBusObj.SMS_content!= null)
				{
					strSets += ",SMS_content="+DH.ToDBString(ChangePrimaryBusObj.SMS_content);
				}
				else
				{
					strSets += ",SMS_content=null";
				}
                if (ChangePrimaryBusObj.SMS_Options != null)
                {
                    strSets += ",SMS_Options=" + DH.ToDBString(ChangePrimaryBusObj.SMS_Options);
                }
                else
                {
                    strSets += ",SMS_Options=null";
                }
                if (ChangePrimaryBusObj.Reply_Option != null)
                {
                    strSets += ",Reply_Option=" + DH.ToDBString(ChangePrimaryBusObj.Reply_Option);
                }
                else
                {
                    strSets += ",Reply_Option=null";
                }
                if (ChangePrimaryBusObj.status>=0)
				{
					strSets += ",status="+ChangePrimaryBusObj.status;
				}
				else
				{
					strSets += ",status=null";
				}
				if(ChangePrimaryBusObj.error_desc!= null)
				{
					strSets += ",error_desc="+DH.ToDBString(ChangePrimaryBusObj.error_desc);
				}
				else
				{
					strSets += ",error_desc=null";
				}
				if(ChangePrimaryBusObj.request_type>=0)
				{
					strSets += ",request_type="+ChangePrimaryBusObj.request_type;
				}
				else
				{
					strSets += ",request_type=null";
				}
				if(strSets.Length > 0)
				{
					DH.ExecuteNonQuery(strUpdate + strSets + strWhere);
				}
			return 1;
		}
		catch(Exception ex)
		{
			throw ex;
		}
	}

	public  int Add(ChangePrimaryBus ChangePrimaryBusObj)
	{
		try
		{
			string strColumns = "";
			string strValues = "";
				strColumns += "MSISDN";
				if(ChangePrimaryBusObj.MSISDN!= null)
				{
					strValues += ""+DH.ToDBString(ChangePrimaryBusObj.MSISDN);
				}
				else
				{
					strValues += "null";
				}
				strColumns += ",receive_date";
				if(ChangePrimaryBusObj.receive_date!= null)
				{
					strValues += ","+DH.ToDBString(ChangePrimaryBusObj.receive_date);
				}
				else
				{
					strValues += ",null";
				}
				strColumns += ",SMS_content";
				if(ChangePrimaryBusObj.SMS_content!= null)
				{
					strValues += ","+DH.ToDBString(ChangePrimaryBusObj.SMS_content);
				}
				else
				{
					strValues += ",null";
				}
                strColumns += ",SMS_Options";
                if (ChangePrimaryBusObj.SMS_Options != null)
                {
                    strValues += "," + DH.ToDBString(ChangePrimaryBusObj.SMS_Options);
                }
                else
                {
                    strValues += ",null";
                }
                strColumns += ",Reply_Option";
                if (ChangePrimaryBusObj.Reply_Option != null)
                {
                    strValues += "," + DH.ToDBString(ChangePrimaryBusObj.Reply_Option);
                }
                else
                {
                    strValues += ",null";
                }
                strColumns += ",status";
				if(ChangePrimaryBusObj.status>=0)
				{
					strValues += ","+ChangePrimaryBusObj.status;
				}
				else
				{
					strValues += ",null";
				}
				strColumns += ",error_desc";
				if(ChangePrimaryBusObj.error_desc!= null)
				{
					strValues += ","+DH.ToDBString(ChangePrimaryBusObj.error_desc);
				}
				else
				{
					strValues += ",null";
				}
				strColumns += ",request_type";
				if(ChangePrimaryBusObj.request_type>=0)
				{
					strValues += ","+ChangePrimaryBusObj.request_type;
				}
				else
				{
					strValues += ",null";
				}
				DH.ExecuteNonQuery("insert into " + strTableName + "(" + strColumns + ") values(" + strValues + ")");
				return 1;
		}
		catch(Exception ex)
		{
			throw ex;
		}
	}

	public  void Delete(string Condition)
	{
		try
		{
			DH.ExecuteNonQuery("Delete from " + strTableName +" Where "+Condition);
		}
		catch(Exception ex)
		{
			throw ex;
		}
	}
}
}

