using UnityEngine;
using System.Collections;

public class cEnemyModel {

	private Vector3 m_Position;
	private Vector3 m_BufPosition;
	private float m_Angle;

	//移動時間
	private float m_MoveTime;
	private float m_MoveTimeCountMax;

	//移動量
	private float m_MoveAdd;

	//回転量
	private float m_AngleMax;
	private float m_AngleAdd;

	//移動の時間と量の最大値
	private const float MoveTimeMax = 5.0f;
	private const float MoveMax = 6.0f;

	//生成時の処理
	public cEnemyModel( Vector3 setPosition ){
		m_Position = setPosition;

		m_MoveTime = 0.0f;
		m_MoveTimeCountMax = 0.0f;

		m_Angle = 0.0f;
	}

	//移動処理
	public void Move(){
		m_MoveTime += Time.deltaTime;

		if (m_MoveTime > 1.0f) {
			m_BufPosition = m_Position;

			m_Angle = m_AngleMax;

			m_Position.x -= Mathf.Sin (m_Angle * Mathf.Deg2Rad) * m_MoveAdd * Time.deltaTime;
			m_Position.y += Mathf.Cos (m_Angle * Mathf.Deg2Rad) * m_MoveAdd * Time.deltaTime;

			//移動時間が最大に達したら
			if (m_MoveTime >= m_MoveTimeCountMax) {
				MoveChange ();
			}
		} else {
			m_Angle += m_AngleAdd * Time.deltaTime;
		}
	}

	//移動方向変更処理
	public void MoveChange(){
		m_MoveTime = 0.0f;

		//移動時間と移動量を変更
		m_MoveTimeCountMax = Random.Range (0.0f, MoveTimeMax) + 1.0f;

		m_MoveAdd = Random.Range (MoveMax * 0.5f, MoveMax);

		m_AngleMax = Random.Range (0.0f, 360.0f);

		m_AngleAdd = Mathf.DeltaAngle (m_Angle, m_AngleMax);
	}

	public void HitMove(){
		m_Position = m_BufPosition;

		MoveChange ();
	}

	//位置を取得
	public Vector3 GetPosition(){
		return m_Position;
	}

	public float GetAngle(){
		return m_Angle;
	}
}