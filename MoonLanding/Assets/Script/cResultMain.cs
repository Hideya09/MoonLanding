using UnityEngine;
using System.Collections;

public class cResultMain : cMain {

	//リザルトシーンのステート
	public enum eResultState{
		ResultState_Init,
		ResultState_FadeIn,
		ResultState_Main,
		ResultState_Comparison,
		ResultState_FadeOut,
		ResultState_End
	}

	public cSceneChangeModel m_scModel;
	public cFadeInOutModel m_fadeModel;
	public cResultScoreModel m_rsModel;

	private eResultState m_State;

	private cGameSceneManager.eGameScene m_RetScene;

	//初期処理
	public void OnEnable(){
		m_State = eResultState.ResultState_Init;

		m_RetScene = cGameSceneManager.eGameScene.GameScene_Result;

		m_scModel.Init ();
	}

	//リザルトシーン時のステート管理
	public override cGameSceneManager.eGameScene State(){

		m_RetScene = cGameSceneManager.eGameScene.GameScene_Result;

		switch (m_State) {
		case eResultState.ResultState_Init:
			Init ();
			break;
		case eResultState.ResultState_FadeIn:
			FadeIn ();
			break;
		case eResultState.ResultState_Main:
			Main ();
			break;
		case eResultState.ResultState_Comparison:
			Comparison ();
			break;
		case eResultState.ResultState_FadeOut:
			FadeOut ();
			break;
		case eResultState.ResultState_End:
			End ();
			break;
		}

		return m_RetScene;
	}

	private void Init(){
		m_rsModel.Init ();
		++m_State;
	}

	//フェードイン
	private void FadeIn(){
		m_fadeModel.FadeExec ();

		if (m_fadeModel.GetState () == cFadeInOutModel.eFadeState.FadeInStop) {
			m_scModel.Init ();

			++m_State;
		}
	}

	//メインループ
	private void Main(){
		m_scModel.FadeFont ();

		if (m_scModel.GetPush ()) {
			++m_State;
		}
	}

	private void Comparison(){
		m_rsModel.SetViewComparison ();
		m_scModel.FadeFont ();

		if (m_scModel.GetPush ()) {
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
		m_RetScene = cGameSceneManager.eGameScene.GameScene_Title;

		m_scModel.Init ();

		m_State = eResultState.ResultState_Init;
	}
}
