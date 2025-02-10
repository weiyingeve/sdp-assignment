using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class Footer : DocumentComponent
    {
        public string Text { get; set; }
        public Footer(string text)
        {
            Text = text;
        }
        public void Render()
        {
            Console.WriteLine($"Footer: {Text}");
        }
    }
}
