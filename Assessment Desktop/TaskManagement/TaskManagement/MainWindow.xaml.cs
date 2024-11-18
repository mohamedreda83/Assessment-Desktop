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

namespace TaskManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TaskManagementEntities taskdb = new TaskManagementEntities();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (username.Text != "" && password.Password != "")
            {
                string user_name = username.Text.ToLower();
                string userpassword = password.Password;
                var login = taskdb.Users.FirstOrDefault(a => a.UserName == user_name && a.UserPassword == userpassword);
                if (login != null && login.UserRole== "Manager")
                {
                    Managementpage managementpage = new Managementpage();
                    managementpage.Show();
                    this.Close();
                }
                else if (login != null && login.UserRole == "Employee")
                {
                    ViewTaskspage viewTaskspage = new ViewTaskspage(login.UserName);
                    viewTaskspage.Show();
                    this.Close();
                }
                else if (login == null)
                {
                    MessageBox.Show($"{user_name} Not Found", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please Enter Full Information", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
