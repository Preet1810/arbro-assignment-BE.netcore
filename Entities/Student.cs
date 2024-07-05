using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentStore.Entities
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        public string AddressLine1 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public int Pincode { get; set; }
    }

    public class FamilyMember
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Relationship { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }

    public class Certification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Institution { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }

    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FatherName { get; set; }

        [Required]
        public string MotherName { get; set; }

        [Required]
        public string Email { get; set; }

        public Address Address { get; set; }

        public List<FamilyMember> FamilyMembers { get; set; }

        public List<Certification> Certifications { get; set; }
    }
}