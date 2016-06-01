using UnityEngine;
using System.Collections;

public class cSelectModel : ScriptableObject {

	public Vector3[] position;

	private int m_Select;

	private bool m_SelectFlag;

	private bool m_ActiveFlag;

	public void Init(){
		m_SelectFlag = false;
		m_ActiveFlag = false;
		m_Select = 0;
	}

	public void UpSelect(){
		if (m_Select > 0 && m_SelectFlag == false && m_ActiveFlag == true) {
			--m_Select;
		}
	}

	public void DownSelect(){
		if (m_Select < position.Length - 1  && m_SelectFlag == false && m_ActiveFlag == true) {
			++m_Select;
		}
	}

	public Vector3 GetPosition(){
		return position [m_Select];
	}

	public bool GetSelectFlag(){
		m_ActiveFlag = true;
		return m_SelectFlag;
	}

	public void SetSelect(){
		if (m_ActiveFlag == true) {
			m_SelectFlag = true;
		}
	}

	public int GetSelectNumber(){
		return m_Select;
	}
}
