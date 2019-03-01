using Assets.Code.UI.Elements;
using Assets.Code.UI.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.UI
{
	public class UIHeroListModule : UIListModule<Hero, UIHeroElement>
	{

		private GameCycle _cycle;

		public void Init(GameCycle cycle)
		{
			_cycle = cycle;
		}

		protected override IEnumerable<Hero> GetBindingData()
		{
			return _cycle.Heroes;
		}

	}
}
