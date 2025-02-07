using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class NoCommand : DocumentCommand
    {
        public NoCommand() { }
        public void execute() { }
        public void undo() { }
        public void redo() { }
    }
}
