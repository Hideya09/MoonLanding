using UnityEngine;
using System.Collections;

public class cGameMain : cMain{

	//ゲームシーンのステート
	enum eGameState{
		GameState_Init, //初期化処理
		GameState_Load, //１ステージごとの初期化処理
		GameState_FadeIn, //フェードイン処理
		GameState_GameStart, //ゲーム開始処理
		GameState_Main, //ゲームメインループ
		GameState_GameOver, //ゲームオーバー時の処理
		GameState_GameClear, //ゲームクリア時の処理
		GameState_FadeOut, //フェードアウト処理
		GameState_End //終了処理
	}

	private eGameState m_State;

	private cGameSceneManager.eGameScene m_RetScene;

	public cPlayerModel m_pModel;

	public cStageModel m_sModel;

	public cGameTextModel m_gtModel;

	public cFadeInOutModel m_fadeModel;

	public cEnemyManagerModel m_eManagerModel;

	//生成時の処理
	public void OnEnable(){
		m_State = eGameState.GameState_Init;

		m_RetScene = cGameSceneManager.eGameScene.GameScene_Game;
	}

	//ゲームシーン時のステート管理
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

	//初期化
	private void Init(){
		m_sModel.StageInit ();
	}

	//フェードイン
	private void FadeIn(){
		m_fadeModel.FadeExec ();

		if (m_fadeModel.GetState () == cFadeInOutModel.eFadeState.FadeInStop) {
			++m_State;
		}
	}

	//ロード
	private void Load(){
		m_sModel.StageLoad ();
		m_pModel.PlayerInfoSet( m_sModel.GetInformation());

		m_gtModel.GameStartInit ();
	}

	//ゲーム開始
	private void GameStart(){
		if (m_gtModel.GameStart () == true) {
			m_pModel.SetMoveFlag ();

			++m_State;
		}
	}

	//メインループ
	private void Main(){

		//プレイヤー移動処理
		m_pModel.MovePosition ();

		//エネミー移動処理
		m_eManagerModel.MoveEnemy ();

		//時間の計算
		m_sModel.TimeCalc ();

		//ゴール判定
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

	//ゲームオーバー
	private void GameOver(){

		m_gtModel.GameOver ();

		m_eManagerModel.MoveEnemy ();

		if (m_gtModel.Next () == true) {
			//返り値のタイトルをセット
			m_RetScene = cGameSceneManager.eGameScene.GameScene_Title;
			m_State = eGameState.GameState_FadeOut;
		}
	}

	//ゲームクリア
	private void GameClear(){

		m_gtModel.GameClear ();

		m_eManagerModel.MoveEnemy ();

		if (m_gtModel.Next () == true) {
			//返り値にリザルトをセット
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

	//終了処理
	private void End(){
		m_sModel.Destroy ();

		m_eManagerModel.Init ();

		if (m_RetScene == cGameSceneManager.eGameScene.GameScene_Result && m_sModel.StageNumberCheck () == false) {
			//全ステージクリアしていない場合ロードに移動
			m_State = eGameState.GameState_Load;

			m_RetScene = cGameSceneManager.eGameScene.GameScene_Game;
		} else {
			m_State = eGameState.GameState_Init;
		}
	}
}
