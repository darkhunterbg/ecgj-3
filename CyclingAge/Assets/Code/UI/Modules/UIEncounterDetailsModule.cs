using UnityEngine.UI;

namespace Assets.Code.UI.Modules
{
	public class UIEncounterDetailsModule : UIModule
	{
		public Text EncounterName;

		public Text TotalAttack;
		public Text TotalDefense;

		private Encounter _encounter;

		public void Init(Encounter encounter)
		{
			_encounter = encounter;
			Refresh();
		}

		public override void Refresh()
		{
			EncounterName.text = _encounter.Name.ToUpper();
			TotalAttack.text = $"ATK: {_encounter.TotalAttack}/{_encounter.Difficulty} ({_encounter.SuccessChance}% Success)";
			TotalDefense.text = $"DEF: {_encounter.TotalDefense}/{_encounter.Difficulty} ({_encounter.Survivability}% Survivability)";
		}
	}
}
