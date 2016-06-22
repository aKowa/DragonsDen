using UnityEngine;
using System.Collections;

public class DragonController : MonoBehaviour
{
	[Range ( 0f, 1f )]
	public float WakeUpThreshold = 0.8f;
	public float TimeToWakeUp = 0.5f;
	public float TimeForDefault = 1f;
	public DragonSounds Sounds;
	private AudioSource _audio;
	private bool _isWaiting = false;

	private void Awake ()
	{
		_audio = this.GetComponent<AudioSource> ();
		this.GetComponentInChildren<KillZone> ().clip = Sounds.Kill;
	}

	public void OnTriggerEnter2D ( Collider2D collision )
	{
		if (collision.tag == "Player")
		{
			PlayDefault ();
			EventManager.PlayerMoveEvent += StartWakeUp;
			EventManager.PlayerStopedMoveEvent += StopWakingUp;
		}
	}

	public void OnTriggerExit2D ( Collider2D collision )
	{
		if (collision.tag == "Player")
		{
			_audio.Stop ();
			EventManager.PlayerMoveEvent -= StartWakeUp;
			EventManager.PlayerStopedMoveEvent -= StopWakingUp;
		}
	}

	public void OnDisable ()
	{
		EventManager.PlayerMoveEvent -= StartWakeUp;
		EventManager.PlayerStopedMoveEvent -= StopWakingUp;
	}

	private void StartWakeUp ( Vector2 movement )
	{
		if (movement.GetHighestAbsoluteValue () > WakeUpThreshold)
		{
			if (!_isWaiting)
				StartCoroutine ( WaitToWakeUp () );
		}
		else if (_isWaiting)
		{
			StopWakingUp();
		}
	}

	private void StopWakingUp()
	{
		_isWaiting = false;
		StopAllCoroutines ();
		StartCoroutine ( WaitAndPlayDefault ( TimeForDefault ) );
	}

	private IEnumerator WaitToWakeUp ()
	{
		PlayWakeUp ();
		_isWaiting = true;
		yield return new WaitForSeconds ( Sounds.WakeUp.length + TimeToWakeUp );
		_isWaiting = false;
		KillPlayer ();
	}

	private void KillPlayer ()
	{
		StopAllCoroutines ();
		EventManager.FirePlayerDied ( Sounds.Kill );
	}

	private IEnumerator WaitAndPlayDefault (float t)
	{
		yield return new WaitForSeconds (t);
		PlayDefault ();
	}

	private void PlayDefault ()
	{
		_audio.clip = Sounds.Default;
		_audio.loop = true;
		_audio.Play ();
	}

	private void PlayWakeUp ()
	{
		_audio.clip = Sounds.WakeUp;
		_audio.loop = false;
		_audio.Play ();
	}

	public void Disable()
	{
		this.enabled = false;
		StopAllCoroutines();
		GetComponent<CircleCollider2D>().enabled = false;
	}
}
