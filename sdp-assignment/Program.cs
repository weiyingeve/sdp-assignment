using sdp_assignment;
using System.ComponentModel.Design;
using System.Reflection.Metadata;
using Document = sdp_assignment.Document;

void Main()
{
    List<User> users = new List<User>();
    users.Add(new User("John"));
    users.Add(new User("Jane"));
    users.Add(new User("Mary"));

    bool mainMenuActive = true;
    while (mainMenuActive)
    {
        int choice = printMainMenu();
        switch (choice)
        {
            case 1: //create new user
                Console.WriteLine("\nEnter new name: ");
                string newAccount = Console.ReadLine();
                User user = new User(newAccount);
                users.Add(user);
                break;
            case 2: //login
                Console.WriteLine("\nEnter your name: ");
                string name = Console.ReadLine();
                foreach (User i in users)
                {
                    if (i.getUsername() == name)
                    {
                        Console.WriteLine($"Welcome, {name}!");
                        bool userMenuActive = true;
                        while (userMenuActive)
                        {
                            int userChoice = printUserMenu();
                            switch (userChoice)
                            {
                                case 1: //create document
                                    Console.WriteLine("Enter type of document: ");
                                    Console.WriteLine("Enter title of document: ");
                                    //i.createDocument();
                                    break;
                                case 2: //edit document
                                    Console.WriteLine("Name of document: ");
                                    string docTitle = Console.ReadLine();
                                    foreach (Document document in i.documents)
                                    {
                                        if (document.title == docTitle)
                                        {
                                            if (i == document.getOwner())
                                            {
                                                bool ownerMenuActive = true;
                                                while (ownerMenuActive)
                                                {
                                                    int ownerChoice = printOwnerMenu();
                                                    switch (ownerChoice)
                                                    {
                                                        case 1: //add collaborators
                                                            Console.WriteLine("Enter name of new collaborator: ");
                                                            string collaboratorName = Console.ReadLine();
                                                            if (!string.IsNullOrEmpty(collaboratorName))
                                                            {
                                                                foreach (User x in users)
                                                                {
                                                                    if (x.getUsername() == collaboratorName)
                                                                    {
                                                                        i.addCollaborator(document, x);
                                                                        break;
                                                                    }
                                                                }
                                                                break;
                                                            }
                                                            Console.WriteLine("Name cannot be empty.");
                                                            break;
                                                        case 2: //edit document
                                                            i.editDocument(document);
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
                                                                        i.submitForApproval(document, x);
                                                                        break;
                                                                    }
                                                                }
                                                                break;
                                                            }
                                                            Console.WriteLine("Name cannot be empty.");
                                                            break;
                                                        case 4: //set file conversion type
                                                            break;
                                                        case 5: //produce converted type
                                                            break;
                                                        case 6: //print document contents
                                                            document.Render();
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
                                            else if (document.collaborators.Contains(i))
                                            {
                                                bool collaboratorMenuActive = true;
                                                while (collaboratorMenuActive)
                                                {
                                                    int collaboratorChoice = printOwnerMenu();
                                                    switch (collaboratorChoice)
                                                    {
                                                        case 1: //edit document
                                                            i.editDocument(document);
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
                                                                        i.submitForApproval(document, x);
                                                                        break;
                                                                    }
                                                                }
                                                                break;
                                                            }
                                                            Console.WriteLine("Name cannot be empty.");
                                                            break;
                                                        case 3: //set file conversion type
                                                            break;
                                                        case 4: //produce converted type
                                                            break;
                                                        case 5: //print document contents
                                                            document.Render();
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
                                            else if (i == document.getApprover())
                                            {
                                                int approverChoice = printApproverMenu();
                                                switch (approverChoice)
                                                {
                                                    case 1:
                                                        Console.WriteLine("Add Comment: ");
                                                        string comment = Console.ReadLine();
                                                        i.pushBackDocument(document, comment);
                                                        break;
                                                    case 2:
                                                        i.approveDocument(document);
                                                        break;
                                                    case 3:
                                                        Console.WriteLine("Add Reason: ");
                                                        string reason = Console.ReadLine();
                                                        i.rejectDocument(document, reason);
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                    Console.WriteLine($"Document {docTitle} does not exist. Try creating new document instead.");
                                    break;
                                case 3: //list documents
                                    int documentchoice = printDocuments();
                                    switch (documentchoice)
                                    {
                                        case 1: //list owned documents
                                            Console.WriteLine("Document List:");
                                            Console.WriteLine("--------------------");
                                            foreach (Document document in i.documents)
                                            {
                                                if (document.getOwner() == i)
                                                {
                                                    Console.WriteLine(document.title);
                                                }
                                            }
                                            break;
                                        case 2: //list accessible documents
                                            Console.WriteLine("Document List:");
                                            Console.WriteLine("--------------------");
                                            foreach (Document document in i.documents)
                                            {
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
                }
                Console.WriteLine($"User {name} does not exist. Try creating new user.");
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
    Console.WriteLine("[0] Return to main menu");
    Console.WriteLine();
    Console.WriteLine("Enter choice: ");
    string input = Console.ReadLine();
    Int32.TryParse(input, out ownerchoice);
    return ownerchoice;
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
    Console.WriteLine("[0] Return to main menu");
    Console.WriteLine();
    Console.WriteLine("Enter choice: ");
    string input = Console.ReadLine();
    Int32.TryParse(input, out collaboratorchoice);
    return collaboratorchoice;
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