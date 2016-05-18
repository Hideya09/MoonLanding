using UnityEngine;
using System.Collections;
using System.IO;

public class cStageModel{

	private int m_StageNumber;

	public void StageInit(){
		m_StageNumber = 0;
	}

	public sPlayerInformation StageLoad(){
		++m_StageNumber;

		sPlayerInformation information = new sPlayerInformation();

		information.m_PlayerPosition.z = 0.0f;

		string filePath = "CSV/StageFile" + m_StageNumber.ToString ();

		TextAsset stageFile = (TextAsset)Resources.Load (filePath);

		StringReader reader = new StringReader (stageFile.text);

		string first = reader.ReadLine ();

		string[] playerData = first.Split (',');

		information.m_PlayerPosition.x = float.Parse (playerData [0]);
		information.m_PlayerPosition.y = float.Parse (playerData [1]);
		information.m_Angle = float.Parse (playerData [2]);
		information.m_MoveX = float.Parse (playerData [3]);
		information.m_MoveY = float.Parse (playerData [4]);


		return information;
	}
}
