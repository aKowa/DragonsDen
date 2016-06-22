using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour
{
	public AudioClip clip;

	public void OnTriggerEnter2D ( Collider2D collision )
	{
		if ( collision.tag == "Player" )
		{
			EventManager.FirePlayerDied ( clip );
		}
	}
}
