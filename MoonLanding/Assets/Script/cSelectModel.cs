using UnityEngine;
using System.Collections;

public class cSelectModel : ScriptableObject {

	public Vector3[] position;

	private int m_Select;

	private bool selectFlag;

	public void Init(){
		selectFlag = false;
		m_Select = 0;
	}

	public void UpSelect(){
		if (m_Select > 0) {
			--m_Select;
		}
	}

	public void DownSelect(){
		if (m_Select < position.Length - 1) {
			++m_Select;
		}
	}

	public Vector3 GetPosition(){
		return position [m_Select];
	}

	public bool GetSelectFlag(){
		return selectFlag;
	}

	public void SetSelect(){
		selectFlag = true;
	}

	public int GetSelectNumber(){
		return m_Select;
	}
}
