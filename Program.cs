using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

namespace StudioPatcher2
{
    class Program
    {
        [DllImport("msvcrt")] static extern int _getch();
        [DllImport("crtdll.dll")] public static extern int _kbhit();
        [STAThread]
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(@"
            _____ _             _ _         _____      _       _               
            / ____| |           | (_)       |  __ \    | |     | |              
            | (___ | |_ _   _  __| |_  ___   | |__) |_ _| |_ ___| |__   ___ _ __ 
            \___ \| __| | | |/ _` | |/ _ \  |  ___/ _` | __/ __| '_ \ / _ \ '__|
            ____) | |_| |_| | (_| | | (_) | | |  | (_| | || (__| | | |  __/ |   
            |_____/ \__|\__,_|\__,_|_|\___/  |_|   \__,_|\__\___|_| |_|\___|_|   

            ");

                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. Patch Studio");
                Console.WriteLine("2. Get Latest Resource Files");
                Console.WriteLine("3. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PatchStudio();
                        break;

                    case "2":
                        GetLatestResourceFiles();
                        break;

                    case "3":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please choose 1, 2 or 3.");
                        Thread.Sleep(2000);
                        break;
                }
            }
        }

        static void PatchStudio()
        {
            Console.WriteLine("Please select RobloxStudioBeta.exe.");

            // Gets the path with RobloxStudioBeta.exe in it
            string appDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string versionsFolderPath = Path.Combine(appDataFolderPath, "Roblox", "Versions");
            var versionsDir = Directory.GetDirectories(versionsFolderPath);
            string targetDir = versionsDir.FirstOrDefault(dir => File.Exists(Path.Combine(dir, "RobloxStudioBeta.exe")));

            // Prompt the user to select a file from the target directory
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = targetDir;
            dialog.Filter = "Roblox Studio|RobloxStudioBeta.exe";
            dialog.Title = "Select RobloxStudioBeta.exe";
            dialog.ShowDialog();

            // Load the .exe into a list
            var list = new List<string> { dialog.FileName };
            Console.WriteLine("Loaded .exe, Press Enter to continue!");

            foreach (string item in list)
            {
                string fileName = Path.GetFileName(item);

                if (fileName == "RobloxStudioBeta.exe")
                {
                    Console.WriteLine("Loaded RobloxStudioBeta");
                    Thread.Sleep(1000);
                    Console.WriteLine("Looking for bytes..");
                    Thread.Sleep(1000);
                    byte[] byt = File.ReadAllBytes(item);
                    for (int i = 0; i <= byt.Length - 1; i++)
                    {
                        if (byt[i] == 0x3A)
                        {
                            if (byt[i + 1] == 0x2F)
                            {
                                if (byt[i + 2] == 0x50)
                                {
                                    if (byt[i + 3] == 0x6C)
                                    {
                                        byt[i] = 0x2E;
                                        byt[i + 1] = 0x2F;
                                        byt[i + 2] = 0x50;
                                        byt[i + 3] = 0x6C;
                                        Console.WriteLine("Found byte & Patching " + byt[i]);
                                    }
                                }
                            }
                        }
                    }
                    Console.WriteLine("Found Bytes and Patched RobloxStudioBeta");
                    Console.WriteLine("Creating Folders");

                    string folder = Path.Combine(Environment.CurrentDirectory, "Platform");
                    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                    string resourceFolder = Path.Combine(folder, "Base");

                    if (!Directory.Exists(resourceFolder)) Directory.CreateDirectory(resourceFolder);
                    string QTFolder = Path.Combine(resourceFolder, "QtUI");

                    if (!Directory.Exists(QTFolder)) Directory.CreateDirectory(resourceFolder);
                    string themeFolder = Path.Combine(resourceFolder, "themes");

                    if (!Directory.Exists(themeFolder)) Directory.CreateDirectory(themeFolder);
                    {
                        string darkTheme = Path.Combine(themeFolder, "DarkTheme.json");
                        string lightTheme = Path.Combine(themeFolder, "LightTheme.json");

                        File.WriteAllText(darkTheme, System.IO.File.ReadAllText(Environment.CurrentDirectory + @"\Resources\DarkTheme.json"));
                        File.WriteAllText(lightTheme, System.IO.File.ReadAllText(Environment.CurrentDirectory + @"\Resources\LightTheme.json"));

                        // Get the original path 
                        string originalFilePath = Path.GetFullPath(item);
                        string originalDirectory = Path.GetDirectoryName(originalFilePath);

                        // Save the patched file to the original path
                        string patchedFilePath = Path.Combine(originalDirectory, "RobloxStudioPatched.exe");
                        File.WriteAllBytes(patchedFilePath, byt);
                        Console.WriteLine("File has been saved to " + originalDirectory);

                        // Open file explorer with patched file selected and close console
                        System.Diagnostics.Process.Start("explorer.exe", "/select, \"" + patchedFilePath + "\"");
                    }

                }
                else
                {
                    Console.WriteLine("Incorrect App Specified");
                }
            }
            Console.WriteLine("Press any key to end...");
            Console.Read();
        }

        static void GetLatestResourceFiles()
        {
            string folderPath = Path.Combine(Environment.CurrentDirectory, "Platform", "Base", "QtUI", "themes");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (WebClient client = new WebClient())
            {
                string darkThemeURL = "https://raw.githubusercontent.com/MaximumADHD/Roblox-Client-Tracker/roblox/QtResources/Platform/Base/QtUI/themes/DarkTheme.json";
                string lightThemeURL = "https://raw.githubusercontent.com/MaximumADHD/Roblox-Client-Tracker/roblox/QtResources/Platform/Base/QtUI/themes/LightTheme.json";

                string darkThemeFilePath = Path.Combine(folderPath, "DarkTheme.json");
                string lightThemeFilePath = Path.Combine(folderPath, "LightTheme.json");

                client.DownloadFile(new Uri(darkThemeURL), darkThemeFilePath);
                client.DownloadFile(new Uri(lightThemeURL), lightThemeFilePath);
            }

            Console.WriteLine("Latest resource files downloaded successfully!", folderPath);
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}
