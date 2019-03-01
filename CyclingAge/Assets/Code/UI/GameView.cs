using Assets.Code.UI.Elements;
using Assets.Code.UI.Modules;
using UnityEngine;

namespace Assets.Code.UI
{
	public class GameView : MonoBehaviour
	{
		public static GameView Instance => FindObjectOfType<GameView>();

		public UIEncounterListModule EncounterListModule;
		public UIHeroListModule HeroListModule;
		public UIEncounterDetailsModule EncounterDetailsModule;

		private UIEncounterElement _encounter;


		public void Initialize(GameController controller)
		{
			EncounterListModule.Init(controller.Cycle);
			HeroListModule.Init(controller.Cycle);
		}

		public void InitialView()
		{
			GameController controller = GameController.Instance;

			HeroListModule.Hide();
			EncounterDetailsModule.Hide();
			EncounterListModule.Show();
			
			EncounterListModule.ElementClickedHandler = AssignHeroesToEncounter;
		}


		private void AssignHeroesToEncounter(UIEncounterElement encounter)
		{
			_encounter = encounter;

			EncounterListModule.SetSelected(new Encounter[] { encounter.Data });
			HeroListModule.Show();
			HeroListModule.SetSelected(encounter.Data.Heroes);
			HeroListModule.ElementClickedHandler = AssignHeroToEncounter;
			EncounterDetailsModule.Init(encounter.Data);
			EncounterDetailsModule.Show();
		}

		private void AssignHeroToEncounter(UIHeroElement hero)
		{
		

			hero.Toggled = !hero.Toggled;
			if(hero.Toggled) {
				if (_encounter.Data.Heroes.Count >= _encounter.Data.MaxHeroes)
					return;

				_encounter.Data.AddHero(hero.Data);
			}
			else {
				_encounter.Data.RemoveHero(hero.Data);
			}

			EncounterDetailsModule.Refresh();
		}
	}
}
