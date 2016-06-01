using UnityEngine;
using System.Collections;

//プレイヤーの基本情報
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

	private bool m_DangerFlag;
	private float m_DangerAlpha;

	private bool m_DrawFlag;

	//重力値
	public float m_Gravity;

	//クリア条件の角度とスピード
	public float m_GoalAngle;
	public float m_GoalSpeed;

	//角度とスピードの加算値
	public float m_AddAngle;
	public float m_AddSpeed;

	//燃料の最大値
	public float m_FuelMax;

	//生成時の処理
	public cPlayerModel(){
		m_Information.m_PlayerPosition.x = 0.0f;
		m_Information.m_PlayerPosition.y = -50.0f;
		m_Information.m_PlayerPosition.z = 0.0f;

		m_EngineFlag = false;
		m_BombFlag = false;
	}

	//初期情報のセットと初期化
	public void PlayerInfoSet( sPlayerInformation setInformation ){
		m_Information = setInformation;

		m_Fuel = m_FuelMax;

		m_MoveFlag = false;

		m_Clearflag = false;

		m_EngineFlag = false;

		m_DrawFlag = true;

		m_BombFlag = false;

		m_DangerAlpha = 0.0f;
	}

	//左回転処理
	public void AddLeftAngle(){
		if (m_MoveFlag == true) {
			m_Information.m_Angle += (m_AddAngle * Time.deltaTime);

			m_Information.m_Angle %= 360;
		}
	}
	//右回転処理
	public void AddRightAngle(){
		if (m_MoveFlag == true) {
			m_Information.m_Angle -= (m_AddAngle * Time.deltaTime);

			m_Information.m_Angle %= 360;
		}
	}
	//角度を０に戻す
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

	//移動処理
	public void MovePosition(){
		if (m_MoveFlag == true) {
			m_Information.m_PlayerPosition.x += (m_Information.m_MoveX * Time.deltaTime);
			m_Information.m_PlayerPosition.y += (m_Information.m_MoveY * Time.deltaTime);

			m_Information.m_MoveY -= (m_Gravity * Time.deltaTime);
		}

		//落下しすぎ
		if (m_Information.m_PlayerPosition.y < -100) {
			m_MoveFlag = false;
		}

		//上昇しすぎ
		if (m_Information.m_PlayerPosition.y > 450) {
			m_MoveFlag = false;
		}

		//警告の表示
		if (m_Information.m_PlayerPosition.y > 230) {
			if (m_DangerFlag == false) {
				m_DangerAlpha += Time.deltaTime;
				if (m_DangerAlpha >= 1.0f) {
					m_DangerAlpha = 1.0f;

					m_DangerFlag = true;
				}
			} else {
				m_DangerAlpha -= Time.deltaTime;
				if (m_DangerAlpha <= 0.0f) {
					m_DangerAlpha = 0.0f;

					m_DangerFlag = false;
				}
			}
		} else {
			m_DangerFlag = false;
			m_DangerAlpha = 0.0f;
		}

		//左右それぞれ限界に来たら場所を変える
		if (m_Information.m_PlayerPosition.x < -1000) {
			m_Information.m_PlayerPosition.x = 1000 + (m_Information.m_PlayerPosition.x + 1000);
		}
		if (m_Information.m_PlayerPosition.x > 1000) {
			m_Information.m_PlayerPosition.x = (m_Information.m_PlayerPosition.x - 1000) - 1000;
		}
	}

	//移動量変更処理
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
		m_DrawFlag = false;
	}

	//爆発エフェクト表示フラグを取得
	public bool GetBombFlag(){
		if (m_BombFlag == true) {
			m_BombFlag = false;

			return true;
		} else {
			return false;
		}
	}

	public bool GetDrawFlag(){
		return m_DrawFlag;
	}

	public float GetDanger(){
		return m_DangerAlpha;
	}

	//秒速を計算して返す
	public float GetSpeed(){
		return Mathf.Sqrt ((m_Information.m_MoveX * m_Information.m_MoveX) + (m_Information.m_MoveY * m_Information.m_MoveY)); 
	}

	//残り燃料％を取得
	public float  GetFuelPercent(){
		return m_Fuel / m_FuelMax;
	}


	//あたり判定処理
	public void HitCheck( Collision2D collision ){
		if (GetSpeed () < m_GoalSpeed && Mathf.Abs (m_Information.m_Angle) < m_GoalAngle) {
			m_Clearflag = true;
		}

		m_MoveFlag = false;
	}

	public void HitCheck( Collider2D collider ){
		if (GetSpeed () < m_GoalSpeed && Mathf.Abs (m_Information.m_Angle) < m_GoalAngle && collider.gameObject.CompareTag("Stage") ) {
			m_Clearflag = true;
		}

		m_MoveFlag = false;
	}

	public void SetMoveFlag(){
		m_MoveFlag = true;
	}
}
