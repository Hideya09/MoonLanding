using UnityEngine;
using System.Collections;

public class cSelectView : MonoBehaviour {

	public cSelectModel m_selModel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = m_selModel.GetPosition ();
	}
}
