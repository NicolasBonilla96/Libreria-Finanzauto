using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appTest.Entities
{
    [Serializable]
    public partial class Authors
    {
        public Authors() { }

        public Authors(Guid id, String name, String lastname)
        {
            idField = id;
            nameField = name;
            lastnameField = lastname;
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

        private String lastnameField;
        public String lastname
        {
            get { return lastnameField; }
            set { lastnameField = value; }
        }
    }
}