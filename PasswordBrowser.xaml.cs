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
using System.Windows.Shapes;

namespace LocalPasswordManager
{
    /// <summary>
    /// Interaction logic for PasswordBrowser.xaml
    /// </summary>
    public partial class PasswordBrowser : Window
    {
        int currRowPos = 2;
        EncryptionHandler handler;
        readonly String passwordLocation = "./passwords.bin";
        public PasswordBrowser()
        {
            InitializeComponent();
            handler = new EncryptionHandler();
            Load_Entries();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewEntry ne = new NewEntry(this);
            ne.ShowDialog();
        }

        public void Create_Entry(LoginInformation info)
        {
            using (FileStream fileStream = new FileStream(passwordLocation, FileMode.Append, FileAccess.Write, FileShare.None))
            using (BinaryWriter file = new BinaryWriter(fileStream, Encoding.UTF8, false))
            {
                String combinedLogin = info.Website.Trim() + '\0' + info.Username.Trim() + '\0' + info.Password.Trim() + '\0';
                file.Write(EncryptionHandler.encrypt(combinedLogin, "Password1")); //REMOVE HARDCODE PASS
            }
            CreateLine(info);
        }

        private void Load_Entries()
        {
            if (File.Exists(passwordLocation))
            {
                using (FileStream fileStream = new FileStream(passwordLocation, FileMode.Open, FileAccess.Read, FileShare.None))
                using (BinaryReader file = new BinaryReader(fileStream, Encoding.UTF8, false))
                {
                    while (file.BaseStream.Position != file.BaseStream.Length)
                    {
                        String text = EncryptionHandler.decrypt(file.ReadBytes(48), "Password1"); //REMOVE HARDCODE PASS
                        String[] spliced = text.Split('\0');
                        LoginInformation loginInformation = new LoginInformation(
                            spliced[0], spliced[1], spliced[2]);

                        CreateLine(loginInformation);
                    }
                }
            }
        }

        void CreateLine(LoginInformation _login)
        {
            RowDefinition rd = new RowDefinition();
            rd.Height = new GridLength(50);
            passwordgrid.RowDefinitions.Add(rd);

            Label newWebsite = CreateLabel(_login.Website, 1, currRowPos);
            Label newUser = CreateLabel(_login.Username, 2, currRowPos);
            Label newPass = CreateLabel(_login.Password, 3, currRowPos);

            passwordgrid.Children.Add(newWebsite);
            passwordgrid.Children.Add(newUser);
            passwordgrid.Children.Add(newPass);

            currRowPos++;
        }

        Label CreateLabel(String text, int gridPosX, int gridPosY)
        {
            Label label = new Label();
            label.Content = text;
            label.Foreground = new SolidColorBrush(Colors.White);
            label.FontSize = 25;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.SetValue(Grid.RowProperty, gridPosY);
            label.SetValue(Grid.ColumnProperty, gridPosX);
            return label;
        }
    }
}
