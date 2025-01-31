using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class DraftState : DocumentState
    {
        private Document document;
        public DraftState(Document document)
        {
            this.document = document;
        }
        public void submit(User approver)
        {
            if (document.getApprover() == null)
            {
                foreach (User collaborator in document.collaborators)
                {
                    if (approver == collaborator)
                    {
                        Console.WriteLine("Approver cannot be a collaborator!");
                        return;
                    }
                }
                document.setApprover(approver);
                //notify collaborators
                //notify approver that they have been set as approver
            }
            document.setState(document.UnderReviewState);
        }
        public void pushBack(string comment)
        {
            Console.WriteLine("Unable to push back in current state.");
        }
        public void approve()
        {
            Console.WriteLine("Unable to approve in current state.");
        }
        public void reject(string reason)
        {
            Console.WriteLine("Unable to reject in current state.");
        }
    }
}
