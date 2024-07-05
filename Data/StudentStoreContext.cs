using Microsoft.EntityFrameworkCore;
using StudentStore.Entities;
namespace StudentStore.Data;

public class StudentStoreContext(DbContextOptions<StudentStoreContext> options)
: DbContext(options)
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<FamilyMember> FamilyMembers { get; set; }
    public DbSet<Certification> Certifications { get; set; }
}
