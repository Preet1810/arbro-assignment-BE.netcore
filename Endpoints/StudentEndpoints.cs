namespace StudentStore;

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
        group.MapPost("/", (CreateStudentDto newStudent) =>
        {
            int newId = students.Count + 1;
            StudentDto student = new StudentDto(
                Id: newId,
                Name: newStudent.Name,
                FatherName: newStudent.FatherName,
                MotherName: newStudent.MotherName,
                Email: newStudent.Email,
                Address: newStudent.Address,
                FamilyMembers: newStudent.FamilyMembers,
                Certifications: newStudent.Certifications
            );

            students.Add(student);

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
