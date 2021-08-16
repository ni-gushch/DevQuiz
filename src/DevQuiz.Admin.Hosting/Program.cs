using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DevQuiz.Admin.Hosting
{
    /// <summary>
    /// Entrypoint of project
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry method
        /// </summary>
        /// <param name="args">Args for project</param>
        /// <returns></returns>
        public static Task Main(string[] args) =>
            CreateHostBuilder(args)
                .Build()
                .RunAsync();
        
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}