using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PostsService.Models;

namespace PostsService.Data
{
    public static class SeedDb
    {
        public static void Seed(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if(!context.Posts.Any())
            {
                Console.WriteLine(" ==> Seeding Database ...");
                context.Posts.AddRange(
                    new Post()
                    {
                        Title = "Title 1",
                        Content = "Content 1",
                        PublishedDate = DateTime.Now
                    },
                    new Post()
                    {
                        Title = "Title 2",
                        Content = "Content 2",
                        PublishedDate = DateTime.Now.AddHours(-10)
                    },
                    new Post()
                    {
                        Title = "Title 3",
                        Content = "Content 3",
                        PublishedDate = DateTime.Now.AddHours(-20)
                    }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("We already have data.");
            }
        }
    }
}