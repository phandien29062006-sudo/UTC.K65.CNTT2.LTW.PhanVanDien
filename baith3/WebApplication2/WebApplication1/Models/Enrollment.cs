using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

[Table("Enrollment")]
[Index("CourseId", Name = "IX_Enrollment_CourseID")]
[Index("LearnerId", Name = "IX_Enrollment_LearnerID")]
public partial class Enrollment
{
    [Key]
    [Column("EnrollmentID")]
    public int EnrollmentId { get; set; }

    [Column("CourseID")]
    public int CourseId { get; set; }

    [Column("LearnerID")]
    public int LearnerId { get; set; }

    public float Grade { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("Enrollments")]
    public virtual Course Course { get; set; } = null!;

    [ForeignKey("LearnerId")]
    [InverseProperty("Enrollments")]
    public virtual Learner Learner { get; set; } = null!;
}
