using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource), typeof(BoxCollider2D) )]
public class ChestController : MonoBehaviour
{
	[Tooltip("The chest pickup sound.")]
	public AudioClip Clip;
	public GameObject Exit;
	public DragonController DragonController;
	private AudioSource source;

	public void Awake ()
	{
		source = GetComponent<AudioSource>();
	}

	public void OnTriggerEnter2D ( Collider2D collision )
	{
		if (collision.tag == "Player")
		{
			if (Clip != null)
			{
				StartCoroutine( PlayAndStop(Clip) );
			}
			Exit.SetActive(true);
			var dragonSource = DragonController.GetComponent<AudioSource>();
			dragonSource.clip = DragonController.Sounds.Kill;
			dragonSource.Play();
			dragonSource.loop = true;
			DragonController.Disable();
		}
	}

	private IEnumerator PlayAndStop(AudioClip clip)
	{
		source.clip = Clip;
		source.Play();
		yield return new WaitForSeconds(clip.length);
		source.Stop();
	}
}
