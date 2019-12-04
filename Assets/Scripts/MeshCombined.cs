using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombined : MonoBehaviour
{

    public void combinedMesh()
    {
        MeshFilter[] Filters = GetComponentsInChildren<MeshFilter>();
        Mesh finalMesh = new Mesh();

        //Matrix4x4 ourMatrix = transform.localToWorldMatrix;
        Quaternion oldRot = transform.rotation;
        Vector3 oldPos = transform.position;

        transform.rotation = Quaternion.identity;
        transform.position = Vector3.zero;

        Debug.Log(name + "combinding Meshes");
        CombineInstance[] combines = new CombineInstance[Filters.Length];
        for (int a = 0; a < Filters.Length; a++)
        {
            if (Filters[a].transform == transform)
                continue;

            combines[a].subMeshIndex = 0;
            combines[a].mesh = Filters[a].sharedMesh;
            combines[a].transform = Filters[a].transform.localToWorldMatrix;
        }

        finalMesh.CombineMeshes(combines);
        GetComponent<MeshFilter>().sharedMesh = finalMesh;
        //SharedMesh is the editors vesion of the mesh

        transform.rotation = oldRot;
        transform.position = oldPos;

        for (int a = 0; a < transform.childCount; a++)
        {
            transform.GetChild(a).gameObject.SetActive(false);
        }
    }
}
