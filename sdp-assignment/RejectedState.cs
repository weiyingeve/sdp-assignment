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
                approver.addDocument(document);
                //notify collaborators
                document.notifyObservers($"{approver.getUsername()} has been appointed as the approver");
                //notify approver that they have been set as approver
                Console.WriteLine($"{approver.getUsername()} received a notification: You have been appointed as the approver of {document.title}.");
            }
            document.notifyObservers($"Document {document.title} has been submitted for approval.");
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
            Console.WriteLine($"Document {document.title} has been edited. Notifying collaborators...");
            document.prevContentSize++;
            document.notifyObservers($"{collaborator.getUsername()} has made an edit to {document.title}");
        }
        public void resubmit()
        {
            Console.WriteLine("Cannot resubmit in current state.");
        }
    }
}
