using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cPlayerView : MonoBehaviour {

	private cPlayerModel m_Model;

	public Text m_uiSpeed;

	public RectTransform m_uiGauge;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		transform.position = m_Model.GetPosition ();
		transform.rotation = Quaternion.AngleAxis ( m_Model.GetAngle (), Vector3.forward);

		m_uiSpeed.text = "Speed " + m_Model.GetSpeed ().ToString ( "f1" ) + "/ms";

		m_uiGauge.localScale = new Vector3 (m_Model.GetFuelPercent () , 1.0f , 1.0f);
	}

	public void SetModel( cPlayerModel setModel ){
		m_Model = setModel;
	}

	void OnCollisionEnter2D( Collision2D collision ){
		m_Model.HitCheck (collision);
	}

	void OnTriggerEnter2D( Collider2D other ){
		m_Model.HitCheck (other);
	}
}
