using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;
using AddinExpress.MSO;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace nsaEmail
{
    /// <summary>
    ///   Add-in Express Add-in Module
    /// </summary>
    [GuidAttribute("7A27042D-0058-4172-9788-B1A1CD6A2D36"), ProgId("nsaEmail.AddinModule")]
    public partial class AddinModule : AddinExpress.MSO.ADXAddinModule
    {
        public AddinModule()
        {
            Application.EnableVisualStyles();
            InitializeComponent();
            // Please add any initialization code to the AddinInitialize event handler
        }
 
        #region Add-in Express automatic code
 
        // Required by Add-in Express - do not modify
        // the methods within this region
 
        public override System.ComponentModel.IContainer GetContainer()
        {
            if (components == null)
                components = new System.ComponentModel.Container();
            return components;
        }
 
        [ComRegisterFunctionAttribute]
        public static void AddinRegister(Type t)
        {
            AddinExpress.MSO.ADXAddinModule.ADXRegister(t);
        }
 
        [ComUnregisterFunctionAttribute]
        public static void AddinUnregister(Type t)
        {
            AddinExpress.MSO.ADXAddinModule.ADXUnregister(t);
        }
 
        public override void UninstallControls()
        {
            base.UninstallControls();
        }

        #endregion

        public static new AddinModule CurrentInstance 
        {
            get
            {
                return AddinExpress.MSO.ADXAddinModule.CurrentInstance as AddinModule;
            }
        }

        public Outlook._Application OutlookApp
        {
            get
            {
                return (HostApplication as Outlook._Application);
            }
        }

        private void adxRibBtnSendEmail_OnClick(object sender, IRibbonControl control, bool pressed)
        {
            try { 
            OutlookClass outlookClass = new OutlookClass();
                outlookClass.SendEmail();
            } catch (Exception ex) {

                MessageBox.Show("Exception in adxRibBtnSendEmail_OnClick" + ex.Message);
            }
        }

        private void AddinModule_AddinStartupComplete_1(object sender, EventArgs e)
        {
            try
            {

                GlobalClass.outlookApplication = (Microsoft.Office.Interop.Outlook.Application)HostApplication;

            }
            catch (Exception ex)
            {




                MessageBox.Show("Exception in AddinModule_AddinStartupComplete" + ex.Message);

            }
        }
    }
}

