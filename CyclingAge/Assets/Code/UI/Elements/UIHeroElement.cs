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
	public class UIHeroElement :  UIListElement<Hero>
	{
		public Text Name;
		public Text Attack;
		public Text Defense;



		public override void Set(Hero hero)
		{
			base.Set(hero);
			Name.text = hero.Name;
			Attack.text = $"ATK: {hero.Attack}";
			Defense.text = $"DEF: {hero.Defense}";
		}

	}
}
