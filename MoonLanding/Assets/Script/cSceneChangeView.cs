using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cSceneChangeView : MonoBehaviour {

	public cSceneChangeModel m_scModel;
	public Text text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		text.color = new Color (1.0f, 1.0f, 0.0f, m_scModel.GetArpha ());
	}
}
