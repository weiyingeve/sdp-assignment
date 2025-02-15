using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class AccessibleDocumentIterator : IDocumentIterator
    {
        private List<Document> documents;
        private User user;
        private int position = 0;

        public AccessibleDocumentIterator(List<Document> documents, User user)
        {
            this.documents = documents;
            this.user = user;
        }

        public bool HasNext()
        {
            while (position < documents.Count)
            {
                Document currentDocument = documents[position];

                // Check if user is a collaborator or an approver
                if (currentDocument.collaborators.Contains(user) || currentDocument.approver == user)

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
