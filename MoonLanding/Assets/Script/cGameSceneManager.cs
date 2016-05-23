using UnityEngine;
using System.Collections;

public class cGameSceneManager : MonoBehaviour {

	public enum eGameScene{
		GameScene_Title,
		GameScene_Game,
		GameScene_Result
	}

	private static GameObject m_SceneManager;

	public eGameScene m_GameScene;

	public cMain[] m_Scene;

	void Awake(){
		if (m_SceneManager == null) {
			DontDestroyOnLoad (gameObject);

			m_SceneManager = gameObject;
		} else {
			Destroy (this);
			return;
		}

		//フレームレート設定
		Application.targetFrameRate = 60;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		eGameScene nextScene = m_Scene [(int)m_GameScene].State ();

		if (nextScene != m_GameScene) {
			UnityEngine.SceneManagement.SceneManager.LoadScene ((int)nextScene);
			m_GameScene = nextScene;
		}
	}
}
