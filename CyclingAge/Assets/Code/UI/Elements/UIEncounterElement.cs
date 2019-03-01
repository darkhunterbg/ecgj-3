using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Code.UI.Elements
{
	public class UIEncounterElement : UIListElement<Encounter>
	{
		public Text Name;
		public Text Difficulty;

		public override void Set(Encounter encounter)
		{
			base.Set(encounter);
			Name.text = Data.Name.ToUpper();
			Difficulty.text = $"DIFFICULTY: {Data.Difficulty}";
		}

	}
}
