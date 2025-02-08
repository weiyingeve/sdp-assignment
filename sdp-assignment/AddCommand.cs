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
        }
        public void execute()
        {
            document.getState().add(newCollaborator);
            //notify observers
        }
        public void undo()
        {
            document.collaborators.Remove(newCollaborator);
            //notify observers
        }
        public void redo()
        {
            execute();
        }
    }
}
