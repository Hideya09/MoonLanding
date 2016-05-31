using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cDangerView : MonoBehaviour {

	public cPlayerModel m_pModel;
	public Text text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Color color = new Color(1.0f, 0.0f, 0.0f, m_pModel.GetDanger());

		text.color = color;
	}
}
