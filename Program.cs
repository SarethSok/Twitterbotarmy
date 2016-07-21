///////////////////////////////////////////////////////////////////
//
// With this software you are able to create a twitter account
// automatically.
//
// mail@mrklintscher.com
//
// Only for academic use! ;-)
//
///////////////////////////////////////////////////////////////////

using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using OpenQA.Selenium.Firefox;

namespace Twitterbotarmy
{
    /// <summary>
    /// Fullname and password must be different
    /// Username does not contain a blank
    /// </summary>

    class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]

        private static extern bool SetWindowPos(
        IntPtr hWnd,
        IntPtr hWndInsertAfter,
        int x,
        int y,
        int cx,
        int cy,
        int uFlags);

        private const int HWND_TOPMOST = -1;
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOSIZE = 0x0001;

        static void Main(string[] args)
        {
            IntPtr hWnd = Process.GetCurrentProcess().MainWindowHandle;

            SetWindowPos(hWnd,
                new IntPtr(HWND_TOPMOST),
                0, 0, 0, 0,
                SWP_NOMOVE | SWP_NOSIZE);

            Console.ForegroundColor = ConsoleColor.Green;
 
            Console.WriteLine("");
            Console.WriteLine("//////////////////////////////////////////");
            Console.WriteLine("//                                      //");
            Console.WriteLine("//     $     TWITTER BOT ARMY     $     //");
            Console.WriteLine("//                                      //");
            Console.WriteLine("//////////////////////////////////////////");
            Console.WriteLine("");

            FirefoxDriver browser = new FirefoxDriver();

            //////////////////////////////////////////////
            //
            // User information
            //
            //////////////////////////////////////////////

            string fullname = "";
            string email = "@fyii.de";
            string password = "";
            string username = "";

            //////////////////////////////////////////////
            //
            // Create Twitter account
            //
            //////////////////////////////////////////////

            if (string.IsNullOrEmpty(fullname))
            {
                browser.Close();

                Console.WriteLine("$$ Come on give me some values!!!");
                Console.ReadLine();
            }

            Console.WriteLine("$$ Let's create the account ...");
            Console.WriteLine("");

            browser.Navigate().GoToUrl("https://twitter.com/signup");

            browser.FindElementById("full-name").SendKeys(fullname);

            Thread.Sleep(2000);

            browser.FindElementById("email").SendKeys(email);

            Thread.Sleep(2000);

            browser.FindElementById("password").SendKeys(password);

            Thread.Sleep(2000);

            browser.FindElementById("submit_button").Click();

            Thread.Sleep(2000);

            browser.FindElementByCssSelector(".skip-link").Click();

            Thread.Sleep(5000);

            browser.FindElementById("username").SendKeys(username);

            Thread.Sleep(5000);

            browser.FindElementById("submit_button").Click();

            Console.WriteLine("$$ Account created.");
            Console.WriteLine("");

            //////////////////////////////////////////////
            //
            // Read confirmation email
            //
            //////////////////////////////////////////////

            Console.WriteLine("$$ Now we have to confirm it ...");
            Console.WriteLine("");

            Thread.Sleep(5000);

            Console.WriteLine("$$ Reading the confirmation mail ...");
            Console.WriteLine("");

            browser.Navigate().GoToUrl("http://www.fyii.de/");
            browser.FindElementById("searchFC").Clear();
            browser.FindElementById("searchFC").SendKeys(email.Replace("@fyii.de", ""));
            browser.FindElementByClassName("imgbutton").Click();

            string source = browser.PageSource;
            string start = "mail-";
            string end = ">";

            int Start = source.IndexOf(start, 0) + start.Length;
            int End = source.IndexOf(end, Start);

            //////////////////////////////////////////////
            //
            // Navigate to confirmation page and login
            //
            //////////////////////////////////////////////

            Console.WriteLine("$$ Navigating to confirmation site ...");
            Console.WriteLine("");

            browser.Navigate().GoToUrl("http://www.fyii.de/mail.php?search=" + email.Replace("@fyii.de", "") + "&nr=" + source.Substring(Start, End - Start).Replace('"', ' ').Trim());

            source = browser.PageSource;
            start = "push the button";
            end = "if gte mso 11";

            Start = source.IndexOf(start, 0) + start.Length;
            End = source.IndexOf(end, Start);

            source = source.Substring(Start, End - Start);

            start = "href=";
            end = ">";

            Start = source.IndexOf(start, 0) + start.Length;
            End = source.IndexOf(end, Start);

            Console.WriteLine("$$ Log in to finish the confirmation process ...");
            Console.WriteLine("");

            browser.Navigate().GoToUrl(source.Substring(Start, End - Start).Replace('"', ' ').Trim());

            Console.WriteLine("$$ DONE!!! ;-)");
            Console.WriteLine("");

            Console.ReadLine();
        }
    }
}
