using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class TechnicalReportFactory : DocumentFactory
    {
        public Document createDocument(User owner)
        {
            return new TechnicalReport(owner, this);
        }

        public Header createHeader(string content)
        {
            return new Header("Technical Report Header");
        }

        public Body createBody(string content)
        {
            return new Body("Technical Report Body Content");
        }

        public Footer createFooter(string content)
        {
            return new Footer("Technical Report Footer");
        }
    }
}
