using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cDistanceView : MonoBehaviour {

	public cStageModel m_sModel;
	public Text m_Text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		m_Text.text = m_sModel.GetDistance ();
	}
}
