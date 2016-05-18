using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ADUserInfo
    {
        private string account;

        public string Account
        {
            get { return account; }
            set { account = value; }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string displayName;

        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }
    }
}
