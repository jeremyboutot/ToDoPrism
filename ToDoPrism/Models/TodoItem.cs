using Prism.Mvvm;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoPrism.Models
{
    public class TodoItem : BindableBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        private string notes;
        public string Notes
        {
            get { return notes; }
            set { SetProperty(ref notes, value); }
        }

        private bool done;
        public bool Done
        {
            get { return done; }
            set { SetProperty(ref done, value); }
        }
       
        private DateTime due;
        public DateTime Due
        {
            get { return due; }
            set { SetProperty(ref due, value); }
        }
    }
}
