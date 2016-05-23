using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cEnemyManagerModel : ScriptableObject {

	private int m_EnemyMax;

	private List< cEnemyModel > m_eModelList;

	public void OnEnable(){
		m_EnemyMax = 0;

		m_eModelList = new List<cEnemyModel> ();
	}

	public void Init(){
		m_EnemyMax = 0;

		m_eModelList.Clear ();
	}

	public int EnemyRegistration( Vector3 setPosition ){
		cEnemyModel eModel = new cEnemyModel (setPosition);

		m_eModelList.Add (eModel);

		++m_EnemyMax;

		return m_EnemyMax - 1;
	}

	public void MoveEnemy(){
		for (int i = 0; i < m_EnemyMax; ++i) {
			m_eModelList [i].Move ();
		}
	}

	public cEnemyModel GetEnemyModel( int number ){
		return m_eModelList [number];
	}
}
