using UnityEngine;

namespace Assets.Scripts
{
	public class SoundColliderController : MonoBehaviour
	{
		public float RadiusMultiplier = 1f;
		private float _minSize = 0.5f;
		private CircleCollider2D _coll;

		public void Awake ()
		{
			_coll = this.GetComponent<CircleCollider2D>();
			_minSize = _coll.radius;
		}

		public void OnEnable ()
		{
			EventManager.PlayerMoveEvent += ResizeCollider;
			EventManager.PlayerStopedMoveEvent += ResizeCollider;
		}

		public void OnDisable ()
		{
			EventManager.PlayerMoveEvent -= ResizeCollider;
			EventManager.PlayerStopedMoveEvent -= ResizeCollider;
		}

		private void ResizeCollider( Vector2 vec )
		{
			_coll.radius = vec.GetHighestAbsoluteValue() * RadiusMultiplier;
		}

		private void ResizeCollider()
		{
			_coll.radius = _minSize;
		}
	}
}
