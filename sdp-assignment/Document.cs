using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class Document : DocumentSubject
    {
        private User owner;
        private User approver;

        public string title { get; set; }
        public List<string> content { get; set; } = new List<string>();
        public int prevContentSize { get; set; } = 0;
        // for state design pattern
        public DocumentState DraftState { get; set; }
        public DocumentState UnderReviewState { get; set; }
        public DocumentState ApprovedState { get; set; }
        public DocumentState RejectedState { get; set; }
        public DocumentState PushedBackState { get; set; }
        private DocumentState state;

        //for observer class
        public List<Observer> collaborators {  get; private set; }

        // for abstract factory design pattern
        public Header header { get; set; }
        public Footer footer { get; set; }

        public void AddContent(string text)
        {
            content.Add(text);
        }

        public virtual void Display()
        {
            Console.WriteLine($"Document Title: {title}");
            footer.Render();
            foreach (var line in content)
            {
                Console.WriteLine(line);
            }
            footer.Render();
        }

        //for strategy design pattern
        public IDocumentConverter DocumentConverter { get; set; }


        // general methods
        public Document(User owner, string title)
        {
            collaborators = new List<Observer>();

            this.owner = owner;
            registerObserver(owner);
            this.title = title;
            content = new List<string>();
            Console.WriteLine(this.ToString());
            DraftState = new DraftState(this);
            UnderReviewState = new UnderReviewState(this);
            ApprovedState = new ApprovedState(this);
            RejectedState = new RejectedState(this);
            PushedBackState = new PushedBackState(this);

            state = DraftState;
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

        public void SetConversionType(IDocumentConverter converter)
        {
            DocumentConverter = converter;
            Console.WriteLine($"Conversion type set to {converter.GetType().Name.Replace("Converter", "")}.");
        }

        //for observer 
        public void notifyObservers(string message)
        {
            foreach (Observer o in collaborators)
            {
                o.update(message);
            }
        }

        public void registerObserver(Observer observer)
        {
            if (observer != null && !collaborators.Contains(observer))
            {
                collaborators.Add(observer);
            }
        }
        public void removeObserver(Observer o)
        {
            collaborators.Remove(o);
        }
    }
}
