using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cResultScoreView : MonoBehaviour {

	public cStageModel m_sModel;

	public Text[] m_Text = new Text[ 3 ];
	public Text m_Total;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//ステージ情報からステージごとのスコアと時間を取得して表示
		for (int i = 0; i < m_sModel.GetStageMax(); ++i) {
			sTime time = m_sModel.StageTimeGet (i);
			m_Text [i].text = "Stage" + i.ToString() + ": Score " + m_sModel.GetStageSore( i ).ToString("D6") + "  Time " + time.m_TimeMinute.ToString("D2") + ":" + time.m_TimeSecond.ToString("D2");
		}

		//合計スコアの表示
		m_Total.text = "合計スコア:" + m_sModel.GetTotalScore ().ToString ();
	}
}
