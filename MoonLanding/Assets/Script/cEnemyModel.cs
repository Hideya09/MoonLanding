using UnityEngine;
using System.Collections;

public class cEnemyModel {

	private Vector3 m_Position;

	private float m_MoveTime;
	private float m_MoveTimeCountMax;

	private float m_MoveX;
	private float m_MoveY;

	private const float MoveTimeMax = 5.0f;
	private const float MoveXMax = 6.0f;
	private const float MoveYMax = 1.0f;

	public cEnemyModel( Vector3 setPosition ){
		m_Position = setPosition;

		m_MoveTime = 0.0f;
		m_MoveTimeCountMax = 0.0f;

		m_MoveX = 0.0f;
		m_MoveY = 0.0f;
	}

	public void Move(){
		m_MoveTime += Time.deltaTime;

		m_Position.x += m_MoveX * Time.deltaTime;
		m_Position.y += m_MoveY * Time.deltaTime;

		if (m_MoveTime >= m_MoveTimeCountMax) {
			MoveChange ();
		}
	}

	public void MoveChange(){
		m_MoveTime = 0.0f;

		m_MoveX = Random.Range (-MoveXMax, MoveXMax);
		m_MoveY = Random.Range (-MoveYMax, MoveYMax);
		m_MoveTimeCountMax = Random.Range (0.0f, MoveTimeMax);
	}

	public Vector3 GetPosition(){
		return m_Position;
	}
}
