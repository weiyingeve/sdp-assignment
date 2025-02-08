using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class Header : DocumentComponent
    {
        public string Content { get; set; }

        public Header(string content)
        {
            Content = content;
        }

        public void Render()
        {
            Console.WriteLine($"Header: {Content}");
        }

        public void Edit(string content)
        {
            Content = content;
        }
    }
}
