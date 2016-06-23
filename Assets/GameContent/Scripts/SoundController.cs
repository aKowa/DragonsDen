using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource), typeof(BoxCollider2D) )]
public class SoundController : MonoBehaviour
{
	[Tooltip("The playable sound.")]
	public AudioClip Sound;
	private AudioSource audioSource;


	public void Awake ()
	{
		audioSource = GetComponent<AudioSource>();
	}


	public void OnTriggerEnter2D ( Collider2D collision )
	{
		if (collision.tag != "Player") return;

		audioSource.clip = Sound;
		audioSource.loop = false;
		audioSource.Play();
		this.GetComponent<BoxCollider2D>().enabled = false;
	}
}
