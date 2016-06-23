using UnityEngine;
using System.Collections;

public class RemoveOnCollision : MonoBehaviour
{
	public GameObject ObjectToRemove;

	public void OnTriggerEnter2D ( Collider2D collision )
	{
		if (collision.tag == "Player")
		{
			Destroy(ObjectToRemove);
			this.GetComponent<AudioSource>().enabled = false;
		}
	}
}
