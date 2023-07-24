using System.ComponentModel.DataAnnotations.Schema;

public class Developer
{
    [Column("id")]
    public Guid Id {get; set;}
    [Column("name")]

    public String? Name {get; set;}
    [Column("email")]

    public String? Email {get; set;}
    [Column("created_at")]

    public DateTime CreatedAt {get; set;}
}

