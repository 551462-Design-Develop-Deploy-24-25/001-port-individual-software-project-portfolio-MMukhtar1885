using System;
using System.Collections.Generic;
using System.IO;

namespace GuidanceHubApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Start();
        }
    }

    static class Application
    {
        public static void Start()
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
                        if (Authenticator.HandleLogin("Student", "student123@hull.ac.uk", "Password"))
                            new Student().ShowMenu();
                        break;
                    case "2":
                        if (Authenticator.HandleLogin("Personal Supervisor", "supervisor123@hull.ac.uk", "Password"))
                            new PersonalSupervisor().ShowMenu();
                        break;
                    case "3":
                        if (Authenticator.HandleLogin("Senior Tutor", "seniortutor@hull.ac.uk", "Password"))
                            new SeniorTutor().ShowMenu();
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

        private static void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    static class Authenticator
    {
        public static bool HandleLogin(string role, string correctEmail, string correctPassword)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"You selected: {role}");
                Console.Write("Enter Email: ");
                string email = Console.ReadLine();

                Console.Write("Enter Password: ");
                string password = Utilities.ReadPassword();

                if (email == correctEmail && password == correctPassword)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("\nInvalid credentials. Login failed.");
                    Console.WriteLine("Select options:");
                    Console.WriteLine("1- Re-enter Credentials");
                    Console.WriteLine("2- Exit");
                    Console.Write("Enter your choice: ");
                    string option = Console.ReadLine();

                    if (option == "2")
                        return false;
                }
            }
        }
    }

    abstract class User
    {
        protected static Dictionary<string, List<string>> inboxes = new Dictionary<string, List<string>>
        {
            { "student", new List<string>() },
            { "ps", new List<string>() },
            { "st", new List<string>() }
        };

        internal static string LogFilePath = "GuidanceHubLogs.txt"; // Changed to 'internal' for accessibility

        public abstract void ShowMenu();

        protected void SendMessage(string sender, string recipient)
        {
            Console.Write("Enter your message: ");
            string message = Console.ReadLine();
            string fullMessage = $"Message from {sender.ToUpper()}: {message}";
            inboxes[recipient].Add(fullMessage);
            Utilities.LogAction(fullMessage);
            Console.WriteLine("Message sent successfully.");
            ShowMenu();
        }

        protected void BookMeeting(string sender, string recipient)
        {
            Console.Write("Enter meeting date and time (DD/MM/YY - HH:MM): ");
            string dateTime = Console.ReadLine();
            string meetingDetails = $"Meeting request from {sender.ToUpper()} on {dateTime}";
            inboxes[recipient].Add(meetingDetails);
            Utilities.LogAction(meetingDetails);
            Console.WriteLine("Meeting booked successfully.");
            ShowMenu();
        }

        protected void CheckInbox(string role)
        {
            Console.WriteLine("Inbox:");
            if (inboxes[role].Count == 0)
                Console.WriteLine("No messages.");
            else
            {
                foreach (var message in inboxes[role])
                    Console.WriteLine($"- {message}");
            }
            ShowMenu();
        }
    }

    class Student : User
    {
        public override void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Student Menu:");
            Console.WriteLine("1. Send message to PS");
            Console.WriteLine("2. Send message to ST");
            Console.WriteLine("3. Book a meeting with PS");
            Console.WriteLine("4. Check Inbox");
            Console.WriteLine("5. Log Out");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    SendMessage("student", "ps");
                    break;
                case "2":
                    SendMessage("student", "st");
                    break;
                case "3":
                    BookMeeting("student", "ps");
                    break;
                case "4":
                    CheckInbox("student");
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    ShowMenu();
                    break;
            }
        }
    }

    class PersonalSupervisor : User
    {
        public override void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Personal Supervisor Menu:");
            Console.WriteLine("1. Send message to Student");
            Console.WriteLine("2. Send message to ST");
            Console.WriteLine("3. Book a meeting with Student");
            Console.WriteLine("4. Check Inbox");
            Console.WriteLine("5. Log Out");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    SendMessage("ps", "student");
                    break;
                case "2":
                    SendMessage("ps", "st");
                    break;
                case "3":
                    BookMeeting("ps", "student");
                    break;
                case "4":
                    CheckInbox("ps");
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    ShowMenu();
                    break;
            }
        }
    }

    class SeniorTutor : User
    {
        public override void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Senior Tutor Menu:");
            Console.WriteLine("1. Send message to Student");
            Console.WriteLine("2. Send message to PS");
            Console.WriteLine("3. Check Inbox");
            Console.WriteLine("4. View Student Supervision Status");
            Console.WriteLine("5. Log Out");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    SendMessage("st", "student");
                    break;
                case "2":
                    SendMessage("st", "ps");
                    break;
                case "3":
                    CheckInbox("st");
                    break;
                case "4":
                    ViewStudentSupervisionStatus();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    ShowMenu();
                    break;
            }
        }

        private void ViewStudentSupervisionStatus()
        {
            Console.WriteLine("Student Supervision Status:");
            DisplayCommunication("student", "ps");
            DisplayCommunication("ps", "student");
            ShowMenu();
        }

        private void DisplayCommunication(string sender, string recipient)
        {
            foreach (var message in inboxes[sender])
            {
                if (message.Contains($"Message from {sender.ToUpper()}"))
                    Console.WriteLine($"- {message}");
            }
        }
    }

    static class Utilities
    {
        public static string ReadPassword()
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
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*");
                }
            } while (key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }

        public static void LogAction(string action)
        {
            File.AppendAllText(User.LogFilePath, $"{DateTime.Now}: {action}{Environment.NewLine}");
        }
    }
}
