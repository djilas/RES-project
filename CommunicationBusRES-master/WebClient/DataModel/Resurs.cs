using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Resurs
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public TipResursa Tip { get; set; }

        public Resurs()
        {

        }

        public Resurs(int id, string naziv, string opis, TipResursa tip)
        {
            this.Id = id;
            this.Naziv = naziv;
            this.Opis = opis;
            this.Tip = tip;
        }

    }
}
