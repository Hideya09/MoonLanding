using UnityEngine;
using System.Collections;
using System.IO;

public class cResultScoreModel : ScriptableObject {
	public cStageModel m_sModel; 

	private bool m_ViewMode;
	private bool m_NewRecordFlag;

	private string[] m_StageScore;
	private string m_TotalScore;

	private string[] m_StageTime;

	private string m_BestScore;

	public void Init(){
		m_ViewMode = false;

		int stageMax = m_sModel.GetStageMax ();

		m_StageScore = new string[stageMax];
		m_StageTime = new string[stageMax];

		for (int i = 0; i < stageMax; ++i) {
			int stageScore = m_sModel.GetStageSore (i);
			if (stageScore == -1) {
				m_StageScore [i] = "----------";
			} else {
				m_StageScore [i] = stageScore.ToString ("D6");
			}
			sTime time = m_sModel.StageTimeGet(i);
			m_StageTime [i] = time.m_TimeMinute.ToString ("D2") + ":" + time.m_TimeSecond.ToString ("D2");
		}

		int totalScore = m_sModel.GetTotalScore ();
		int bestScore = m_sModel.GetBestScore ();

		if (totalScore > bestScore) {
			m_NewRecordFlag = true;

			FileInfo file = new FileInfo( Application.dataPath + "/Resources/CSV/BestScore.csv");

			StreamWriter write = file.CreateText ();

			write.WriteLine (totalScore);

			m_sModel.SetBestScore (totalScore);

			write.Flush ();
			write.Close ();

			PlayerPrefs.SetInt ("BestScore", totalScore);

		} else {
			m_NewRecordFlag = false;
		}

		m_TotalScore = totalScore.ToString ("D6");
		m_BestScore = bestScore.ToString ("D6");
	}

	public void SetViewComparison(){
		m_ViewMode = true;
	}

	public bool GetViewMode(){
		return m_ViewMode;
	}

	public string[] StageScore(){
		return m_StageScore;
	}

	public string[] StageTime(){
		return m_StageTime;
	}

	public string TotalScore(){
		return m_TotalScore;
	}

	public string BestScore(){
		return m_BestScore;
	}

	public bool GetNewRecordFlag(){
		return m_NewRecordFlag;
	}
}
