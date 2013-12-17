using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace MappingApplication
{
    public partial class about : PhoneApplicationPage //class to show information about the app
    {
        public about()
        {
		//loads page information
            InitializeComponent();
        }

        private void map_Click(object sender, EventArgs e)
        {
			//event listener to return the application to the main page
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative)); 

        }
    }
}