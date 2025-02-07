using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class Body : DocumentComponent
    {
        public string Content { get; set; }

        public Body(string content)
        {
            Content = content;
        }

        public void Render()
        {
            Console.WriteLine($"Body: {Content}");
        }

        public void Edit(string content)
        {
            Content = content;
        }
    }
}
