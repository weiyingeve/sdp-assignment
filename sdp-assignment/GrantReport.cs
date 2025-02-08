using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class GrantReport: Document
    {
        private GrantReportFactory grantReportFactory;

        public GrantReport(User owner, GrantReportFactory grantReportFactory) : base(owner)
        {
            this.grantReportFactory = grantReportFactory;
        }

        public GrantReport(User owner, DocumentFactory factory, string content) : base(owner)
        {
            Header = factory.createHeader(content);
            Body = factory.createBody(content);
            Footer = factory.createFooter(content);
        }
    }
}
