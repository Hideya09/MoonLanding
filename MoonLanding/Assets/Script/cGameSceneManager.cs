using UnityEngine;
using System.Collections;

public class cGameSceneManager : MonoBehaviour {

	cGameMain m_GameMain;

	void Awake(){
		m_GameMain = new cGameMain ();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		m_GameMain.GameManage ();
	}
}
