using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PointerUpScript: MonoBehaviour {
	//オブジェクト参照
	public GameObject mainSceneManager; //メイン画面マネージャー
	public GameObject imageChara; //キャラクター画像

	//離した時の処理
	public void PointerUp(){
		//移動を止める
		mainSceneManager.GetComponent<MainSceneManager>().posMouse.x = 0;
		mainSceneManager.GetComponent<MainSceneManager>().posMouseDown.x = 0;

		imageChara.GetComponent<Image> ().sprite = imageChara.GetComponent<CharacterManager> ().chara [1];
	}
}
