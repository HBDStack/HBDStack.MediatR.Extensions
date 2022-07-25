using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HBD.MediatR.DDD.Tests.Data;

public class TestEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }= default!;

    [MaxLength(500)] public string Name { get; set; } = default!;
}