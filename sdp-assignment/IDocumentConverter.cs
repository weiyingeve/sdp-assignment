using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    // Strategy Interface
    public interface IDocumentConverter
    {
        void Convert(Document document);
    }
}
