using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class MeshCombine : MonoBehaviour
{
    private void Awake()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combines = new CombineInstance[meshFilters.Length];
        for(int i = 0; i < meshFilters.Length; i++)
        {
            combines[i].mesh = meshFilters[i].sharedMesh;
            combines[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
        }
        MeshFilter meshFiler = transform.GetComponent<MeshFilter>();
        meshFiler.mesh = new Mesh();
        meshFiler.mesh.CombineMeshes(combines);
        GetComponent<MeshCollider>().sharedMesh = meshFiler.mesh;
        transform.gameObject.SetActive(true);
    }
}
