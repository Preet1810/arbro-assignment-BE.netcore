namespace StudentStore;

public record class AddressDto(
        string AddressLine1,
        string City,
        string State,
        int Pincode
    );

public record class FamilyMemberDto(
        int Id,
        string Name,
        string Relationship
    );

public record class CertificationDto(
        int Id,
        string Name,
        string Institution
    );
public record class StudentDto(
       int Id,
       string Name,
       string FatherName,
       string MotherName,
       string Email,
       AddressDto Address,
       List<FamilyMemberDto> FamilyMembers,
       List<CertificationDto> Certifications
   );

public record class CreateStudentDto(
        string Name,
        string FatherName,
        string MotherName,
        string Email,
        AddressDto Address,
        List<FamilyMemberDto> FamilyMembers,
        List<CertificationDto> Certifications
    );
