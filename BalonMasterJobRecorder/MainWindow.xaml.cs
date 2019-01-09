using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Logic;
using Model;

/*
 * Small application for my office 
 * made by [aleksa.djdj  @  gmail  .  com]
 */

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
            string date = dateTextBox.Text.Replace('-', '.');
            string widthHeight;

            if (numberTimesTextBox.Text.Equals(String.Empty))
                widthHeight = String.Concat(dimensionTextBox.Text, "x", dimensionTextBox2.Text, "cm");

            else
                widthHeight = String.Concat(dimensionTextBox.Text, "x", dimensionTextBox2.Text, "cm x", numberTimesTextBox.Text, "kom");

            _balonLogic.Write(new Ballon()
            {
                Date = date,
                Store = storeSelectedItem.Content.ToString(),
                Dimension = widthHeight,
                Color = colorComboBox.Text,
                Description = descriptionTextBox.Text,
                QueryInputDate = DateTime.Now
            });

            dimensionTextBox.Text = String.Empty;
            dimensionTextBox2.Text = String.Empty;
            numberTimesTextBox.Text = String.Empty;
            descriptionTextBox.Text = String.Empty;
        }

        private void ShowData_Click(object sender, RoutedEventArgs e)
        {
            var result = _balonLogic.CreateHTMLFile();
            if(result)
            {
               var result2 = _balonLogic.LunchHtmlFile();
               if(result2 == false)
                    MessageBox.Show("Error: Can't open HTML file!");
            }
            else
                MessageBox.Show("Error: Can't creating HTML file!");
        }
    }
}
