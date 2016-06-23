using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource), typeof(BoxCollider2D) )]
public class ChestController : MonoBehaviour
{
	[Tooltip("The chest pickup sound.")]
	public AudioClip PickUpClip;
	[Tooltip("The Exit game object to be set active on collision.")]
	public GameObject Exit;
	private AudioSource audioSource;


	public void Awake ()
	{
		audioSource = GetComponent<AudioSource>();
		if (Exit == null)
		{
			Debug.LogError("Did not find Exit game object! please set to active!");
		}
		else
		{
			Exit.SetActive(false);
		}
	}


	public void OnTriggerEnter2D ( Collider2D collision )
	{
		if (collision.tag != "Player") return;

		audioSource.clip = PickUpClip;
		audioSource.loop = false;
		audioSource.Play();
		GameObject.Find("Dragon").GetComponent<DragonWakUpController>().WakeUp();
		Exit.SetActive(true);
		Exit.GetComponent<AudioSource>().Play();
		this.GetComponent<BoxCollider2D>().enabled = false;
	}
}
