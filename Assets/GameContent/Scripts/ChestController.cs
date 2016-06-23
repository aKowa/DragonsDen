using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource), typeof(BoxCollider2D) )]
public class ChestController : MonoBehaviour
{
	[Tooltip("The chest pickup sound.")]
	public AudioClip PickUpClip;

	private AudioSource audioSource;


	public void Awake ()
	{
		audioSource = GetComponent<AudioSource>();
	}


	public void OnTriggerEnter2D ( Collider2D collision )
	{
		if (collision.tag != "Player") return;
		EventManager.FireFoundChest();
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
