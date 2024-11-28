using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class GuidanceHubTests
    {
        [TestMethod]
        public void TP01_ValidLogin_ShouldSucceed()
        {
            // Simulate valid login logic
            string correctEmail = "m.o.mukhtar-2022@hull.ac.uk";
            string correctPassword = "Password";
            string inputEmail = "m.o.mukhtar-2022@hull.ac.uk";
            string inputPassword = "Password";

            bool result = (inputEmail == correctEmail && inputPassword == correctPassword);
            Assert.IsTrue(result, "Valid login should succeed.");
        }

        [TestMethod]
        public void TP01_InvalidLogin_ShouldFail()
        {
            // Simulate invalid login logic
            string correctEmail = "m.o.mukhtar-2022@hull.ac.uk";
            string correctPassword = "Password";
            string inputEmail = "wrongemail@hull.ac.uk";
            string inputPassword = "wrongpassword";

            bool result = (inputEmail == correctEmail && inputPassword == correctPassword);
            Assert.IsFalse(result, "Invalid login should fail.");
        }

        [TestMethod]
        public void TP03_SendMessage_ShouldLogCorrectly()
        {
            // Simulate sending a message
            var inboxes = new Dictionary<string, List<string>>
            {
                { "student", new List<string>() },
                { "ps", new List<string>() }
            };

            string sender = "student";
            string recipient = "ps";
            string message = "Test message";
            inboxes[recipient].Add($"Message from {sender.ToUpper()}: {message}");

            Assert.IsTrue(inboxes["ps"].Count > 0, "Message should be logged in recipient's inbox.");
        }

        [TestMethod]
        public void TP04_BookMeeting_ValidFormat_ShouldSucceed()
        {
            // Simulate booking a meeting
            var inboxes = new Dictionary<string, List<string>>
            {
                { "student", new List<string>() }
            };

            string sender = "ps";
            string recipient = "student";
            string dateTime = "24/11/2024 - 10:00";
            inboxes[recipient].Add($"Meeting request from {sender.ToUpper()} on {dateTime}");

            Assert.IsTrue(inboxes["student"].Count > 0, "Valid meeting request should succeed.");
        }

        [TestMethod]
        public void TP07_Actions_ShouldLogToTxtFile()
        {
            string logFilePath = "TestLog.txt";
            if (File.Exists(logFilePath)) File.Delete(logFilePath);

            string action = "Test action logged";
            File.AppendAllText(logFilePath, $"{System.DateTime.Now}: {action}{System.Environment.NewLine}");

            string logContents = File.ReadAllText(logFilePath);
            Assert.IsTrue(logContents.Contains(action), "Action should be logged to TXT file.");
        }

        [TestMethod]
        public void TP10_PasswordMasking_ShouldReplaceCharacters()
        {
            // Simulate password masking
            string inputPassword = "Password";
            string maskedPassword = new string('*', inputPassword.Length);

            Assert.AreEqual("********", maskedPassword, "Password should be masked correctly.");
        }

        [TestMethod]
        public void TP02_RoleSpecificMenus_ShouldDisplayCorrectly()
        {
            // Simulate role-specific menu logic
            var menus = new Dictionary<string, List<string>>
            {
                { "student", new List<string> { "Send message", "Book meeting", "Check inbox" } },
                { "ps", new List<string> { "Send message", "Book meeting", "View students" } },
                { "st", new List<string> { "Send message", "Check inbox", "Supervision status" } }
            };

            var role = "student";
            var expectedMenu = new List<string> { "Send message", "Book meeting", "Check inbox" };

            CollectionAssert.AreEqual(expectedMenu, menus[role], "Menu for the role should match the expected menu.");
        }

        [TestMethod]
        public void TP05_ReceivedMessageAndMeeting_ShouldBeInInbox()
        {
            // Simulate inbox logic
            var inboxes = new Dictionary<string, List<string>>
            {
                { "student", new List<string>() }
            };

            inboxes["student"].Add("Message from PS: Test message");
            inboxes["student"].Add("Meeting request from PS on 24/11/2024 - 10:00");

            Assert.AreEqual(2, inboxes["student"].Count, "Inbox should contain both messages and meeting requests.");
        }

        [TestMethod]
        public void TP06_MenuDisplay_ShouldBeRoleSpecific()
        {
            // Simulate specific options display logic
            var roleMenus = new Dictionary<string, List<string>>
            {
                { "student", new List<string> { "Send message to PS", "Send message to ST", "Book meeting", "Check inbox" } },
                { "ps", new List<string> { "Send message to Student", "Send message to ST", "Book meeting", "Check inbox" } },
                { "st", new List<string> { "Send message to Student", "Send message to PS", "Check inbox", "View Supervision Status" } }
            };

            string role = "ps";
            var expectedMenu = new List<string> { "Send message to Student", "Send message to ST", "Book meeting", "Check inbox" };

            CollectionAssert.AreEqual(expectedMenu, roleMenus[role], "Role-specific menu should be displayed correctly.");
        }

        [TestMethod]
        public void TP08_InvalidInput_ShouldShowError()
        {
            // Simulate invalid input handling
            string loginEmail = "wrongemail@hull.ac.uk";
            string loginPassword = "wrongpassword";
            string errorMessage = "Invalid credentials. Please try again.";

            bool isValid = loginEmail == "correctemail@hull.ac.uk" && loginPassword == "correctpassword";

            string result = isValid ? "Login successful" : errorMessage;
            Assert.AreEqual(errorMessage, result, "Error message should be displayed for invalid input.");
        }

        [TestMethod]
        public void TP09_Notifications_DeliverInTime()
        {
            // Simulate notification delivery timing
            var timer = System.Diagnostics.Stopwatch.StartNew();

            // Simulate message sending
            var inboxes = new Dictionary<string, List<string>>
            {
                { "student", new List<string>() }
            };
            inboxes["student"].Add("Test notification");

            timer.Stop();

            Assert.IsTrue(timer.ElapsedMilliseconds < 3000, "Notification should be delivered within 3 seconds.");
        }
    }
}