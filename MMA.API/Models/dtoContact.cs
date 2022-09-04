using MMA.API.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MMA.API.Models
{
  public abstract partial class dtoContactBase
  {
    public int ClientId { get; set; }

    [StringLength(50)]
    public string ContactType { get; set; }
    [StringLength(50)]
    public string Title { get; set; }
    [StringLength(50)]
    public string FullName { get; set; }
    [StringLength(50)]
    public string Phone { get; set; }

    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    [StringLength(100)]
    public string EmailAddress { get; set; }

  }

  public partial class dtoContact : dtoContactBase
  {
    public int Id { get; set; }
    public string ClientName { get; set; }

  }

  public partial class dtoContactOnly : dtoContactBase
  {
    public int Id { get; set; }
    public string ClientName { get; set; }

  }

  public partial class dtoContactCreate : dtoContactBase
  {
    public int Id { get; set; }

  }
  public partial class dtoContactUpdate : dtoContactBase
  {
    public int Id { get; set; }

  }

}