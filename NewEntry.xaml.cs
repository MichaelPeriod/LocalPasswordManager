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
using System.Windows.Shapes;

namespace LocalPasswordManager
{
    /// <summary>
    /// Interaction logic for NewEntry.xaml
    /// </summary>
    public partial class NewEntry : Window
    {
        PasswordBrowser? pb;
        
        public NewEntry()
        {
            InitializeComponent();
        }

        public NewEntry(PasswordBrowser _pb)
        {
            InitializeComponent();
            pb = _pb;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(pb != null)
            {
                LoginInformation login = new LoginInformation(
                    websitetb.Text,
                    usertb.Text,
                    passtb.Text
                    );
                pb.Create_Entry(login);
            }
            this.Close();
        }
    }
}
