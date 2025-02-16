using sdp_assignment;
using System.ComponentModel.Design;
using System.Globalization;
using System.Reflection.Metadata;
using System.Xml.Linq;
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
            case 1: // Create new user
                string newAccount;
                do
                {
                    Console.Write("\nEnter new name: ");
                    newAccount = Console.ReadLine()?.Trim();

                    // Check if input is empty or null
                    if (string.IsNullOrEmpty(newAccount))
                    {
                        Console.WriteLine("Name cannot be empty. Please enter a valid name.");
                        continue;
                    }

                    TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
                    newAccount = textInfo.ToTitleCase(newAccount.ToLower());

                    // Check if user already exists (case-insensitive)
                    if (users.Exists(u => u.getUsername().Equals(newAccount, StringComparison.OrdinalIgnoreCase)))
                    {
                        Console.WriteLine($"User '{newAccount}' already exists. Please choose a different name.");
                        continue;
                    }

                    break;

                } while (true);

                users.Add(new User(newAccount));
                Console.WriteLine($"User '{newAccount}' created successfully!");
                break;

            case 2: // Login
                Console.Write("\nEnter your name: ");
                string name = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Name cannot be empty.");
                    break;
                }

                // Convert input to Title Case for matching
                TextInfo textInfoLogin = CultureInfo.InvariantCulture.TextInfo;
                name = textInfoLogin.ToTitleCase(name.ToLower());

                User user = users.Find(u => u.getUsername().Equals(name, StringComparison.OrdinalIgnoreCase));
                if (user == null)
                {
                    Console.WriteLine($"User '{name}' does not exist. Try creating a new user.");
                    break;
                }

                Console.WriteLine($"Welcome, {name}!");
                UserMenu(users, user);
                break;

            case 3: // List all users
                Console.WriteLine("\nUser List:");
                Console.WriteLine("--------------------");

                if (users.Count == 0)
                {
                    Console.WriteLine("No users found.");
                }
                else
                {
                    foreach (User i in users)
                    {
                        Console.WriteLine(i.getUsername());
                    }
                }
                break;

            case 0: // Exit
                Console.WriteLine("Goodbye!");
                mainMenuActive = false;
                break;

            default:
                Console.WriteLine("Invalid choice. Please enter a valid option.");
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
            case 1: // create document
                int type;
                do
                {
                    Console.Write("Select document type (1) Grant Proposal (2) Technical Report: ");
                    if (int.TryParse(Console.ReadLine(), out type) && (type == 1 || type == 2))
                    {
                        break; // Valid input, exit loop
                    }
                    Console.WriteLine("Invalid input. Please enter 1 for Grant Proposal or 2 for Technical Report.");
                } while (true);

                string title;
                do
                {
                    Console.Write("Enter document title: ");
                    title = Console.ReadLine()?.Trim();
                    if (!string.IsNullOrEmpty(title)) break;
                    Console.WriteLine("Title cannot be empty. Please enter a valid title.");
                } while (true);

                string headerText;
                do
                {
                    Console.Write("Enter header text: ");
                    headerText = Console.ReadLine()?.Trim();
                    if (!string.IsNullOrEmpty(headerText)) break;
                    Console.WriteLine("Header cannot be empty. Please enter a valid header.");
                } while (true);

                string footerText;
                do
                {
                    Console.Write("Enter footer text: ");
                    footerText = Console.ReadLine()?.Trim();
                    if (!string.IsNullOrEmpty(footerText)) break;
                    Console.WriteLine("Footer cannot be empty. Please enter a valid footer.");
                } while (true);

                List<string> content = new List<string>();
                Console.WriteLine("Enter content lines (type END to finish):");
                string line;
                while ((line = Console.ReadLine()) != "END") 
                {
                    content.Add(line);
                }

                DocumentFactory factory = (type == 1) ? new GrantProposalFactory() : new TechnicalReportFactory();
                Document newdoc = user.createDocument(factory, title, headerText, footerText, content, type);

                newdoc.setType(type);
                documentCollection.AddDocument(newdoc);

                Console.WriteLine("\nDocument Created:\n");
                newdoc.Display();
                break;

            case 2: //edit document
                Console.WriteLine("Name of document: ");
                string docTitle = Console.ReadLine();
                Document doc = user.documents.OfType<Document>().FirstOrDefault(d => d.title == docTitle);
                if (doc == null)
                {
                    Console.WriteLine("Document not found.");
                    break;
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

            case 3: // list documents
                while (true)
                {
                    int documentChoice = printDocuments();

                    if (documentChoice == 0)
                    {
                        Console.WriteLine("Returning to User Menu...");
                        break; // Exit the loop, returning to the User Menu
                    }

                    switch (documentChoice)
                    {
                        case 1: // list owned documents
                            Console.WriteLine("Owned Documents:");
                            Console.WriteLine("--------------------");
                            var ownedIterator = documentCollection.GetOwnedDocumentsIterator(user);
                            while (ownedIterator.HasNext())
                            {
                                Document document = ownedIterator.Next();
                                Console.WriteLine(document.title);
                            }
                            break;

                        case 2: // list accessible documents
                            Console.WriteLine("Accessible Documents:");
                            Console.WriteLine("--------------------");
                            var accessibleIterator = documentCollection.GetAccessibleDocumentsIterator(user);
                            while (accessibleIterator.HasNext())
                            {
                                Document document = accessibleIterator.Next();
                                Console.WriteLine(document.title);
                            }
                            break;

                        case 3: // list by type
                            Console.WriteLine("Enter document type to filter by (1) Grant Proposal (2) Technical Report:");
                            if (int.TryParse(Console.ReadLine(), out int filterType) && (filterType == 1 || filterType == 2))
                            {
                                var typeIterator = documentCollection.GetTypeDocumentsIterator(filterType, user);
                                Console.WriteLine("\nDocuments of type: " + (filterType == 1 ? "Grant Proposal" : "Technical Report"));
                                Console.WriteLine("--------------------");

                                while (typeIterator.HasNext())
                                {
                                    Document document = typeIterator.Next();
                                    Console.WriteLine(document.title);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter 1 for Grant Proposal or 2 for Technical Report.");
                            }
                            break;

                        case 4: // list by document state
                            Console.WriteLine("Enter document state to filter by (1) Draft (2) UnderReview (3) Approved (4) Rejected (5) PushedBack):");
                            if (int.TryParse(Console.ReadLine(), out int stateChoice) && stateChoice >= 1 && stateChoice <= 5)
                            {
                                string state = stateChoice switch
                                {
                                    1 => "Draft",
                                    2 => "Under Review",
                                    3 => "Approved",
                                    4 => "Rejected",
                                    5 => "Pushed Back",
                                    _ => "Unknown"
                                };

                                var stateIterator = documentCollection.GetStateDocumentsIterator(state, user);
                                Console.WriteLine($"\nDocuments with state '{state}':");
                                Console.WriteLine("--------------------");

                                while (stateIterator.HasNext())
                                {
                                    Document document = stateIterator.Next();
                                    Console.WriteLine($"- {document.title}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                            }
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please enter a valid option.");
                            break;
                    }
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
    Console.WriteLine("Document Menu");
    Console.WriteLine("[1] List owned documents");
    Console.WriteLine("[2] List all accessible documents");
    Console.WriteLine("[3] List documents by type");
    Console.WriteLine("[4] List documents by state");
    Console.WriteLine("[0] Return to user menu");
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
                string collaboratorName;
                do
                {
                    Console.Write("Enter collaborator name: ");
                    collaboratorName = Console.ReadLine()?.Trim();

                    if (string.IsNullOrEmpty(collaboratorName))
                    {
                        Console.WriteLine("Name cannot be empty. Please enter a valid name.");
                        continue;
                    }
                    collaboratorName = char.ToUpper(collaboratorName[0]) + collaboratorName.Substring(1).ToLower();
                    User collaborator = users.Find(u => u.getUsername() == collaboratorName);
                    if (collaborator != null)
                    {
                        owner.addCollaborator(document, collaborator);
                        document.addCollaborator(collaborator);
                        break;
                    }

                    Console.WriteLine("User not found. Please enter a valid collaborator name.");
                } while (true);
                break;
            case 2: //edit document
                owner.editDocument(document);
                break;
            case 3: //submit document for approval
                string approverName;
                do
                {
                    Console.Write("Enter name of approver: ");
                    approverName = Console.ReadLine()?.Trim();

                    if (string.IsNullOrEmpty(approverName))
                    {
                        Console.WriteLine("Name cannot be empty. Please enter a valid name.");
                        continue;
                    }
                    approverName = char.ToUpper(approverName[0]) + approverName.Substring(1).ToLower();

                    User approver = users.Find(u => u.getUsername() == approverName);
                    if (approver != null)
                    {
                        owner.submitForApproval(document, approver);
                        break;
                    }

                    Console.WriteLine("Approver not found. Please enter a valid name.");
                } while (true);
                break;
            case 4: //resubmit document
                owner.resubmitDocument(document);
                break;
            case 5: //set file conversion type
                Console.WriteLine("Choose a format to set for conversion:");
                Console.WriteLine("1. PDF");
                Console.WriteLine("2. Microsoft Word");
                Console.Write("Enter your choice (1 or 2): ");

                string formatChoice = Console.ReadLine()?.Trim();
                if (formatChoice == "1")
                {
                    document.SetConversionType(new PdfConverter());
                }
                else if (formatChoice == "2")
                {
                    document.SetConversionType(new WordConverter());
                }
                else
                {
                    Console.WriteLine("Invalid choice. Conversion type not set.");
                }
                break;
            case 6: //produce converted type
                document.ConvertDocument();
                break;
            case 7: //print document content
                document.Display();
                break;
            case 8: //undo last command
                owner.undoCommand();
                break;
            case 9: //redo command
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
    Console.WriteLine("[4] Resubmit document");
    Console.WriteLine("[5] Set file conversion type");
    Console.WriteLine("[6] Produce converted type");
    Console.WriteLine("[7] Print document contents");
    Console.WriteLine("[8] Undo last command");
    Console.WriteLine("[9] Redo last command");
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
                string approverName;
                do
                {
                    Console.Write("Enter name of approver: ");
                    approverName = Console.ReadLine()?.Trim();

                    if (string.IsNullOrEmpty(approverName))
                    {
                        Console.WriteLine("Name cannot be empty. Please enter a valid name.");
                        continue;
                    }
                    approverName = char.ToUpper(approverName[0]) + approverName.Substring(1).ToLower();
                    User approver = users.FirstOrDefault(x => x.getUsername() == approverName);
                    if (approver != null)
                    {
                        collaborator.submitForApproval(document, approver);
                        break;
                    }

                    Console.WriteLine("Approver not found. Please enter a valid name.");

                } while (true);
                break;
            case 3: //resubmit document
                collaborator.resubmitDocument(document);
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
            case 6: //print document contents
                document.Display();
                break;
            case 7: //undo command
                collaborator.undoCommand();
                break;
            case 8: //redo command
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
    Console.WriteLine("[3] Resubmit Document");
    Console.WriteLine("[4] Set file conversion type");
    Console.WriteLine("[5] Produce converted type");
    Console.WriteLine("[6] Print document contents");
    Console.WriteLine("[7] Undo last command");
    Console.WriteLine("[8] Redo last command");
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
            case 1: //push back 
                Console.WriteLine("Add Comment: ");
                string comment = Console.ReadLine();
                approver.pushBackDocument(document, comment);
                break;
            case 2: //approve
                approver.approveDocument(document);
                break;
            case 3: //reject
                Console.WriteLine("Add Reason: ");
                string reason = Console.ReadLine();
                approver.rejectDocument(document, reason);
                break;
            case 4: //print document contents
                document.Display();
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
    Console.WriteLine("[4] Print document contents");
    Console.WriteLine("[0] Return to main menu");
    Console.WriteLine();
    Console.WriteLine("Enter choice: ");
    string input = Console.ReadLine();
    Int32.TryParse(input, out approverchoice);
    return approverchoice;
}