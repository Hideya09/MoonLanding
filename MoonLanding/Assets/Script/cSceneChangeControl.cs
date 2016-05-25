using UnityEngine;
using System.Collections;

public class cSceneChangeControl : MonoBehaviour {

	public cSceneChangeModel m_scModel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//入力処理
		if( Input.GetKeyDown( KeyCode.Return ) ){
			m_scModel.SetPush ();
		}
	}
}
