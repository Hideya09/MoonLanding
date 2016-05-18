using UnityEngine;
using System.Collections;

public class cPlayerControl {

	private cPlayerModel m_Model;
	private cPlayerView m_View;

	public cPlayerControl( cPlayerModel setModel , cPlayerView setView ){
		m_Model = setModel;
		m_View = setView;

		m_View.SetModel (m_Model);
	}

	public void RotateKey(){
		if (Input.GetKey (KeyCode.RightArrow)) {
			m_Model.AddRightAngle ();
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			m_Model.AddLeftAngle ();
		}
	}

	public void PropulsionKey(){
		if (Input.GetKey (KeyCode.DownArrow)) {
			m_Model.CalcDirection ();
		}
	}

	public void HorizontalKey(){
		if (Input.GetKey (KeyCode.UpArrow)) {
			m_Model.HorizontalAngle();
		}
	}
}
