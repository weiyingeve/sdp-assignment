using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class OwnedDocumentIterator : IDocumentIterator
    {
        private List<Document> documents;
        private User user;
        private int position = 0;

        public OwnedDocumentIterator(List<Document> documents, User user)
        {
            this.documents = documents;
            this.user = user;
        }

        public bool HasNext()
        {
            while (position < documents.Count)
            {
                if (documents[position].getOwner() == user)
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
