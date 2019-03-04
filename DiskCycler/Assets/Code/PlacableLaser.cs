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

		public AudioClip LaserSFX;
		public AudioClip ChargingSFX;

		public ParticleSystem CharngingFX;
		public Animator Animator;

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

			CharngingFX.Stop();
		}

		private IEnumerator Shoot()
		{
			ShootingFX.SetActive(false);

			float fxTime = CharngingFX.main.duration + CharngingFX.main.startLifetime.constant;

			while (true) {
				yield return new WaitForSeconds(ShootingInterval - fxTime);
				CharngingFX.Play();
				GameController.Instance.AudioSource.PlayOneShot(ChargingSFX, 1f);
				yield return new WaitForSeconds(fxTime);
				ShootingFX.SetActive(true);
				GameController.Instance.AudioSource.PlayOneShot(LaserSFX, 1f);			
				yield return new WaitForSeconds(ShootingDuration);
				ShootingFX.SetActive(false);
			}
		}


		private void OnDestroy()
		{
			StopShooting();
		}
	}
}
