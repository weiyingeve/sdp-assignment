using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class TypeDocumentIterator : IDocumentIterator
    {
        private List<Document> documents;
        private int type;
        private User user;
        private int position = 0;

        public TypeDocumentIterator(List<Document> documents, int type, User user)
        {
            this.documents = documents;
            this.type = type;
            this.user = user;
        }

        public bool HasNext()
        {
            while (position < documents.Count)
            {
                Document doc = documents[position];
                if (doc.getType() == type &&
                    (doc.getOwner() == user || doc.collaborators.Contains(user) || doc.approver == user))
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