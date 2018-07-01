using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgeCollisionTimingImage : MonoBehaviour {

	//オブジェクト参照
	public GameObject specialMoveManager; //SpecialMoveManager

	//オーブと当たった時
	void OnTriggerEnter2D(Collider2D col){
		//入ったことを伝える
		this.GetComponent<Image>().color = Color.green;
		specialMoveManager.GetComponent<SpecialMoveManager>().isCollision = true;
		specialMoveManager.GetComponent<SpecialMoveManager> ().whichOrb = col.gameObject;

	}

	//オーブとの接触が終わった時
	void OnTriggerExit2D(Collider2D col){
		//出たことを伝える
		this.GetComponent<Image>().color = Color.white;
		specialMoveManager.GetComponent<SpecialMoveManager>().isCollision = false;

	}

}
