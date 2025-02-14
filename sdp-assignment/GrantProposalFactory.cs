using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class GrantProposalFactory : DocumentFactory
    {
        public Header createHeader(string text)
        {
            return new Header(text);
        }
        public Footer createFooter(string text)
        {
            return new Footer(text);
        }
        public Document createDocument(User owner, string title)
        {
            return new GrantProposal(owner, title);
        }
    }

}
