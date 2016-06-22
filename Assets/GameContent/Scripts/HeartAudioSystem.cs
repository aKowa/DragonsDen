using UnityEngine;
using System.Collections;
using Kowa.MemoRandom;

public class HeartAudioSystem : MonoBehaviour
{
	[Tooltip ( "The time to wait between heart beats." )]
	public float OffsetTime = 1f;
	[Tooltip ( "How much faster should the heart rate go the close the player is to an enemy?" )]
	public float EncounterScalar = 1f;
	[Tooltip ( "The minimal offset time for playing the heart beat." )]
	public float MinOffset = 0.1f;
	[Tooltip ( "The maximal offset time for playing the heart beat." )]
	public float MaxOffset = 1f;
	[Tooltip ( "Should the heart beat always play from the start?" )]
	public bool PlayInBackground = false;
	[Tooltip ( "The heart beat audio clips" )]
	public AudioClip[] Clips;

	private float initOffsetTime;
	private AudioSource audioSource;


	private void Awake ()
	{
		initOffsetTime = OffsetTime;
		audioSource = GetComponent<AudioSource> ();
		if (Clips == null)
		{
			Debug.LogError ( "No AudioClips on Heart!" );
		}
	}


	private void OnEnable ()
	{
		EventManager.EnemyEnterSightEvent += OnEnemyEnterSight;
		EventManager.EnemyInSightEvent += OnEnemyStayInSight;
		EventManager.EnemyExitSightEvent += OnEnemyLeftSight;
		EventManager.PlayerDied += OnPlayerDeath;
		if (PlayInBackground)
		{
			StartCoroutine ( WaitAndPlayAudio () );
		}
	}


	private void OnDisable ()
	{
		if (!PlayInBackground)
		{
			StopAllCoroutines ();
		}
		EventManager.EnemyEnterSightEvent -= OnEnemyEnterSight;
		EventManager.EnemyInSightEvent -= OnEnemyStayInSight;
		EventManager.EnemyExitSightEvent -= OnEnemyLeftSight;
		EventManager.PlayerDied -= OnPlayerDeath;
	}


	private void OnEnemyEnterSight ()
	{
		if (!PlayInBackground)
		{
			StartCoroutine ( WaitAndPlayAudio () );
		}
	}


	private void OnEnemyStayInSight ( float distanceToEnemy )
	{
		OffsetTime = Mathf.Clamp ( initOffsetTime * distanceToEnemy * EncounterScalar, MinOffset, MaxOffset );
	}


	private void OnEnemyLeftSight ()
	{
		if (!PlayInBackground)
		{
			StopAllCoroutines ();
		}
		OffsetTime = initOffsetTime;
	}


	private IEnumerator WaitAndPlayAudio ()
	{
		audioSource.clip = Clips.DrawNext ();
		audioSource.Play ();
		yield return new WaitForSeconds ( audioSource.clip.length + OffsetTime );
		StartCoroutine ( WaitAndPlayAudio () );
	}


	private void OnPlayerDeath ( AudioClip clip )
	{
		this.enabled = false;
	}
}
