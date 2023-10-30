using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Data;

[Table("Currency")]
public partial class Currency
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Description { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DateCreate { get; set; }

    [InverseProperty("IdCurrencyNavigation")]
    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();
}
