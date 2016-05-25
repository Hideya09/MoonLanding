using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cTimeView : MonoBehaviour {

	public Text m_Time;
	public cStageModel m_sModel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//時間を取得し表示する
		sTime time = m_sModel.NowStageTimeGet (); 
		m_Time.text = "Time " + time.m_TimeMinute.ToString ("D2") + ":" + time.m_TimeSecond.ToString ("D2"); 
	}
}
