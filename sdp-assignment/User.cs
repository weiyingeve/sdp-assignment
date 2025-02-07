using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_assignment
{
    // User Class
    public class User
    {
        public string Name { get; set; }
        public User(string name)
        {
            name = Name;
        }
        public Document CreateDocument(DocumentFactory factory, User owner, string header, string body, string footer)
        {
            var document = factory.createDocument(this);
            document.Header.Edit(header);
            document.Body.Edit(body);
            document.Footer.Edit(footer);
            return document;
        }

        public void EditDocument(Document document, string newContent)
        {
            document.Body.Edit(newContent);
            Console.WriteLine("Document edited successfully.");
        }

        public void SubmitForApproval(Document document, User approver)
        {
            // input logic
        }

        public void ApproveDocument(Document document)
        {
            // input logic
        }

        public void RejectDocument(Document document)
        {
            // input logic
        }
    }

}
