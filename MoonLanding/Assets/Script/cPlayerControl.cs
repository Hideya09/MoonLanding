using UnityEngine;
using System.Collections;

public class cPlayerControl : MonoBehaviour{

	public cPlayerModel m_Model;

	//入力処理
	void FixedUpdate(){
		RotateKey ();
		PropulsionKey ();
		HorizontalKey ();
	}

	//回転入力
	public void RotateKey(){
		if (Input.GetKey (KeyCode.RightArrow)) {
			m_Model.AddRightAngle ();
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			m_Model.AddLeftAngle ();
		}
	}

	//推進処理
	public void PropulsionKey(){
		if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.Z)) {
			m_Model.CalcDirection ();
		} else {
			m_Model.EngineStop ();
		}
	}

	//
	public void HorizontalKey(){
		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.X)) {
			m_Model.HorizontalAngle ();
		}
	}

	void OnCollisionEnter2D( Collision2D collision ){
		m_Model.HitCheck (collision);
	}

	void OnTriggerEnter2D( Collider2D other ){
		m_Model.HitCheck (other);
	}
}
