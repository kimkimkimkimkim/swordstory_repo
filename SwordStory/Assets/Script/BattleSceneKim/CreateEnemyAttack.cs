using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateEnemyAttack : MonoBehaviour {

	//オブジェクト参照
	public GameObject canvas; //キャンバス
	public GameObject prefabEnemyAttack; //敵の攻撃オブジェクトプレハブ
	public GameObject imageShield; //シールド
	public GameObject defenseManager; //ディフェンス
	public GameObject myAttack; //MyAttack
	public GameObject textComment; //コメントテキスト
	public GameObject battleSceneManager; //BattleSceneKimManager

	//グローバル変数
	public float timeOut; //敵の攻撃の時間間隔
	public bool enableAttack = true; //攻撃ターンかどうか

	//メンバ変数
	private float timeElapsed; //時間を蓄積させる
	private int counterAttack = 0; //攻撃した回数
	private int maxAttackCount = 25; //休憩に移るまでの攻撃回数

	// Use this for initialization
	void Start () {
		EnemyAttack ();
	}

	void OnEnable(){
		timeElapsed = 0;
	}

	void Update() {

		if (enableAttack) {
			timeElapsed += Time.deltaTime;
		}

		//指定した間隔（秒）で敵の攻撃生成
		if(timeElapsed >= timeOut) {
			// Do anything
			EnemyAttack();
			timeElapsed = 0.0f;
			counterAttack++;
		}

		//指定した回数攻撃したら敵が休憩
		if (counterAttack >= maxAttackCount) {
			//休憩に移る
			counterAttack = 0;
			imageShield.SetActive(false);

			//攻撃が終わったことを知らせる
			battleSceneManager.GetComponent<BattleSceneKimManager>().EndEnemyAttack();
			enableAttack = false;

			textComment.GetComponent<Text>().text = "敵が疲れている！\n今のうちに攻撃だ！";
		}

	}


	//敵の攻撃
	void EnemyAttack(){
		GameObject attack = (GameObject)Instantiate (prefabEnemyAttack);
		attack.transform.SetParent (canvas.transform, false);
		attack.transform.localPosition = new Vector3 (
			UnityEngine.Random.Range (-550.0f, 550.0f),
			UnityEngine.Random.Range (-350.0f, 800.0f),
			0f);
		attack.transform.SetSiblingIndex (4);
	}


}
