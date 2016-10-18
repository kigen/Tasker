using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Models
{
    [Table]
    public class Task : INotifyPropertyChanged
    {

        public Task()
        {
            _due = DateTime.Now;
            _taskCreated = DateTime.Now;

        }
        private int _id;
        private string _title;
        private string _description;
        private DateTime _due;
        private DateTime _taskCreated;
        private string _priority;
        private string _status;
        private bool _reminder;
        private string _location;

        [Column(IsPrimaryKey = true, Storage = "_id",IsDbGenerated = true)]
        public int Id
        {
            get { return _id; }
            set { this.SetProperty(ref this._id, value); }
        }


        [Column( Storage = "_title")]
        public string Title
        {
            get { return _title; }
            set { this.SetProperty(ref this._title, value); }
        }

         [Column(Storage = "_description")]
        public string Description
        {
            get { return _description; }
            set { this.SetProperty(ref this._description, value); }
        }
         [Column(Storage = "_due")]
        public DateTime Due
        {
            get { return _due; }
            set { this.SetProperty(ref this._due, value); }
        }
         [Column(Storage = "_taskCreated")]
        public DateTime TaskCreated
        {
            get { return _taskCreated; }
            set { this.SetProperty(ref this._taskCreated, value); }
        }
         [Column(Storage = "_priority")]
        public string Priority
        {
            get { return _priority; }
            set { this.SetProperty(ref this._priority, value); }
        }
        [Column(Storage = "_status")]
        public string Status
        {
            get { return _status; }
            set { this.SetProperty(ref this._status, value); }
        }
         [Column(Storage = "_reminder",DbType = "Bit")]
        public bool Reminder
        {
            get { return _reminder; }
            set { this.SetProperty(ref this._reminder, value); }
        }
         [Column(Storage = "_location")]
         public string Location
         {
             get { return _location; }
             set { this.SetProperty(ref this._location, value); }
         }
        
        [Column]
        public bool Completed { get; set; }


        // Property Change logic
       public event PropertyChangedEventHandler PropertyChanged;
       protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
       {
           if (object.Equals(storage, value)) return false;
           storage = value;
           if (propertyName != null) this.OnPropertyChanged(propertyName);
           return true;
       }

       protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
       {
           var eventHandler = this.PropertyChanged;
           if (eventHandler != null)
               eventHandler(this, new PropertyChangedEventArgs(propertyName));
       }


    }
}
