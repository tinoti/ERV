using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERV.Model
{
    class Date
    {
		public Date(DateTime CurrentDate)
		{
			this.CurrentDate = CurrentDate;
			Vacation = false;
			Trip = false;
			Sick = false;
		}

		public DateTime CurrentDate { get; set; }
		public bool Vacation { get; set; }
		public bool Trip { get; set; }
		public bool Sick { get; set; }

	}
}
