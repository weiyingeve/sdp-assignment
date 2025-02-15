using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class TechnicalReport : Document
    {
        public TechnicalReport(User owner, string title, int docType) : base(owner, title, docType) { }
    }
}
