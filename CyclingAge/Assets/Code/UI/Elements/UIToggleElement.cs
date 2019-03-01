using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI.Elements
{
	public class UIToggleElement : MonoBehaviour
	{
		[Header("Toggle")]
		public Image Background;

		public Color NormalColor = Color.white;
		public Color ToggleColor = Color.red;

		private bool _toggle = false;

		public bool Toggled
		{
			get
			{
				return _toggle;
			}
			set
			{
				if (_toggle == value)
					return;
				SetToggle(value);
			}
		}

		private void SetToggle(bool toggle)
		{
			_toggle = toggle;
			Background.color = Toggled ? ToggleColor : NormalColor;
		}

	}
}
