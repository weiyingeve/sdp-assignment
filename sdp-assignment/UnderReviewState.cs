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
            document.notifyObservers($"Document has been pushed back by approver - {comment}");
            document.setState(document.PushedBackState);
        }
        public void approve()
        {
            Console.WriteLine("Document has been approved.");
            document.notifyObservers($"Document has been approved by approver.");
            document.setState(document.ApprovedState);
        }
        public void reject(string reason)
        {
            Console.WriteLine("Document has been rejected. Reason - " + reason);
            document.setApprover(null);
            document.notifyObservers($"Document has been rejected by approver - {reason}");
            document.setState(document.RejectedState);
        }
        public void add(User collaborator)
        {
            if (!document.collaborators.Contains(collaborator))
            {
                collaborator.addDocument(document);
                Console.WriteLine("Collaborator added.");
                document.notifyObservers($"User {collaborator.getUsername()} has been added to document {document.title}.");
            }
            else
            {
                Console.WriteLine("User has already been added.");
            }
        }
        public void edit(User collaborator)
        {
            Console.WriteLine("Unable to edit when document is under review.");
        }
        public void resubmit()
        {
            Console.WriteLine("Unable to resubmit in current state.");
        }
    }
}
