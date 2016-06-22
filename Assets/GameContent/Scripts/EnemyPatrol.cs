using UnityEngine;
using System.Collections;

public class EnemyPatrol : MonoBehaviour
{
	public PatrolLine Line;
	public float Speed = 1f;
	private Transform _enemy;
	
	private void Awake()
	{
		_enemy = transform.parent;
		StartCoroutine ( Patrol () );
	}

	private IEnumerator Patrol()
	{
		for (float t = 0; t <= 1; t += Speed * Time.deltaTime)
		{
			_enemy.position = (Vector3) Vector2.Lerp(Line.Points[0], Line.Points[1], t);
			yield return null;
		}

		for (float i = 1; i >= 0; i -= Speed * Time.deltaTime)
		{
			_enemy.position = (Vector3)Vector2.Lerp (Line.Points[0], Line.Points[1], i);
			yield return null;
		}

		StartCoroutine(Patrol());
	}
}
