using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;

namespace MappingApplication
{
    public partial class Diary : PhoneApplicationPage
    {


        //need to  instantiate an instance of IsolatedStorageSettings to store
        //data on a WP8 app
        IsolatedStorageSettings isoSettings = IsolatedStorageSettings.ApplicationSettings;

        public Diary()
        {
            //constructor
            InitializeComponent();
            loadDiary();
        }

        private void saveDiary_Click(object sender, RoutedEventArgs e)
        {
            //saves the diary.
            //Checks to see if an Isolated Storage file exists
            //if it does not it creates a new one and inserts the
            //inputted text into it.
            if (!isoSettings.Contains("uDiary"))
            {
                isoSettings.Add("uDiary", userDiary.Text);
            }
            else
            {
                //if the file exists data is stored
                isoSettings["uDiary"] = userDiary.Text;
            }

            //feedback shown to the user to inform them changes have been saved
            MessageBox.Show("Diary Saved", "Saved", MessageBoxButton.OK);
            //.Save() method called to save the data
            isoSettings.Save();
        }

        private void deleteDiary_Click(object sender, RoutedEventArgs e)
        {

            //creates a message box that prompts the user for input
            MessageBoxResult delIsoSto = MessageBox.Show("Are you sure you want to delete your diary?", "Warning", MessageBoxButton.OKCancel);

            //if the user accepts the message box the file is deleted and
            //the text inputted into the text box is deleted.
            //feedback is shown
            if (delIsoSto == MessageBoxResult.OK && isoSettings.Contains("uDiary"))
            {
                IsolatedStorageSettings.ApplicationSettings.Remove("uDiary");
                userDiary.Text = "";
                MessageBox.Show("Diary Successfully Deleted", "Deleted", MessageBoxButton.OK);
            }
            else
            {
                //Do nothing
            }
        }

        private void loadDiary()
        {
            //method is run when diary view is open.
            //if file exists the text is loaded into the user diary
            if (isoSettings.Contains("uDiary"))
            {
                userDiary.Text += IsolatedStorageSettings.ApplicationSettings["uDiary"] as string;
            }
            else
            {
                userDiary.Text = "";
            }
        }


        private void map_Click(object sender, EventArgs e)
        {
            //method to return to tge map view.
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}