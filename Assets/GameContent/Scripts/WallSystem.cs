using UnityEngine;
using Kowa.MemoRandom;

[RequireComponent(typeof(AudioSource))]
public class WallSystem : MonoBehaviour
{
	private AudioSource audioSource;
	private WallAudio wall;

	private void Start ()
	{
		audioSource = this.GetComponent<AudioSource> ();
		wall = this.transform.parent.GetComponent<WallAudio>();
	}

	private void OnTriggerEnter2D ( Collider2D collision )
	{
		if (collision.transform.tag != "Player") return;
		audioSource.clip = wall.Clips.DrawNext();
		audioSource.Play ();
	}
}
