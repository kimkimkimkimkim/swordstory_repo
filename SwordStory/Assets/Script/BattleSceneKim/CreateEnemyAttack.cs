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
	public GameObject enemyAttackPatternCollection; //EnemyAttackPatternCollection

	//グローバル変数
	public float timeOut; //敵の攻撃の時間間隔
	public bool enableAttack = true; //攻撃ターンかどうか

	//メンバ変数
	private float timeElapsed; //時間を蓄積させる
	private int counterAttack = 0; //攻撃した回数
	private int maxAttackCount = 25; //休憩に移るまでの攻撃回数


	// Use this for initialization
	void Start () {
		
	}

	void OnEnable(){
		
	}

	//敵の攻撃を生成
	public void GenerateEnemyAttack(List<EnemyAttackPattern> list){

		//変数宣言
		int listLen = list.Count;

		//技を生成
		for (int i = 0; i < listLen; i++) {
			Debug.Log ("list[" + i + "].timing = " + list[i].timing + " , " 
				+ "list[" + i + "].attackType = " + list[i].attackType);
			StartCoroutine (ChoiceEnemyAttack (list[i].timing, list[i].attackType));
		}

	}

	/*
	void Update() {

		if (enableAttack) {
			timeElapsed += Time.deltaTime;
		}

		//指定した間隔（秒）で敵の攻撃生成
		if(timeElapsed >= timeOut) {
			//EnemyAttack();
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
	*/


	/*
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
	*/

	//攻撃を生成
	IEnumerator ChoiceEnemyAttack(float delay, int num)
	{
		//delay秒待つ
		yield return new WaitForSeconds(delay);
		/*処理*/
		switch (num) {
		case 1:
			enemyAttackPatternCollection.GetComponent<EnemyAttackPatternCollection> ().Vertical1 ();
			break;
		case 2:
			enemyAttackPatternCollection.GetComponent<EnemyAttackPatternCollection>().Vertical2();
			break;
		case 3:
			enemyAttackPatternCollection.GetComponent<EnemyAttackPatternCollection>().Vertical3();
			break;
		case 4:
			enemyAttackPatternCollection.GetComponent<EnemyAttackPatternCollection>().Horizontal1();
			break;
		case 5:
			enemyAttackPatternCollection.GetComponent<EnemyAttackPatternCollection>().Horizontal2();
			break;
		case 6:
			enemyAttackPatternCollection.GetComponent<EnemyAttackPatternCollection>().Horizontal3();
			break;
		case 7:
			enemyAttackPatternCollection.GetComponent<EnemyAttackPatternCollection>().BackSlash();
			break;
		case 8:
			enemyAttackPatternCollection.GetComponent<EnemyAttackPatternCollection>().Slash();
			break;
		default:
			break;
		}
	}


}
