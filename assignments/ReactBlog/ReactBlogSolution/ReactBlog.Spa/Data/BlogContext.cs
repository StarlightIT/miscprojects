using Microsoft.EntityFrameworkCore;
using ReactBlog.Spa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactBlog.Spa.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        public DbSet<BlogPost> BlogPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPost>().ToTable("BlogPost");

            for (int i = 0; i < 12; i++)
            {
                int months = (i + 1) * (-1);
                modelBuilder.Entity<BlogPost>().HasData(new BlogPost
                {
                    Id = Guid.NewGuid(),
                    Title = SampleData.Titles[i],
                    Ingress = SampleData.Ingresses[i],
                    Text = SampleData.Texts[i],
                    Author = SampleData.Authors[i],
                    AuthorEmail = SampleData.Emails[i],
                    Published = DateTimeOffset.UtcNow.AddMonths(months)
                });
            }
        }
    }
}
