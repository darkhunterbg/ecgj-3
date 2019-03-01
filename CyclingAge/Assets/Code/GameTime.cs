using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code
{
	[Serializable]
	public class GameTime
	{
		public int Year = 1;
		public int Season = 1;

		public void ProgressTime(int interval = 1)
		{
			++Season;
			if(Season >4) {
				Season = 1;
				Year = 2;
			}
		}
	}
}
