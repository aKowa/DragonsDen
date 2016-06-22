using UnityEngine;

public class Spawn : MonoBehaviour
{
	public static Transform[] Points;

	public void Awake ()
	{
		if (Points != null)
		{
			Destroy(this.gameObject);
		}
		else
		{
			var p = this.GetComponentsInChildren<Transform>();
			Points = new Transform[p.Length - 1];
			for (var i = 0; i < Points.Length; i++)
			{
				Points[i] = p[i + 1];
			}
			DontDestroyOnLoad(gameObject);
		}
	}
}
