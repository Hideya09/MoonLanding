using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public struct sTime{
	public int m_TimeSecond;
	public int m_TimeMinute;

	public float m_TimeMilliSecond;
}

public class cStageModel : ScriptableObject{

	private int m_StageNumber;
	public int m_StageMax;

	private sTime[] m_Time;

	private sPlayerInformation m_playerInformation;

	private List< Vector2 > m_StagePosition;

	GameObject m_Stage;
	List< GameObject > m_Enemy;

	public void OnEnable(){
		m_StageNumber = 0;

		m_Enemy = new List< GameObject > ();

		m_StagePosition = new List<Vector2> ();
	}

	public void StageInit(){
		m_StageNumber = 0;

		m_Time = new sTime[m_StageMax];
	}

	public void StageLoad(){
		m_StagePosition.Clear ();

		++m_StageNumber;

		m_playerInformation = new sPlayerInformation();

		m_playerInformation.m_PlayerPosition.z = 0.0f;

		string filePath = "CSV/StageFile" + m_StageNumber.ToString ();

		TextAsset stageFile = (TextAsset)Resources.Load (filePath);

		StringReader reader = new StringReader (stageFile.text);

		string first = reader.ReadLine ();

		string[] playerData = first.Split (',');

		m_playerInformation.m_PlayerPosition.x = float.Parse (playerData [0]);
		m_playerInformation.m_PlayerPosition.y = float.Parse (playerData [1]);
		m_playerInformation.m_Angle = float.Parse (playerData [2]);
		m_playerInformation.m_MoveX = float.Parse (playerData [3]);
		m_playerInformation.m_MoveY = float.Parse (playerData [4]);

		//reader.Close ();

		//string filename = "CSV/Enemy" + m_StageNumber.ToString ();

		//TextAsset enemyFile = (TextAsset)Resources.Load (filename);

		//StringReader enemyReader = new StringReader (enemyFile.text);

		while (reader.Peek () > -1) {

			string enemyString = reader.ReadLine ();
			string[] enemyData = enemyString.Split (',');

			Vector3 enemyPosition;

			enemyPosition.x = float.Parse (enemyData [0]);
			enemyPosition.y = float.Parse (enemyData [1]);
			enemyPosition.z = 0;

			GameObject enemy;

			if (int.Parse (enemyData [2]) == 1) {
				enemy = (GameObject)Resources.Load ("Prefab/Enemy2");
			} else {
				enemy = (GameObject)Resources.Load ("Prefab/Enemy1");
			}

			enemy.transform.position = enemyPosition;
			m_Enemy.Add(GameObject.Instantiate (enemy));
		}

		reader.Close ();

		TextAsset stageInformation = (TextAsset)Resources.Load ("CSV/StageInformation");

		StringReader stageReader = new StringReader (stageInformation.text);

		List<Vector3> vertex = new List<Vector3>();
		List<Vector2> uv = new List<Vector2> ();
		List<int> triangle = new List<int> ();

		int count = 0;

		while( stageReader.Peek() > -1 ){
			string vertexLine = stageReader.ReadLine ();
			string[] vertexString = vertexLine.Split (',' );
			Vector3 vec = new Vector3 (float.Parse (vertexString [0]), float.Parse (vertexString [1]), 0.0f);
			vertex.Add (vec);
			uv.Add (new Vector2 (0.0f, 0.0f));

			m_StagePosition.Add ( new Vector2( vec.x , vec.y ) );

			vec.y = -10.0f;
			vertex.Add (vec);
			uv.Add (new Vector2 (0.0f, 0.0f));

			if ((count * 2) >= 2) {
				triangle.Add (count * 2);
				triangle.Add ((count * 2) - 1);
				triangle.Add ((count * 2) - 2);

				triangle.Add ((count * 2) + 1);
				triangle.Add ((count * 2) - 1);
				triangle.Add (count * 2);
			}

			++count;
		}

		Mesh mesh = new Mesh ();

		mesh.vertices = vertex.ToArray ();
		mesh.uv = uv.ToArray ();
		mesh.triangles = triangle.ToArray ();

		vertex.Clear ();
		uv.Clear ();
		triangle.Clear ();

		m_Stage = (GameObject)Resources.Load ("Prefab/Stage");

		m_Stage.GetComponent< MeshFilter > ().mesh = mesh;
		m_Stage.GetComponent< EdgeCollider2D> ().points = m_StagePosition.ToArray ();

		m_Stage = GameObject.Instantiate (m_Stage);

		stageReader.Close ();
	}

	public void TimeCalc(){
		m_Time[ m_StageNumber - 1 ].m_TimeMilliSecond += Time.deltaTime;
		if (m_Time[ m_StageNumber - 1 ].m_TimeMilliSecond >= 1.0f) {
			m_Time[ m_StageNumber - 1 ].m_TimeMilliSecond -= 1.0f;

			++m_Time[ m_StageNumber - 1 ].m_TimeSecond;

			if (m_Time[ m_StageNumber - 1 ].m_TimeSecond >= 60) {
				++m_Time[ m_StageNumber - 1 ].m_TimeMinute;

				m_Time[ m_StageNumber - 1 ].m_TimeSecond -= 60;
			}
		}
	}

	public sTime NowStageTimeGet(){
		if (m_StageNumber > m_StageMax || m_StageNumber <= 0) {
			return new sTime();
		}
		return m_Time [m_StageNumber - 1];
	}

	public sTime StageTimeGet( int stageNumber ){
		if (stageNumber >= m_StageMax || m_StageNumber < 0) {
			return new sTime();
		}
		return m_Time [stageNumber];
	}

	public sPlayerInformation GetInformation(){
		return m_playerInformation;
	}

	public bool StageNumberCheck(){
		return m_StageMax == m_StageNumber;
	}

	public int GetStageMax(){
		return m_StageMax;
	}

	public bool CheckGoal( Vector3 position ){
		int index = 0;

		while (position.x > m_StagePosition [index].x) {
			++index;
			if (index >= m_StagePosition.Count) {
				break;
			}
		}

		if (index <= 0 || index >= m_StagePosition.Count) {
			return false;
		}

		if ((int)m_StagePosition [index].y == (int)m_StagePosition [index - 1].y) {
			return true;
		}

		return false;
	}

	public void Destroy(){
		Destroy (m_Stage);
		for (int i = 0; i < m_Enemy.Count; ++i) {
			Destroy (m_Enemy [i]);
		}

		m_Enemy.Clear ();
	}
}
