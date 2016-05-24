using UnityEngine;
using System.Collections;

public struct sPlayerInformation{
	public Vector3 m_PlayerPosition;
	public float m_Angle;
	public float m_MoveX;
	public float m_MoveY;
}

public class cPlayerModel : ScriptableObject{

	private sPlayerInformation m_Information;

	private float m_Fuel;

	private bool m_MoveFlag;

	private bool m_Clearflag;

	private bool m_EngineFlag;

	private bool m_BombFlag;

	public float m_Gravity;

	public float m_GoalAngle;
	public float m_GoalSpeed;

	public float m_AddAngle;
	public float m_AddSpeed;

	public float m_FuelMax;

	public cPlayerModel(){
		m_Information.m_PlayerPosition.x = 0.0f;
		m_Information.m_PlayerPosition.y = -50.0f;
		m_Information.m_PlayerPosition.z = 0.0f;

		m_EngineFlag = false;
		m_BombFlag = false;
	}

	public void PlayerInfoSet( sPlayerInformation setInformation ){
		m_Information = setInformation;

		m_Fuel = m_FuelMax;

		m_MoveFlag = false;

		m_Clearflag = false;

		m_EngineFlag = false;
	}

	public void AddLeftAngle(){
		if (m_MoveFlag == true) {
			m_Information.m_Angle += (m_AddAngle * Time.deltaTime);

			m_Information.m_Angle %= 360;
		}
	}
	public void AddRightAngle(){
		if (m_MoveFlag == true) {
			m_Information.m_Angle -= (m_AddAngle * Time.deltaTime);

			m_Information.m_Angle %= 360;
		}
	}
	public void HorizontalAngle(){
		if (m_MoveFlag == true) {
			if (Mathf.DeltaAngle (m_Information.m_Angle, 0.0f) > m_AddAngle * 6 * Time.deltaTime) {
				m_Information.m_Angle += m_AddAngle * 6 * Time.deltaTime;
			} else if (Mathf.DeltaAngle (m_Information.m_Angle, 0.0f) < -m_AddAngle * 6 * Time.deltaTime) {
				m_Information.m_Angle -= m_AddAngle * 6 * Time.deltaTime;
			} else {
				m_Information.m_Angle = 0.0f;
			}

			m_Information.m_Angle %= 360;
		}
	}

	public void MovePosition(){
		if (m_MoveFlag == true) {
			m_Information.m_PlayerPosition.x += (m_Information.m_MoveX * Time.deltaTime);
			m_Information.m_PlayerPosition.y += (m_Information.m_MoveY * Time.deltaTime);

			m_Information.m_MoveY -= (m_Gravity * Time.deltaTime);
		}

		if (m_Information.m_PlayerPosition.x < -1000) {
			m_Information.m_PlayerPosition.x = 1000 + (m_Information.m_PlayerPosition.x + 1000);
		}

		if (m_Information.m_PlayerPosition.x > 1000) {
			m_Information.m_PlayerPosition.x = (m_Information.m_PlayerPosition.x - 1000) - 1000;
		}
	}

	public void CalcDirection(){
		if (m_MoveFlag == true) {
			if (m_Fuel > 0.0f) {
				m_Information.m_MoveX -= Mathf.Sin (m_Information.m_Angle * Mathf.Deg2Rad) * m_AddSpeed * Time.deltaTime;
				m_Information.m_MoveY += Mathf.Cos (m_Information.m_Angle * Mathf.Deg2Rad) * m_AddSpeed * Time.deltaTime;

				m_Fuel -= Time.deltaTime;

				if (m_Fuel < 0.0f) {
					m_Fuel = 0.0f;
				}

				m_EngineFlag = true;
			} else {
				m_EngineFlag = false;
			}
		} else {
			m_EngineFlag = false;
		}

	}

	public void EngineStop(){
		m_EngineFlag = false;
	}

	public bool GetMoveFlag(){
		return m_MoveFlag;
	}
	public bool GetClearFlag(){
		return m_Clearflag;
	}

	public Vector3 GetPosition(){
		return m_Information.m_PlayerPosition;
	}

	public float GetAngle(){
		return m_Information.m_Angle;
	}

	public bool GetEngineFlag(){
		return m_EngineFlag;
	}

	public void SetBombFlag(){
		m_BombFlag = true;
	}

	public bool GetBombFlag(){
		if (m_BombFlag == true) {
			m_BombFlag = false;

			return true;
		} else {
			return false;
		}
	}

	public float GetSpeed(){
		return Mathf.Sqrt ((m_Information.m_MoveX * m_Information.m_MoveX) + (m_Information.m_MoveY * m_Information.m_MoveY)); 
	}

	public float  GetFuelPercent(){
		return m_Fuel / m_FuelMax;
	}

	public void HitCheck( Collision2D collision ){
		if (GetSpeed () < m_GoalSpeed && Mathf.Abs (m_Information.m_Angle) < m_GoalAngle) {
			m_Clearflag = true;
		}

		m_MoveFlag = false;
	}

	public void HitCheck( Collider2D collider ){
		if (GetSpeed () < m_GoalSpeed && Mathf.Abs (m_Information.m_Angle) < m_GoalAngle) {
			m_Clearflag = true;
		}

		m_MoveFlag = false;
	}

	public void SetMoveFlag(){
		m_MoveFlag = true;
	}
}
