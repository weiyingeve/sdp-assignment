using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public interface DocumentFactory
    {
        public Document createDocument(User owner);
        public Header createHeader(string content);
        public Footer createFooter(string content);
        public Body createBody(string content);
    }
}
