using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReactBlog.Spa.Data;
using ReactBlog.Spa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactBlog.Spa.Services
{
    public class BlogPostsService : IBlogPostsService
    {
        private ILogger<BlogPostsService> _logger;
        private readonly BlogContext _context;

        public BlogPostsService(ILogger<BlogPostsService> logger, BlogContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<BlogPost>> GetBlogPosts()
        {
            return await _context.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost> GetBlogPost(Guid id)
        {
            return await _context.BlogPosts.FindAsync(id);
        }

        public async Task<bool> PutBlogPost(BlogPost blogPost)
        {

            _context.Entry(blogPost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogPostExists(blogPost.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<BlogPost> PostBlogPost(BlogPost blogPost)
        {
            _context.BlogPosts.Add(blogPost);
            await _context.SaveChangesAsync();
            return blogPost;
        }
        public async Task<bool> DeleteBlogPost(Guid id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return false;
            }

            _context.BlogPosts.Remove(blogPost);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool BlogPostExists(Guid id)
        {
            return _context.BlogPosts.Any(e => e.Id == id);
        }
    }
}
