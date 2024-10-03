using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Versioning;

namespace InfinityOfficialNetwork.Management.OperatingSystem.Windows.EventRecorder.Core
{
    [SupportedOSPlatform("windows")]
    public class Program
    {
        const string ServiceName = "InfinityOfficialNetwork.Management.OperatingSystem.Windows.EventRecorder";



        public static void Main(string[] args)
        {
            if (args.Length > 0)
                if (args[0] == "/Install")
                {
                    string executablePath = Path.Combine(AppContext.BaseDirectory, Process.GetCurrentProcess().MainModule?.FileName ?? throw new NullReferenceException());
                    ProcessStartInfo processStartInfo = new ProcessStartInfo(fileName: "sc", new string[] { "create", ServiceName, $"binPath={executablePath}", "start=auto" });
                    processStartInfo.RedirectStandardError = true;
                    processStartInfo.RedirectStandardOutput = true;

                    using (Process process = new())
                    {
                        process.StartInfo = processStartInfo;
                        process.OutputDataReceived += (sender, args) =>
                        {
                            Console.WriteLine($"INFO sc.exe: {args.Data}");
                        };
                        process.ErrorDataReceived += (sender, args) =>
                        {
                            Console.WriteLine($"ERROR sc.exe: {args.Data}");
                        };

                        process.Start();
                        process.BeginErrorReadLine();
                        process.BeginOutputReadLine();


                        process.WaitForExit();
                        Thread.Sleep(5000);
                    }
                    return;
                }
                else if (args[0] == "/Uninstall")
                {
                    {
                        ProcessStartInfo processStartInfo = new ProcessStartInfo(fileName: "sc", new string[] { "stop", ServiceName });
                        processStartInfo.RedirectStandardError = true;
                        processStartInfo.RedirectStandardOutput = true;

                        using (Process process = new())
                        {
                            process.StartInfo = processStartInfo;
                            process.OutputDataReceived += (sender, args) =>
                            {
                                Console.WriteLine($"INFO sc.exe: {args.Data}");
                            };
                            process.ErrorDataReceived += (sender, args) =>
                            {
                                Console.WriteLine($"ERROR sc.exe: {args.Data}");
                            };

                            process.Start();
                            process.BeginErrorReadLine();
                            process.BeginOutputReadLine();

                            process.WaitForExit();
                        }
                    }

                    {
                        ProcessStartInfo processStartInfo = new ProcessStartInfo(fileName: "sc", new string[] { "delete", ServiceName });
                        processStartInfo.RedirectStandardError = true;
                        processStartInfo.RedirectStandardOutput = true;

                        using (Process process = new())
                        {
                            process.StartInfo = processStartInfo;
                            process.OutputDataReceived += (sender, args) =>
                            {
                                Console.WriteLine($"STDOUT sc.exe: {args.Data}");
                            };
                            process.ErrorDataReceived += (sender, args) =>
                            {
                                Console.WriteLine($"STDERR sc.exe: {args.Data}");
                            };

                            process.Start();
                            process.BeginErrorReadLine();
                            process.BeginOutputReadLine();

                            process.WaitForExit();
                        }
                    }
                    Thread.Sleep(5000);
                    return;
                }


            StartService(args);
        }

        internal static void StartService(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddWindowsService(options =>
                {
                    options.ServiceName = "InfinityOfficialNetwork.Management.OperatingSystem.Windows.EventRecorder";
                });

            builder.Services.AddDbContext<EventDbContext>(options =>
                {
                    options.UseSqlServer(@"Server=ION-PC-0017;Database=events;Trusted_Connection=True;TrustServerCertificate=True");
                });

            builder.Services.AddHostedService<Worker>();

            //LoggerProviderOptions.RegisterProviderOptions<EventLogSettings, EventLogLoggerProvider>(builder.Services);

            var host = builder.Build();
            host.Run();
        }
    }
}