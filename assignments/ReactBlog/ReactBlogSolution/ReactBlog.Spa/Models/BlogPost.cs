using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReactBlog.Spa.Models
{
    public class BlogPost
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(250)]
        public string Ingress { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        [StringLength(40)]
        public string Author { get; set; }

        [Required]
        [EmailAddress]
        public string AuthorEmail { get; set; }

        public DateTimeOffset Published { get; set; }
    }
}
