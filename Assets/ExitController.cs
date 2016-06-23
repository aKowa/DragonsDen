using UnityEngine;
using System.Collections;

public class ExitController : MonoBehaviour
{
	private AudioSource audioSource;
	private BoxCollider2D boxCollider;

	private void Awake ()
	{
		audioSource = this.GetComponent<AudioSource>();
		boxCollider = this.GetComponent<BoxCollider2D>();
		SetActive(false);
	}

	private void OnEnable ()
	{
		EventManager.FoundChest += OnFoundChest;
	}


	private void OnDisable ()
	{
		EventManager.FoundChest -= OnFoundChest;
	}


	private void OnFoundChest ( AudioClip[] clips )
	{
		SetActive(true);
	}


	public void OnTriggerEnter2D ( Collider2D collision )
	{
		if (collision.tag == "Player")
		{
			EventManager.FireReachedExit();
			SetActive(false);
		}
	}

	private void SetActive(bool state)
	{
		boxCollider.enabled = state;
		audioSource.enabled = state;
	}
}
