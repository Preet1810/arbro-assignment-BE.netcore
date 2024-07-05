using StudentStore;
using StudentStore.Data;
using StudentStore.Entities;
using Microsoft.EntityFrameworkCore;
namespace StudentStore.Endpoints;

public static class StudentEndpoints
{
    private static readonly List<StudentDto> students = new List<StudentDto>
    {
    };
    public static RouteGroupBuilder MapStudentsEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("students");
        group.MapGet("/", async (StudentStoreContext dbContext) =>
            {
                var students = await dbContext.Students.ToListAsync();
                return Results.Ok(students);
            }).WithName("GetStudents");

        // GET /students/1
        group.MapGet("/{id}", (int id, StudentStoreContext dbContext) =>
             {
                 Student? student = dbContext.Students
                                    .Include(s => s.Address)
                                    .Include(s => s.FamilyMembers)
                                    .Include(s => s.Certifications)
                                    .FirstOrDefault(s => s.Id == id);
                 return student is null ? Results.NotFound() : Results.Ok(student);
             }).WithName("GetStudent");

        // POST /students
        group.MapPost("/", async (CreateStudentDto newStudent, StudentStoreContext dbContext) =>
        {

            // Map AddressDto to Address
            Address address = new Address
            {
                AddressLine1 = newStudent.Address.AddressLine1,
                City = newStudent.Address.City,
                State = newStudent.Address.State,
                Pincode = newStudent.Address.Pincode
            };

            // Map FamilyMemberDto to FamilyMember
            List<FamilyMember> familyMembers = newStudent.FamilyMembers.Select(fm => new FamilyMember
            {
                Name = fm.Name,
                Relationship = fm.Relationship,
            }).ToList();

            // Map CertificationDto to Certification
            List<Certification> certifications = newStudent.Certifications.Select(c => new Certification
            {
                Name = c.Name,
                Institution = c.Institution,
            }).ToList();

            Student student = new Student
            {
                Name = newStudent.Name,
                FatherName = newStudent.FatherName,
                MotherName = newStudent.MotherName,
                Email = newStudent.Email,
                Address = address,
                FamilyMembers = familyMembers,
                Certifications = certifications
            };

            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute("GetStudent", new { id = student.Id }, student);
        });


        // DELETE /students
        group.MapDelete("/{id}", async (int id, StudentStoreContext dbContext) =>
            {
                var student = await dbContext.Students.SingleOrDefaultAsync(s => s.Id == id);
                if (student == null)
                {
                    return Results.NotFound();
                }

                dbContext.Students.Remove(student);
                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            });

        return group;
    }
}
