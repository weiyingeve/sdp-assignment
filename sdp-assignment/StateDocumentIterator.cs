using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class StateDocumentIterator : IDocumentIterator
    {
        private List<Document> documents;
        private string state;
        private int position = 0;

        public StateDocumentIterator(List<Document> documents, string state, User user)
        {
            this.documents = documents;
            this.state = state;
        }

        public bool HasNext()
        {
            while (position < documents.Count)
            {
                if (documents[position].GetState().Equals(state, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                position++;
            }
            return false;
        }


        public Document Next()
        {
            return documents[position++];
        }
    }

}
