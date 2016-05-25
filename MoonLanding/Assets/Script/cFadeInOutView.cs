using UnityEngine;
using System.Collections;

public class cFadeInOutView : MonoBehaviour {

	public cFadeInOutModel m_fadeModel;
	public SpriteRenderer m_Sprite;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//α値を取得
		Color color = new Color (1.0f, 1.0f, 1.0f, m_fadeModel.GetArpha ());

		m_Sprite.color = color;
	}
}
