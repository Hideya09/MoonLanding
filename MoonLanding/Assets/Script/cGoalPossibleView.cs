﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cGoalPossibleView : MonoBehaviour {

	public cStageModel m_sModel;
	public Text m_Text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (m_sModel.GetGoalPossible () == true) {
			m_Text.text = "ゴール可能地点上";
		} else {
			m_Text.text = "";
		}
	}
}
