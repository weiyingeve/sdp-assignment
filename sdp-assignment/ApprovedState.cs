using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class ApprovedState : DocumentState
    {
        private Document document;
        public ApprovedState(Document document)
        {
            this.document = document;
        }
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
        public void add(User collaborator)
        {
            Console.WriteLine("Unable to add collaborators, document has already been approved and finalised.");
        }
        public void edit(User collaborator)
        {
            Console.WriteLine("Unable to edit document, document has already been approved and finalised.");
        }
        public void resubmit()
        {
            Console.WriteLine("Unable to resubmit document, document has already been approved and finalised.");
        }
    }
}
