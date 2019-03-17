using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(RoomAllocationScript))]
public class RoomAllocationEditor : Editor
{

	public override void OnInspectorGUI()
	{
		// Not sre what this does really
		RoomAllocationScript RAS = (RoomAllocationScript)target;

		EditorGUILayout.Space();


		EditorGUILayout.LabelField("Room Tracking System");


		EditorGUILayout.BeginHorizontal();
		{
			for (int i = -1; i < 6; i++)
			{
				if (i == -1)
				{
					EditorGUILayout.LabelField("People: ", GUILayout.MaxWidth(60f));
				}
				else
				{
					EditorGUILayout.TextField(RAS.NPCS[i], GUILayout.MaxWidth(60f));
				}
			}
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		{
			for (int i = -1; i < 6; i++)
			{
				if (i == -1)
				{
					EditorGUILayout.LabelField("Rooms: ", GUILayout.MaxWidth(60f));
				}
				else
				{
					if (RAS.Locations[i, 1] != null)
					{
						EditorGUILayout.TextField(RAS.Locations[i, 1], GUILayout.MaxWidth(60f));
					}
					else
					{
						EditorGUILayout.TextField("Null", GUILayout.MaxWidth(60f));
					}
				}
			}
		}
		EditorGUILayout.EndHorizontal();
	}
}
