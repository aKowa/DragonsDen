using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour
{
	private AudioClip[] _stepClips;
	private AudioClip[] _rotateClips;

	private AudioClip[] StepClips
	{
		get
		{
			if (_stepClips != null)
			{
				return _stepClips;
			}
			else
			{
				_stepClips = this.transform.parent.GetComponent<GroundSound>().StepClips;

				if (_stepClips == null)
				{
					Debug.LogError ( "StepClips not found. Not assigned to parent?" );
				}
				return _stepClips;
			}
		}
	}

	private AudioClip[] RotateClips
	{
		get
		{
			if (_rotateClips != null)
			{
				return _rotateClips;
			}
			else
			{
				_rotateClips = this.transform.parent.GetComponent<GroundSound> ().RotateClips;

				if (_rotateClips == null)
				{
					Debug.LogError ( "RotateClips not found. Not assigned to parent?" );
				}
				return _rotateClips;
			}
		}
	}

	private void OnTriggerEnter2D ( Collider2D other )
	{
		if (other.tag == "Player")
		{
			EventManager.FirePlayerChangedGround ( StepClips, RotateClips );
		}
	}
}
