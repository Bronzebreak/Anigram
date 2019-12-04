using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(MeshCombined))]
public class MeshCombinedEditor : Editor
{
    private void OnSceneGUI()
    {
        /*
        MeshCombined mc = target as MeshCombined;

        if (Handles.Button(mc.transform.position + Vector3.up, Quaternion.LookRotation(Vector3.up), 1, 1, Handles.CylinderCap)) ;
        {
            mc.combinedMesh();
        }
        */
    }
}
