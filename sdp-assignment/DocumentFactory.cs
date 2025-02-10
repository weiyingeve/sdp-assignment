using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public interface DocumentFactory
    {
        public Document createDocument(User owner, string title);
        public Header createHeader(string text);
        public Footer createFooter(string text);
    }
}
