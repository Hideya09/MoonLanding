using UnityEngine;
using System.Collections;

public class cStageView : MonoBehaviour {

	public Material material;
	private cStageModel m_sModel;

	// Use this for initialization
	void Start () {
		Mesh mesh = new Mesh();

		mesh.vertices = new Vector3[]{ new Vector3 (0, 0, 0), new Vector3 (0, 10, 0), new Vector3 (10, 0, 0) };
		mesh.uv = new Vector2[]{ new Vector2 (0.5f, 0.5f), new Vector2 (0.5f, 0.5f), new Vector2 (0.5f, 0.5f) };
		mesh.triangles = new int[]{ 0 , 1 , 2 };



		mesh.RecalculateNormals ();
		mesh.RecalculateBounds ();

		GetComponent< MeshFilter > ().mesh = mesh;
	}
	
	// Update is called once per frame
	void Update () {
		//Graphics.DrawMesh (mesh, Vector3.zero, Quaternion.identity, material, 0);
	}
}
