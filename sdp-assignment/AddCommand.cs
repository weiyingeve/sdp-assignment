using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class AddCommand : DocumentCommand
    {
        private Document document;
        private User newCollaborator;
        public AddCommand(Document document, User newCollaborator)
        {
            this.document = document;
            this.newCollaborator = newCollaborator;
        }
        public void execute()
        {
            document.getState().add(this.newCollaborator);
        }
        public void undo()
        {
            document.removeObserver(this.newCollaborator);
            document.notifyObservers($"Collaborator {newCollaborator.getUsername()} was removed from {document.title}.");
        }
        public void redo()
        {
            execute();
        }
    }
}
