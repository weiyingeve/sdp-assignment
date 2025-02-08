using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class WordConverter : IDocumentConverter
    {
        public void Convert(Document document)
        {
            Console.WriteLine($"Converting document '{document.title}' to Word...");
            // Conversion logic here
        }
    }
}
