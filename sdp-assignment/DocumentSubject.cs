using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public interface DocumentSubject
    {
        void addCollaborator(User collaborator);
        void Attach(Observer observer);
        void Detach(Observer observer);
        void Notify();
        void setState(DocumentState state);
    }
}
