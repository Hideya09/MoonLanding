using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public struct sTextInformation{
	public float m_Arpha;
	public string m_Text;
	public float m_Size;

	public int m_State;
}

public class cGameTextModel : ScriptableObject{

	private float m_Count;

	private sTextInformation m_Info;

	private bool m_StartFlag;

	private bool m_InputNext;

	public void GameStartInit(){
		m_Count = 0;

		m_StartFlag = false;

		m_Info.m_Arpha = 1.0f;
		m_Info.m_Text = "Ready";
		m_Info.m_Size = 0.0f;

		m_Info.m_State = 0;

		m_InputNext = false;
	}

	public bool GameStart(){
		m_Count += Time.deltaTime;

		m_Info.m_Arpha -= Time.deltaTime;
		m_Info.m_Size +=  Time.deltaTime;

		if (m_Count >= 1.0f) {
			m_Count = 0.0f;

			if (m_StartFlag == true) {
				return true;
			} else {
				m_Info.m_Arpha = 1.0f;
				m_Info.m_Size = 0.0f;

				m_Info.m_Text = "Go!";

				m_StartFlag = true;
			}
		}
		return false;
	}
		
	public void GameOver(){
		m_Info.m_Arpha = 1.0f;
		m_Info.m_Text = "Game Over";
		m_Info.m_Size = 1.0f;

		m_Info.m_State = 1;
	}
		
	public void GameClear(){
		m_Info.m_Arpha = 1.0f;
		m_Info.m_Text = "Game Clear";
		m_Info.m_Size = 1.0f;

		m_Info.m_State = 2;
	}

	public void NextInput(){
		m_InputNext = true;
	}

	public bool Next(){
		return m_InputNext;
	}

	public sTextInformation GetInfo(){
		return m_Info;
	}
}
