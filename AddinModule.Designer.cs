namespace nsaEmail
{
    partial class AddinModule
    {
        /// <summary>
        /// Required by designer
        /// </summary>
        private System.ComponentModel.IContainer components;
 
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
        /// Required by designer support - do not modify
        /// the following method
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.adxRibTabMailOutlookExplorer = new AddinExpress.MSO.ADXRibbonTab(this.components);
            this.adxRibGroupMailExplorer = new AddinExpress.MSO.ADXRibbonGroup(this.components);
            this.adxRibBtnSendEmail = new AddinExpress.MSO.ADXRibbonButton(this.components);
            // 
            // adxRibTabMailOutlookExplorer
            // 
            this.adxRibTabMailOutlookExplorer.Caption = "Send Custom Email";
            this.adxRibTabMailOutlookExplorer.Controls.Add(this.adxRibGroupMailExplorer);
            this.adxRibTabMailOutlookExplorer.Id = "adxRibbonTab_acca161fb0f54af5b2c04a058ad1c8ee";
            this.adxRibTabMailOutlookExplorer.IdMso = "TabMail";
            this.adxRibTabMailOutlookExplorer.Ribbons = AddinExpress.MSO.ADXRibbons.msrOutlookExplorer;
            // 
            // adxRibGroupMailExplorer
            // 
            this.adxRibGroupMailExplorer.Caption = "Send Customized Emails";
            this.adxRibGroupMailExplorer.Controls.Add(this.adxRibBtnSendEmail);
            this.adxRibGroupMailExplorer.Id = "adxRibbonGroup_f3d6b7ef9b1b4c3991561e84fcd61297";
            this.adxRibGroupMailExplorer.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.adxRibGroupMailExplorer.InsertAfterIdMso = "GroupMailRespond";
            this.adxRibGroupMailExplorer.Ribbons = AddinExpress.MSO.ADXRibbons.msrOutlookExplorer;
            // 
            // adxRibBtnSendEmail
            // 
            this.adxRibBtnSendEmail.Caption = "Send NSA Email";
            this.adxRibBtnSendEmail.Id = "adxRibbonButton_1af261fb31004f05b28711de7cd371e6";
            this.adxRibBtnSendEmail.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.adxRibBtnSendEmail.Ribbons = AddinExpress.MSO.ADXRibbons.msrOutlookExplorer;
            this.adxRibBtnSendEmail.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(this.adxRibBtnSendEmail_OnClick);
            // 
            // AddinModule
            // 
            this.AddinName = "nsaEmail";
            this.SupportedApps = AddinExpress.MSO.ADXOfficeHostApp.ohaOutlook;
            this.AddinStartupComplete += new AddinExpress.MSO.ADXEvents_EventHandler(this.AddinModule_AddinStartupComplete_1);

        }
        #endregion

        private AddinExpress.MSO.ADXRibbonTab adxRibTabMailOutlookExplorer;
        private AddinExpress.MSO.ADXRibbonGroup adxRibGroupMailExplorer;
        private AddinExpress.MSO.ADXRibbonButton adxRibBtnSendEmail;
    }
}

