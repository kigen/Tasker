using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Tasker
{
    public partial class AddTaskPage : PhoneApplicationPage
    {
        public AddTaskPage()
        {
            InitializeComponent();
        }

        private void CancelButtton_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {

            //Get data from UI.

            //Save Task..
            

            //Navigate back>
            NavigationService.GoBack();
        }
    }
}