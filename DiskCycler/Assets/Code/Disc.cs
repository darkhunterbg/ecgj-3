using Assets.Code.Motions;
using Skaillz.EditInline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code
{
	[DefaultExecutionOrder(-200)]
	public class Disc : MonoBehaviour, ICollisionReciever
	{
		public DiscMotionBlender Motion;
		public ParticleSystem TrailFX;
		public ParticleSystem DeathFX;
		public ParticleSystem DampenFX;
		public Animator Animator;

		public Color SimColor = Color.white;
		public Color FailColor = Color.red;

		public ParticleSystem SimTrailFX;
		public ParticleSystem SimDeathFX;

		public AudioClip DestroyedSFX;
		public AudioClip MovementSFX;
		public AudioClip DampenedSFX;

		public AudioSource AudioSource;

		public SpriteRenderer Visuals;

		private float _elapsed = 0.0f;
		private Vector3 _startingPos;

		public float MaxVelocity = 10;
		public float MinVelocity = 1;
		public float Velocity = 10;
		public float VelocityRecoveryRate = 10;

		public float MaxEnergy = 100;
		public float Energy = 100;

		public float EnergyCostSecond = 50;
		public float EnergyRecoverySecond = 25;

		public bool IsSlowdownActive;

		public bool Immortal;

		private bool _isSim = false;

		public bool IsPlaying { get; private set; } = false;

		private Action<Collider2D> _collisionCallback;

		public void Init()
		{
			_startingPos = transform.position;
			Velocity = MaxVelocity;
			Energy = MaxEnergy;

			Animator.enabled = false;
		}

		public void Play(Action<Collider2D> collisionCallback, bool simulation)
		{
			AudioSource.clip = MovementSFX;
			AudioSource.Play();

			Animator.enabled = true;

			_isSim = simulation;

			if (simulation) {
				Visuals.color = SimColor;
				if (!SimTrailFX.isPlaying) SimTrailFX?.Play();

			}
			else {
				Visuals.color = Color.white;
				if (!TrailFX.isPlaying) TrailFX?.Play();
			}

			IsPlaying = true;
			_collisionCallback = collisionCallback;
		}
		public void Stop()
		{
			AudioSource.Stop();

			Animator.enabled = false;

			if (_isSim) {
				if (SimTrailFX.isPlaying) SimTrailFX?.Stop(false, ParticleSystemStopBehavior.StopEmitting);
			}
			else {
				if (TrailFX.isPlaying) TrailFX?.Stop(false, ParticleSystemStopBehavior.StopEmitting);
			}

			if (DampenFX.isPlaying) DampenFX?.Stop(false, ParticleSystemStopBehavior.StopEmitting);

			IsPlaying = false;
		}
		public void ResetDisc()
		{
			AudioSource.Stop();

			IsPlaying = false;
			Velocity = MaxVelocity;
			Energy = MaxEnergy;

			if (DampenFX.isPlaying) DampenFX?.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);

			Visuals.enabled = true;
			if (DeathFX.isPlaying) DeathFX?.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
			if (TrailFX.isPlaying || TrailFX.isPaused) TrailFX?.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
			if (SimTrailFX.isPlaying) SimTrailFX?.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
			if (SimDeathFX.isPlaying) SimDeathFX?.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);

			Visuals.color = Color.white;


			_elapsed = 0;
			transform.position = _startingPos;
		}

		public void Kill()
		{
			Stop();

			if (!_isSim) {
				Visuals.enabled = false;
				if (!DeathFX.isPlaying) DeathFX?.Play();
			}

			AudioSource.PlayOneShot(DestroyedSFX);
		}

		public void DiscPause()
		{
			AudioSource.Stop();

			Animator.enabled = false;

			IsPlaying = false;

			if (_isSim) {
				if (!SimTrailFX.isPaused) SimTrailFX?.Pause();
			}
			else {
				if (!TrailFX.isPaused) TrailFX?.Pause();
			}

		}

		private bool _dampen = false;


		public void FixedUpdate()
		{
			if (!IsPlaying)
				return;

			_elapsed += Time.fixedDeltaTime * Velocity;

			var offset = GetStepOffset(_elapsed);

			transform.position = _startingPos + new Vector3(offset.x, offset.y);

			if ( IsSlowdownActive) {
				float energyCost = EnergyCostSecond * Time.fixedDeltaTime;

				if (Input.GetKey(KeyCode.Space) && Energy > energyCost) {
					Energy -= energyCost;
					Velocity = MinVelocity;

					if (!_dampen) {
						DampenFX.Play();
						AudioSource.pitch = 0.5f;
					}

					_dampen = true;
				
				}
				else {
					if (_dampen || DampenFX.isPlaying) {
						DampenFX.Stop();
						AudioSource.pitch = 1.5f;
					}

					_dampen = false;

					Energy = Mathf.Min(MaxEnergy, Energy + EnergyRecoverySecond * Time.fixedDeltaTime);
					Velocity = Mathf.Min(MaxVelocity, Velocity + VelocityRecoveryRate * Time.fixedDeltaTime);
				}
			}
		}

		private void UseSlowdown()
		{
			Velocity = MinVelocity;
		}
		private void UsePhasing()
		{

		}

		public Vector2 GetStepOffset(float time)
		{
			return Motion.GetStepOffset(time);
		}


		public void OnCollision(Collider2D collision)
		{
			if (!IsPlaying)
				return;

			_collisionCallback?.Invoke(collision);
		}
	}
}
