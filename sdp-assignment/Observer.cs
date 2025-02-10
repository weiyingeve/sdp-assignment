using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public interface Observer
    {
        void update(WorkflowDocument workflowDocument);
        void update(string message);
    }
}
