using UnityEngine;
using System.Collections;

[System.Serializable]
public class PatrolLine
{
	public Vector2[] Points;

	public void Reset ()
	{
		this.Points = new Vector2[2] { Vector2.zero, Vector2.right };
	}

	// TODO: not working
	public Vector2 GetPosition(float t)
	{
		for (var i = 0; i < Points.Length - 1; i++)
		{
			if (t > RelativeLength[i] && t < RelativeLength[i + 1])
			{
				return Vector2.Lerp ( Points[i], Points[i + 1], t - RelativeLength[i] );
			}
		}
		return Vector2.zero;
	}

	public float LengthInUnits
	{
		get
		{
			float target = 0;
			for (var i = 0; i < Points.Length - 1; i++)
			{
				target += Vector2.Distance ( Points[i], Points[i + 1] );
			}
			return target;
		}
	}

	// TODO: Not working
	public float[] RelativeLength
	{
		get
		{
			var target = new float[Points.Length - 1];
			for (int i = 0; i < Points.Length - 1; i++)
			{
				target[i] += Vector2.Distance ( Points[i], Points[i + 1] ) / LengthInUnits;
				Debug.Log("Index: " + i + " Position: " + target[i]);
			}
			return target;
		}
	}
}
