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
		public SpriteRenderer Visuals;

		private float _elapsed = 0.0f;
		private Vector3 _startingPos;

		public float Velocity = 10;

		private bool _play = false;

		private Action<Collider2D> _collisionCallback;

		public void Init()
		{
			_startingPos = transform.position;
		}

		public void Play(Action<Collider2D> collisionCallback)
		{
			TrailFX?.Play();

			   _play = true;
			_collisionCallback = collisionCallback;
		}
		public void Stop()
		{

			TrailFX?.Stop();
			_play = false;
		}
		public void ResetDisc()
		{
			_play = false;

			Visuals.enabled = true;
			DeathFX?.Stop();
			DeathFX?.Clear();
			TrailFX?.Stop();
			TrailFX?.Clear();


			_elapsed = 0;
			transform.position = _startingPos;
		}

		public void Kill()
		{
			_play = false;
			Visuals.enabled = false;
			DeathFX?.Play();
			TrailFX?.Stop(false, ParticleSystemStopBehavior.StopEmitting);
		}

		public void FixedUpdate()
		{
			if (!_play)
				return;

			_elapsed += Time.fixedDeltaTime * Velocity;

			var offset = GetStepOffset(_elapsed);

			transform.position = _startingPos + new Vector3(offset.x, offset.y) ;
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
