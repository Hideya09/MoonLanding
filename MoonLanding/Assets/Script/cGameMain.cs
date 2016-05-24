using UnityEngine;
using System.Collections;

public class cGameMain : cMain{

	enum eGameState{
		GameState_Init,
		GameState_Load,
		GameState_FadeIn,
		GameState_GameStart,
		GameState_Main,
		GameState_GameOver,
		GameState_GameClear,
		GameState_FadeOut,
		GameState_End
	}

	private eGameState m_State;

	private cGameSceneManager.eGameScene m_RetScene;

	public cPlayerModel m_pModel;

	public cStageModel m_sModel;

	public cGameTextModel m_gtModel;

	public cFadeInOutModel m_fadeModel;

	public cEnemyManagerModel m_eManagerModel;

	public void OnEnable(){
		m_State = eGameState.GameState_Init;

		m_RetScene = cGameSceneManager.eGameScene.GameScene_Game;
	}

	public override cGameSceneManager.eGameScene State(){
		switch (m_State) {
		case eGameState.GameState_Init:
			Init ();
			++m_State;
			break;
		case eGameState.GameState_Load:
			Load ();
			++m_State;
			break;
		case eGameState.GameState_FadeIn:
			FadeIn ();
			break;
		case eGameState.GameState_GameStart:
			GameStart ();
			break;
		case eGameState.GameState_Main:
			Main ();
			break;
		case eGameState.GameState_GameOver:
			GameOver ();
			break;
		case eGameState.GameState_GameClear:
			GameClear ();
			break;
		case eGameState.GameState_FadeOut:
			FadeOut ();
			break;
		case eGameState.GameState_End:
			End ();
			return m_RetScene;
		}

		return cGameSceneManager.eGameScene.GameScene_Game;
	}

	private void Init(){
		m_sModel.StageInit ();
	}

	private void FadeIn(){
		m_fadeModel.FadeExec ();

		if (m_fadeModel.GetState () == cFadeInOutModel.eFadeState.FadeInStop) {
			++m_State;
		}
	}

	private void Load(){
		m_sModel.StageLoad ();
		m_pModel.PlayerInfoSet( m_sModel.GetInformation());

		m_gtModel.GameStartInit ();
	}

	private void GameStart(){
		if (m_gtModel.GameStart () == true) {
			m_pModel.SetMoveFlag ();

			++m_State;
		}
	}

	private void Main(){

		m_pModel.MovePosition ();

		m_eManagerModel.MoveEnemy ();

		m_sModel.TimeCalc ();

		if (m_pModel.GetMoveFlag () == false) {
			bool clearFlag = m_sModel.CheckGoal (m_pModel.GetPosition ());
			if (clearFlag == true && m_pModel.GetClearFlag ()) {
				m_State = eGameState.GameState_GameClear;
			} else {
				m_pModel.SetBombFlag ();
				m_State = eGameState.GameState_GameOver;
			}
		}
	}

	private void GameOver(){

		m_gtModel.GameOver ();

		m_eManagerModel.MoveEnemy ();

		if (m_gtModel.Next () == true) {
			m_RetScene = cGameSceneManager.eGameScene.GameScene_Title;
			m_State = eGameState.GameState_FadeOut;
		}
	}

	private void GameClear(){

		m_gtModel.GameClear ();

		m_eManagerModel.MoveEnemy ();

		if (m_gtModel.Next () == true) {
			m_RetScene = cGameSceneManager.eGameScene.GameScene_Result;
			m_State = eGameState.GameState_FadeOut;
		}
	}

	private void FadeOut(){

		m_fadeModel.FadeExec ();

		if (m_fadeModel.GetState () == cFadeInOutModel.eFadeState.FadeOutStop) {
			++m_State;
		}
	}

	private void End(){
		m_sModel.Destroy ();

		m_eManagerModel.Init ();

		if (m_RetScene == cGameSceneManager.eGameScene.GameScene_Result && m_sModel.StageNumberCheck () == false) {
			m_State = eGameState.GameState_Load;

			m_RetScene = cGameSceneManager.eGameScene.GameScene_Game;
		} else {
			m_State = eGameState.GameState_Init;
		}
	}
}
