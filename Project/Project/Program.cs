using System;

namespace GuidanceHubApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to GuidanceHub");
                Console.WriteLine("Please choose your role:");
                Console.WriteLine("1. Student");
                Console.WriteLine("2. Personal Supervisor (PS)");
                Console.WriteLine("3. Senior Tutor (ST)");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice (1-4): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        HandleLogin("Student", "student123@hull.ac.uk", "Password");
                        break;
                    case "2":
                        HandleLogin("Personal Supervisor", "supervisor123@hull.ac.uk", "Password");
                        break;
                    case "3":
                        HandleLogin("Senior Tutor", "seniortutor@hull.ac.uk", "Password");
                        break;
                    case "4":
                        Console.WriteLine("Thank you for using GuidanceHub. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Pause();
                        break;
                }
            }
        }

        static void HandleLogin(string role, string correctEmail, string correctPassword)
        {
            Console.Clear();
            Console.WriteLine($"You selected: {role}");
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = ReadPassword();

            if (email == correctEmail && password == correctPassword)
            {
                Console.WriteLine($"\nLogin successful! Welcome, {role}.");
            }
            else
            {
                Console.WriteLine("\nInvalid credentials. Login failed.");
            }

            Pause();
        }

        static string ReadPassword()
        {
            string password = string.Empty;
            ConsoleKey key;

            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b"); // Removes the character from console
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*"); // Masks password with *
                }
            } while (key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }

        static void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
