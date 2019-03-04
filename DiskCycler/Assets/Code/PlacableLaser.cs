using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code
{
	public class PlacableLaser : MonoBehaviour
	{
		public float ShootingInterval;
		public float ShootingDuration;

		public GameObject ShootingFX;

		private Coroutine _shootingCrt;

		public void Start()
		{
			ShootingFX.SetActive(false);
		}

		public void StartShooting()
		{
			if (_shootingCrt == null)
				_shootingCrt = StartCoroutine(Shoot());
		}
		public void StopShooting()
		{
			if (_shootingCrt != null) {
				ShootingFX.SetActive(false);
				StopCoroutine(_shootingCrt);
				_shootingCrt = null;
			}
		}

		private IEnumerator Shoot()
		{
			yield return new WaitForSeconds(ShootingInterval);

			while (true) {
				ShootingFX.SetActive(true);
				yield return new WaitForSeconds(ShootingDuration);
				ShootingFX.SetActive(false);
				yield return new WaitForSeconds(ShootingInterval );
			}
		}


		private void OnDestroy()
		{
			StopShooting();
		}
	}
}
