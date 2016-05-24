using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cGameTextView : MonoBehaviour {

	public cGameTextModel m_gtModel;

	public Text m_Text;
	public RectTransform m_Transform;

	public ParticleSystem m_Particle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		sTextInformation info = m_gtModel.GetInfo ();
		m_Transform.localScale = new Vector3 (info.m_Size, info.m_Size, 1.0f);
		m_Text.color = new Color (1.0f, 1.0f, 1.0f, info.m_Arpha);
		m_Text.text = info.m_Text;

		if (info.m_State == 2) {
			m_Particle.Play ();
		} else {
			m_Particle.Stop ();
		}
	}
}
