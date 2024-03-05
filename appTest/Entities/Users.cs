using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appTest.Entities
{
    [Serializable]
    public partial class Users
    {
        public Users() { }
        
        public Users(Guid id, String name, String email) 
        {
            idField = id;
            nameField = name;
            emailField = email;
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

        private String emailField;
        public String email
        {
            get { return emailField; }
            set { emailField = value; }
        }
    }
}