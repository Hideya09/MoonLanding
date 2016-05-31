using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cScoreView : MonoBehaviour {

	public cStageModel m_sModel;
	public Text m_Text;

	public int m_StageNumber;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//スコアを取得し表示
		m_Text.text = "Stage" + ( m_StageNumber + 1 ).ToString() + ":" + m_sModel.GetStageSore ( m_StageNumber ).ToString ("D6");
	}
}
