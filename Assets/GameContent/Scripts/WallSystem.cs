using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class WallSystem : MonoBehaviour
{
	private AudioSource _source;

	private void Start ()
	{
		_source = this.GetComponent<AudioSource> ();
		if (_source == null)
		{
			Debug.LogError ( "Did not find an Audio. Not assigned on " + this.name + "?" );
		}
		else if (_source.clip == null)
		{
			Debug.Log ( "No audio clip assigned on Audio of " + this.name );
		}
	}

	private void OnTriggerEnter2D ( Collider2D collision )
	{
		if (collision.transform.tag != "Player") return;
		_source.Play ();
	}
}
