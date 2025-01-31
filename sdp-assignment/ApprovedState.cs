using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class ApprovedState : DocumentState
    {
        public void submit(User approver)
        {
            Console.WriteLine("Unable to submit document, document has already been approved and finalised..");
        }
        public void pushBack(string comment)
        {
            Console.WriteLine("Unable to push back document, document has already been approved and finalised..");
        }
        public void approve()
        {
            Console.WriteLine("Unable to approve, document has already been approved and finalised.");
        }
        public void reject(string reason)
        {
            Console.WriteLine("Unable to reject, document has already been approved and finalised.");
        }
    }
}
