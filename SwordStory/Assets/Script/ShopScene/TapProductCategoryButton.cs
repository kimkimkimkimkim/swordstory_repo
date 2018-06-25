using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapProductCategoryButton : MonoBehaviour {

	//オブジェクト参照
	public GameObject containerButton; //商品カテゴリボタン
	public GameObject productSelectview; //商品選択画面

	//商品カテゴリボタンを非表示、商品選択画面を表示
	public void OnClick(){
		containerButton.SetActive (false);
		productSelectview.SetActive (true);
	}
}
