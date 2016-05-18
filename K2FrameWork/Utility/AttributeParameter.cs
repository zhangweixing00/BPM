using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    public sealed class AttributeParameter
    {
        private string name;
        private string value;

        public AttributeParameter()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public AttributeParameter(string attributeName, string attributeValue)
        {
            this.name = attributeName;
            this.value = attributeValue;
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }
}
