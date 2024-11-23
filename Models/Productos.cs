using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaWeb.Models;
public class Productos
{
    [Key]
    public int id {get; set;}
    [Display(Name ="Nombre del producto")]
    [Required(ErrorMessage ="Ingresa el Nombre del rpoducto")]
    public string? nombre{get; set;}
    [Display(Name ="Precio del producto")]
    [Required(ErrorMessage ="Ingresa el Precio del rpoducto")]
    public double precio{get; set;}
    [Display(Name ="Categoria")]
    public int id_categorias{get; set;}
    [ForeignKey("id_categoria")]
    public Categorias? categorias{get; set;}
    [Display(Name ="Imagen del Producto")]
    public string? UrlImagen{get; set;}
}