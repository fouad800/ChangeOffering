namespace ChangeOfferingWS
{
    partial class srvCdrHist
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private System.Timers.Timer ChangeOfferingWSTimer;
        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // srvCdrHist
            // 
            this.components = new System.ComponentModel.Container();
            this.ChangeOfferingWSTimer = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)
             (this.ChangeOfferingWSTimer)).BeginInit();

            this.ChangeOfferingWSTimer.Interval = double.Parse(System.Configuration.ConfigurationManager.AppSettings["TimeBand"]);
            this.ChangeOfferingWSTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.ChangeOfferingWSTimer_Elapsed);
            // MyService
            this.ServiceName = "ChangeOfferingWS";
            ((System.ComponentModel.ISupportInitialize)(this.ChangeOfferingWSTimer)).EndInit();


        }

        #endregion
    }
}
