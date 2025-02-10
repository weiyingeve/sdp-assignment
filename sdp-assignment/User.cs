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
        //public List<Document> documents { get; set; } = new List<Document>();

        //attribute for observer
        public List<DocumentSubject> documents {  get; private set; }

        //attributes for command
        private DocumentCommand slot;
        private DocumentCommand prevCommand;
        public User(string username)
        {
            this.username=username;

            DocumentCommand noCommand = new NoCommand();
            prevCommand = noCommand;

            documents = new List<DocumentSubject>();
        }
        public string getUsername()
        {
            return username;
        }

        public string Username { get; private set; }

        // for abstract factory
        public Document createDocument(DocumentFactory factory, string title, string headerText, string footerText, List<string> content)
        {
            Document doc = factory.createDocument(this, title);
            doc.header = factory.createHeader(headerText);
            doc.footer = factory.createFooter(footerText);
            foreach (var line in content)
            {
                doc.AddContent(line);
            }
            documents.Add(doc);
            doc.registerObserver(this);
            return doc;
        }

        //for observer
        public void update(string message)
        {
            Console.WriteLine($"{Username} received a notification: {message}");
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
    }
}