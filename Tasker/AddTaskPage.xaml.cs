using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Toolkit;
using Microsoft.Phone.Shell;
using Tasker.Converters;
using Tasker.Models;

namespace Tasker
{
    public partial class AddTaskPage : PhoneApplicationPage
    {
        public Task NewTask { get; set; }
        private App _app;
        public AddTaskPage()
        {
            NewTask = new Task();
            InitializeComponent();
            DataContext = NewTask;

            _app = Application.Current as App;
            if (_app != null) lpLocation.ItemsSource = _app.Database.Locations.ToList();

            //Set the Priority Itemsource.
            lpPriority.ItemsSource = GetPriority();
        }

        private void CancelButtton_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {

            //Get data from UI.

            //Old School... 
            //NewTask.Title = txtTitle.Text;

            //New school (databinding)

            //Save Task..
            _app.Database.Tasks.InsertOnSubmit(NewTask);
            _app.Database.SubmitChanges();

            //Navigate back>
            NavigationService.GoBack();
        }



        public List<Priority> GetPriority()
        {
            return new List<Priority>()
            {
                new Priority(){Name = "High"},
                new Priority(){Name = "Normal"},
                new Priority(){Name = "Low"}
            };
        }

        public List<Location> GetLocations()
        {
            return _app.Database.Locations.ToList();
        } 

       

        //Selection of Dropdown fields handling.
        #region OnListSelected.
        private void LpPriority_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lpPriority.SelectedIndex > -1)
            {
                int index = lpPriority.SelectedIndex;
                NewTask.Priority = GetPriority()[index].Name;

            }
        }
        private void LpLocation_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lpLocation.SelectedIndex > -1)
            {
                int index = lpLocation.SelectedIndex;
                NewTask.Location = GetLocations()[index].Name;
            }
        }
        #endregion 

        //Simple class to hold Priority (Can be an Enum)
        public class Priority
        {
            public string Name { get; set; }
        }

    }
}