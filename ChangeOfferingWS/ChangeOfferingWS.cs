using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using Database.DAL;
using System.Net;
using System.Xml;
using System.Configuration;
namespace ChangeOfferingWS
{
    public partial class srvCdrHist : ServiceBase
    {
        public srvCdrHist()
        {
            InitializeComponent();
        }
        public static string ESPCon = "";
        public static string CRMPCon = "";
        public static int ApplyVAT = 0;
        public static string ControlService = "0";
        public static string HybridOffers = "";
        protected override void OnStart(string[] args)
        {
            this.ChangeOfferingWSTimer.Enabled = true;
            Intialize();
        }
        public void Intialize()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            
                Start();
            }
            catch (Exception ex)
            {
                MyLogEvent(ex.Message);
            }
        }
        protected override void OnStop()
        {
            this.ChangeOfferingWSTimer.Enabled = false;
        }
        protected override void OnContinue()
        {
            
            Start();
        }
        private void ChangeOfferingWSTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Start();
        }
        public void Start()
        {
            try
            {
                ChangeOfferingBusiness.Common.Writelog=ConfigurationManager.AppSettings["Writelog"].ToString() == "1" ? true : false;
               
                ControlService =ConfigurationManager.AppSettings["ControlService"];
                this.ChangeOfferingWSTimer.Interval = double.Parse(ConfigurationManager.AppSettings["TimeBand"]);
                if(ControlService=="1")
                {
                    ChangeOfferingBusiness.Common MyCo = new ChangeOfferingBusiness.Common();
                    MyCo.Download();
                }
            }
            catch (Exception ex)
            {
                MyLogEvent(ex.Message);
            }
        }
        private static void MyLogEvent(string Message)
        {
            try
            {
                bool flag = ConfigurationManager.AppSettings["Writelog"].ToString() == "1" ? true : false;
                if (flag)
                    if (!EventLog.SourceExists("ChangeOfferingWS"))
                        EventLog.CreateEventSource("ChangeOfferingWS", "ChangeOfferingWS");

                EventLog.WriteEntry("ChangeOfferingWS", Message);
            }
            catch (Exception)
            {
                
            }
        }
    }
}
