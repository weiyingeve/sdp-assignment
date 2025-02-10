using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class Header : DocumentComponent
    {
        public string Text { get;  set; }
        public Header(string text)
        {
            Text = text;
        }
        public void Render()
        {
            Console.WriteLine($"Header: {Text}");
        }
    }

}
