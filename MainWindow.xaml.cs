using System;
using System.Collections.Generic;
using System.IO;
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

namespace LocalPasswordManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly String testkeyLoc = ".\\testkey.bin";
        private static readonly String passwordVerificationTest = "VerifyPassword";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Password verification
            if (!File.Exists(testkeyLoc))
            {
                using (FileStream fileStream = new FileStream(testkeyLoc, FileMode.Create, FileAccess.Write, FileShare.None))
                using (BinaryWriter file = new BinaryWriter(fileStream, Encoding.UTF8, false))
                {
                    //Generate password on first run through application
                    file.Write(EncryptionHandler.encrypt(passwordVerificationTest, passwordtb.Text));
                }
            }
            else
            {
                using (FileStream fileStream = new FileStream(testkeyLoc, FileMode.Open, FileAccess.Read, FileShare.None))
                using (BinaryReader file = new BinaryReader(fileStream, Encoding.UTF8, false))
                {
                    String testPassword = EncryptionHandler.decrypt(file.ReadBytes((int)file.BaseStream.Length), passwordtb.Text);
                    if (testPassword != passwordVerificationTest) return;
                }
            }
                
            //Pass the key
            PasswordBrowser pb = new PasswordBrowser();
            pb.password = passwordtb.Text;
            this.Hide();
            pb.ShowDialog();
            this.Close();
        }
    }
}
