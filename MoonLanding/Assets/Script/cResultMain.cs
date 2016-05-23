﻿using UnityEngine;
using System.Collections;

public class cResultMain : cMain {
	
	public enum eResultState{
		ResultState_FadeIn,
		ResultState_Main,
		ResultState_FadeOut,
		ResultState_End
	}

	public cSceneChangeModel m_scModel;
	public cFadeInOutModel m_fadeModel;

	private eResultState m_State;

	private cGameSceneManager.eGameScene m_RetScene;

	public void OnEnable(){
		m_State = eResultState.ResultState_FadeIn;

		m_RetScene = cGameSceneManager.eGameScene.GameScene_Result;

		m_scModel.Init ();
	}

	public override cGameSceneManager.eGameScene State(){

		m_RetScene = cGameSceneManager.eGameScene.GameScene_Result;

		switch (m_State) {
		case eResultState.ResultState_FadeIn:
			FadeIn ();
			break;
		case eResultState.ResultState_Main:
			Main ();
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

	private void FadeIn(){
		m_fadeModel.FadeExec ();

		if (m_fadeModel.GetState () == cFadeInOutModel.eFadeState.FadeInStop) {
			m_scModel.Init ();

			++m_State;
		}
	}

	private void Main(){
		m_scModel.FadeFont ();

		if (m_scModel.GetPush ()) {
			++m_State;
		}
	}

	private void FadeOut(){
		m_fadeModel.FadeExec ();

		if (m_fadeModel.GetState () == cFadeInOutModel.eFadeState.FadeOutStop) {
			++m_State;
		}
	}

	private void End(){
		m_RetScene = cGameSceneManager.eGameScene.GameScene_Title;

		m_scModel.Init ();

		m_State = eResultState.ResultState_FadeIn;
	}
}