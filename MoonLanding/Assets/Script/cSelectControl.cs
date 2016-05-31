using UnityEngine;
using System.Collections;

public class cSelectControl : MonoBehaviour {

	public cSelectModel m_selModel;

	public KeyCode m_UpCode;
	public KeyCode m_DownCode;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			m_selModel.SetSelect ();
		}

		if (Input.GetKeyDown (m_UpCode)) {
			m_selModel.UpSelect ();
		}

		if (Input.GetKeyDown (m_DownCode)) {
			m_selModel.DownSelect ();
		}
	}
}
