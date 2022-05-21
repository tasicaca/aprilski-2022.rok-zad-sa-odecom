using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Template.Models
{
    [Table("Prodavnica")]
    public class Prodavnica
    {
        [Key] 
        public int ID { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Neophodno je uneti naziv !")]
       
        public string Naziv { get; set; }

        public List<Proizvod> Proizvod {get;set;}

     //   public List<Brend> Brend {get;set;}

     //   public List<Tip> Tip{get;set;}
        
        public Prodavnica()
        {
            this.Proizvod = new List<Proizvod>();
        }
    }
}
