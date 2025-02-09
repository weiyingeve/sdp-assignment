using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace sdp_assignment
{
    // User Class
    public class User : Observer
    {
        private string username;
        public List<Document> documents { get; set; } = new List<Document>();

        //attributes for command
        private DocumentCommand slot;
        private DocumentCommand prevCommand;
        public User(string username)
        {
            this.username=username;

            DocumentCommand noCommand = new NoCommand();
            prevCommand = noCommand;
        }
        public string getUsername()
        {
            return username;
        }

        public string Username { get; private set; }
        public void Update(WorkflowDocument document)
        {
            Console.WriteLine($"{Username} received a notification: Document '{document.title}' has been updated.");
        }
        public void viewDocuments()
        {
            Console.WriteLine("Your Documents:");
            Console.WriteLine("--------------------");
            foreach (Document document in documents)
            {
                Console.Write(document.title);
                if (document.getOwner() == this)
                {
                    Console.Write(" -- Owned");
                }
                Console.Write("\n");
            }
        }
        public Document createDocument(DocumentFactory factory, User owner, string header, string body, string footer)
        {
            var document = factory.createDocument(this);
            document.Header.Edit(header);
            document.Body.Edit(body);
            document.Footer.Edit(footer);
            return document;
        }
        public void submitForApproval(Document document, User approver)
        {
            //only draft state allows users to undo submitting of document for approval
            if (document.getState() == document.DraftState)
            {
                SubmitCommand submit = new SubmitCommand(document, document.getState(), approver);
                setCommand(submit);
                executeCommand();
            }
            else
            {
                document.submitForApproval(approver);
            }
        }
        public void editDocument(Document document)
        {
            EditCommand edit = new EditCommand(document, this);
            setCommand(edit);
            executeCommand();
        }
        public void pushBackDocument(Document document, string comment)
        {
            if (this == document.getApprover())
            {
                document.pushBackDocument(comment);
            }
            else
            {
                Console.WriteLine("You are not the approver. Unable to push back document.");
            }
        }
        public void approveDocument(Document document)
        {
            if (this == document.getApprover())
            {
                document.approveDocument();
            }
            else
            {
                Console.WriteLine("You are not the approver. Unable to approve document.");
            }
        }

        public void rejectDocument(Document document, string reason)
        {
            if (this == document.getApprover())
            {
                document.rejectDocument(reason);
            }
            else
            {
                Console.WriteLine("You are not the approver. Unable to reject document.");
            }
        }
        public void addCollaborator(Document document,User collaborator)
        {
            if (this == document.getOwner())
            {
                AddCommand add = new AddCommand(document, collaborator);
                setCommand(add);
                executeCommand();
            }
            else
            {
                Console.WriteLine("You are not the owner. Unable to add collaborator.");
            }
        }
        public void resubmitDocument(Document document)
        {
            document.resubmitDocument();
        }
        //methods for command
        public void setCommand(DocumentCommand command)
        {
            this.slot = command;
        }
        public void executeCommand()
        {
            prevCommand = slot;
            slot.execute();
        }
        public void undoCommand()
        {
            prevCommand.undo();
        }
        public void redoCommand()
        {
            prevCommand.redo();
        }

        public void update(WorkflowDocument workflowDocument)
        {
            Console.WriteLine($"{Username} received a notification: Document '{workflowDocument.title}' has been updated.");
        }
    }

}
