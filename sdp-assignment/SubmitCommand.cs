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
        public SubmitCommand(Document document, DocumentState newState)
        {
            this.document = document;
            this.newState = newState;
        }
        public void execute(User collaborator)
        {
            this.collaborator = collaborator;
            prevState = document.getState();
            document.getState().submit(this.collaborator);
            //notify observers
        }
        public void undo()
        {
            document.setState(prevState);
            //notify observers
        }
        public void redo()
        {
            execute(this.collaborator);
        }
    }
}
