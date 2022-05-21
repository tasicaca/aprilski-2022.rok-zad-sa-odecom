using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;////dodato za toListAsync
using Microsoft.Extensions.Logging;
using Template.Models;

namespace Template.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IspitController : ControllerBase
    {
        IspitDbContext Context { get; set; }

        public IspitController(IspitDbContext context)
        {
            Context = context;
        }

        [Route("PreuzmiProdavnice")]
        [HttpGet]
        public async Task<List<Prodavnica>> PreuzmiProdavnice()
        {
          //var vraceneProdavnice=await Context.Prodavnica.FindAsync(idProdavnice);
          return await Context.Prodavnica.ToListAsync();
        }

        [Route("PreuzmiBrendove/{idProdavnice}")]
        [HttpGet]
        public async Task<JsonResult> PreuzmiBrendove(int idProdavnice)
        {
          var vraceniProizvodi=await Context.Proizvod.Include(p=>p.Brend).Where(p=>p.Prodavnica.ID==idProdavnice).ToListAsync();

          List<Brend> vraceniBrendovi=new List<Brend>();
          List<Brend> listaSvihBrendova=new List<Brend>();
           // if (vraceniProizvodi!=null)
           // {
          
          foreach (Proizvod El in vraceniProizvodi ){
            var vraceniBrnd=await Context.Brend.Include(p=>p.Proizvod).Where(p=>p.Proizvod.Contains(El)).FirstOrDefaultAsync();
            if (vraceniBrnd!=null)
            {
              listaSvihBrendova.Add(vraceniBrnd);
            }
         //   i++;         
          } 
          return new JsonResult(listaSvihBrendova);
          //} 
          //else return null;
          
        }

        ////RADI ALI SAM PROBAO DRUGACIJE
        /* [Route("PreuzmiBrendove/{idProdavnice}")]
        [HttpGet]
        public async Task<List<Brend>> PreuzmiBredove(int idProdavnice)
        {
          var vraceneProdavnice=await Context.Prodavnica.Where(p=>p.ID==idProdavnice).FirstAsync();
          var vraceniBrendovi=await Context.Brend.Where(p=>p.Prodavnica.Contains(vraceneProdavnice)).ToListAsync();
          return vraceniBrendovi;
        }
        */

       /* [Route("PreuzmiTip/{idProdavnice}")]
        [HttpGet]
        public async Task<List<Tip>> PreuzmiTip(int idProdavnice)
        {
          var vraceneProdavnice=await Context.Prodavnica.Where(p=>p.ID==idProdavnice).FirstAsync();
          var vraceniTipovi=await Context.Tip.Where(p=>p.Prodavnica.Contains(vraceneProdavnice)).ToListAsync();
          return vraceniTipovi;
        }
   */

        [Route("PreuzmiTip/{idProdavnice}")]
        [HttpGet]
        public async Task<JsonResult> PreuzmiTip(int idProdavnice)
        {
          var vraceniProizvodi=await Context.Proizvod.Include(p=>p.Brend).Where(p=>p.Prodavnica.ID==idProdavnice).ToListAsync();

          List<Tip> vraceniBrendovi=new List<Tip>();
          List<Tip> listaSvihBrendova=new List<Tip>();
          
         // int i=0;
            
          foreach (Proizvod El in vraceniProizvodi ){
            var vraceniBrnd=await Context.Tip.Include(p=>p.Proizvod).Where(p=>p.Proizvod.Contains(El)).FirstOrDefaultAsync();
            if (vraceniBrnd!=null)
            {
              listaSvihBrendova.Add(vraceniBrnd);
            }      
          } 
          return new JsonResult(listaSvihBrendova);
          //} 
          //else return null;
        }

        [Route("PrijemPodataka/{nizacena}/{visacena}/{idBrenda}/{idTipa}/{idProd}")]
        [HttpGet]
        public async Task<JsonResult> PrijemPodataka(int nizacena, int visacena, int idBrenda, int idTipa,int idProd)
        {
          var nadjeniPodaci = await Context.Proizvod.Where(p=>p.Prodavnica.ID==idProd && p.Brend.ID==idBrenda && p.Tip.ID==idTipa && p.cena>=nizacena  && p.cena<=visacena).Include(p=>p.Tip).ToListAsync(); ///moras tamo gde je await da imas i async
          if (nadjeniPodaci!=null){
                       
              return new JsonResult(nadjeniPodaci);
              }
          else 
          {
            return new JsonResult(nadjeniPodaci=null);
          }
        }
        [Route("KupovinaKonfiguracije/{nizIzabranihProizvoda}/{idProdavnice}")]
        [HttpPut]
        public async Task<ActionResult> KupovinaKonfiguracije(string nizIzabranihProizvoda, int idProdavnice)
        {///radi preko contains posto je to jedini nacin da pretrazujes da li je neki manji string u vecem stringu!
        //niz izabranih proizvoda sastoji se od id-eva elemenata i slova a koje ih razdvaja.

          do{
          
          string prviID=nizIzabranihProizvoda.Substring(0,nizIzabranihProizvoda.IndexOf("a"));
          var Element=await Context.Proizvod.Where(p=>((p.ID).ToString()==prviID) && p.Prodavnica.ID==idProdavnice).FirstAsync();
        
                Element.kolicina--;
                if (Element.kolicina==0){
                    Context.Proizvod.Remove(Element);}
                    else if (Element.kolicina==-1){
                    return BadRequest("Nema dovoljno proizvoda za realizovanje kupovine");}
                  else{
                    Context.Proizvod.Update(Element);
                }
               
              int loc=nizIzabranihProizvoda.IndexOf("a");
              nizIzabranihProizvoda=nizIzabranihProizvoda.Remove(0, loc+1); //izbacuje deo stringa izmedju dva slova a
          }
          while ( nizIzabranihProizvoda.IndexOf("a")!=-1);

          await Context.SaveChangesAsync();//////context se tek zapamti na kraju tako da mozes raditi sta god hoces, neces obrisati polovinu elemenata a polovinu ne npr
          return Ok("uspesno pretrazeni proizvodi");
        /*
          while ( nizIzabranihProizvoda.IndexOf("a")!=-1){//-1 je vrednost ako ne nadje string a u nizuIzabranihProizvoda
            var NadjeniProizvod=await Context.Proizvod.Where(p=>nizIzabranihProizvoda.Contains((p.ID).ToString()) && p.Prodavnica.ID==idProdavnice).ToListAsync();
          
            foreach(Proizvod Element in NadjeniProizvod)
               { 
                Element.kolicina--;
                if (Element.kolicina<=0)
                    Context.Proizvod.Remove(Element);
                else{
                    Context.Proizvod.Update(Element);
                }
               }
              int loc=nizIzabranihProizvoda.IndexOf("a");
              nizIzabranihProizvoda=nizIzabranihProizvoda.Remove(0, loc+1); //izbacuje deo stringa izmedju dva slova a
            }
          await Context.SaveChangesAsync();
          return Ok("uspesno pretrazeni proizvodi");
*/
        }
    }
}


//

/*Jedino sto smo dodali u ovom Template-u je ,
  "ConnectionStrings": {
    "IspitCS": "Server=(localdb)\\MSSQLLocalDB;Database=TestBazaPodataka"   
  },
  "AllowedHosts": "*" i index.html */