using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public interface DocumentSubject
    {
        void registerObserver(Observer observer);
        void removeObserver(Observer observer);
        void notifyObservers(string message);
    }
}
