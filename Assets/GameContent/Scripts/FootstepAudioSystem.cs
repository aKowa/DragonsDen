using System.Collections;
using UnityEngine;
using Kowa.MemoRandom;

public class FootstepAudioSystem : MonoBehaviour
{
	public float OffsetTime = 1f;
	public float MinVolume = 0.1f;
	private AudioSource audioSource;
	private AudioClip[] stepClips;
	private AudioClip[] rotateClips;
	private bool canPlay = true;

	private void OnEnable ()
	{
		audioSource = GetComponent<AudioSource> ();
		EventManager.PlayerMoveEvent += OnPlayerMoveEvent;
		EventManager.PlayerChangedGround += ChangeClip;
		EventManager.PlayerRotateEvent += OnRotate;
		
	}

	private void OnDisable ()
	{
		EventManager.PlayerMoveEvent -= OnPlayerMoveEvent;
		EventManager.PlayerChangedGround -= ChangeClip;
		EventManager.PlayerRotateEvent -= OnRotate;
	}

	private void ChangeClip ( AudioClip[] newStepClips, AudioClip[] newRotateClips )
	{
		stepClips = newStepClips;
		rotateClips = newRotateClips;
	}

	private void OnPlayerMoveEvent ( Vector2 movement )
	{
		if (!canPlay) return;

		audioSource.volume = Mathf.Clamp ( movement.GetHighestAbsoluteValue (), MinVolume, 1.0f );
		audioSource.clip = stepClips.DrawNext();
		audioSource.Play ();
		StopAllCoroutines ();
		StartCoroutine ( AudioOffset ( OffsetTime ) );
	}

	private IEnumerator AudioOffset (float t)
	{
		if (!(t > 0)) yield break;
		canPlay = false;
		yield return new WaitForSeconds (t);
		canPlay = true;
	}

	private void OnRotate(float angle, System.Action e)
	{
		audioSource.clip = rotateClips.DrawNext();
		audioSource.Play();
	}
}
