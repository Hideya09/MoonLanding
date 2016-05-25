using UnityEngine;
using System.Collections;

public class cGameSceneManager : MonoBehaviour {

	//ゲームシーン
	public enum eGameScene{
		GameScene_Title, //タイトルシーン
		GameScene_Game, //ゲームシーン
		GameScene_Result //リザルトシーン
	}

	private static GameObject m_SceneManager;

	//現在のシーン
	public eGameScene m_GameScene;

	//シーン
	public cMain[] m_Scene;

	void Awake(){
		//ゲームシーンマネージャーは一つだけしか作らない
		if (m_SceneManager == null) {
			DontDestroyOnLoad (gameObject);

			m_SceneManager = gameObject;
		} else {
			Destroy (gameObject);
			return;
		}

		//フレームレート設定
		//Application.targetFrameRate = 60;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//返り値が現在のシーン番号と違う場合はシーンを切り替える
		eGameScene nextScene = m_Scene [(int)m_GameScene].State ();

		if (nextScene != m_GameScene) {
			UnityEngine.SceneManagement.SceneManager.LoadScene ((int)nextScene);
			m_GameScene = nextScene;
		}
	}
}
