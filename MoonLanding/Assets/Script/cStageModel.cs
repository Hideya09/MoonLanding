using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

//経過時間格納構造体
public struct sTime{
	public int m_TimeSecond;
	public int m_TimeMinute;

	public float m_TimeMilliSecond;
}

public class cStageModel : ScriptableObject{

	//現ステージ番号と最大値
	private int m_StageNumber;
	public int m_StageMax;

	//時間情報
	private sTime[] m_Time;

	//ステージごとのスコアとトータル
	private int[] m_Score;
	private int m_TotalScore;

	//プレイヤーの初期情報
	private sPlayerInformation m_playerInformation;

	//ステージのノード
	private List< Vector2 > m_StagePosition;

	GameObject m_Stage;
	List< GameObject > m_Enemy;

	//初期処理
	public void OnEnable(){
		m_StageNumber = 0;

		m_Enemy = new List< GameObject > ();

		m_StagePosition = new List<Vector2> ();
	}

	//初期化処理
	public void StageInit(){
		m_StageNumber = 0;

		m_Time = new sTime[m_StageMax];
		m_Score = new int[m_StageMax];

		m_TotalScore = 0;
	}

	//ステージのロード
	public void StageLoad(){
		//ステージ一の解放
		m_StagePosition.Clear ();

		//ステージ番号を一つ増やす
		++m_StageNumber;

		//プレイヤー情報の読み込み
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

		//敵情報の読み込みと生成
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

		//ステージ情報の読み込み
		TextAsset stageInformation = (TextAsset)Resources.Load ("CSV/StageInformation");

		StringReader stageReader = new StringReader (stageInformation.text);

		List<Vector3> vertex = new List<Vector3>();
		List<Vector2> uv = new List<Vector2> ();
		List<int> triangle = new List<int> ();

		int count = 0;

		while( stageReader.Peek() > -1 ){
			//ステージの頂点を読み込み、メッシュを構成する

			string vertexLine = stageReader.ReadLine ();
			string[] vertexString = vertexLine.Split (',' );
			Vector3 vec = new Vector3 (float.Parse (vertexString [0]), float.Parse (vertexString [1]), 0.0f);
			vertex.Add (vec);
			uv.Add (new Vector2 (0.0f, 0.0f));

			m_StagePosition.Add ( new Vector2( vec.x , vec.y ) );

			vec.y = -100.0f;
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

		//メッシュ情報を入れ込む
		Mesh mesh = new Mesh ();

		mesh.vertices = vertex.ToArray ();
		mesh.uv = uv.ToArray ();
		mesh.triangles = triangle.ToArray ();

		vertex.Clear ();
		uv.Clear ();
		triangle.Clear ();

		//ステージを生成し、メッシュを入れ込む
		m_Stage = (GameObject)Resources.Load ("Prefab/Stage");

		m_Stage.GetComponent< MeshFilter > ().mesh = mesh;
		m_Stage.GetComponent< EdgeCollider2D> ().points = m_StagePosition.ToArray ();

		m_Stage = GameObject.Instantiate (m_Stage);

		stageReader.Close ();
	}

	//時間の計算
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

	//現ステージの時間を取得
	public sTime NowStageTimeGet(){
		if (m_StageNumber > m_StageMax || m_StageNumber <= 0) {
			return new sTime();
		}
		return m_Time [m_StageNumber - 1];
	}

	//指定番号のステージの時間を取得
	public sTime StageTimeGet( int stageNumber ){
		if (stageNumber >= m_StageMax || m_StageNumber < 0) {
			return new sTime();
		}
		return m_Time [stageNumber];
	}

	//現在のステージのスコアを取得
	public int GetNowStageScore(){
		return m_Score [m_StageNumber - 1];
	}

	//指定番号のステージのスコアを取得
	public int GetStageSore( int stageNumber ){
		return m_Score[ stageNumber ];
	}

	//トータルのスコアを取得
	public int GetTotalScore(){
		return m_TotalScore;
	}

	public sPlayerInformation GetInformation(){
		return m_playerInformation;
	}

	//最終ステージかの確認
	public bool StageNumberCheck(){
		return m_StageMax == m_StageNumber;
	}

	public int GetStageMax(){
		return m_StageMax;
	}

	//ゴール時にクリア位置かどうかを計算
	public bool CheckGoal( Vector3 position ){
		float distance =  Vector3.Distance (m_playerInformation.m_PlayerPosition, position);

		//if (distance > 1000.0f) {
		//	distance = 2000.0f - distance;
		//}

		m_Score [m_StageNumber - 1] = Mathf.RoundToInt (distance * 100);

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

	//ステージとエネミー情報を破棄
	public void Destroy(){
		m_TotalScore += m_Score [m_StageNumber - 1];

		Destroy (m_Stage);
		for (int i = 0; i < m_Enemy.Count; ++i) {
			Destroy (m_Enemy [i]);
		}

		m_Enemy.Clear ();
	}
}
