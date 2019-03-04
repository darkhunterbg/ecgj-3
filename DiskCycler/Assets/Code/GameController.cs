using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Assets.Code
{
	[DefaultExecutionOrder(-100000)]
	public class GameController : MonoBehaviour
	{
		public static GameController Instance => _instance;

		public GameObject LoadingScreen;
		public List<string> Levels = new List<string>();


		private int _level = 0;

		private static GameController _instance;

		public bool MainMenu;

		public void Start()
		{
			if (_instance != null) {
				GameObject.Destroy(this.gameObject);
				return;
			}
			else {
				if (FindBasesScene() != null) {
					_instance = this;
					LoadingScreen.gameObject.SetActive(false);
				}
				else {
					SceneManager.LoadSceneAsync("BaseGame", LoadSceneMode.Additive).completed += (e) =>
					{
						GameObject.Destroy(this.gameObject);
					};
					return;
				}
			}

			if (!MainMenu) {
				_level = -1;
				return;
			}

			var existing = Level.Instance;
			if (!existing) {
				LoadNextLevel();
			}
			else {
				var s = SceneManager.GetActiveScene().name;
				int i = Levels.FindIndex(p => p == s);
				if (i > -1)
					_level = i;
			}
		}

		private Scene? FindBasesScene()
		{
			for (int i = 0; i < SceneManager.sceneCount; ++i) {
				var scene = SceneManager.GetSceneAt(i);
				if (scene.name == "BaseGame")
					return scene;
			}
			return null;
		}

		public void EndLevel(bool mainMenu)
		{
			if (!mainMenu)
				++_level;
			else
				_level = -1;

			var scene = SceneManager.GetSceneAt(0);
			if (scene.name == "BaseGame") {
				if (SceneManager.sceneCount > 1) {
					SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1)).completed += (e) =>
					{
						if (mainMenu)
							LoadMainMenu();
						else
							LoadNextLevel();
					};

				}
				else {

					if (mainMenu)
						LoadMainMenu();
					else
						LoadNextLevel();
				}
			}
			else if (SceneManager.sceneCount > 1) {
				SceneManager.UnloadSceneAsync(scene).completed += (e) =>
				{
					if (mainMenu)
						LoadMainMenu();
					else
						LoadNextLevel();
				};

			}
			else {
				if (mainMenu)
					LoadMainMenu();
				else
					LoadNextLevel();
			}

		}

		public void LoadNextLevel()
		{
			LoadingScreen.gameObject.SetActive(true);
			SceneManager.LoadSceneAsync(Levels[_level], LoadSceneMode.Additive).completed += (e) =>
			{
				LoadingScreen.gameObject.SetActive(false);
			};
		}

		public void LoadMainMenu()
		{
			LoadingScreen.gameObject.SetActive(true);
			SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive).completed += (e) =>
			{
				LoadingScreen.gameObject.SetActive(false);
			};
		}
	}
}
