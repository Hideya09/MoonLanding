using UnityEngine;
using System.Collections;

public class cSceneChangeModel : ScriptableObject {

	private bool m_AddFlag;

	private float m_Arpha;
	private bool m_PushFlag;

	public void Init(){
		m_Arpha = 0.0f;
		m_PushFlag = false;

		m_AddFlag = true;
	}

	public void FadeFont(){
		if (m_AddFlag == true) {
			m_Arpha += Time.deltaTime;
			if (m_Arpha >= 1.0f) {
				m_AddFlag = false;
			}
		} else {
			m_Arpha -= Time.deltaTime;
			if (m_Arpha <= 0.0f) {
				m_AddFlag = true;
			}
		}
	}

	public float GetArpha(){
		return m_Arpha;
	}

	public void SetPush(){
		m_PushFlag = true;
	}

	public bool GetPush(){
		return m_PushFlag;
	}
}
