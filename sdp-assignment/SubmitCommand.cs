using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class SubmitCommand : DocumentCommand
    {
        private Document document;
        private DocumentState prevState;
        private DocumentState newState;
        private User collaborator;
        public SubmitCommand(Document document, DocumentState newState, User collaborator)
        {
            this.document = document;
            this.newState = newState;
            this.collaborator = collaborator;
        }
        public void execute()
        {
            prevState = document.getState();
            document.getState().submit(this.collaborator);
        }
        public void undo()
        {
            document.setState(prevState);
            document.notifyObservers($"Submission of document {document.title} was undone. Document has been reverted to previous state.");
            Console.WriteLine($"{document.getApprover().getUsername()} received a notification: Document has been unsubmitted.");
        }
        public void redo()
        {
            execute();
        }
    }
}
