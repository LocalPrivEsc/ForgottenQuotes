using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ForgottenQuote
{
    class Program
    {
        static void PrintBanner()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                                                                                    ");
            Console.WriteLine("     ███████████████████████████████████████████████████████████████████████████████");
            Console.WriteLine("                                                                                    ");
            Console.WriteLine("     ███████╗ ██████╗ ██████╗  ██████╗  ██████╗ ████████╗████████╗███████╗███╗   ██╗");
            Console.WriteLine("     ██╔════╝██╔═══██╗██╔══██╗██╔════╝ ██╔═══██╗╚══██╔══╝╚══██╔══╝██╔════╝████╗  ██║");
            Console.WriteLine("     █████╗  ██║   ██║██████╔╝██║  ███╗██║   ██║   ██║      ██║   █████╗  ██╔██╗ ██║");
            Console.WriteLine("     ██╔══╝  ██║   ██║██╔══██╗██║   ██║██║   ██║   ██║      ██║   ██╔══╝  ██║╚██╗██║");
            Console.WriteLine("     ██║     ╚██████╔╝██║  ██║╚██████╔╝╚██████╔╝   ██║      ██║   ███████╗██║ ╚████║");
            Console.WriteLine("     ╚═╝      ╚═════╝ ╚═╝  ╚═╝ ╚═════╝  ╚═════╝    ╚═╝      ╚═╝   ╚══════╝╚═╝  ╚═══╝");
            Console.WriteLine("                                                                                    ");
            Console.WriteLine("                      ██████╗ ██╗   ██╗ ██████╗ ████████╗███████╗                   ");
            Console.WriteLine("                     ██╔═══██╗██║   ██║██╔═══██╗╚══██╔══╝██╔════╝                   ");
            Console.WriteLine("                     ██║   ██║██║   ██║██║   ██║   ██║   █████╗                     ");
            Console.WriteLine("                     ██║▄▄ ██║██║   ██║██║   ██║   ██║   ██╔══╝                     ");
            Console.WriteLine("                     ╚██████╔╝╚██████╔╝╚██████╔╝   ██║   ███████╗                   ");
            Console.WriteLine("                      ╚══▀▀═╝  ╚═════╝  ╚═════╝    ╚═╝   ╚══════╝                   ");
            Console.WriteLine("              [ U n q u o t e d - S e r v i c e - P a t h - F i n d e r ]           ");
            Console.WriteLine("                      [ B a s e d - O n - P o w e r U p . p s 1 ]                   ");
            Console.WriteLine("                                                                                    ");
            Console.WriteLine("     ███████████████████████████████████████████████████████████████████████████████");
            Console.WriteLine("                                                                                    ");
            Console.ForegroundColor = ConsoleColor.Green;
        }

        static void Main(string[] args)
        {
            PrintBanner();
            ServiceController[] SCServiceList;
            SCServiceList = ServiceController.GetServices();
            foreach (ServiceController SCArray in SCServiceList)
            {
                if (SCArray.Status == ServiceControllerStatus.Running)
                {
                    ManagementObject WMIService;
                    WMIService = new ManagementObject("Win32_Service.Name='" + SCArray.ServiceName + "'");
                    WMIService.Get();
                    if (WMIService["StartName"] != null)
                    {
                        //Store the path name as a string for sanity checks
                        string PathName = WMIService["PathName"] as string;
                        string[] Spaces = PathName.Split(' ');

                        //Check if path starts with quotes if so do nothing
                        if (PathName.StartsWith("\""))
                            continue;

                        //Check if path does not contain spaces if so do nothing
                        if (!PathName.Contains(" ")) 
                            continue;

                        //Check if path does not contain .exe if so do nothing
                        if (!PathName.EndsWith(".exe"))
                            continue;

                        Console.WriteLine("[!] Service Detected");
                        Console.WriteLine("[+] Executable                {0}", WMIService["PathName"]);
                        Console.WriteLine("[+] Status                    {0}", WMIService["Status"]);
                        Console.WriteLine("[+] State:                    {0}", WMIService["State"]);
                        Console.WriteLine("[+] Start Mode:               {0}", WMIService["StartMode"]);
                        Console.WriteLine("[+] Start Name:               {0}", WMIService["StartName"]);
                        
                        foreach (string Path in Spaces)
                        {
                            if (!Path.EndsWith(".exe"))
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("[!] VULNERABLE PATH LOCATION: {0}", Path);
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                        }

                        Console.WriteLine("");

                    }
                }
            }
            Console.ReadKey();
        }
    }
}