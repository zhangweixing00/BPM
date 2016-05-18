using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pkurg.PWorldBPM.WorkFlowRule
{
    [Serializable]
    public class AttachmentItem
    {
        public string FileName
        {
            get;
            set;
        }

        public string FilePath
        {
            get;
            set;
        }

        public int FileSize
        {
            get;
            set;
        }

        public Guid Attachment_ID
        {
            get;
            set;
        }

        public DateTime Created_On
        {
            get;
            set;
        }
    }
}
