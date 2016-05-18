using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class ADUserInfo
    {
        public ADUserInfo()
        { }

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
