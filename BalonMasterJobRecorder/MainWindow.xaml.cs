using System;
using System.Windows;
using System.Windows.Controls;
using Logic;

/*
 * Small application for my office (simple data recorder)
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

            WindowLoaded();
            dateTextBox.Text = DateTime.Now.ToShortDateString();
            dateTextBox.Focus();
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
            GetInputData(out string date, out string store, out string dimension,
                         out string color, out string description);

            var result = _balonLogic.Write(date, store, dimension, color, description);

            if (result == true)
                ClearTextBox();
            else
                MessageBox.Show("Error: Can't write to database!");

        }

        private void GetInputData(out string date, out string store, out string dimension, out string color, out string description)
        {
            date = dateTextBox.Text.Replace('-', '.');
            var storeSelectedItem = storeListBox.SelectedItem as ListBoxItem;
            store = storeSelectedItem.Content.ToString();
            if (numberTimesTextBox.Text.Equals(String.Empty))
                dimension = String.Concat(dimensionTextBox.Text, "x", dimensionTextBox2.Text, "cm");
            else
                dimension = String.Concat(dimensionTextBox.Text, "x", dimensionTextBox2.Text, "cm x", numberTimesTextBox.Text, "kom");

            color = colorComboBox.Text;
            description = descriptionTextBox.Text;
        }

        private void ClearTextBox()
        {
            dimensionTextBox.Text = String.Empty;
            dimensionTextBox2.Text = String.Empty;
            numberTimesTextBox.Text = String.Empty;
            descriptionTextBox.Text = String.Empty;
        }

        private void ShowData_Click(object sender, RoutedEventArgs e)
        {
            var result = _balonLogic.CreateHTMLFile();
            if(result == true)
            {
               var result2 = _balonLogic.LunchHtmlFile();

               if(result2 == false)
                    MessageBox.Show("Error: Can't open HTML file!", "ERROR");
            }
            else
                MessageBox.Show("Error: Can't create/save HTML file!", "ERROR");
        }

        private void DateButton_Click(object sender, RoutedEventArgs e)
        {
            dateTextBox.Text = String.Empty;
            dateTextBox.Text = DateTime.Now.ToShortDateString();
        }
    }
}
