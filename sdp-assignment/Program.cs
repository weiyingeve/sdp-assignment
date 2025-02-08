using sdp_assignment;
using System.Reflection.Metadata;
using Document = sdp_assignment.Document;

void Main()
{
    List<User> users = new List<User>();
    users.Add(new User("John"));
    users.Add(new User("Jane"));
    users.Add(new User("Mary"));
    int choice = printMainMenu();

    if (choice == 0) return;
    bool mainMenuActive = true;
    while (mainMenuActive)
    {
        switch (choice)
        {
            case 1:
                Console.WriteLine("\nEnter new name: ");
                string newAccount = Console.ReadLine();
                User user = new User(newAccount);
                users.Add(user);
                break;
            case 2:
                Console.WriteLine("\nEnter your name: ");
                string name = Console.ReadLine();
                foreach (User i in users)
                {
                    if (i.getUsername() == name)
                    {
                        Console.WriteLine($"Welcome, {name}!");
                        int userChoice = printUserMenu();
                        bool userMenuActive = true;
                        while (userMenuActive)
                        {
                            switch (userChoice)
                            {
                                case 1:
                                    Console.WriteLine("Enter type of document: ");
                                    Console.WriteLine("Enter title of document: ");
                                    //i.createDocument();
                                    break;
                                case 2:
                                    Console.WriteLine("Name of document: ");
                                    string docTitle = Console.ReadLine();
                                    foreach (Document document in i.documents)
                                    {
                                        if (document.title == docTitle)
                                        {

                                        }
                                    }
                                    Console.WriteLine($"Document {docTitle} does not exist. Try creating new document instead.");
                                    break;
                                case 3:
                                    Console.WriteLine("Document List:");
                                    Console.WriteLine("--------------------");
                                    foreach (Document document in i.documents)
                                    {
                                        Console.WriteLine(document.title);
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
            case 3:
                Console.WriteLine();
                Console.WriteLine("User List:");
                Console.WriteLine("--------------------");
                foreach (User i in users)
                {
                    Console.WriteLine($"{i.getUsername()}");
                }
                break;
            case 0:
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
