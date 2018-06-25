using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour {

	//定数定義

	//オブジェクト参照
	public GameObject containerBack; //背景全体
	public GameObject imageChara; //キャラクター画像

	//メンバ変数


	//グローバル変数
	public float speed = 0.5f; //キャラの進む速度
	public float rangeWidthCondition = 50.0f; //キャラが動くための最低距離
	public Vector3 posMouseDown; //マウスを押した時の位置
	public Vector3 posMouse;	  //押している時のマウスの位置
	
	// Update is called once per frame
	void Update () {

		DetermineWheterMove ();
	}

	// クリックした位置と今の位置を取得して移動するか判定
	private void DetermineWheterMove(){
		//変数宣言
		float rangeWidth; 

		// クリック開始地点の座標取得
		if (Input.GetMouseButtonDown(0)){
			posMouseDown = Input.mousePosition;
		}
		// 押している時のマウスの座標取得
		if (Input.GetMouseButton (0)) {
			posMouse = Input.mousePosition;
		}

		rangeWidth = posMouse.x - posMouseDown.x;

		if(Mathf.Abs(rangeWidth) >= rangeWidthCondition){
			//変数宣言
			int direction = (int)(rangeWidth/Mathf.Abs(rangeWidth));

			//キャラが動く
			MoveCharacter(direction);
		}

	}

	//キャラクターと背景画像を動かす
	private void MoveCharacter(int direction){
		//変数宣言
		float posxBack = containerBack.transform.localPosition.x; //背景画像のx座標
		float posyBack = containerBack.transform.localPosition.y; //背景画像のy座標

		if (posxBack >= -1581 && posxBack <= 1581) {
			containerBack.transform.localPosition = new Vector3 (posxBack - (direction * speed), posyBack, 0.0f);

			//右移動か左移動かでキャラの画像変更
			if (direction > 0) {
				//右移動
				imageChara.GetComponent<Image> ().sprite = imageChara.GetComponent<CharacterManager> ().chara [2];
			} else {
				//左移動
				imageChara.GetComponent<Image> ().sprite = imageChara.GetComponent<CharacterManager> ().chara [0];
			}
		} else {
			if(posxBack < 0)containerBack.transform.localPosition = new Vector3 (-1581, posyBack, 0.0f);
			if(posxBack > 0)containerBack.transform.localPosition = new Vector3 (1581, posyBack, 0.0f);
		}
	}
}
