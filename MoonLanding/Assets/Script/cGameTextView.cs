using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cGameTextView : MonoBehaviour {

	public cGameTextModel m_gtModel;

	public Text m_Text;
	public RectTransform m_Transform;

	public ParticleSystem m_Particle;

	public Text m_NextText;
	public Text m_TtitleText;

	public GameObject m_Arrow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//文字情報を取得し表示する
		sTextInformation info = m_gtModel.GetInfo ();
		m_Transform.localScale = new Vector3 (info.m_Size, info.m_Size, 1.0f);
		m_Text.color = new Color (1.0f, 1.0f, 1.0f, info.m_Arpha);
		m_Text.text = info.m_Text;

		//ステートによってはエフェクトを出す
		if (info.m_State == 2) {
			m_Particle.Play ();

			m_Arrow.SetActive (false);
			m_NextText.enabled = false;
			m_TtitleText.enabled = false;
		} else {
			if (info.m_State == 1) {
				m_Arrow.SetActive (true);
				m_NextText.enabled = true;
				m_TtitleText.enabled = true;
			} else {
				m_Arrow.SetActive (false);
				m_NextText.enabled = false;
				m_TtitleText.enabled = false;
			}
			m_Particle.Stop ();
		}
	}
}
