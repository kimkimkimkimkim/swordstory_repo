using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenStageSelectViewScript : MonoBehaviour {

	//オブジェクト参照
	public GameObject prefabView; //ステージセレクトビュープレハブ
	public GameObject canvasUI; //UIキャンバス

	//ステージセレクトビューを表示
	public void OnClick(){
		GameObject view = (GameObject)Instantiate (prefabView);
		view.transform.SetParent (canvasUI.transform, false);
	}
}
