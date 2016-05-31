using UnityEngine;
using System.Collections;

public class cGameTextControl : MonoBehaviour{

	public cGameTextModel m_gtModel;

	void Update(){
		NextInput ();
	}

	public void NextInput(){
		//キーが押されたことを知らせる
		if (Input.GetKeyDown (KeyCode.Return)) {
			m_gtModel.NextInput ();
		}
	}
}
