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
	private int stagenum; //何番目のステージか


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
			if (i == listLen - 1) {
				//最後の技の時
				Invoke("ComeToRestTime",0.5f);

			}
		}

	}

	void ComeToRestTime(){
		battleSceneManager.GetComponent<BattleSceneKimManager> ().intoRestTime (stagenum);
	}

	//攻撃を生成
	IEnumerator ChoiceEnemyAttack(float delay, int num)
	{
		//delay秒待つ
		yield return new WaitForSeconds(delay);
		/*処理*/

		stagenum = num;

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
