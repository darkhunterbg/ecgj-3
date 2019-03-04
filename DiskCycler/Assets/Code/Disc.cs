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

		public Color SimColor = Color.white;
		public Color FailColor = Color.red;

		public ParticleSystem SimTrailFX;
		public ParticleSystem SimDeathFX;

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


		private bool _isSim = false;

		public bool IsPlaying { get; private set; } = false;

		private Action<Collider2D> _collisionCallback;

		public void Init()
		{
			_startingPos = transform.position;
			Velocity = MaxVelocity;
			Energy = MaxEnergy;
		}

		public void Play(Action<Collider2D> collisionCallback, bool simulation)
		{
			_isSim = simulation;

			if (simulation) {
				Visuals.color = SimColor;
				SimTrailFX?.Play();

			}
			else {
				Visuals.color = Color.white;
				TrailFX?.Play();
			}

			IsPlaying = true;
			_collisionCallback = collisionCallback;
		}
		public void Stop()
		{
			if (_isSim) {
				SimTrailFX?.Stop(false, ParticleSystemStopBehavior.StopEmitting);
			}
			else {
				TrailFX?.Stop(false, ParticleSystemStopBehavior.StopEmitting);
			}

			IsPlaying = false;
		}
		public void ResetDisc()
		{
			IsPlaying = false;
			Velocity = MaxVelocity;
			Energy = MaxEnergy;

			Visuals.enabled = true;
			DeathFX?.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
			TrailFX?.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
			SimTrailFX?.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
			SimDeathFX?.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);

			Visuals.color = Color.white;


			_elapsed = 0;
			transform.position = _startingPos;
		}

		public void Kill()
		{
			Stop();

			if (!_isSim) {
				Visuals.enabled = false;
				DeathFX?.Play();
			}
		}

		public void DiscPause()
		{
			IsPlaying = false;

			if (_isSim) {
				SimTrailFX?.Pause();
			}
			else {
				TrailFX?.Pause();
			}

		}

		public void FixedUpdate()
		{
			if (!IsPlaying)
				return;

			_elapsed += Time.fixedDeltaTime * Velocity;

			var offset = GetStepOffset(_elapsed);

			transform.position = _startingPos + new Vector3(offset.x, offset.y);

			if (IsPlaying && IsSlowdownActive) {
				float energyCost = EnergyCostSecond * Time.fixedDeltaTime;

				if (Input.GetKey(KeyCode.Space) && Energy > energyCost) {
					Energy -= energyCost;
					Velocity = MinVelocity;
				}
				else {
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
			_collisionCallback?.Invoke(collision);
			_collisionCallback = null;
		}
	}
}
