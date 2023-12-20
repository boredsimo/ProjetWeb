using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetWeb.Models;

public class Vol
{
    [Key]
    public int Id { get; set; }
    
    public string? Compagnie { get; set; }
    [Display(Name = "Code Vol")]
    public string? CodeVol { get; set; }

    public string? Ville { get; set; } 
    [Display(Name = "Heure Prévue")]
    public DateTime HeurePrevue { get; set; }

    [Display(Name = "Heure Révisée")]
    public DateTime HeureRevisee { get; set; }
    public string? Statut { get; set; }
    public ICollection<Evenement>? Evenements { get; set; }
}