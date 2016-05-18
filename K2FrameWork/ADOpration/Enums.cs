using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;

namespace ADOpration
{
    /// <summary>
    /// Summary description for Enums
    /// </summary>
    public class Enums
    {
        public Enums()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum VerifyResult
        {
            Success,
            Failure
        }

        public enum ComputerType
        {
            Unknow,
            Server,
            WorkStation
        }

        public enum HandlePrinterLockType
        {
            Set,
            Get
        }

        public enum HandlePrinterLogType
        {
            Status,
            Log
        }
        public enum LogType
        {
            User = 0,
            Computer = 1,
            Dns = 2,
            Printer = 3,
            Exception = 4,
            Warning = 5,
            JobTitleQuery = 6
        }

        public enum FolderType
        {
            UserUploldFolder,
            ComputerUploldFolder,
            PrinterUploldFolder,
            DNSUploldFolder,
            JobTitleQueryUploldFolder
        }

        public enum PrinterQueueStatus
        {
            queue,
            processing,
            success,
            failed
        }


        public enum RoleType
        {
            HelpDesk,
            Venue,
            Admin,
            JobTitleQuery
        }

        public enum EntryType
        {
            User,
            Computer,
            Group
        }

        public enum ExcelType
        {
            User,
            Computer,
            Printer,
            DNS
        }

        public enum ScriptLanguage
        {
            JScript,
            VBscript,
            JavaScript
        }

        public enum UserAction
        {
            NewUser,
            DisableUser,
            AddUserToRole,
            RemoveUserFromRole
        }

        public enum RecordClass
        {
            IN = 1,
            CS = 2,
            CH = 3,
            HS = 4
        }

        public enum ZoneType
        {
            Primary = 0,
            Secondary = 1,
            Stub = 2,
            Forwarder = 3
        }

        public enum TaskStatus
        {
            AllStatus = -1,
            Success = 1,
            Failure = 2,
            VerifySuccess = 3,
            VerifyFailure = 4,
            Unknown = 0
        }

        public static ArrayList GetEnums(Type enumType)
        {
            ArrayList result = new ArrayList();
            //int i = 0;
            Array values = Enum.GetValues(enumType);
            foreach (int v in values)
            {
                result.Add(new Member(Enum.GetName(enumType, v), v));
                //i++;
            }

            return result;
        }

        public static ArrayList GetEnumsName(Type enumType)
        {
            ArrayList result = new ArrayList();
            FieldInfo[] fields = enumType.GetFields();
            int count = fields.Length;
            for (int i = 1; i < count; i++)
            {
                FieldInfo field = fields[i];
                result.Add(field.Name);
            }

            return result;
        }
    }

    public class Member
    {
        private string _Name;
        private int _Value;
        private string _StrValue;

        public string Name
        {
            set { _Name = value; }
            get { return _Name; }
        }
        public int Value
        {
            set { _Value = value; }
            get { return _Value; }
        }
        public string StrValue
        {
            set { _StrValue = value; }
            get { return _StrValue; }
        }
        public Member(string name, int value)
        {
            _Name = name; _Value = value;
        }
        public Member(string name, string value)
        {
            _Name = name; _StrValue = value;
        }
    }
}
