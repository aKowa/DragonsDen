using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource), typeof(BoxCollider2D) )]
public class ChestController : MonoBehaviour
{
	[Tooltip("The chest pickup sound.")]
	public AudioClip PickUpClip;

	private GameObject exit;
	private AudioSource audioSource;


	public void Awake ()
	{
		audioSource = GetComponent<AudioSource>();
		exit = GameObject.Find("Exit");
		if (exit == null)
		{
			Debug.LogError("Did not find Exit game object! please set to active!");
		}
		else
		{
			exit.SetActive(false);
		}
	}


	public void OnTriggerEnter2D ( Collider2D collision )
	{
		if (collision.tag != "Player") return;

		audioSource.clip = PickUpClip;
		audioSource.loop = false;
		GameObject.Find("Dragon").GetComponent<DragonWakUpController>().WakeUp();
		exit.SetActive(true);
		exit.GetComponent<AudioSource>().Play();
		this.GetComponent<BoxCollider2D>().enabled = false;
	}
}
