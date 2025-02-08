using sdp_assignment;

public class WorkflowDocument : Document, DocumentSubject
{
    private List<Observer> observers = new List<Observer>();

    public WorkflowDocument(User owner, string title) : base(owner, title) { }

    public void Attach(Observer observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }

    public void Detach(Observer observer)
    {
        observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in observers)
        {
            observer.update(this);
        }
    }

    public override void setState(DocumentState state)
    {
        base.setState(state);  // Update document state
        Notify();  // Notify all observers (collaborators)
    }

    public override void addCollaborator(User collaborator)
    {
        base.addCollaborator(collaborator);
        Attach(collaborator);  // Add the user as an observer
    }
}
