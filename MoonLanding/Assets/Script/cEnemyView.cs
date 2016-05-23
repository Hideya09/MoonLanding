﻿using UnityEngine;
using System.Collections;

public class cEnemyView : MonoBehaviour {

	private int m_enemyNumber;

	public cEnemyManagerModel m_emanagerModel;

	public bool m_MoveFlag;

	void Awake(){
		m_enemyNumber = -1;
	}

	// Use this for initialization
	void Start () {
		m_enemyNumber = m_emanagerModel.EnemyRegistration (transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		if (m_MoveFlag == true && m_enemyNumber != -1) {
			transform.position = m_emanagerModel.GetEnemyModel (m_enemyNumber).GetPosition ();
		}
	}
}