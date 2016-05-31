using UnityEngine;
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
		//エネミー情報を登録し、番号を貰う
		m_enemyNumber = m_emanagerModel.EnemyRegistration (transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		//位置情報をマネージャーに問い合わせ取得する
		if (m_MoveFlag == true && m_enemyNumber != -1) {
			transform.rotation = Quaternion.AngleAxis (m_emanagerModel.GetEnemyModel (m_enemyNumber).GetAngle (), Vector3.forward);
			transform.position = m_emanagerModel.GetEnemyModel (m_enemyNumber).GetPosition ();
		}
	}

	void OnCollisionEnter2D( Collision2D collision ){
		m_emanagerModel.GetEnemyModel (m_enemyNumber).HitMove ();
	}

	void OnTriggerEnter2D( Collider2D other ){
		m_emanagerModel.GetEnemyModel (m_enemyNumber).HitMove ();
	}
}
