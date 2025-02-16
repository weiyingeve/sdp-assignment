using sdp_assignment;

public class TypeDocumentIterator : IDocumentIterator
{
    private List<Document> documents;
    private int type;
    private int position = 0;

    public TypeDocumentIterator(List<Document> documents, int type, User user)
    {
        this.documents = documents;
        this.type = type;
    }

    public bool HasNext()
    {
        while (position < documents.Count)
        {
            if (documents[position].getType() == type)  // 🔴 Compare int values
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
