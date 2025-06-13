using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class ApplicationDbContext : DbContext
{
    public DbSet<Language> Language { get; set; }
    public DbSet<Student> Student { get; set; }
    public DbSet<Record> Record { get; set; }
    public DbSet<Models.Task> Task { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
}