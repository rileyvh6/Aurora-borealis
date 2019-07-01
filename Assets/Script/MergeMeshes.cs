using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MergeMeshes : MonoBehaviour {

	public delegate void FuncMod ();
	public delegate void FuncMod<T> (T parma1);

	[SerializeField]
	private GameObject[] affectedParentsToMerge;
	public bool GenerateUV2 = true;
	public GameObject GeneratedMeshesParent{ get; set;}
	public GameObject objToMerge_01{ get; set;}
	public GameObject objToMerge_02{ get; set;}
	private GameObject[] generatedDirs;
	public string genName{ get; set;}

	public void Merge(FuncMod func1, FuncMod<Mesh> func2)
	{
		StoreNested<MeshFilter>[] nestedMeshFilterGroups = new StoreNested<MeshFilter>[affectedParentsToMerge.Length];
		generatedDirs = new GameObject[affectedParentsToMerge.Length];
		for(int i = 0; i < affectedParentsToMerge.Length; i++)
		{
			nestedMeshFilterGroups [i] = new StoreNested<MeshFilter> (affectedParentsToMerge [i]);
			generatedDirs[i] = new GameObject();
			genName = "MergedMeshes_" + i;
			func1 ();
			generatedDirs [i].gameObject.name = genName;
			generatedDirs [i].transform.parent = GeneratedMeshesParent.transform;
			//testing = nestedMeshFilterGroups [i].Children [32];
			Material mergeMat = nestedMeshFilterGroups [i].Children [0].gameObject.GetComponent<Renderer> ().sharedMaterial;
			CombineInstance[] combine = new CombineInstance[nestedMeshFilterGroups[i].Children.Length];
			for(int x = 0; x < nestedMeshFilterGroups[i].Children.Length; x++)
			{
				combine [x].subMeshIndex = 0;
				//if(mergeMat == nestedMeshFilterGroups [i].Children [x].gameObject.GetComponent<Renderer>().sharedMaterial)
					combine [x].mesh = nestedMeshFilterGroups [i].Children [x].sharedMesh;
				combine [x].transform = nestedMeshFilterGroups [i].Children [x].transform.localToWorldMatrix;
			}
			MeshFilter genMeshFilter = generatedDirs [i].AddComponent<MeshFilter> () as MeshFilter;
			MeshRenderer genMeshRenderer = generatedDirs [i].AddComponent<MeshRenderer> () as MeshRenderer;
			Mesh finalMesh = new Mesh ();
			finalMesh.CombineMeshes (combine);
			func2 (finalMesh);
			generatedDirs[i].GetComponent<MeshFilter>().sharedMesh = finalMesh;
			genMeshRenderer.sharedMaterial = mergeMat;
		}

		//Mesh[] mergedAll = new Mesh ();
		Debug.Log (name + nestedMeshFilterGroups[0].Children.Length +" Merged meshes");
	}

	public void Merge(GameObject objX, GameObject objY)
	{
		GameObject mergeIntoObj = new GameObject ();
		genName = objX.name + "_Plus_" + objY;
		mergeIntoObj.name = genName;
		mergeIntoObj.transform.parent = GeneratedMeshesParent.transform;
		MeshFilter[] mergingTwo = new MeshFilter[]{objX.GetComponent<MeshFilter>(), objY.GetComponent<MeshFilter>()};
		CombineInstance[] combine = new CombineInstance[mergingTwo.Length];
		for(int i = 0; i < mergingTwo.Length; i++)
		{
			combine [i].subMeshIndex = 0;
			combine [i].mesh = mergingTwo [i].sharedMesh;
			combine [i].transform = mergingTwo [i].transform.localToWorldMatrix;
		}
		MeshFilter genMeshFilter = mergeIntoObj.AddComponent<MeshFilter> () as MeshFilter;
		MeshRenderer genMeshRenderer = mergeIntoObj.AddComponent<MeshRenderer> () as MeshRenderer;
		Mesh finalMesh = new Mesh ();
		finalMesh.CombineMeshes (combine);
		mergeIntoObj.GetComponent<MeshFilter>().sharedMesh = finalMesh;
		genMeshRenderer.sharedMaterial = mergingTwo [0].GetComponent<Renderer> ().sharedMaterial;
	}

	public void HideToggle(bool toggle)
	{
		for(int i = 0; i < affectedParentsToMerge.Length; i++)
		{
			affectedParentsToMerge [i].SetActive (toggle);
		}

	}
}

public class StoreNested <U> : MonoBehaviour
{
	public GameObject Parent{ get; set;}
	public U[] Children{ get; set;}
	public StoreNested(GameObject parent)
	{
		Parent = parent;
		Children = parent.GetComponentsInChildren<U> ();
	}
}
