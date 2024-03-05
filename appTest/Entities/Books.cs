using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appTest.Entities
{
    [Serializable]
    public partial class Books
    {
        public Books() { }
        
        public Books(Guid id, Guid author_id, Guid editorial_id, Guid category_id, String publication_year, String name) 
        { 
            idField = id;
            author_idField = author_id;
            editorial_idField = editorial_id;
            category_idField = category_id;
            publication_yearField = publication_year;
            nameField = name;
        }

        private Guid idField;
        public Guid id
        {
            get { return idField; } 
            set { idField = value; }
        }

        private Guid author_idField;
        public Guid author_id
        {
            get { return author_idField; }
            set { author_idField = value; }
        }

        private Guid editorial_idField;
        public Guid editorial_id
        {
            get { return editorial_idField;}
            set { editorial_idField = value;            }
        }

        private Guid category_idField;
        public Guid category_id
        {
            get { return category_idField; }
            set { category_idField = value; }
        }

        private String publication_yearField;
        public String publication_year
        {
            get { return publication_yearField; }
            set { publication_yearField = value; }
        }

        private String nameField;
        public string name
        {
            get { return nameField; }
            set { nameField = value; }
        }
    }
}