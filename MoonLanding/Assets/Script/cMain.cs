using UnityEngine;
using System.Collections;

//シーンの基底クラス
public abstract class cMain : ScriptableObject {

	public abstract cGameSceneManager.eGameScene State();
}
