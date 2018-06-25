using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseStageSelectViewScript : MonoBehaviour {

	//オブジェクト参照
	public GameObject prefabView; //ステージセレクトビュープレハブ

	//ステージセレクトビューを閉じる
	public void OnClick(){
		Destroy (prefabView);
	}
}
