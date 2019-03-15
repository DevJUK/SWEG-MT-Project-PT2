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
					EditorGUILayout.TextField(RAS.NPCS[i]);
				}
			}
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		{
			for (int i = -1; i < 6; i++)
			{
				switch (i)
				{
					case -1:
						EditorGUILayout.LabelField("Rooms: ", GUILayout.MaxWidth(60f));
						break;
					case 0:
						EditorGUILayout.EnumPopup(RAS.CallyRoom);
						break;
					case 1:
						EditorGUILayout.EnumPopup(RAS.MirandaRoom);
						break;
					case 2:
						EditorGUILayout.EnumPopup(RAS.PoliceManRoom);
						break;
					case 3:
						EditorGUILayout.EnumPopup(RAS.WitchRoom);
						break;
					case 4:
						EditorGUILayout.EnumPopup(RAS.CatRoom);
						break;
					case 5:
						EditorGUILayout.EnumPopup(RAS.KyleRoom);
						break;
					default:
						break;
				}
			}
		}
		EditorGUILayout.EndHorizontal();
	}



	internal void UpdateEnum(RoomAllocationScript.Room Enum)
	{
		
	}
}
