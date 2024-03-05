using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appTest.Entities
{
    [Serializable]
    public partial class Categories
    {
        public Categories() { }

        public Categories(Guid id, String name) 
        {
            idField = id;
            nameField = name;
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
    }
}