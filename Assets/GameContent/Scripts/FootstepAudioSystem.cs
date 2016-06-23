using System.Collections;
using Assets.Scripts;
using UnityEngine;
using Kowa.MemoRandom;

public class FootstepAudioSystem : MonoBehaviour
{
	[Tooltip("How much time should be between footsteps.")]
	public float OffsetTime = 1f;
	[Tooltip("The lowest possible volume for footsteps.")]
	public float MinVolume = 0.1f;
	[Tooltip("How much time should be between footsteps, when player is running from the dragon? FOREST GUMP RUNNING STYLE MODE!!!")]
	public float RunOffsetTime = 0.2f;

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
		EventManager.FoundChest += OnFoundChest;
		EventManager.DragonAwake += OnDragonAwake;
	}

	private void OnDisable ()
	{
		EventManager.PlayerMoveEvent -= OnPlayerMoveEvent;
		EventManager.PlayerChangedGround -= ChangeClip;
		EventManager.PlayerRotateEvent -= OnRotate;
		EventManager.FoundChest -= OnFoundChest;
		EventManager.DragonAwake -= OnDragonAwake;
	}

	private void ChangeClip ( AudioClip[] newStepClips, AudioClip[] newRotateClips )
	{
		if (this.name == "Coins") return;
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
		if (rotateClips.Length <= 0) return;

		audioSource.clip = rotateClips.DrawNext();
		audioSource.Play ();
	}


	private void OnFoundChest( AudioClip[] clips )
	{
		if (clips.Length <= 0) return;

		var clone = Instantiate(this.gameObject);
		clone.name = "Coins";
		clone.transform.parent = this.transform;
		clone.transform.localPosition = Vector3.zero;
		Destroy(clone.GetComponent<CircleCollider2D>());
		Destroy(clone.GetComponent<SoundColliderController>());
		var cloneSystem = clone.GetComponent<FootstepAudioSystem>();
		cloneSystem.stepClips = clips;
		cloneSystem.rotateClips = clips;
	}


	private void OnDragonAwake()
	{
		OffsetTime = RunOffsetTime;
	}
}
