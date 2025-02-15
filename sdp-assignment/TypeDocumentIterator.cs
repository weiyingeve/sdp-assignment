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
        private string docType;
        private int position = 0;

        public TypeDocumentIterator(List<Document> documents, string docType)
        {
            this.documents = documents;
            this.docType = docType;
        }

        public bool HasNext()
        {
            while (position < documents.Count)
            {
                if (documents[position].GetType().Name.Equals(docType, StringComparison.OrdinalIgnoreCase))
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
