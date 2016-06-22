using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
	[Tooltip("Should the player be able to move sideways?")]
	public bool EnableSideways = false;

	private Transform player;
	private bool isRotating = false;
	private bool isMoving = false;
	private event Action DoneRotating;

	private void Awake ()
	{
		player = GameObject.FindWithTag ( "Player" ).transform;

		if (player == null)
			Debug.LogError ( "Did not find player game object" );
	}

	private void OnEnable ()
	{
		EventManager.PlayerDied += OnPlayerDeath;
		DoneRotating += OnDoneRotating;
	}

	private void OnDisable ()
	{
		EventManager.PlayerDied -= OnPlayerDeath;
		DoneRotating -= OnDoneRotating;
	}

	private void Update ()
	{
		MovePlayer ();
		RotatePlayer ();
	}

	private void MovePlayer ()
	{
		if (this.isRotating) return;

		var v = Input.GetAxis("Vertical");
		var h = EnableSideways ? Input.GetAxis("Horizontal") : 0f;

		if (Mathf.Abs(h) > 0 || Mathf.Abs(v) > 0)
		{
			isMoving = true;
			EventManager.FirePlayerMove(new Vector2(h, v));
		}
		else if (isMoving)
		{
			isMoving = false;
			EventManager.FirePlayerStopedMoveEvent();
		}
	}

	private void RotatePlayer ()
	{
		var h = Input.GetAxis ( "HorizontalRightStick" ).CeilFloorToInt ();

		if (h == 0 || this.isRotating) return;
		this.isRotating = true;
		EventManager.FirePlayerRotate(h, DoneRotating);
	}

	private void OnDoneRotating()
	{
		isRotating = false;
	}

	private void OnPlayerDeath ( AudioClip clip )
	{
		this.enabled = false;
	}
}
