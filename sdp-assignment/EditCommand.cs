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
            prevContent = new List<string>(document.content);
            document.editDocument(collaborator);

        }
        public void undo()
        {
            Console.WriteLine();
            document.content = prevContent;
            Console.WriteLine($"Last edit made by {collaborator.getUsername()} in document {document.title} was removed.");
        }
        public void redo()
        {
            execute();
        }
    }
}
