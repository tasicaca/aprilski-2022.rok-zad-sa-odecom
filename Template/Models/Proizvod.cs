using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Template.Models
{
    [Table("Proizvod")]
    public class Proizvod
    {
        [Key]
        [Column("ID")]    
        public int ID{get;set;}

        [Column("Sifra")]
        public int sifra{get;set;}
        
        [Column("Naziv")]
        public string naziv{get;set;}

        [Column("Kolicina")]
        public int kolicina{get;set;}

        [Column("Cena")]
        public int cena{get;set;}
        public virtual Prodavnica Prodavnica {get;set;}

        public virtual Brend Brend {get;set;}
       
        public virtual Tip Tip {get;set;}

        public Proizvod()
        {
        }
    }
}