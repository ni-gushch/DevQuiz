using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DevQuiz.TelegramBot
{
    /// <summary>
    /// App entry point
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main class
        /// </summary>
        /// <param name="args">Application arguments</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creating web app host
        /// </summary>
        /// <param name="args">Application arguments</param>
        /// <returns>Web app host object</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
