using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Device.Location;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Tasker.Models
{
    [Table] // Mark this class as a Db entity.
    public class Location:INotifyPropertyChanged
   {

       private string _name;
       private double _longitude;
       private double _latitude;

       [Column(IsPrimaryKey = true,IsDbGenerated = false,Storage = "_name")]
       public string Name
       {
           get { return _name; }
           set { this.SetProperty(ref this._name, value); }
       }
        [Column(Storage = "_latitude")]
       public double Latitude
       {
           get { return _latitude; }
           set { this.SetProperty(ref this._latitude, value); }
       }
          [Column(Storage = "_longitude")]
       public double Longitude
       {
           get { return _longitude; }
           set { this.SetProperty(ref this._longitude, value); }
       }

       public GeoCoordinate TaskLocation
       {
           get
           {
               return new GeoCoordinate(Latitude,Longitude);
           }
       }

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
