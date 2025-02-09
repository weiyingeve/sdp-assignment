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
            //notifyCollaborators
            document.Notify();
        }
        public void resubmit()
        {
            if (document.content.Count > document.prevContentSize)
            {
                //notify collaborators
                document.Notify();
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
