using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appTest.Entities
{
    [Serializable]
    public partial class Comments
    {
        public Comments() { }

        public Comments(Guid id, String content, Guid user_id, Guid book_id) 
        {
            idField = id;
            contentField = content;
            user_idField = user_id;
            book_idField = book_id;
        }

        private Guid idField;
        public Guid id
        {
            get { return idField; }
            set { idField = value; }
        }

        private String contentField;
        public String content
        {
            get { return contentField; }
            set { contentField = value; }
        }

        private Guid user_idField;
        public Guid user_id
        {
            get { return user_idField; }
            set { user_idField = value; }
        }

        private Guid book_idField;
        public Guid book_id
        {
            get { return book_idField; }
            set { book_idField = value; }
        }
    }
}