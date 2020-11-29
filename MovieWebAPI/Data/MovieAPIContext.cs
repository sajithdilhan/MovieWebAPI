using Microsoft.EntityFrameworkCore;
using MovieWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebAPI.Data
{
    public class MovieAPIContext : DbContext
    {
        public MovieAPIContext(DbContextOptions<MovieAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }
        public DbSet<User> User { get; set; }
    }
}
