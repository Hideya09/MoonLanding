using UnityEngine;
using System.Collections;

public class cGameMain{

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

	private cPlayerControl m_pControl;
	private cPlayerModel m_pModel;

	private cStageModel m_sModel;

	public cGameMain(){
		m_pModel = new cPlayerModel();

		m_pControl = new cPlayerControl( m_pModel , GameObject.Find( "SpaceShuttle" ).GetComponent< cPlayerView >() );

		m_sModel = new cStageModel();

		m_State = eGameState.GameState_Init;
	}

	public bool GameManage(){
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
			++m_State;
			break;
		case eGameState.GameState_GameStart:
			++m_State;
			break;
		case eGameState.GameState_Main:
			Main ();
			break;
		case eGameState.GameState_GameOver:
			break;
		case eGameState.GameState_GameClear:
			break;
		case eGameState.GameState_FadeOut:
			break;
		case eGameState.GameState_End:
			break;
		}

		return false;
	}

	private void Init(){
		m_sModel.StageInit ();
	}

	private void FadeIn(){
	}

	private void Load(){
		m_pModel.PlayerInfoSet (m_sModel.StageLoad ());
	}

	private void GameStart(){
	}

	private void Main(){
		m_pControl.RotateKey ();
		m_pControl.HorizontalKey ();
		m_pControl.PropulsionKey ();

		m_pModel.MovePosition ();
	}

	private void GameOver(){
	}

	private void GameClear(){
	}

	private void FadeOut(){
	}

	private void End(){
	}
}
