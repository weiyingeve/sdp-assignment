using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class TechnicalReport : Document
    {
        private string content;
        private TechnicalReportFactory technicalReportFactory;

        public TechnicalReport(User owner, string content) : base(owner)
        {
            this.content = content;
        }

        public TechnicalReport(User owner, TechnicalReportFactory technicalReportFactory) : base(owner)
        {
            this.technicalReportFactory = technicalReportFactory;
        }

        public TechnicalReport(User owner, DocumentFactory factory, string content) : base(owner)
        {
            Header = factory.createHeader(content);
            Body = factory.createBody(content);
            Footer = factory.createFooter(content);
        }
    }
}
