using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class GrantProposal : Document
    {
        public GrantProposal(User owner, string title) : base(owner, title) { }
    }
}
