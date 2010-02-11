using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ASCOM.FocVide
{
    [ComVisible(false)]					// Form not registered for COM!
    public partial class SetupDialogForm : Form
    {
        public SetupDialogForm()
        {
            InitializeComponent();
            Properties.Settings.Default.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Default_PropertyChanged);
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            Properties.Settings.Default.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(Default_PropertyChanged);
            Dispose();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
            Properties.Settings.Default.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(Default_PropertyChanged);
            Dispose();
        }

        private void BrowseToAscom(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://ascom-standards.org/");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void SetupDialogForm_Load(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
            HelpTempComp.Text = "If probed temperature is between " + TempMini.Value.ToString() + " K and " +
                TempMaxi.Value.ToString() + " K, then the focuser will move forward by " +
                Properties.Settings.Default.sStepPerDeg.ToString() + " steps each time the temperature increases by 1 K (and move backwards if decreases). " +
                "Temperature is checked every " + Properties.Settings.Default.sTempCompPeriod.ToString() + " seconds.";
        }

        private void IsTemperature_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.sTempCompAvailable = Properties.Settings.Default.sIsTemperature;
            Properties.Settings.Default.sTempComp = Properties.Settings.Default.sIsTemperature;
        }

        private void IsTempCompAvailable_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.sTempComp = Properties.Settings.Default.sTempCompAvailable;
        }

        void Default_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            HelpTempComp.Text = "If probed temperature is between " + TempMini.Value.ToString() + " K and " +
                TempMaxi.Value.ToString() + " K, then the focuser will move forward by " +
                Properties.Settings.Default.sStepPerDeg.ToString() + " steps each time the temperature increases by 1 K (and move backwards if decreases). " +
                "Temperature is checked every " + Properties.Settings.Default.sTempCompPeriod.ToString() + " seconds.";

        }

    }
}