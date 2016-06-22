using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	private void OnTriggerEnter2D ( Collider2D other )
	{
		if (other.tag == "Player")
		{
			EventManager.FireEnemyEnterSightEvent();
		}
	}

	private void OnTriggerStay2D ( Collider2D other )
	{
		if (other.tag == "Player")
		{
			DistanceToEnemy ( other.transform );
		}
	}

	private void OnCollisionStay2D ( Collision2D coll )
	{
		if (coll.transform.tag == "Player")
		{
			DistanceToEnemy ( coll.transform );
		}
	}

	private void OnTriggerExit2D ( Collider2D other )
	{
		if (other.tag == "Player")
		{
			EventManager.FireEnemyExitSight ();
		}
	}

	private void DistanceToEnemy ( Transform other )
	{
		float distance = Vector2.Distance ( other.position, this.transform.position );
		EventManager.FireEnemyInSight ( distance );
	}
}
