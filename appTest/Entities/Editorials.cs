using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appTest.Entities
{
    [Serializable]
    public partial class Editorials
    {
        public Editorials() { }

        public Editorials(Guid id, String name, String location) 
        { 
            idField = id;
            nameField = name;
            locationField = location;
        }

        private Guid idField;
        public Guid id
        {
            get { return idField; }
            set { idField = value; }
        }

        private String nameField;
        public String name
        {
            get { return nameField; }
            set { nameField = value; }
        }

        private String locationField;
        public String location
        {
            get { return locationField; }
            set { locationField = value; }
        }
    }
}