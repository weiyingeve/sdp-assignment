using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public interface DocumentFactory
    {
        public Document CreateDocument(User owner, string title);
        public Header CreateHeader(string text);
        public Footer CreateFooter(string text);
    }
}
