using sdp_assignment;

void Main()
{
    int choice;
    Console.WriteLine("1) Create new user");
    Console.WriteLine("2) Login as user");
    Console.WriteLine("3) List users");
    Console.WriteLine("4) List documents");
    Console.WriteLine();
    Console.WriteLine("0) Exit System");
    Console.Write("Your choice? ");
    choice = Console.Read();

    if (choice == 0) return;

    while (true)
    {
        switch (choice)
        {
            case 1:
                Console.WriteLine("\nEnter new name: ");
                string newAccount = Console.ReadLine();
                // create account logic
                break;
            case 2:
                Console.WriteLine("\nEnter your name: ");
                string name = Console.ReadLine();
                // login logic
                printMenu();
                break;
            case 3:
                // list users logic
                break;
            case 4:
                // list documents logic
                break;
        }
    }
}

void printMenu()
{
    int choice;
    Console.WriteLine("1) Create new document");
    Console.WriteLine("2) Edit existing document");
    Console.WriteLine("3) List your documents");
    Console.WriteLine();
    Console.WriteLine("0) Logout");
    Console.Write("Your choice? ");
    choice = Console.Read();
}
