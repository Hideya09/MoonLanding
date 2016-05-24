using UnityEngine;
using System.Collections;

public class cPlayerControl : MonoBehaviour{

	public cPlayerModel m_Model;

	void Update(){
		RotateKey ();
		PropulsionKey ();
		HorizontalKey ();
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
		} else {
			m_Model.EngineStop ();
		}
	}

	public void HorizontalKey(){
		if (Input.GetKey (KeyCode.UpArrow)) {
			m_Model.HorizontalAngle();
		}
	}

	void OnCollisionEnter2D( Collision2D collision ){
		m_Model.HitCheck (collision);
	}

	void OnTriggerEnter2D( Collider2D other ){
		m_Model.HitCheck (other);
	}
}
