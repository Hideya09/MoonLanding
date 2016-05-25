using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cPlayerView : MonoBehaviour {

	public cPlayerModel m_Model;

	public Text m_uiSpeed;

	public RectTransform m_uiGauge;

	public ParticleSystem m_Particle;

	public ParticleSystem m_Bomb;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		//位置の取得
		transform.position = m_Model.GetPosition ();
		transform.rotation = Quaternion.AngleAxis ( m_Model.GetAngle (), Vector3.forward);

		//噴射エフェクト表示判定
		if (m_Model.GetEngineFlag () == true && m_Particle.isPlaying == false) {
			m_Particle.Play ();
		} else if (m_Model.GetEngineFlag () == false && m_Particle.isPlaying == true) {
			m_Particle.Stop ();
		}

		//爆発エフェクト表示
		if (m_Model.GetBombFlag ()) {
			GetComponent< SpriteRenderer > ().enabled = false;

			m_Bomb.Play ();
		}

		//現在の速度と残燃料の表示
		m_uiSpeed.text = "Speed " + m_Model.GetSpeed ().ToString ( "f1" ) + "/ms";

		m_uiGauge.localScale = new Vector3 (m_Model.GetFuelPercent () , 1.0f , 1.0f);
	}
}
