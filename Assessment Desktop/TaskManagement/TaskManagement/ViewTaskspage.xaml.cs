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
    /// Interaction logic for ViewTaskspage.xaml
    /// </summary>
    public partial class ViewTaskspage : Window
    {
        TaskManagementEntities task=new TaskManagementEntities();
        Task newtask = new Task();
        public string Task_Status;
        public ViewTaskspage(string name)
        {
            InitializeComponent();
            labelname.Content = name;
            viewdata();
            viewdataCompleted();
             cmbview();
        }
        public void viewdata()
        {
            
                InProgress.ItemsSource = task.Tasks.Where(a => a.Task_Status == "Pending" || a.Task_Status == "In Progress").ToList();

        }
        public void viewdataCompleted()
        {
            Completed.ItemsSource = task.Tasks.Where(a => a.Task_Status == "Completed" ).ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            newtask.Task_Status = Task_Status;
            task.SaveChanges();
            viewdata();
            viewdataCompleted();
        }
        public void cmbview() {
            List<string> tasks = new List<string>();
            tasks.Add("Pending");
            tasks.Add("In Progress");
            tasks.Add("Completed");
            cmb.ItemsSource =tasks.ToList(); 
            cmb.SelectedIndex = 2;
                }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb.SelectedItem is string st)
            {
                Task_Status=st;
            }
        }

        private void InProgress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InProgress.SelectedItem is Task task)
            {
                newtask = task;
                idlabel.Content = task.Task_id.ToString();
            }
        }

        private void Completed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Completed.SelectedItem is Task task)
            {
                newtask = task;
                idlabel.Content = task.Task_id.ToString();
            }
        }
    }
}
