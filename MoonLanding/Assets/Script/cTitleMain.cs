using UnityEngine;
using System.Collections;

public class cTitleMain : cMain {

	//タイトルシーンのステート
	public enum eTitleState{
		TitleState_Init,
		TitleState_FadeIn,
		TitleState_Main,
		TitleState_FadeOut,
		TitleState_End
	}

	public cFadeInOutModel m_fadeModel;
	public cSelectModel m_selModel;

	public cGameMain m_gMain;

	private eTitleState m_State;

	private cGameSceneManager.eGameScene m_RetScene;

	//初期化処理
	public void OnEnable(){
		m_State = eTitleState.TitleState_Init;

		m_RetScene = cGameSceneManager.eGameScene.GameScene_Title;
	}

	//タイトルシーン時のステート管理
	public override cGameSceneManager.eGameScene State(){

		m_RetScene = cGameSceneManager.eGameScene.GameScene_Title;

		switch (m_State) {
		case eTitleState.TitleState_Init:
			Init ();
			break;
		case eTitleState.TitleState_FadeIn:
			FadeIn ();
			break;
		case eTitleState.TitleState_Main:
			Main ();
			break;
		case eTitleState.TitleState_FadeOut:
			FadeOut ();
			break;
		case eTitleState.TitleState_End:
			End ();
			break;
		}

		return m_RetScene;
	}

	private void Init(){
		m_selModel.Init ();
		++m_State;
	}

	//フェードイン
	private void FadeIn(){
		m_fadeModel.FadeExec ();

		if (m_fadeModel.GetState () == cFadeInOutModel.eFadeState.FadeInStop) {
			++m_State;
		}
	}

	//メインループ
	private void Main(){
		if (m_selModel.GetSelectFlag ()) {
			m_gMain.SetStage (m_selModel.GetSelectNumber ());

			++m_State;
		}
	}

	//フェードアウト
	private void FadeOut(){
		m_fadeModel.FadeExec ();

		if (m_fadeModel.GetState () == cFadeInOutModel.eFadeState.FadeOutStop) {
			++m_State;
		}
	}

	//終了処理
	private void End(){
		m_RetScene = cGameSceneManager.eGameScene.GameScene_Game;

		m_selModel.Init ();

		m_State = eTitleState.TitleState_Init;
	}
}
