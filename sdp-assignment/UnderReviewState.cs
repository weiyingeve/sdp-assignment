using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class UnderReviewState : DocumentState
    {
        private Document document;
        public UnderReviewState(Document document)
        {
            this.document=document;
        }

        public void submit(User approver)
        {
            Console.WriteLine("Unable to submit document in current state.");
        }
        public void pushBack(string comment)
        {
            Console.WriteLine("Document has been pushed back. Comment - " + comment);
            //notify observers
            document.setState(document.DraftState);
        }
        public void approve()
        {
            Console.WriteLine("Document has been approved.");
            //notify observers
            document.setState(document.ApprovedState);
        }
        public void reject(string reason)
        {
            Console.WriteLine("Document has been rejected. Reason - " + reason);
            //notify observers
            document.setState(document.RejectedState);
        }
    }
}
