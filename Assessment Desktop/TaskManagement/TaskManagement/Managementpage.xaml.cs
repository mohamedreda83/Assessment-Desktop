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

namespace TaskManagement
{
    /// <summary>
    /// Interaction logic for Managementpage.xaml
    /// </summary>
    public partial class Managementpage : Window
    {
        TaskManagementEntities task = new TaskManagementEntities();
        public string Task_Statu ;
        View_tasks view_ = new View_tasks();
        int select =0;
        
        public Managementpage()
        {
            InitializeComponent();
            loaddata();
            cmbview(select);
        }

        public void loaddata()
        {
            data.ItemsSource = task.View_tasks.ToList();
        }

        private void Completed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (data.SelectedItem is View_tasks view)
            {
                view_ = view;
                task_title.Text= view_.Title;
                task_id.Text = view_.Task_id.ToString();
                Task_Description.Text = view_.Task_Description;
                empteaxt.Text= view_.UserName;
                if (view_.Task_Status== "Pending")
                {
                    cmbview(0);
                }
                else if (view_.Task_Status == "In Progress")
                {
                    cmbview(1);
                }
                else if (view_.Task_Status == "In Completed")
                {
                    cmbview(2);
                }
                
            }
        }
        public void cmbview(int selectid)
        {
            List<string> tasks = new List<string>();
            tasks.Add("Pending");
            tasks.Add("In Progress");
            tasks.Add("Completed");
            cmb.ItemsSource = tasks.ToList();
            cmb.SelectedIndex = selectid;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb.SelectedItem is string st)
            {
              Task_Statu=st;
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var nameemb = task.Users.FirstOrDefault(a=> a.UserName ==empteaxt.Text);
            Task newtask = new Task()
            {
                Task_id =int.Parse(task_id.Text),
                Title = task_title.Text,
                UserID = nameemb.UserID,
                Task_Description = Task_Description.Text,
                Task_Status = Task_Statu,
                DueDate = DateTime.Now,
            };
            task.Tasks.Add(newtask);
            task.SaveChanges();
            loaddata();
            clear();
        }
        public void clear()
        {
            task_id.Text = task_title.Text = empteaxt.Text = Task_Description.Text = "";
            select = 0;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var nameemb = task.Users.FirstOrDefault(a => a.UserName == empteaxt.Text);
            var update = task.Tasks.FirstOrDefault(a => a.Task_id == view_.Task_id);
            update.Task_id = int.Parse(task_id.Text);
            update.Title = task_title.Text;
            update.UserID = nameemb.UserID;
            update.Task_Description = Task_Description.Text;
            update.Task_Status = Task_Statu.ToString();
            task.SaveChanges();
            clear();
            loaddata();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
         var delete = task.Tasks.FirstOrDefault(a => a.Task_id == view_.Task_id);
            task.Tasks.Remove(delete);
            task.SaveChanges();
            loaddata();
            clear();
        }
    }
}
