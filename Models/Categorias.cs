using System.ComponentModel.DataAnnotations;

namespace TiendaWeb.Models;
public class Categorias
{
    [Key]
    public int id {get; set;}
    [Display(Name ="Nombre Categoria")]
    [Required(ErrorMessage ="Ingresa el nombtr de la categoria")]
    public string nombre {get; set;}
}