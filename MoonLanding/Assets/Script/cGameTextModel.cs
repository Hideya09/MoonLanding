using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//表示する文字の情報
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

	private bool m_GoalFlag;

	//初期化処理
	public void GameStartInit(){
		m_Count = 0;

		m_StartFlag = false;

		m_Info.m_Arpha = 1.0f;
		m_Info.m_Text = "Ready";
		m_Info.m_Size = 0.0f;

		m_Info.m_State = 0;

		m_InputNext = false;
		m_GoalFlag = false;
	}

	//ゲームスタート時に文字を表示する処理
	public bool GameStart(){

		m_Count += Time.deltaTime;

		//徐々に大きくしつつα値を下げる
		m_Info.m_Arpha -= Time.deltaTime;
		m_Info.m_Size +=  Time.deltaTime;

		//一秒経過で次の文字を出す。
		if (m_Count >= 1.0f) {
			m_Count = 0.0f;

			if (m_StartFlag == true) {
				//２秒立った場合ステートを変える
				m_GoalFlag = false;
				return true;
			} else {
				m_Info.m_Arpha = 1.0f;
				m_Info.m_Size = 0.0f;

				m_Info.m_Text = "Go!";

				//一秒経過のフラグ
				m_StartFlag = true;
			}
		}
		return false;
	}

	//ゲームオーバー時表示処理
	public void GameOver(){
		m_GoalFlag = true;

		m_Info.m_Arpha = 1.0f;
		m_Info.m_Text = "Game Over";
		m_Info.m_Size = 1.0f;

		m_Info.m_State = 1;
	}

	//ゲームクリア時表示処理
	public void GameClear(){
		m_GoalFlag = true;

		m_Info.m_Arpha = 1.0f;
		m_Info.m_Text = "Stage Clear";
		m_Info.m_Size = 1.0f;

		m_Info.m_State = 2;

	}

	//ゲームオーバーroクリア時にのみ入力時の処理を行う
	public void NextInput(){
		if (m_GoalFlag == true) {
			m_InputNext = true;
		}
	}

	//次のステートに変えるかを取得
	public bool Next(){
		return m_InputNext;
	}

	//文字情報を取得
	public sTextInformation GetInfo(){
		return m_Info;
	}
}
