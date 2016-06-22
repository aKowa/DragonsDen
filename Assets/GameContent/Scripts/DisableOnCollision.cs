using UnityEngine;
using System.Collections;

public class DisableOnCollision : MonoBehaviour
{
	public MonoBehaviour ToDisable;
	public string OtherTag;

	private void OnTriggerEnter2D ( Collider2D collision )
	{
		if (collision.transform.tag == OtherTag)
		{
			ToDisable.enabled = false;
		}
	}

	private void OnTriggerExit2D ( Collider2D collision )
	{
		if (collision.transform.tag == OtherTag)
		{
			ToDisable.enabled = true;
		}
	}
}
