using System;
using UnityEngine;

public static class EventManager 
{
	public static event Action<Vector2> PlayerMoveEvent;
	public static event Action PlayerStopedMoveEvent;
	public static event Action<float, Action> PlayerRotateEvent;
	public static event Action<float> EnemyInSightEvent;
	public static event Action EnemyEnterSightEvent;
	public static event Action EnemyExitSightEvent;
	public static event Action<AudioClip[], AudioClip[]> PlayerChangedGround;
	public static event Action<AudioClip> PlayerDied;
	public static event Action<AudioClip[]> FoundChest;
	public static event Action ReachedExit;
	public static event Action DragonAwake;

	public static void FirePlayerMove(Vector2 movement)
	{
		if (PlayerMoveEvent != null)
			PlayerMoveEvent(movement);		
	}

	public static void FirePlayerStopedMoveEvent()
	{
		if (PlayerStopedMoveEvent != null)
			PlayerStopedMoveEvent();
	}

	public static void FirePlayerRotate(float direction, Action e)
	{
		if (PlayerRotateEvent != null)
			PlayerRotateEvent(direction, e);
	}

	public static void FireEnemyEnterSightEvent ()
	{
		if (EnemyEnterSightEvent != null)
			EnemyEnterSightEvent ();
	}

	public static void FireEnemyInSight(float distanceToEnemy)
	{
		if (EnemyInSightEvent != null)
			EnemyInSightEvent(distanceToEnemy);
	}

	public static void FireEnemyExitSight()
	{
		if (EnemyExitSightEvent != null)
			EnemyExitSightEvent();
	}

	public static void FirePlayerChangedGround(AudioClip[] stepClips, AudioClip[] rotateClips )
	{
		if (PlayerChangedGround != null)
			PlayerChangedGround( stepClips, rotateClips);
	}

	public static void FirePlayerDied (AudioClip clip )
	{
		if (PlayerDied != null)
			PlayerDied (clip);
	}

	public static void FireFoundChest ( AudioClip[] clips )
	{
		if (FoundChest != null)
			FoundChest (clips);
	}

	public static void FireReachedExit ()
	{
		if (ReachedExit != null)
			ReachedExit ();
	}

	public static void FireDragonAwake ()
	{
		if (DragonAwake != null)
			DragonAwake ();
	}
}
