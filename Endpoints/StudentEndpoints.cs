using StudentStore;
using StudentStore.Data;
using StudentStore.Entities;

namespace StudentStore.Endpoints;

public static class StudentEndpoints
{
    private static readonly List<StudentDto> students = new List<StudentDto>
    {
    };
    public static RouteGroupBuilder MapStudentsEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("students");
        // GET /students
        group.MapGet("/", () => students);

        // GET /students/1
        group.MapGet("/{id}", (int id) =>
        {
            StudentDto? student = students.Find(student => student.Id == id);
            return student is null ? Results.NotFound() : Results.Ok(student);
        }).WithName("GetStudent");

        // POST /students
        group.MapPost("/", (CreateStudentDto newStudent, StudentStoreContext dbContext) =>
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

            dbContext.Students.Add(student);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute("GetStudent", new { id = student.Id }, student);
        });


        // DELETE /students
        group.MapDelete("/{id}", (int id) =>
        {
            students.RemoveAll(student => student.Id == id);
            return Results.NoContent();
        });
        return group;
    }
}
