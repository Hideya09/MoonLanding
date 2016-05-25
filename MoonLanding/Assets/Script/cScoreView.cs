using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cScoreView : MonoBehaviour {

	public cStageModel m_sModel;
	public Text m_Text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//スコアを取得し表示
		m_Text.text = "Score " + m_sModel.GetTotalScore ().ToString ("D6");
	}
}
