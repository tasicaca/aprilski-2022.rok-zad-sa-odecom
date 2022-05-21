
import {Proizvod} from "./Proizvod.js"
export class Prodavnica {
   
    constructor(id, naziv) {
        this.id = id;
        this.naziv = naziv;
        this.kontejner = null;
        this.nizIdIzabranihProizvoda="";//definicija stringa jer ne Saljes Niz kroz fetch
    }

    crtajPK(host) {

        if (!host) {
            throw new Exception("Roditeljski element ne postoji");
        }
        this.kontejner = document.createElement("div");
        this.kontejner.classList.add("kontejner");
        host.appendChild(this.kontejner);
        this.crtajFormu(this.kontejner);
    }
    crtajFormu(host) {
        const kontForma = document.createElement("div");
        kontForma.className = "kontForma";
        host.appendChild(kontForma);

        const kontForma1 = document.createElement("div");
        kontForma1.className = "kontForma1";
        kontForma.appendChild(kontForma1);

        var divZaIzborKategorije = document.createElement("div");

        let labela = document.createElement("label");
        labela.innerHTML = "Brendovi";
        divZaIzborKategorije.appendChild(labela);

        var sel = document.createElement("select");
        sel.name = "selectBrend";
        divZaIzborKategorije.appendChild(sel);
        kontForma1.appendChild(divZaIzborKategorije);

        fetch("https://localhost:5001/Ispit/PreuzmiBrendove/" + this.id, {
            method: "GET",
        }).then(p => {
            p.json().then(data => {
                data.forEach((dataa) => {
                    var opcija = document.createElement("option");
                    opcija.innerHTML = dataa.naziv;
                    opcija.value = dataa.id;
                    sel.appendChild(opcija);
                })
            })
        })

        var divZaIzborKategorije2  = document.createElement("div");
        labela = document.createElement("label");
        labela.innerHTML = "Velicina:                                ";
        divZaIzborKategorije2.appendChild(labela);
        kontForma1.appendChild(divZaIzborKategorije2);

        fetch("https://localhost:5001/Ispit/PreuzmiTip/" + this.id, {/////tip odnosno u ovoj verziji zadatka velicina
            method: "GET",
        }).then(p => {
            p.json().then(data => {
                data.forEach((dataa) => {
                    var opcija = document.createElement("input");
                    opcija.innerHTML = dataa.naziv;
                    opcija.type= "radio";
                    opcija.name= "izborVelicine";
                    opcija.value = dataa.id;
                    divZaIzborKategorije2.appendChild(opcija);

                    var elLabela = document.createElement("label");
                    elLabela.innerHTML = dataa.naziv;
                    divZaIzborKategorije2.appendChild(elLabela);
                })
            })
        })
        
        var elLabela = document.createElement("label");
        elLabela.innerHTML = "cena od";
        kontForma1.appendChild(elLabela);

        var inputDuzina = document.createElement("input");
        inputDuzina.className = "cena";
        kontForma1.appendChild(inputDuzina);

        var elLabelaSirina = document.createElement("label");
        elLabelaSirina.innerHTML = "cena do";
        kontForma1.appendChild(elLabelaSirina);

        var inputSirina = document.createElement("input");
        inputSirina.className = "sirina";
        kontForma1.appendChild(inputSirina);

        var dugme = document.createElement("button");
        dugme.innerHTML = "Pretrazi po izabranim parametrima";
        kontForma1.appendChild(dugme);

        var kontForma2 = document.createElement("div");
        kontForma2.className = "kontForma2";
        kontForma.appendChild(kontForma2);

        /*var kontForma2ZaBrisanje = document.createElement("div");
        kontForma2ZaBrisanje.className = "kontForma2KojiMozeDaSeBrise";
        kontForma2.appendChild(kontForma2ZaBrisanje);
*/
            dugme.onclick = (ev) => {
            this.crtanjeRezultataPretrage(kontForma2,dugme);
        }
    }
    crtanjeRezultataPretrage(kontForma2,dugme){/////////Ovo je lepa praksa koju bi mogao da koristis cesce, da kad pozivas neki dogadjaj to bude preko funkcije
       // dugme.onclick = (ev) => {
            this.nizIdIzabranihProizvoda="";
            var cena = parseInt(this.kontejner.querySelector(".cena").value);
            var sirina = parseInt(this.kontejner.querySelector(".sirina").value);
           
            var idBrenda = this.kontejner.querySelector('select[name="selectBrend"]').value;
            var idTipa = this.kontejner.querySelector('input[name="izborVelicine"]:checked').value;//uzimanje vrednosti radio buttona
           
            fetch("https://localhost:5001/Ispit/PrijemPodataka/" + cena + "/" + sirina + "/" + idBrenda + "/" + idTipa + "/"+ this.id , {
            method: "GET",
            headers: 
            {
                    "Content-Type": "application/json"
            },
            }).then(p => {
                p.json().then( data => {

                    var pronadjiDivZaBrisanje=this.kontejner.querySelector(".kontForma2KojiMozeDaSeBrise");
                   
                    if (pronadjiDivZaBrisanje!=null){///obrati paznju, bez te provere ne radi
                        (pronadjiDivZaBrisanje.parentNode).removeChild(pronadjiDivZaBrisanje);
                    }
                    
                    const kontForma2ZaBrisanje = document.createElement("div");
                    kontForma2ZaBrisanje.className = "kontForma2KojiMozeDaSeBrise";
                    kontForma2.appendChild(kontForma2ZaBrisanje); //ovo se ponavlja zato sto se obrise div
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///tabela se pravi lako , pravis redove i onda ih dodajes u table body, a naravno nazive kolona dodajes u thead
                        let table = document.createElement('table');
                        table.className="table";
                   
                        let thead = document.createElement('thead');
                        let tbody = document.createElement('tbody');

                        let row_1 = document.createElement('tr');
                        let heading_1 = document.createElement('th');
                        heading_1.innerHTML ="sifra";
                        let heading_2 = document.createElement('th');
                        heading_2.innerHTML = "naziv";
                        let heading_3 = document.createElement('th');
                        heading_3.innerHTML = "cena";
                        let heading_4 = document.createElement('th');
                        heading_4.innerHTML = "količina";

                    row_1.appendChild(heading_1);
                    row_1.appendChild(heading_2);
                    row_1.appendChild(heading_3);
                    row_1.appendChild(heading_4)
                    thead.appendChild(row_1);

                    var dugmeKupi=document.createElement("button");
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    data.forEach((dataa) => {

                        var opcijaDiv = document.createElement("div");
                        opcijaDiv.className="divZaCrtanjeNadjenihProizvoda";//+dataa.id+"";
                        opcijaDiv.innerHTML = dataa.naziv;

                        var praznired=document.createElement("br");
                        opcijaDiv.appendChild(praznired);

                        let labela=document.createElement("label");
                        labela.innerHTML=" kolicina na stanju "+dataa.kolicina;
                        opcijaDiv.appendChild(labela);

                        var praznired=document.createElement("br");
                        opcijaDiv.appendChild(praznired);

                        let labela1=document.createElement("label");
                        labela1.innerHTML=" cena: "+dataa.cena;
                        opcijaDiv.appendChild(labela1);

                        let labela2=document.createElement("label");
                        labela2.innerHTML="    velicina: "+dataa.tip.naziv;
                        opcijaDiv.appendChild(labela2);

                        var praznired=document.createElement("br");
                        opcijaDiv.appendChild(praznired);

                        var dugme1 = document.createElement("button");
                        dugme1.innerHTML = "Dodaj u korpu";
                        dugme1.value=dataa.id;

                        dugme1.name="dugme1";
                        opcijaDiv.appendChild(dugme1);

                        kontForma2ZaBrisanje.appendChild(opcijaDiv);

                        dugme1.onclick = (ev)=>{////dugme koje sluzi za dodavanje elemenata u tabelu konfiguracija

                            if (this.kontejner.querySelectorAll(".dugmeKupiKonfiguraciju").length==0){
                           // var dugmeKupi=document.createElement("button");
                                dugmeKupi.className="dugmeKupiKonfiguraciju";
                                dugmeKupi.innerHTML="Kupi konfiguraciju"
                                kontForma2ZaBrisanje.appendChild(dugmeKupi);
                            }

                            this.nizIdIzabranihProizvoda=this.nizIdIzabranihProizvoda.concat(dataa.id,"a");

                            var nadjenaKolicina=this.kontejner.querySelector(".vrednostKolicine"+dataa.id);////nalazenje da li postoji polje sa istim id-em
                            //ono sto sam primetio je da className ne moze biti samo brojcana vrednost
                            
                            if (nadjenaKolicina==undefined){
                                
                                let row_2 = document.createElement('tr');
                                row_2.className="red"+dataa.id;

                                let tableData1 = document.createElement('td');
                                tableData1.innerHTML = dataa.sifra ;

                                let tableData2 = document.createElement('td');
                                tableData2.value=dataa.naziv;
                                tableData2.innerHTML = dataa.naziv ;

                                let tableData3 = document.createElement('td');
                                tableData3.innerHTML = dataa.cena ;

                                let tableData4 = document.createElement('td');
                                tableData4.innerHTML=1 ;
                                tableData4.value=1;
                                tableData4.className="vrednostKolicine"+dataa.id;

                                row_2.appendChild(tableData1);
                                row_2.appendChild(tableData2);
                                row_2.appendChild(tableData3);
                                row_2.appendChild(tableData4);
                                tbody.appendChild(row_2);

                                table.appendChild(thead);
                                table.appendChild(tbody);
                                kontForma2ZaBrisanje.appendChild(table);
                            }
                            else
                            {
                                nadjenaKolicina.innerHTML=nadjenaKolicina.value+1;
                                nadjenaKolicina.value++; //////////////ovde je potrebno podesiti da kad se dodaje isti proizvod, mora da se nadje
                                //taj red u kom je kolicina tog proizvoda (to se radi preko value i preko className-a koje ima to polje u tabelici)

                            }
                        }
                    })
                    dugmeKupi.onclick = (ev) => {

                       ///salje se string id-eva proizvoda koji se onda pretrazuje na Backendu
                    fetch("https://localhost:5001/Ispit/KupovinaKonfiguracije/" + this.nizIdIzabranihProizvoda + "/" + this.id,
                        {
                            method: "PUT",
                            headers: {
                                "Content-Type": "application/json"
                            },
                        }).then(p => {
                            if (p.ok) {
                               
                                alert("Uspesna kupovina");
                                var kontForma2=this.kontejner.querySelector(".kontForma2");
                                this.crtanjeRezultataPretrage(kontForma2,dugme);
                                
                            }
                            else{
                                var kontForma2=this.kontejner.querySelector(".kontForma2");
                                this.crtanjeRezultataPretrage(kontForma2,dugme);
                                alert("Neuspesna kupovina");
                                //document.location.reload();////Da ne bi koristio ovaj reload, crtanjeRezultataPretrage si prebacio u zasebnu funkciju
                            }
                        })

                    }  
                })
            })
        
    }
}