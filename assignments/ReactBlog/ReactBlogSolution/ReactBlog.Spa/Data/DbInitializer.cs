using ReactBlog.Spa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactBlog.Spa.Data
{
    public static class DbInitializer
    {
        

        public static void Initialize(BlogContext context)
        {
            context.Database.EnsureCreated();

            // Look for any blog posts.
            if (context.BlogPosts.Any())
            {
                return;   // DB has been seeded
            }

            var blogPosts = new List<BlogPost>();

            for (int i=0;i<12;i++)
            {
                int months = (i + 1) * (-1);

                var blogPost = new BlogPost
                {
                    Title = SampleData.Titles[i],
                    Ingress = SampleData.Ingresses[i],
                    Text = SampleData.Texts[i],
                    Author = SampleData.Authors[i],
                    AuthorEmail = SampleData.Emails[i],
                    Published = DateTimeOffset.UtcNow.AddMonths(months)
                };
                blogPosts.Add(blogPost);
            }

            context.BlogPosts.AddRange(blogPosts);
            context.SaveChanges();
        }
    }
}
