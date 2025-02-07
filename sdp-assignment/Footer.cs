using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class Footer : DocumentComponent
    {
        public string Content { get; set; }

        public Footer(string content)
        {
            Content = content;
        }

        public void Render()
        {
            Console.WriteLine($"Footer: {Content}");
        }

        public void Edit(string content)
        {
            Content = content;
        }
    }
}
