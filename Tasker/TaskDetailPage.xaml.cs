using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Tasker.Models;

namespace Tasker
{
    public partial class TaskDetailPage : PhoneApplicationPage
    {
        private Task task = new Task();
        private App app;
        public TaskDetailPage()
        {
            InitializeComponent();
            app = Application.Current as App;

            if (app != null) task = app.SelectedTask;
            DataContext = task;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

        }
    }
}