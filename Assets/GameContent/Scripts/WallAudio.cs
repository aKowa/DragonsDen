using UnityEngine;
using System.Collections;

public class WallAudio : MonoBehaviour
{
	public AudioClip[] Clips;

	private void Awake ()
	{
		if (Clips == null)
		{
			Debug.LogError("No AudioClips on Walls!");
		}
	}
}
