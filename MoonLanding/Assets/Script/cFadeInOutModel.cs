using UnityEngine;
using System.Collections;

public class cFadeInOutModel : ScriptableObject{

	//フェードのステート
	public enum eFadeState{
		FadeIn,
		FadeInEnd,
		FadeInStop,
		FadeOut,
		FadeOutStop,
		FadeOutEnd
	}
		

	//現在のフェードステート
	private eFadeState m_State;

	private float m_Arpha;

	// Use this for initialization
	public void OnEnable () {
		m_State = eFadeState.FadeIn;

		m_Arpha = 1.0f;
	}
	
	// Update is called once per frame
	public void FadeExec () {
		switch (m_State) {
		case eFadeState.FadeIn:
			//徐々に白いテクスチャを薄くしていく
			m_Arpha -= Time.deltaTime;

			if (m_Arpha <= 0.0f) {
				m_State = eFadeState.FadeInStop;
			}
			break;
		case eFadeState.FadeInStop:
			m_State = eFadeState.FadeOut;
			break;
		case eFadeState.FadeOut:
			//徐々に白いテクスチャを濃くしていく
			m_Arpha += Time.deltaTime;

			if (m_Arpha >= 1.0f) {
				m_State = eFadeState.FadeOutStop;
			}
			break;
		case eFadeState.FadeOutStop:
			m_State = eFadeState.FadeIn;
			break;
		default:
			//処理なし
			break;
		}
	}

	public float GetArpha(){
		return m_Arpha;
	}

	public eFadeState GetState(){
		return m_State;
	}
}
