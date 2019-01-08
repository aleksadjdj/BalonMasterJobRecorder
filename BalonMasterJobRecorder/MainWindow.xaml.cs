using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Logic;
using Model;

namespace BalonMasterJobRecorder
{
    public partial class MainWindow : Window
    {
        private readonly BalonLogic _balonLogic = new BalonLogic();

        public MainWindow()
        {
            InitializeComponent();

            dateTextBox.Focus();
            dateTextBox.Text = DateTime.Now.ToShortDateString();
            WindowLoaded();
        }

        private void WindowLoaded()
        {
            var result = _balonLogic.TestDBConnection();
            
            if(result == false)
            {
                MessageBox.Show("Error: Can't connect to DB", "ERROR");
                this.Close();
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            var storeSelectedItem = storeListBox.SelectedItem as ListBoxItem;

            _balonLogic.Write(new Ballon()
            {
                Date = dateTextBox.Text,
                Store = storeSelectedItem.Content.ToString(),
                Dimension = dimensionTextBox.Text,
                Color = colorComboBox.Text,
                Description = descriptionTextBox.Text,
                QueryInputDate = DateTime.Now
            });

            dimensionTextBox.Text = "";
            descriptionTextBox.Text = "";


        }

        private void ShowData_Click(object sender, RoutedEventArgs e)
        {
            _balonLogic.CreateHTMLFile();
        }
    }
}
