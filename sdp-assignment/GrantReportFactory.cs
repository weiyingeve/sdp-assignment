using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class GrantReportFactory : DocumentFactory
    {
        public Document createDocument(User owner)
        {
            return new GrantReport(owner, this);
        }

        public Header createHeader(string content)
        {
            return new Header("Grant Report Header");
        }

        public Body createBody(string content)
        {
            return new Body("Grant Report Body Content");
        }

        public Footer createFooter(string content)
        {
            return new Footer("Grant Report Footer");
        }
    }
    }
