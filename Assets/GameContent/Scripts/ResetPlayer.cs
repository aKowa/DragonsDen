using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResetPlayer : MonoBehaviour
{
	public void OnEnable ()
	{
		EventManager.PlayerDied += Reset;
	}

	public void OnDisable ()
	{
		EventManager.PlayerDied -= Reset;
	}

	public void Reset ( AudioClip clip )
	{
		if (clip != null)
		{
			DeathSound.audioSource.clip = clip;
			DeathSound.audioSource.Play ();
			StartCoroutine ( WaitAndReload ( clip.length ) );
		}
		else
		{
			StartCoroutine ( WaitAndReload(0f) );
		}
	}

	private IEnumerator WaitAndReload ( float time )
	{
		yield return new WaitForSeconds ( time );
		SceneManager.LoadScene ( 0 );
	}
}
