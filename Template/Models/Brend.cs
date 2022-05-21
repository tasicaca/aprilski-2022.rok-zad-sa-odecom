using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Template.Models{
    [Table("Brend")]
    public class Brend
    {
        [Key]
        public int ID{get;set;}

        public string Naziv{get;set;}

        /*public List<Prodavnica> Prodavnica{get;set;}*/

        [JsonIgnore]/////////////dodato da spreci lancanu zavisnost
        public List<Proizvod> Proizvod{get;set;}
     
        public Brend()
        {
           //this.Prodavnica=new List<Prodavnica>();
           this.Proizvod=new List<Proizvod>();
        }
    }
}
