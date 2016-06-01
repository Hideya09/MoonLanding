using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cScoreView : MonoBehaviour {

	public cStageModel m_sModel;
	public Text[] m_Text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//スコアを取得し表示

		int textNumber = 0;

		for (int i = 0; i < m_sModel.GetStageMax (); ++i) {
			int score = m_sModel.GetStageSore (i);
			if (score == -1) {
				m_Text [textNumber].text = "Stage" + (i + 1).ToString () + ":" + "----------";

				++textNumber;
			} else if (score != -2) {
				m_Text [textNumber].text = "Stage" + (i + 1).ToString () + ":" + score.ToString ("D6");

				++textNumber;
			}
		}

		for (int i = textNumber; i < m_sModel.GetStageMax (); ++i) {
			m_Text [i].text = "";
		}
	}
}
