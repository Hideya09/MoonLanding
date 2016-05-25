using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cEnemyManagerModel : ScriptableObject {

	//エネミーの最大数
	private int m_EnemyMax;

	//　エネミーのリスト
	private List< cEnemyModel > m_eModelList;

	//初期化
	public void OnEnable(){
		m_EnemyMax = 0;

		m_eModelList = new List<cEnemyModel> ();
	}

	//１ステージごとの初期化
	public void Init(){
		m_EnemyMax = 0;

		m_eModelList.Clear ();
	}

	//エネミー情報の登録
	public int EnemyRegistration( Vector3 setPosition ){
		cEnemyModel eModel = new cEnemyModel (setPosition);

		m_eModelList.Add (eModel);

		++m_EnemyMax;

		return m_EnemyMax - 1;
	}

	//エネミー情報の移動処理
	public void MoveEnemy(){
		for (int i = 0; i < m_EnemyMax; ++i) {
			m_eModelList [i].Move ();
		}
	}

	//エネミー情報の取得
	public cEnemyModel GetEnemyModel( int number ){
		return m_eModelList [number];
	}
}
