using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource), typeof(BoxCollider2D) )]
public class ChestController : MonoBehaviour
{
	[Tooltip("The chest pickup sound.")]
	public AudioClip PickUpClip;
	[Tooltip("The coin audio clips to be played in addition to the footsteps after chest was found.")]
	public AudioClip[] FootstepCoinClips;

	private AudioSource audioSource;


	public void Awake ()
	{
		audioSource = GetComponent<AudioSource>();
	}


	public void OnTriggerEnter2D ( Collider2D collision )
	{
		if (collision.tag != "Player") return;
		EventManager.FireFoundChest(FootstepCoinClips);
		OnChestFound();
	}


	private void OnChestFound()
	{
		audioSource.clip = PickUpClip;
		audioSource.loop = false;
		audioSource.Play ();
		this.GetComponent<BoxCollider2D> ().enabled = false;
	}
}
