using UnityEngine;
using System.Collections;

public class cCameraView : MonoBehaviour {

	public cPlayerModel m_pModel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = m_pModel.GetPosition();
		position.z = -52;

		transform.position = position;
	}
}
