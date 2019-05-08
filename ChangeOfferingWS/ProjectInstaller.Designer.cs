namespace ChangeOfferingWS
{
    partial class ProjectInstaller
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ChangeOfferingWSSrv = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller1 = new System.ServiceProcess.ServiceInstaller();
            // 
            // ChangeOfferingWSSrv
            // 
            this.ChangeOfferingWSSrv.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.ChangeOfferingWSSrv.Password = null;
            this.ChangeOfferingWSSrv.Username = null;
            // 
            // serviceInstaller1
            // 
            this.serviceInstaller1.Description = "This Process for  production- contact Fouad Abdel azeem ";
            this.serviceInstaller1.ServiceName = "ChangeOfferingWS";
            this.serviceInstaller1.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller1_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ChangeOfferingWSSrv,
            this.serviceInstaller1});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ChangeOfferingWSSrv;
        private System.ServiceProcess.ServiceInstaller serviceInstaller1;
    }
}