using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class EditCommand : DocumentCommand
    {
        private Document document;
        private User collaborator;
        private List<string> prevContent;
        public EditCommand(Document document, User collaborator)
        {
            this.document = document;
            this.collaborator = collaborator;
        }
        public void execute()
        {
            prevContent = document.content;
            document.editDocument(collaborator);
            //notify observers

        }
        public void undo()
        {
            document.content = prevContent;
            //notify observers
        }
        public void redo()
        {
            execute();
        }
    }
}
