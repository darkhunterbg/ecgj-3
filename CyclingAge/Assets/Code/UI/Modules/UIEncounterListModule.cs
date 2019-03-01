using Assets.Code.UI.Elements;
using Assets.Code.UI.Modules;
using System.Collections.Generic;

namespace Assets.Code.UI
{
	public class UIEncounterListModule : UIListModule<Encounter, UIEncounterElement>
	{

		private GameCycle _cycle;

		public void Init(GameCycle cycle)
		{
			_cycle = cycle;
		}

		protected override IEnumerable<Encounter> GetBindingData()
		{
			return _cycle.AvailableEncounters;
		}
	}
}
