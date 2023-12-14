using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetWeb.Models;

public class Evenement{
    public int EvenementID { get; set; }

    [ForeignKey("Vol")]
    public int IDVol { get; set; }
    public Vol? Vol { get; set; }

    [Display(Name = "Heure Révisée")]
    public DateTime HeureRevisee { get; set; }
    public string? Statut { get; set; } 

}