using UnityEngine;
using System.Collections;
using Kowa.MemoRandom;

public class HeartAudioSystem : MonoBehaviour 
{
	[Tooltip("The time to wait between heart beats.")]
	public float OffsetTime = 1f;
	[Tooltip("How much faster should the heart rate go the close the player is to an enemy?")]
	public float EncounterRateModifier = 1f;
	[Tooltip("The minimal heart rate.")]
	public float MinRate = 0.1f;
	[Tooltip("The maximal heart rate.")]
	public float MaxRate = 1f;
	[Tooltip("The heart beat audio clips")]
	public AudioClip[] Clips;

	private float _initOffsetTime;
	private AudioSource _audioSource;

	private void Awake()
	{
		_initOffsetTime = OffsetTime;
		_audioSource = GetComponent<AudioSource>();
	}

	private void OnEnable()
	{
		EventManager.EnemyEnterSightEvent += OnEnemyEnterSight;
		EventManager.EnemyInSightEvent += OnEnemyStayInSight;
		EventManager.EnemyExitSightEvent += OnEnemyLeftSight;
		EventManager.PlayerDied += OnPlayerDeath;
	}

	private void OnDisable()
	{
		StopAllCoroutines();
		EventManager.EnemyEnterSightEvent -= OnEnemyEnterSight;
		EventManager.EnemyInSightEvent -= OnEnemyStayInSight;
		EventManager.EnemyExitSightEvent -= OnEnemyLeftSight;
		EventManager.PlayerDied -= OnPlayerDeath;
	}

	private void OnEnemyEnterSight()
	{
		StartCoroutine ( WaitAndPlayAudio () );
	}

	private void OnEnemyStayInSight(float distanceToEnemy)
	{
		OffsetTime = Mathf.Clamp( _initOffsetTime * distanceToEnemy * EncounterRateModifier, MinRate, MaxRate);
	}

	private void OnEnemyLeftSight()
	{
		StopAllCoroutines();
		OffsetTime = _initOffsetTime;
	}

	private IEnumerator WaitAndPlayAudio()
	{
		_audioSource.clip = Clips.DrawNext();
		Debug.Log("Played: " + _audioSource.clip);
		_audioSource.Play();
		yield return new WaitForSeconds(OffsetTime);
		StartCoroutine( WaitAndPlayAudio() );
	}

	private void OnPlayerDeath ( AudioClip clip )
	{
		this.enabled = false;
	}
}
