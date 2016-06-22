using UnityEngine;


public static class ExtensionMethods 
{
	public static float GetHighestValue (this Vector2 vec)
	{
		if ( vec.x >= vec.y )
		{
			return vec.x;
		}
		else
		{
			return vec.y;
		}
	}

	public static float GetHighestAbsoluteValue(this Vector2 vec)
	{
		vec.x = Mathf.Abs(vec.x);
		vec.y= Mathf.Abs ( vec.y);
		return vec.GetHighestValue();
	}

	public static Vector3[] ConvertToVector3(this Vector2[] vec2)
	{
		var targetVec3 = new Vector3[vec2.Length];
		for (int i = 0; i < vec2.Length; i++)
		{
			targetVec3[i] = (Vector3) vec2[i];
		}
		return targetVec3;
	}

	public static int CeilFloorToInt(this float f)
	{
		if (f > 0)
		{
			f = Mathf.CeilToInt(f);
		} 
		else if ( f < 0)
		{
			f = Mathf.FloorToInt(f);
		}
		return (int)f;
	}
}
