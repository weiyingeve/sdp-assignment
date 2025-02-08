using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class Document
    {
        private User owner;
        private User approver;
        public List<User> collaborators { get; } = new List<User>();
        public string title { get; set; }
        public List<string> content { get; set; } = new List<string>();
        public int prevContentSize { get; set; }
        // for state design pattern
        public DocumentState DraftState { get; private set; }
        public DocumentState UnderReviewState { get; private set; }
        public DocumentState ApprovedState { get; private set; }
        public DocumentState RejectedState { get; private set; }
        public DocumentState PushedBackState { get; private set; }
        private DocumentState state;
        
        // for abstract factory design pattern
        public Header Header { get; set; }
        public Footer Footer { get; set; }
        public Body Body { get; set; }
        public void Render()
        {
            Console.WriteLine("=== Document Content: ===");
            Header.Render();
            Body.Render();
            Footer.Render();
            Console.WriteLine("------------------------\n");
        }

        //for strategy design pattern
        public IDocumentConverter DocumentConverter { get; set; }


        // general methods
        public Document(User owner, string title)
        {
            this.owner = owner;
            collaborators.Add(owner);
            this.title = title;
        }

        public User getOwner()
        {
            return owner;
        }

        public User getApprover()
        {
            return approver;
        }
        public void setApprover(User user)
        {
            this.approver = user;
        }

        //for state design pattern
        public DocumentState getState()
        {
            return state;
        }
        public virtual void setState(DocumentState state)
        {
            this.state = state;
        }
        public void submitForApproval(User approver)
        {
            state.submit(approver);
        }
        public void pushBackDocument(string comment)
        {
            state.pushBack(comment);
        }
        public void approveDocument()
        {
            state.approve();
        }
        public void rejectDocument(string reason)
        {
            state.reject(reason);
        }

        public virtual void addCollaborator(User collaborator)
        {
            state.add(collaborator);
        }

        public void editDocument(User collaborator)
        {
            state.edit(collaborator);
        }
        public void resubmitDocument()
        {
            state.resubmit();
        }

        //for strategy design pattern
        public void ConvertDocument()
        {
            if (DocumentConverter == null)
            {
                Console.WriteLine("No document converter set.");
            }
            else
            {
                DocumentConverter.Convert(this);
            }
        }
    }
}
