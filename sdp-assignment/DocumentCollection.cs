﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    public class DocumentCollection
    {
        private List<Document> documents = new List<Document>();

        public void AddDocument(Document document)
        {
            documents.Add(document);
        }

        public List<Document> GetDocuments()
        {
            return documents;
        }

        public IDocumentIterator GetOwnedDocumentsIterator(User user)
        {
            return new OwnedDocumentIterator(documents, user);
        }

        public IDocumentIterator GetAccessibleDocumentsIterator(User user)
        {
            return new AccessibleDocumentIterator(documents, user);
        }

        public IDocumentIterator GetTypeDocumentsIterator(int documentType, User user)
        {
            return new TypeDocumentIterator(documents, documentType, user);
        }

        public IDocumentIterator GetStateDocumentsIterator(string state, User user)
        {
            return new StateDocumentIterator(documents, state, user);
        }
    }

}
