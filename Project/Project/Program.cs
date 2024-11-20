using System;
using System.Collections.Generic;
using System.IO;

namespace GuidanceHubApp
{
    class Program
    {
        static Dictionary<string, List<string>> inboxes = new Dictionary<string, List<string>>
        {
            { "student", new List<string>() },
            { "ps", new List<string>() },
            { "st", new List<string>() }
        };

        static string logFilePath = "GuidanceHubLogs.txt";

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
                        if (HandleLogin("Student", "student123@hull.ac.uk", "Password"))
                            StudentActions();
                        break;
                    case "2":
                        if (HandleLogin("Personal Supervisor", "supervisor123@hull.ac.uk", "Password"))
                            PersonalSupervisorActions();
                        break;
                    case "3":
                        if (HandleLogin("Senior Tutor", "seniortutor@hull.ac.uk", "Password"))
                            SeniorTutorActions();
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

        static bool HandleLogin(string role, string correctEmail, string correctPassword)
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
                Pause();
                return true;
            }
            else
            {
                Console.WriteLine("\nInvalid credentials. Login failed.");
                Pause();
                return false;
            }
        }

        static void StudentActions()
        {
            while (true)
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
                        Pause();
                        break;
                }
            }
        }

        static void PersonalSupervisorActions()
        {
            while (true)
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
                        Pause();
                        break;
                }
            }
        }

        static void SeniorTutorActions()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Senior Tutor Menu:");
                Console.WriteLine("1. Send message to Student");
                Console.WriteLine("2. Send message to PS");
                Console.WriteLine("3. Check Inbox");
                Console.WriteLine("4. Log Out");
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
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Pause();
                        break;
                }
            }
        }

        static void SendMessage(string sender, string recipient)
        {
            Console.Write("Enter your message: ");
            string message = Console.ReadLine();
            string fullMessage = $"Message from {sender.ToUpper()}: {message}";
            inboxes[recipient].Add(fullMessage);
            LogAction(fullMessage);
            Console.WriteLine("Message sent successfully.");
            Pause();
        }

        static void BookMeeting(string sender, string recipient)
        {
            Console.Write("Enter meeting date and time (DD/MM/YY - HH:MM): ");
            string dateTime = Console.ReadLine();
            string meetingDetails = $"Meeting request from {sender.ToUpper()} on {dateTime}";
            inboxes[recipient].Add(meetingDetails);
            LogAction(meetingDetails);
            Console.WriteLine("Meeting booked successfully.");
            Pause();
        }

        static void CheckInbox(string role)
        {
            Console.WriteLine("Inbox:");
            if (inboxes[role].Count == 0)
            {
                Console.WriteLine("No messages.");
            }
            else
            {
                foreach (var message in inboxes[role])
                {
                    Console.WriteLine($"- {message}");
                }
            }
            Pause();
        }

        static void LogAction(string action)
        {
            File.AppendAllText(logFilePath, $"{DateTime.Now}: {action}{Environment.NewLine}");
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

        static void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
