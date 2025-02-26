﻿using System;
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
            if (!document.collaborators.Contains(collaborator) && collaborator != document.getApprover())
            {
                collaborator.addDocument(document);
                Console.WriteLine("Collaborator added.");
                document.notifyObservers($"User {collaborator.getUsername()} has been added to document {document.title}.");
            }
            else
            {
                Console.WriteLine("User has already been added as a collaborator or is currently the approver.");
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
            document.notifyObservers($"{collaborator.getUsername()} has made an edit to {document.title}");
        }
        public void resubmit()
        {
            if (document.content.Count > document.prevContentSize)
            {
                //notify collaborators
                document.notifyObservers($"{document.title} has been resubmitted for approval.");
                Console.WriteLine($"{document.getApprover().getUsername()} received a notification: {document.title} is ready for review.");
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
