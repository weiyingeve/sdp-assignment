using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class PdfConverter : IDocumentConverter
    {
        public void Convert(Document document)
        {
            Console.WriteLine($"Converting document '{document.title}' to PDF...");
            // Conversion logic here
        }
    }
}
