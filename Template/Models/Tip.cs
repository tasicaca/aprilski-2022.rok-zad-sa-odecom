using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Template.Models{
    [Table("Tip")]
    public class Tip//velicina u konkretnom zadatku
    {
        [Key]
        public int ID{get;set;}

        public string Naziv{get;set;}

      //  public List<Prodavnica> Prodavnica{get;set;}

        [JsonIgnore]////////////////Ovo je omogucilo da imam ovakav response, json ignore sprecava kruznu zavisnost////////[{"id":3114,"sifra":111,"naziv":"majica","kolicina":5,"cena":1,"prodavnica":null,"brend":null,"tip":{"id":31,"naziv":" M ","prodavnica":[]}}]
        public List<Proizvod> Proizvod{get;set;}
     
        public Tip()
        {
        //   this.Prodavnica=new List<Prodavnica>();
           this.Proizvod=new List<Proizvod>();
        }
    }
}
