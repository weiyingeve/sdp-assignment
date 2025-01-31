using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public interface DocumentState
    {
        void submit(User approver);
        void pushBack(string comment);
        void approve();
        void reject(string reason);
    }
}
