using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GrammarWorkbook.Data;
using GrammarWorkbook.Data.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GrammarWorkbook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                //db.Database.EnsureCreated();
                db.Database.Migrate();

                if (!db.Units.Any())
                {
                    db.Units.Add(new Unit()
                        {
                            Id = Guid.Parse("2CCB48AD-1627-4349-1717-08D667F7E9FF"),
                            Title = "Unit 1"
                        }
                    );
                    db.SaveChanges();
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}