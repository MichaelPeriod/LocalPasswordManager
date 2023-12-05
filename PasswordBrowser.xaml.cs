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
    /// Interaction logic for PasswordBrowser.xaml
    /// </summary>
    public partial class PasswordBrowser : Window
    {
        int currRowPos = 2;
        public PasswordBrowser()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewEntry ne = new NewEntry(this);
            ne.ShowDialog();
        }

        public void Create_Entry(LoginInformation info)
        {
            CreateLine(info);
        }

        private void CreateLine(LoginInformation _login)
        {
            RowDefinition rd = new RowDefinition();
            rd.Height = new GridLength(50);
            passwordgrid.RowDefinitions.Add(rd);

            Label newWebsite = CreateLabel(_login.GetWebsite(), 1, currRowPos);
            Label newUser = CreateLabel(_login.GetUsername(), 2, currRowPos);
            Label newPass = CreateLabel(_login.GetPassword(), 3, currRowPos);

            passwordgrid.Children.Add(newWebsite);
            passwordgrid.Children.Add(newUser);
            passwordgrid.Children.Add(newPass);

            currRowPos++;
        }

        private Label CreateLabel(String text, int gridPosX, int gridPosY)
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
