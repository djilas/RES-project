using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
	public class Veza
	{
		public int Id { get; set; }
		public int IdPrvog { get; set; }
		public int IdDrugog { get; set; }
		public TipVeze Tip { get; set; }

		public Veza()
		{

		}

		public Veza (int id, int idPrvog, int idDrugog, TipVeze tip)
		{
			this.Id = id;
			this.IdPrvog = idPrvog;
			this.IdDrugog = idDrugog;
			this.Tip = tip;
		}

	}
}
