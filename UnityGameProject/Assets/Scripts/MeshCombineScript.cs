using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombineScript : MonoBehaviour
{



	public void Combine()
	{
		MeshFilter[] Filters = GetComponentsInChildren<MeshFilter>();



		Mesh FinalMesh = new Mesh();

		CombineInstance[] Comb = new CombineInstance[Filters.Length];

		Debug.Log(Filters.Length);



		for (int i = 0; i < Filters.Length; i++)
		{

			Comb[i].subMeshIndex = 0;
			Comb[i].mesh = Filters[i].sharedMesh;
			Comb[i].transform = Filters[i].transform.localToWorldMatrix;

		}


		FinalMesh.CombineMeshes(Comb);

		GetComponent<MeshFilter>().sharedMesh = FinalMesh;


	}

}
