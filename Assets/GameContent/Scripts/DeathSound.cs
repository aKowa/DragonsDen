using UnityEngine;
using System.Collections;

public class DeathSound : MonoBehaviour
{
	public static AudioSource audioSource;

	private void Awake ()
	{
		audioSource = this.GetComponent<AudioSource> ();
	}
}
