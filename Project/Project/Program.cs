using System;
using System.Collections.Generic;
using System.IO;


namespace GuidanceHubApp
{
   class Program
   {
       static void Main(string[] args)
       {
           GuidanceHubApp app = new GuidanceHubApp();
           app.Start();
       }
   }


   class GuidanceHubApp
   {
       private Authenticator authenticator;
       private Dictionary<string, List<string>> inboxes;
       private string logFilePath = "GuidanceHubLogs.txt";


       public GuidanceHubApp()
       {
           authenticator = new Authenticator();
           inboxes = new Dictionary<string, List<string>>
           {
               { "student", new List<string>() },
               { "ps", new List<string>() },
               { "st", new List<string>() }
           };
       }


       public void Start()
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
                       if (authenticator.HandleLogin("Student", "m.o.mukhtar-2022@hull.ac.uk", "Password"))
                           new Student(inboxes, logFilePath).DisplayMenu();
                       break;
                   case "2":
                       if (authenticator.HandleLogin("Personal Supervisor", "john.whelan@hull.ac.uk", "Password"))
                           new PersonalSupervisor(inboxes, logFilePath).DisplayMenu();
                       break;
                   case "3":
                       if (authenticator.HandleLogin("Senior Tutor", "f.a.polack@hull.ac.uk", "Password"))
                           new SeniorTutor(inboxes, logFilePath).DisplayMenu();
                       break;
                   case "4":
                       Console.WriteLine("Thank you for using GuidanceHub. Goodbye!");
                       return;
                   default:
                       Console.WriteLine("Invalid choice. Please try again.");
                       Utilities.Pause();
                       break;
               }
           }
       }
   }


   class Authenticator
   {
       public bool HandleLogin(string role, string correctEmail, string correctPassword)
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
                   Console.WriteLine($"\nLogin successful! Welcome, {role}.");
                   return true;
               }
               else
               {
                   Console.WriteLine("\nInvalid credentials. Login failed.");
                   Console.WriteLine("Select options:");
                   Console.WriteLine("1- Re-enter Credentials");
                   Console.WriteLine("2- Exit");
                   Console.Write("Enter your choice (1-2): ");


                   string choice = Console.ReadLine();


                   if (choice == "2")
                   {
                       return false;
                   }
               }
           }
       }
   }


   abstract class User
   {
       protected Dictionary<string, List<string>> Inboxes;
       protected string LogFilePath;


       protected User(Dictionary<string, List<string>> inboxes, string logFilePath)
       {
           Inboxes = inboxes;
           LogFilePath = logFilePath;
       }


       public abstract void DisplayMenu();


       protected void SendMessage(string sender, string recipient)
       {
           Console.Write("Enter your message: ");
           string message = Console.ReadLine();
           string fullMessage = $"Message from {sender.ToUpper()}: {message}";
           Inboxes[recipient].Add(fullMessage);
           LogAction(fullMessage);
           Console.WriteLine("Message sent successfully.");
           Utilities.Pause();
       }


       protected void BookMeeting(string sender, string recipient)
       {
           Console.Write("Enter meeting date and time (DD/MM/YY - HH:MM): ");
           string dateTime = Console.ReadLine();
           string meetingDetails = $"Meeting request from {sender.ToUpper()} on {dateTime}";
           Inboxes[recipient].Add(meetingDetails);
           LogAction(meetingDetails);
           Console.WriteLine("Meeting booked successfully.");
           Utilities.Pause();
       }


       protected void CheckInbox(string role)
       {
           Console.WriteLine("Inbox:");
           if (Inboxes[role].Count == 0)
           {
               Console.WriteLine("No messages.");
           }
           else
           {
               foreach (var message in Inboxes[role])
               {
                   Console.WriteLine($"- {message}");
               }
           }
           Utilities.Pause();
       }


       protected void LogAction(string action)
       {
           File.AppendAllText(LogFilePath, $"{DateTime.Now}: {action}{Environment.NewLine}");
       }
   }


   class Student : User
   {
       public Student(Dictionary<string, List<string>> inboxes, string logFilePath)
           : base(inboxes, logFilePath) { }


       public override void DisplayMenu()
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
                       Utilities.Pause();
                       break;
               }
           }
       }
   }


   class PersonalSupervisor : User
   {
       public PersonalSupervisor(Dictionary<string, List<string>> inboxes, string logFilePath)
           : base(inboxes, logFilePath) { }


       public override void DisplayMenu()
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
                       Utilities.Pause();
                       break;
               }
           }
       }
   }


   class SeniorTutor : User
   {
       public SeniorTutor(Dictionary<string, List<string>> inboxes, string logFilePath)
           : base(inboxes, logFilePath) { }


       public override void DisplayMenu()
       {
           while (true)
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
                       ViewSupervisionStatus();
                       break;
                   case "5":
                       return;
                   default:
                       Console.WriteLine("Invalid choice. Please try again.");
                       Utilities.Pause();
                       break;
               }
           }
       }


       private void ViewSupervisionStatus()
       {
           Console.WriteLine("Student Supervision Status:");
           foreach (var message in Inboxes["student"])
           {
               Console.WriteLine($"- {message}");
           }
           foreach (var message in Inboxes["ps"])
           {
               Console.WriteLine($"- {message}");
           }
           Utilities.Pause();
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


       public static void Pause()
       {
           Console.WriteLine("Press 1 to go back");
           Console.ReadKey();
       }
   }
}



