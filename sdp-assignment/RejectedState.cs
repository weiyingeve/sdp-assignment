using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class RejectedState : DocumentState
    {
        private Document document;
        public RejectedState(Document document)
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
                document.Notify();
                //notify approver that they have been set as approver
                document.setState(document.UnderReviewState);
            }
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
        public void add(User collaborator)
        {
            document.collaborators.Add(collaborator);
        }
        public void edit(User collaborator)
        {
            string newLine;
            Console.Write("Enter string to add to document: ");
            newLine = Console.ReadLine();
            while (string.IsNullOrEmpty(newLine))
            {
                Console.WriteLine("String cannot be empty! Try again.");
                Console.Write("Enter string to add to document: ");
                newLine = Console.ReadLine();
            }
            document.content.Add(newLine);
            document.prevContentSize++;
            //notifyCollaborators
            document.Notify();
        }
        public void resubmit()
        {
            Console.WriteLine("Cannot resubmit in current state.");
        }
    }
}
