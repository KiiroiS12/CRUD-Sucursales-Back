using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Data;

[Table("Branch")]
public partial class Branch
{
    [Key]
    public int Id { get; set; }

    public int Code { get; set; }

    [StringLength(250)]
    public string Description { get; set; } = null!;

    [StringLength(250)]
    public string Address { get; set; } = null!;

    [StringLength(50)]
    public string Identification { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DateCreate { get; set; }

    public int IdCurrency { get; set; }

    [ForeignKey("IdCurrency")]
    [InverseProperty("Branches")]
    public virtual Currency IdCurrencyNavigation { get; set; } = null!;
}
