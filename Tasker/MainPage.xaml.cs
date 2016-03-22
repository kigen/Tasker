using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Reactive;
using Microsoft.Phone.Shell;
using Tasker.Misc;
using Tasker.Models;
using Tasker.Resources;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Tasker
{
    public partial class MainPage : PhoneApplicationPage
    {

        //App Instance; 
        private App app;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            //Initialize App.
            app = Application.Current as App;

            //Set the current Class as the DataContext (viewModel) for our UI.
            //DataContext = this; ->Easy way to do Databinding. 

            //Setting Listbox ItemSource Manually.
            LoadData();

            
            //Track the change in location.. register current location.
            var watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
            watcher.PositionChanged += watcher_PositionChanged;
        }


        public void LoadData()
        {
            lvAllTasks.ItemsSource = AllTasks;
            lvNearbyTasks.ItemsSource = NearByTasks;
            lvTodayTasks.ItemsSource = TodayTasks;
            lvUrgentTasks.ItemsSource = UrgentTasks;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadData();
        }

        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            CurrentLocation = e.Position.Location;
            LoadData();
        }

        private void AddButton_Click(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddTaskPage.xaml", UriKind.Relative));
        }

        private void SettingsButton_Click(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
        }
        // When a task is tapped.. 
        private void TaskTapped(object sender, GestureEventArgs e)
        {
            //Find the Object tapped. 
            Task item = (sender as FrameworkElement).DataContext as Task;
            //Two ways of passing values across pages.
            
            //1. Through public/global variables in App.cs Class
            app.SelectedTask = item;

            //Through URL Variables(this case pass the id of the task)
            NavigationService.Navigate(new Uri("/TaskDetailPage.xaml?id="+item.Id,UriKind.Relative)); 

        }

        
        
        //Databindings.. 
        //Since this class has been set as a DataContext for our View:
        public List<Task> TodayTasks
        {
            get
            {
                var tasks = from t in app.Database.Tasks
                    where t.Due.Date == DateTime.Now.Date 
                    select t;

                return tasks.ToList();

            }
        }

        public ObservableCollection<Task> AllTasks
        {
            get
            {
                var tasks= app.Database.Tasks.ToList();
                return new ObservableCollection<Task>(tasks);
            }
        }

        public List<Task> UrgentTasks
        {
            get
            {
                var tasks = from t in app.Database.Tasks
                    where t.Priority == "High"
                    select t;

                return tasks.ToList();

            }
        }

        public List<Task> NearByTasks
        {
            get
            {
                return GetNearby();

            }
        }

        public GeoCoordinate CurrentLocation { get; set; }

        public List<Task> GetNearby()
        {
            //Only execute this code if location has been fetched.
            if (CurrentLocation != null)
            {
                var tasks = app.Database.Tasks.ToList();
                List<Task> nearby = new List<Task>();


                foreach (var task in tasks)
                {
                    //Find location attached to the Task.
                    Location l = app.Database.Locations.First(t => t.Name == task.Location);
                    if (l != null)
                    {
                        //Check if the Location for the task is within 2000M of the current location.
                        var isnearby = l.IsNearBy(CurrentLocation, 2000);
                        if (isnearby)
                        {
                            //Add to list
                            nearby.Add(task);
                        }
                    }
                }


                return nearby.ToList();
            }
           return new List<Task>();
        }

    }
}