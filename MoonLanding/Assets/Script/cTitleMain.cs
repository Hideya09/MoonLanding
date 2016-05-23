﻿using UnityEngine;
using System.Collections;

public class cTitleMain : cMain {

	public enum eTitleState{
		TitleState_FadeIn,
		TitleState_Main,
		TitleState_FadeOut,
		TitleState_End
	}

	public cSceneChangeModel m_scModel;
	public cFadeInOutModel m_fadeModel;

	private eTitleState m_State;

	private cGameSceneManager.eGameScene m_RetScene;

	public void OnEnable(){
		m_State = eTitleState.TitleState_FadeIn;

		m_RetScene = cGameSceneManager.eGameScene.GameScene_Title;

		m_scModel.Init ();
	}

	public override cGameSceneManager.eGameScene State(){

		m_RetScene = cGameSceneManager.eGameScene.GameScene_Title;

		switch (m_State) {
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
		m_RetScene = cGameSceneManager.eGameScene.GameScene_Game;

		m_scModel.Init ();

		m_State = eTitleState.TitleState_FadeIn;
	}
}