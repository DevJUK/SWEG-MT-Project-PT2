using UnityEngine;
using UnityEditor;

public class CombineMeshes : EditorWindow
{

	[MenuItem("Window/CombineMeshes #C")]
	public static void ShowWindow()
	{
		GetWindow<CombineMeshes>("Combine Meshes Editor");
	}




}
