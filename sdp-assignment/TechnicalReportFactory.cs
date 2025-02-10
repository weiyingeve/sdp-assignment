using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class TechnicalReportFactory : DocumentFactory
    {
        public Header CreateHeader(string text)
        {
            return new Header(text);
        }
        public Footer CreateFooter(string text)
        {
            return new Footer(text);
        }
        public Document CreateDocument(User owner, string title)
        {
            return new TechnicalReport(owner, title);
        }
    }
}
