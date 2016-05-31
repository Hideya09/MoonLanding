using UnityEngine;
using System.Collections;

public class cSpeedView : MonoBehaviour {

	public cPlayerModel m_pModel;
	public TextMesh m_Text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		m_Text.text = "Speed " + m_pModel.GetSpeed ().ToString ( "f1" ) + "/ms";

		Vector3 position = m_pModel.GetPosition ();

		position.y += 8.0f;

		transform.position = position;
	}
}
