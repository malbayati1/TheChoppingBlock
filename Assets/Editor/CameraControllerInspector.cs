using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraController))]
public class CameraControllerInspector : Editor
{
    public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		if(GUILayout.Button("Set Camera Position"))
		{
			CameraController c = target as CameraController;
			c.UpdateRotationAndOffset();
			c.UpdatePosition(false);
		}
	}
}
