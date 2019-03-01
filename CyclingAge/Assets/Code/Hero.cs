using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code
{
	[Serializable]
	public class Hero
	{
		public string Name;

		public int BaseAttack = 20;
		public int BaseDefense = 20;

		public int Attack => BaseAttack;
		public int Defense => BaseDefense;
	}
}
