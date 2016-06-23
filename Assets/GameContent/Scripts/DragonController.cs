using UnityEngine;
using System.Collections;

public class DragonController : MonoBehaviour
{
	[Tooltip ( "How fast should the Dragon be while following the player." )]
	public float FolllowSpeed = 1f;
	[Tooltip ( "The time in addition to the length of WakeUpClip before the Dragon starts following a player." )]
	public float AdditionalOffsetTime = 0f;
	[Tooltip ( "The AudioClip to play, when Dragon wakes up." )]
	public AudioClip WakeUpClip;
	[Tooltip ( "The AudioClip to play, while the Dragon follows the player." )]
	public AudioClip FollowingClip;
	[Tooltip("When the chest was found, this value is added to the audio source max distance and the sight collider radius.")]
	public float AddDistance = 0f;
	[Tooltip("How fast should the dragons audio fade out, after the player has reached the exit?")]
	public float FadeSpeed = 1f;

	private Transform playerTransform;
	private AudioSource audioSource;
	private bool followPlayer = false;


	private void OnEnable ()
	{
		EventManager.FoundChest += OnChestFound;
		EventManager.ReachedExit += OnReachedExit;
	}


	private void OnDisable ()
	{
		EventManager.FoundChest -= OnChestFound;
		EventManager.ReachedExit -= OnReachedExit;
	}


	public void OnChestFound ( AudioClip[] clips )
	{
		audioSource = this.GetComponent<AudioSource> ();
		audioSource.clip = WakeUpClip;
		audioSource.loop = false;
		audioSource.maxDistance += AddDistance;
		this.GetComponent<CircleCollider2D>().radius += AddDistance;
		audioSource.Play ();
		playerTransform = GameObject.FindGameObjectWithTag ( "Player" ).transform;
		StartCoroutine ( WaitToFollow ( audioSource.clip.length + AdditionalOffsetTime ) );
	}


	private IEnumerator WaitToFollow ( float t )
	{
		yield return new WaitForSeconds ( t );
		audioSource.clip = FollowingClip;
		audioSource.loop = true;
		audioSource.Play ();
		followPlayer = true;
		EventManager.FireDragonAwake();
	}


	private void Update ()
	{
		if (followPlayer)
		{
			Follow ();
		}
	}


	private void Follow ()
	{
		this.transform.position = Vector3.Lerp ( this.transform.position,
												playerTransform.position,
												FolllowSpeed * Time.deltaTime );
	}

	private void OnReachedExit()
	{
		StopAllCoroutines();
		followPlayer = false;
		StartCoroutine(FadeOutVolume(FadeSpeed));
	}


	private IEnumerator FadeOutVolume(float speed)
	{
		var initVolume = audioSource.volume;
		for (float t = 0; t <= 1; t += speed * Time.deltaTime)
		{
			audioSource.volume = Mathf.Lerp(initVolume, 0, t);
			yield return null;
		}
	}
}
