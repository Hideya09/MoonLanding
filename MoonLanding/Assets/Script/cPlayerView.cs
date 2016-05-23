using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cPlayerView : MonoBehaviour {

	public cPlayerModel m_Model;

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
}
