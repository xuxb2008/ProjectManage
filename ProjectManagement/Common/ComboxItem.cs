using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManagement
{
    public class ComboxItem
    {
        public object Value { get; set; }
        public string ShowText { get; set; }

        public override string ToString()
        {
            if (this.ShowText != null)
            {
                return ShowText;
            }
            else
            {
                return "";
            }
        }
    }
}
