void mainMenu()
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