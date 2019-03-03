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

		private Action _collisionCallback;

		private void Start()
		{
			_startingPos = transform.position;
		}

		public void Play(Action collisionCallback)
		{
			TrailFX?.Play();

			   _play = true;
			_collisionCallback = collisionCallback;
		}
		public void ResetDisc()
		{
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
			Visuals.enabled = false;
			DeathFX?.Play();
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
			TrailFX?.Stop();
		

			_collisionCallback?.Invoke();
			_collisionCallback = null;
			_play = false;
		}
	}
}
