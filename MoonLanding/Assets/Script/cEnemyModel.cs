using UnityEngine;
using System.Collections;

public class cEnemyModel {

	private Vector3 m_Position;

	//移動時間
	private float m_MoveTime;
	private float m_MoveTimeCountMax;

	//移動量
	private float m_MoveX;
	private float m_MoveY;

	//移動の時間と量の最大値
	private const float MoveTimeMax = 5.0f;
	private const float MoveXMax = 6.0f;
	private const float MoveYMax = 1.0f;

	//生成時の処理
	public cEnemyModel( Vector3 setPosition ){
		m_Position = setPosition;

		m_MoveTime = 0.0f;
		m_MoveTimeCountMax = 0.0f;

		m_MoveX = 0.0f;
		m_MoveY = 0.0f;
	}

	//移動処理
	public void Move(){
		m_MoveTime += Time.deltaTime;

		m_Position.x += m_MoveX * Time.deltaTime;
		m_Position.y += m_MoveY * Time.deltaTime;

		//移動時間が最大に達したら
		if (m_MoveTime >= m_MoveTimeCountMax) {
			MoveChange ();
		}
	}

	//移動方向変更処理
	public void MoveChange(){
		m_MoveTime = 0.0f;

		//移動時間と移動量を変更
		m_MoveX = Random.Range (-MoveXMax, MoveXMax);
		m_MoveY = Random.Range (-MoveYMax, MoveYMax);
		m_MoveTimeCountMax = Random.Range (0.0f, MoveTimeMax);
	}

	//位置を取得
	public Vector3 GetPosition(){
		return m_Position;
	}
}
