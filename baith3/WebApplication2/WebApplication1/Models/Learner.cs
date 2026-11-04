using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

[Table("Learner")]
[Index("MajorId", Name = "IX_Learner_MajorID")]
public partial class Learner
{
    [Key]
    [Column("LearnerID")]
    public int LearnerId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstMidName { get; set; } = null!;

    public DateTime EnrollmentDate { get; set; }

    [Column("MajorID")]
    public int MajorId { get; set; }

    [InverseProperty("Learner")]
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    [ForeignKey("MajorId")]
    [InverseProperty("Learners")]
    public virtual Major Major { get; set; } = null!;
}
