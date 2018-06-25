using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapBoxButtonScript : MonoBehaviour {

	//オブジェクト参照
	public GameObject canvasUI; //UIキャンバス
	public GameObject canvasBox; //ボックスキャンバス

	//UIキャンバスを非表示,ボックスキャンバスを表示
	public void OnClick(){
		canvasUI.SetActive (false);
		canvasBox.SetActive (true);
	}
}
