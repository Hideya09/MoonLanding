using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cResultScoreView : MonoBehaviour {

	public cStageModel m_sModel;

	public Text[] m_Text = new Text[ 3 ];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < m_sModel.GetStageMax(); ++i) {
			sTime time = m_sModel.StageTimeGet (i);
			m_Text [i].text = "Stage" + i.ToString() + ": Score " + 000000.ToString("D6") + "  Time " + time.m_TimeMinute.ToString("D2") + ":" + time.m_TimeSecond.ToString("D2");
		}
	}
}
