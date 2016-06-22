using UnityEngine;
using System.Collections;

public class DragonWakUpController : MonoBehaviour
{
	[Tooltip ( "How fast should the Dragon be while following the player." )]
	public float FolllowSpeed = 1f;
	[Tooltip ( "The time in addition to the length of WakeUpClip before the Dragon starts following a player." )]
	public float AdditionalOffsetTime = 0f;
	[Tooltip("The AudioClip to play, when Dragon wakes up.")]
	public AudioClip WakeUpClip;
	[Tooltip("The AudioClip to play, while the Dragon follows the player.")]
	public AudioClip FollowingClip;

	private Transform playerTransform;
	private AudioSource source;
	private bool followPlayer = false;


	public void WakeUp ()
	{
		source = this.GetComponent<AudioSource> ();
		source.clip = WakeUpClip;
		source.loop = false;
		source.Play ();
		playerTransform = GameObject.FindGameObjectWithTag ( "Player" ).transform;
		StartCoroutine ( WaitToFollow ( source.clip.length + AdditionalOffsetTime ) );
	}


	private IEnumerator WaitToFollow ( float t )
	{
		yield return new WaitForSeconds ( t );
		source.clip = FollowingClip;
		source.loop = true;
		source.Play();
		followPlayer = true;
	}


	private void Update ()
	{
		if (followPlayer)
		{
			Follow();
		}
	}


	private void Follow()
	{
		this.transform.position = Vector3.Lerp( this.transform.position, 
												playerTransform.position, 
												FolllowSpeed * Time.deltaTime );
	}
}
