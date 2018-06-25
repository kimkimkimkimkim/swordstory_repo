using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseProductSelectViewScript : MonoBehaviour {

	//オブジェクト参照
	public GameObject containerButton; //商品カテゴリボタン
	public GameObject productSelectView; //商品選択画面

	//商品選択画面を非表示、商品カテゴリ画面を表示
	public void OnClick(){
		containerButton.SetActive (true);
		productSelectView.SetActive (false);
	}

}
