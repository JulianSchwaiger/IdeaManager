using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IdeaManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string IDENTIFICATION_CODE = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGenerateCode_Click(object sender, RoutedEventArgs e)
        {
            //Call Backend function for generating a new user identification code.
            //Display it in the txtBox

            //popup asking if user is sure that he wants to create a new code
            lblNoCodeFound.Visibility = Visibility.Collapsed;

            SetIdentificationCode("testCode");
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            LoadIdentificationCode();
            btnDeleteCode.Visibility = Visibility.Visible;
        }


        private void LoadIdentificationCode()
        {
            var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("IdeaFinder");
            if (key == null)
            {
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("IdeaFinder");
                SettingsTab.IsSelected = true;
                lblNoCodeFound.Visibility = Visibility.Visible;
            }
            else
            {
                var idCode = key.GetValue("IdentificationCode");

                if (idCode == null)
                {
                    lblNoCodeFound.Visibility = Visibility.Visible;
                    txtCode.Text = "";
                }
                else
                {
                    txtCode.Text = idCode.ToString();
                    IDENTIFICATION_CODE = idCode.ToString();
                }
            }
            key.Close();
        }

        private void SetIdentificationCode(string newCode)
        {
            var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("IdeaFinder", true);
            if(key != null)
                key.SetValue("IdentificationCode", newCode);

            LoadIdentificationCode();
        }

        /// <summary>
        ///     USED FOR DEBUG PURPOSES ONLY
        /// </summary>
        private void DeleteIdentificationCode()
        {
            var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("IdeaFinder", true);
            if (key != null) key.DeleteValue("IdentificationCode");
        }


        /// <summary>
        ///     USED FOR DEBUG PURPOSES ONLY
        /// </summary>
        private void btnDeleteCode_Click(object sender, RoutedEventArgs e)
        {
            DeleteIdentificationCode();
            LoadIdentificationCode();
        }
    }
}
