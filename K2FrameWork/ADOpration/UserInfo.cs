using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADOpration
{
    /// <summary>
    /// UserInfo 的摘要说明
    /// </summary>
    public class UserInfo
    {


        #region Data Members

        string _username;
        string _userDisplayName;
        string _samaccountname;
        string _mail;
        string _password;
        string _description;


        #endregion

        #region Properties

        public string UserName
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                }
            }
        }

        public string UserDisplayName
        {
            get { return _userDisplayName; }
            set
            {
                if (_userDisplayName != value)
                {
                    _userDisplayName = value;
                }
            }
        }

        public string SamAccountName
        {
            get { return _samaccountname; }
            set
            {
                if (_samaccountname != value)
                {
                    _samaccountname = value;
                }
            }
        }

        public string Mail
        {
            get { return _mail; }
            set
            {
                if (_mail != value)
                {
                    _mail = value;
                }
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                }
            }
        }

        #endregion
    }
}
