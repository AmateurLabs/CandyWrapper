using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmateurLabs.CandyWrapper.Scr
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Process.GetCurrentProcess().MainModule.FileName;
            string directory = Path.GetDirectoryName(path);
            string filename = Path.GetFileNameWithoutExtension(path);

            Environment.CurrentDirectory = directory;

            bool dataDirectoryExists = Directory.Exists("Data/");

            string configPath = "config.txt";
            if (!File.Exists(configPath) && dataDirectoryExists)
                configPath = "Data/config.txt";

            string executable = ((dataDirectoryExists) ? "Data/" : "") + filename + ".exe";
            string arguments = "";
            bool handleDefault = true;
            bool handlePreview = false;
            bool handleSettings = false;

            if (File.Exists(configPath)) {
                string[] lines = File.ReadAllLines(configPath);
                foreach (string line in lines)
                {
                    string key = line.Substring(0, line.IndexOf('='));
                    string value = line.Substring(line.IndexOf('='));
                    if (key == "executable") executable = value;
                    else if (key == "arguments") arguments = value;
                    else if (key == "handleDefault") handleDefault = value == "true";
                    else if (key == "handlePreview") handlePreview = value == "true";
                    else if (key == "handleSettings") handleSettings = value == "true";
                }
            }

            bool run = false;

            foreach (string arg in args)
            {
                if (arg.StartsWith("/s") && handleDefault) run = true;
                if (arg.StartsWith("/p") && handlePreview) run = true;
                if (arg.StartsWith("/l") && handlePreview) run = true;
                if (arg.StartsWith("/c") && handleSettings) run = true;
            }

            if (!run) return;
            ProcessStartInfo info = new ProcessStartInfo(executable, string.Join(" ", args));
            info.WorkingDirectory = directory;
            info.Arguments += " " + arguments;
            Process process = Process.Start(info);
            process.WaitForExit();
        }
    }
}
