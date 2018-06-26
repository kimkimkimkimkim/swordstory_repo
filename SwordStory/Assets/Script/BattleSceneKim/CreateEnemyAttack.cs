using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemyAttack : MonoBehaviour {

	//オブジェクト参照
	public GameObject canvas; //キャンバス
	public GameObject prefabEnemyAttack; //敵の攻撃オブジェクトプレハブ

	// Use this for initialization
	void Start () {
		GameObject attack = (GameObject)Instantiate (prefabEnemyAttack);
		attack.transform.SetParent (canvas.transform, false);
		attack.transform.localPosition = new Vector3 (
			UnityEngine.Random.Range (-300.0f, 300.0f),
			UnityEngine.Random.Range (-140.0f, 500.0f),
			0f);
		attack.transform.SetSiblingIndex (4);
	}

}
