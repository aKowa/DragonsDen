using UnityEngine;
using UnityEditor;

[CustomEditor ( typeof ( EnemyPatrol ) )]
public class EnemyPatrolInspector : Editor
{
	private EnemyPatrol _enemyPatrol;
	private Transform _handleTransform;
	private Quaternion _handleRotation;

	private void OnSceneGUI ()
	{
		_enemyPatrol = target as EnemyPatrol;
		_handleTransform = _enemyPatrol.transform;
		_handleRotation = Tools.pivotRotation == PivotRotation.Local
			? _handleTransform.rotation : Quaternion.identity;

		if (_enemyPatrol.Line.Points == null) return;
		
		for (var i = 0; i < _enemyPatrol.Line.Points.Length; i++)
		{
			ShowPoint ( i );
		}
		DrawLines ();
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		if (GUILayout.Button("Reset Patrol Line"))
		{
			Undo.RecordObject(_enemyPatrol, "Reset Patrol Line");
			_enemyPatrol.Line.Reset();
			EditorUtility.SetDirty(_enemyPatrol);
		}
	}

	private void ShowPoint ( int index )
	{
		var point = _enemyPatrol.Line.Points[index];
		EditorGUI.BeginChangeCheck ();
		point = Handles.DoPositionHandle ( point, _handleRotation );
		if (EditorGUI.EndChangeCheck ())
		{
			Undo.RecordObject ( _enemyPatrol, "Moved Patrol Line" );
			EditorUtility.SetDirty ( _enemyPatrol );
			_enemyPatrol.Line.Points[index] = point;
		}
	}

	private void DrawLines ()
	{
		Handles.color = Color.gray;
		for (int i = 0; i < _enemyPatrol.Line.Points.Length - 1; i++)
		{
			var p0 = _enemyPatrol.Line.Points[i];
			var p1 = _enemyPatrol.Line.Points[i + 1];
			Handles.DrawLine (p0, p1);
		}
	}
}
