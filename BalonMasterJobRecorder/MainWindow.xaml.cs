using System;
using System.Windows;
using System.Windows.Controls;
using Logic;

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
            #region GET INPUT DATA
            string date = dateTextBox.Text.Replace('-', '.');

            var storeSelectedItem = storeListBox.SelectedItem as ListBoxItem;
            string store = storeSelectedItem.Content.ToString();

            string dimension;
            if (numberTimesTextBox.Text.Equals(String.Empty))
                dimension = String.Concat(dimensionTextBox.Text, "x", dimensionTextBox2.Text, "cm");

            else
                dimension = String.Concat(dimensionTextBox.Text, "x", dimensionTextBox2.Text, "cm x", numberTimesTextBox.Text, "kom");

            string color = colorComboBox.Text;
            string description = descriptionTextBox.Text;
   
            #endregion

            _balonLogic.Write(date, store, dimension, color, description);

            ClearTextBox();
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
            if(result)
            {
               var result2 = _balonLogic.LunchHtmlFile();
               if(result2 == false)
                    MessageBox.Show("Error: Can't open HTML file!", "ERROR");
            }
            else
                MessageBox.Show("Error: Can't creating HTML file!", "ERROR");
        }
    }
}
