using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cResultScoreView : MonoBehaviour {

	public cResultScoreModel m_rsModel;

	public Text[] m_Text = new Text[ 3 ];
	public Text m_Total;

	public Text m_Best;
	public Text m_Now;
	public Text m_New;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (m_rsModel.GetViewMode () == true) {
			for (int i = 0; i < m_Text.Length; ++i) {
				m_Text [i].enabled = false;
			}

			m_Total.enabled = false;
			m_Best.enabled = true;
			m_Now.enabled = true;
			m_New.enabled = m_rsModel.GetNewRecordFlag ();

			m_Best.text = "BestScore " + m_rsModel.BestScore ();
			m_Now.text = "TotalScore " + m_rsModel.TotalScore ();

		} else {
			m_Best.enabled = false;
			m_Now.enabled = false;
			m_New.enabled = false;

			string[] score = m_rsModel.StageScore ();
			string[] time = m_rsModel.StageTime ();

			//ステージ情報からステージごとのスコアと時間を取得して表示
			for (int i = 0; i < m_Text.Length ; ++i) {
				m_Text [i].enabled = true;

				m_Text [i].text = "Stage" + i.ToString () + ": Score " + score [i] + "  Time " + time [i];
			}

			//合計スコアの表示
			m_Total.enabled = true;
			m_Total.text = "合計スコア:" + m_rsModel.TotalScore ();
		}
	}
}
