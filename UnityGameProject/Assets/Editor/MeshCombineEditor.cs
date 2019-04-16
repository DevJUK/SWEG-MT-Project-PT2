using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshCombineScript))]
public class MeshCombineEditor : Editor
{

	public void OnSceneGUI()
	{
		MeshCombineScript MCS = target as MeshCombineScript;

		if (Handles.Button(MCS.transform.position+Vector3.up * 5, Quaternion.LookRotation(Vector3.up), 1, 1, Handles.CylinderCap))
		{
			MCS.Combine();
		}
	}
}
