using System;
using System.Collections;
using UnityEngine;
using Kowa.MemoRandom;

public class PlayerTransformSystem : MonoBehaviour
{
	[Tooltip ( "How fast should the player be able to move?" )]
	public float MoveSpeed = 1f;
	[Tooltip("How fast should the player rotate?")]
	public float RotationSpeed = 1f;
	[Tooltip ( "By which angle should the player rotate, when ticking the right stick?" )]
	public float AngelPerTick = 90;

	private void Start()
	{
		var spawnPoint = Spawn.Points.DrawNext();
		this.transform.position = spawnPoint.position;
		this.transform.rotation = spawnPoint.rotation;
	}

	private void OnEnable()
	{
		EventManager.PlayerMoveEvent += OnMove;
		EventManager.PlayerRotateEvent += OnRotate;
	}

	private void OnDisable()
	{
		EventManager.PlayerMoveEvent -= OnMove;
		EventManager.PlayerRotateEvent -= OnRotate;
	}

	private void OnMove(Vector2 movement)
	{
		movement *= MoveSpeed * Time.deltaTime;
		this.transform.position += this.transform.right * movement.x;
		this.transform.position += this.transform.up * movement.y;
	}

	private void OnRotate(float direction, Action doneRotating)
	{
		StartCoroutine ( Rotate ( AngelPerTick * direction, doneRotating ) );
	}

	private IEnumerator Rotate(float angle, Action e)
	{
		var initZ = this.transform.rotation.eulerAngles.z;
		var targetZ= initZ + angle;
		for (var t = 0f; t < 1; t += RotationSpeed * Time.deltaTime)
		{
			var r = Mathf.Lerp ( initZ, targetZ, t );
			this.transform.rotation = Quaternion.Euler ( Vector3.forward * r );
			yield return null;
		}

		if (e != null)
		{
			e();
		}
	}
}
