using sdp_assignment;
using System.ComponentModel.Design;
using System.Reflection.Metadata;
using Document = sdp_assignment.Document;

DocumentCollection documentCollection = new DocumentCollection();
Main();


void Main()
{
    List<User> users = new List<User>();
    users.Add(new User("John"));
    users.Add(new User("Jane"));
    users.Add(new User("Mary"));
    MainMenu(users);
}

void MainMenu(List<User> users)
{
    bool mainMenuActive = true;
    while (mainMenuActive)
    {
        int choice = printMainMenu();
        switch (choice)
        {
            case 1: //create new user
                Console.WriteLine("\nEnter new name: ");
                string newAccount = Console.ReadLine();
                if (users.Exists(u => u.getUsername() == newAccount))
                {
                    Console.WriteLine("User already exists.");
                    break;
                }
                users.Add(new User(newAccount));
                Console.WriteLine("User created successfully!");
                break;
            case 2: //login
                Console.WriteLine("\nEnter your name: ");
                string name = Console.ReadLine();
                User user = users.Find(u => u.getUsername() == name);
                if (user == null)
                {
                    Console.WriteLine($"User {name} does not exist. Try creating a new user.");
                    return;
                }
                Console.WriteLine($"Welcome, {name}!");
                UserMenu(users, user);
                break;
            case 3: // list all users
                Console.WriteLine();
                Console.WriteLine("User List:");
                Console.WriteLine("--------------------");
                foreach (User i in users)
                {
                    Console.WriteLine($"{i.getUsername()}");
                }
                break;
            case 0: // exit
                Console.WriteLine("Goodbye!");
                mainMenuActive = false;
                break;
            default:
                Console.WriteLine("Please enter a valid choice.");
                break;
        }

    }
}

int printMainMenu()
{
    int choice;
    Console.WriteLine();
    Console.WriteLine("Main Menu");
    Console.WriteLine("--------------------");
    Console.WriteLine("[1] Create new user");
    Console.WriteLine("[2] Login as user");
    Console.WriteLine("[3] List users");
    Console.WriteLine("[0] Exit");
    Console.WriteLine();
    Console.Write("Enter choice: ");
    string input = Console.ReadLine();
    Int32.TryParse(input, out choice);
    return choice;
}

void UserMenu(List<User> users, User user)
{
    bool userMenuActive = true;
    while (userMenuActive)
    {
        int userChoice = printUserMenu();
        switch (userChoice)
        {
            case 1: //create document
                Console.Write("Select document type (1) Grant Proposal (2) Technical Report: ");
                int type = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter document title: ");
                string title = Console.ReadLine();

                Console.Write("Enter header text: ");
                string headerText = Console.ReadLine();

                Console.Write("Enter footer text: ");
                string footerText = Console.ReadLine();

                List<string> content = new List<string>();
                Console.WriteLine("Enter content lines (type END to finish):");
                string line;
                while ((line = Console.ReadLine()) != "END")
                {
                    content.Add(line);
                }

                if (type == 1)
                {
                    DocumentFactory factory = new GrantReportFactory();
                    Document newdoc = user.createDocument(factory, title, headerText, footerText, content);

                    // Add the new document to the collection
                    documentCollection.AddDocument(newdoc);

                    Console.WriteLine("\nDocument Created:\n");
                    newdoc.Display();
                }
                else if (type == 2)
                {
                    DocumentFactory factory = new TechnicalReportFactory();
                    Document newdoc = user.createDocument(factory, title, headerText, footerText, content);

                    // Add the new document to the collection
                    documentCollection.AddDocument(newdoc);

                    Console.WriteLine("\nDocument Created:\n");
                    newdoc.Display();
                }
                break;
            case 2: //edit document
                Console.WriteLine("Name of document: ");
                string docTitle = Console.ReadLine();
                Document doc = user.documents.OfType<Document>().FirstOrDefault(d => d.title == docTitle);
                if (doc == null)
                {
                    Console.WriteLine("Document not found.");
                    return;
                }

                if (doc.getOwner() == user)
                {
                    OwnerMenu(users, user, doc);
                }
                else if (doc.collaborators.Contains(user))
                {
                    CollaboratorMenu(users, user, doc);
                }
                else if (doc.getApprover() == user)
                {
                    ApproverMenu(user, doc);
                }
                else
                {
                    Console.WriteLine("You do not have permission to edit this document.");
                }
                break;


            case 3: //list documents
                int documentchoice = printDocuments();
                switch (documentchoice)
                {
                    case 1: //list owned documents
                        Console.WriteLine("Document List:");
                        Console.WriteLine("--------------------");
                        var ownedIterator = documentCollection.GetOwnedDocumentsIterator(user);
                        while (ownedIterator.HasNext())
                        {
                            Document document = ownedIterator.Next();
                            Console.WriteLine(document.title);
                        }
                        break;
                    case 2: //list accessible documents
                        Console.WriteLine("Document List:");
                        Console.WriteLine("--------------------");
                        var accessibleIterator = documentCollection.GetAccessibleDocumentsIterator(user);
                        while (accessibleIterator.HasNext())
                        {
                            Document document = accessibleIterator.Next();
                            Console.WriteLine(document.title);
                        }
                        break;

                    default:
                        Console.WriteLine("Enter a valid choice.");
                        break;
                }
                break;

            case 0:
                Console.WriteLine("Returning to main menu:");
                userMenuActive = false;
                break;
            default:
                Console.WriteLine("Please enter a valid choice.");
                break;
        }
    }
}
int printUserMenu()
{
    int userchoice;
    Console.WriteLine();
    Console.WriteLine("User Menu");
    Console.WriteLine("--------------------");
    Console.WriteLine("[1] Create new document");
    Console.WriteLine("[2] Edit existing document");
    Console.WriteLine("[3] List your documents");
    Console.WriteLine("[0] Return to main menu");
    Console.WriteLine();
    Console.WriteLine("Enter choice: ");
    string input = Console.ReadLine();
    Int32.TryParse(input, out userchoice);
    return userchoice;
}

int printDocuments()
{
    int choice;
    Console.WriteLine();
    Console.WriteLine("[1] List owned documents");
    Console.WriteLine("[2] List all accessible documents");
    Console.WriteLine();
    Console.WriteLine("Enter choice: ");
    string input = Console.ReadLine();
    Int32.TryParse(input, out choice);
    return choice;
}

void OwnerMenu(List<User> users, User owner, Document document)
{
    bool ownerMenuActive = true;
    while (ownerMenuActive)
    {
        int ownerChoice = printOwnerMenu();
        switch (ownerChoice)
        {
            case 1: // add collaborators
                Console.Write("Enter collaborator name: ");
                string collaboratorName = Console.ReadLine();
                User collaborator = users.Find(u => u.getUsername() == collaboratorName);
                if (collaborator != null)
                {
                    owner.addCollaborator(document, collaborator);
                    document.addCollaborator(collaborator);
                }
                else
                {
                    Console.WriteLine("User not found.");
                }
                break;
            case 2: //edit document
                owner.editDocument(document);
                break;
            case 3: //submit document for approval
                Console.WriteLine("Enter name of approver: ");
                string approverName = Console.ReadLine();
                if (!string.IsNullOrEmpty(approverName))
                {
                    foreach (User x in users)
                    {
                        if (x.getUsername() == approverName)
                        {
                            owner.submitForApproval(document, x);
                            break;
                        }
                    }
                    break;
                }
                Console.WriteLine("Name cannot be empty.");
                break;
            case 4: //set file conversion type
                Console.WriteLine("Choose a format to set for conversion:");
                Console.WriteLine("1. PDF");
                Console.WriteLine("2. Microsoft Word");
                Console.Write("Enter your choice (1 or 2): ");
                string formatChoice = Console.ReadLine().Trim();

                switch (formatChoice)
                {
                    case "1":
                        document.SetConversionType(new PdfConverter());
                        break;
                    case "2":
                        document.SetConversionType(new WordConverter());
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Conversion type not set.");
                        break;
                }
                break;
            case 5: //produce converted type
                document.ConvertDocument();
                break;
            case 6: //print document content
                document.Display();
                break;
            case 7: //undo last command
                owner.undoCommand();
                break;
            case 8: //redo command
                owner.redoCommand();
                break;
            case 0:
                Console.WriteLine("Returning to User Menu...");
                ownerMenuActive = false;
                break;
            default:
                Console.WriteLine("Enter a valid choice.");
                break;
        }
    }
}
int printOwnerMenu()
{
    int ownerchoice;
    Console.WriteLine();
    Console.WriteLine("Owner Menu");
    Console.WriteLine("--------------------");
    Console.WriteLine("[1] Add new collaborator");
    Console.WriteLine("[2] Edit document");
    Console.WriteLine("[3] Submit for review");
    Console.WriteLine("[4] Set file conversion type");
    Console.WriteLine("[5] Produce converted type");
    Console.WriteLine("[6] Print document contents");
    Console.WriteLine("[7] Undo last command");
    Console.WriteLine("[8] Redo last command");
    Console.WriteLine("[0] Return to main menu");
    Console.WriteLine();
    Console.WriteLine("Enter choice: ");
    string input = Console.ReadLine();
    Int32.TryParse(input, out ownerchoice);
    return ownerchoice;
}

void CollaboratorMenu(List<User> users, User collaborator, Document document)
{
    bool collaboratorMenuActive = true;
    while (collaboratorMenuActive)
    {
        int collaboratorChoice = printCollaboratorMenu();
        switch (collaboratorChoice)
        {
            case 1: //edit document
                collaborator.editDocument(document);
                break;
            case 2://submit for approval
                Console.WriteLine("Enter name of approver: ");
                string approverName = Console.ReadLine();
                if (!string.IsNullOrEmpty(approverName))
                {
                    foreach (User x in users)
                    {
                        if (x.getUsername() == approverName)
                        {
                            collaborator.submitForApproval(document, x);
                            break;
                        }
                    }
                    break;
                }
                Console.WriteLine("Name cannot be empty.");
                break;
            case 3: //set file conversion type
                Console.WriteLine("Choose a format to set for conversion:");
                Console.WriteLine("1. PDF");
                Console.WriteLine("2. Microsoft Word");
                Console.Write("Enter your choice (1 or 2): ");
                string formatChoice = Console.ReadLine().Trim();

                switch (formatChoice)
                {
                    case "1":
                        document.SetConversionType(new PdfConverter());
                        break;
                    case "2":
                        document.SetConversionType(new WordConverter());
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Conversion type not set.");
                        break;
                }
                break;
            case 4: //produce converted type

                document.ConvertDocument();
                break;
            case 5: //print document contents
                document.Display();
                break;
            case 6: //undo command
                collaborator.undoCommand();
                break;
            case 7: //redo command
                collaborator.redoCommand();
                break;
            case 0:
                Console.WriteLine("Returning to User Menu...");
                collaboratorMenuActive = false;
                break;
            default:
                Console.WriteLine("Enter a valid choice.");
                break;
        }
    }
}
int printCollaboratorMenu()
{
    int collaboratorchoice;
    Console.WriteLine();
    Console.WriteLine("Collaborator Menu");
    Console.WriteLine("--------------------");
    Console.WriteLine("[1] Edit document");
    Console.WriteLine("[2] Submit for review");
    Console.WriteLine("[3] Set file conversion type");
    Console.WriteLine("[4] Produce converted type");
    Console.WriteLine("[5] Print document contents");
    Console.WriteLine("[6] Undo last command");
    Console.WriteLine("[7] Redo last command");
    Console.WriteLine("[0] Return to main menu");
    Console.WriteLine();
    Console.WriteLine("Enter choice: ");
    string input = Console.ReadLine();
    Int32.TryParse(input, out collaboratorchoice);
    return collaboratorchoice;
}

void ApproverMenu(User approver, Document document)
{
    bool approverMenuActive = true;
    while (approverMenuActive)
    {
        int approverChoice = printApproverMenu();
        switch (approverChoice)
        {
            case 1:
                Console.WriteLine("Add Comment: ");
                string comment = Console.ReadLine();
                approver.pushBackDocument(document, comment);
                break;
            case 2:
                approver.approveDocument(document);
                break;
            case 3:
                Console.WriteLine("Add Reason: ");
                string reason = Console.ReadLine();
                approver.rejectDocument(document, reason);
                break;
            case 0:
                Console.WriteLine("Returning to User Menu...");
                approverMenuActive = false;
                break;
            default:
                Console.WriteLine("Enter a valid choice.");
                break;
        }
    }
}

int printApproverMenu()
{
    int approverchoice;
    Console.WriteLine();
    Console.WriteLine("Approver Menu");
    Console.WriteLine("--------------------");
    Console.WriteLine("[1] Push Back");
    Console.WriteLine("[2] Approve");
    Console.WriteLine("[3] Reject");
    Console.WriteLine("[0] Return to main menu");
    Console.WriteLine();
    Console.WriteLine("Enter choice: ");
    string input = Console.ReadLine();
    Int32.TryParse(input, out approverchoice);
    return approverchoice;
}