using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonScriptInBox : MonoBehaviour {

	//オブジェクト参照
	public GameObject canvasUI; //UIキャンバス
	public GameObject canvasBox; //ボックスキャンバス

	//UIキャンバスを表示,ボックスキャンバスを非表示
	public void OnClick(){
		canvasUI.SetActive (true);
		canvasBox.SetActive (false);
	}
}
