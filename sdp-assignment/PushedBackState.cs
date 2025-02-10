using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class PushedBackState : DocumentState
    {
        private Document document;
        public PushedBackState(Document document)
        {
            this.document=document;
        }

        public void submit(User approver)
        {
            Console.WriteLine("Unable to submit in current state. Try resubmitting instead.");
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
            document.registerObserver(collaborator);
            document.notifyObservers($"User {collaborator.getUsername} has been added to document {document.title}.");
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
            //notifyCollaborators
            document.notifyObservers($"{collaborator.getUsername} has made an edit to {document.title}.");
        }
        public void resubmit()
        {
            if (document.content.Count > document.prevContentSize)
            {
                //notify collaborators
                document.notifyObservers($"{document.title} has been resubmitted for approval.");
                document.setState(document.UnderReviewState);
            }
            else
            {
                Console.WriteLine("Please make an edit to the document before attempting to resubmit.");
                return;
            }
        }
    }
}
