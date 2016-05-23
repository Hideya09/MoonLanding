using UnityEngine;
using System.Collections;

public struct sPlayerInformation{
	public Vector3 m_PlayerPosition;
	public float m_Angle;
	public float m_MoveX;
	public float m_MoveY;
}

public class cPlayerModel : ScriptableObject{

	//角度の加算値(秒速)
	private const float AddAngle = 60.0f;
	private const float AddSpeed = 6.0f;

	private const float FuelMax = 30.0f;

	private sPlayerInformation m_Information;

	private float m_Fuel;

	private bool m_MoveFlag;

	private bool m_Clearflag;

	public cPlayerModel(){
		m_Information.m_PlayerPosition.x = 0.0f;
		m_Information.m_PlayerPosition.y = -50.0f;
		m_Information.m_PlayerPosition.z = 0.0f;
	}

	public void PlayerInfoSet( sPlayerInformation setInformation ){
		m_Information = setInformation;

		m_Fuel = FuelMax;

		m_MoveFlag = false;

		m_Clearflag = false;
	}

	public void AddLeftAngle(){
		if (m_MoveFlag == true) {
			m_Information.m_Angle += (AddAngle * Time.deltaTime);

			m_Information.m_Angle %= 360;
		}
	}
	public void AddRightAngle(){
		if (m_MoveFlag == true) {
			m_Information.m_Angle -= (AddAngle * Time.deltaTime);

			m_Information.m_Angle %= 360;
		}
	}
	public void HorizontalAngle(){
		if (m_MoveFlag == true) {
			if (Mathf.DeltaAngle (m_Information.m_Angle, 0.0f) > AddAngle * 6 * Time.deltaTime) {
				m_Information.m_Angle += AddAngle * 6 * Time.deltaTime;
			} else if (Mathf.DeltaAngle (m_Information.m_Angle, 0.0f) < -AddAngle * 6 * Time.deltaTime) {
				m_Information.m_Angle -= AddAngle * 6 * Time.deltaTime;
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

			m_Information.m_MoveY -= (1.622f * Time.deltaTime);
		}
	}

	public void CalcDirection(){
		if (m_Fuel > 0.0f) {
			m_Information.m_MoveX -= Mathf.Sin (m_Information.m_Angle * Mathf.Deg2Rad) * AddSpeed * Time.deltaTime;
			m_Information.m_MoveY += Mathf.Cos (m_Information.m_Angle * Mathf.Deg2Rad) * AddSpeed * Time.deltaTime;
		}

		m_Fuel -= Time.deltaTime;
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

	public float GetSpeed(){
		return Mathf.Sqrt ((m_Information.m_MoveX * m_Information.m_MoveX) + (m_Information.m_MoveY * m_Information.m_MoveY)); 
	}

	public float  GetFuelPercent(){
		return m_Fuel / FuelMax;
	}

	public void HitCheck( Collision2D collision ){
		if (collision.collider.name == "StageGoal") {
			Debug.Log ("ゴールに当たった");
		} else {
			Debug.Log ("それ以外");
		}
	}

	public void HitCheck( Collider2D collider ){
		if (collider.name == "StageGoal1") {
			if (GetSpeed () < 3.0f && Mathf.Abs (m_Information.m_Angle) < 5) {
				m_Clearflag = true;
				Debug.Log ("ゴールに当たった");
			} else {
				Debug.Log ("それ以外");
			}
		} else {
			Debug.Log ("それ以外");
		}

		m_MoveFlag = false;
	}

	public void SetMoveFlag(){
		m_MoveFlag = true;
	}
}
