using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Tasker.Models
{
    public class Db : DataContext
    {
        public Db(string fileOrConnection) : base(fileOrConnection)
        {
        }

        public Db(string fileOrConnection, MappingSource mapping) : base(fileOrConnection, mapping)
        {
        }


        //Register all Tables... 
        public Table<Task> Tasks
        {
            get { return this.GetTable<Task>(); }
        }

        public Table<Location> Locations
        {
            get { return this.GetTable<Location>(); }
        }
    }
}
