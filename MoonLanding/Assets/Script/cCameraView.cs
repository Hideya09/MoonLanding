﻿using UnityEngine;
using System.Collections;

public class cCameraView : MonoBehaviour {

	public cPlayerModel m_pModel;

	public float m_UpMax;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//カメラの位置をプレイヤーの位置に更新する
		Vector3 position = m_pModel.GetPosition();
		if (position.y > m_UpMax) {
			position.y = m_UpMax;
		}
		position.z = -52;

		transform.position = position;
	}
}
