using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackPatternCollection : MonoBehaviour {

	//オブジェクト参照
	public GameObject canvas; //キャンバス
	public GameObject prefabEnemyAttack; //敵の攻撃オブジェクトプレハブ

	//メンバ変数
	private float timeElapsed = 0.0f; //時間を蓄積させる
	private Vector3 posTopLeft = new Vector3(-500f, 700f, 0f); //攻撃を表示する範囲の左上
	private Vector3 posTopRight = new Vector3(500f, 700f, 0f); //攻撃を表示する範囲の右上
	private Vector3 posBottomLeft = new Vector3(-500f, -600f, 0f); //攻撃を表示する範囲の左下
	private Vector3 posBottomRight = new Vector3(500f, -600f, 0f); //攻撃を表示する範囲の右下
	private float distanceSpan33 = 500f; //3×3の攻撃の時の距離間隔
	private float timeSpan33 = 0.25f; //3×3の攻撃の時の時間間隔
	private bool isCalled = false; //攻撃を生成し始めたかどうか

	//攻撃を生成
	IEnumerator CreateEnemyAttack(float delay, float x, float y){
		//delay秒待つ
		yield return new WaitForSeconds(delay);
		/*処理*/
		GameObject attack = (GameObject)Instantiate (prefabEnemyAttack);
		attack.transform.SetParent (canvas.transform, false);
		attack.transform.localPosition = new Vector3 (x, y, 0f);
		attack.transform.SetSiblingIndex (5);
	}


	/*
	 *  +..
	 *  +..
	 *  +..
	 */
	public void Vertical1(){
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 0, -distanceSpan33, distanceSpan33));
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 1, -distanceSpan33, 0f));
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 2, -distanceSpan33, -distanceSpan33));
	}

	/*
	 *  .+.
	 *  .+.
	 *  .+.
	 */
	public void Vertical2(){
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 0, 0f, distanceSpan33));
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 1, 0f, 0f));
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 2, 0f, -distanceSpan33));
	}

	/*
	 *  ..+
	 *  ..+
	 *  ..+
	 */
	public void Vertical3(){
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 0, distanceSpan33, distanceSpan33));
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 1, distanceSpan33, 0f));
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 2, distanceSpan33, -distanceSpan33));
	}

	/*
	 *  +++
	 *  ...
	 *  ...
	 */
	public void Horizontal1(){
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 0, -distanceSpan33, distanceSpan33));
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 1, 0f, distanceSpan33));
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 2, distanceSpan33, distanceSpan33));
	}

	/*
	 *  ...
	 *  +++
	 *  ...
	 */
	public void Horizontal2(){
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 0, -distanceSpan33, 0f));
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 1, 0f, 0f));
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 2, distanceSpan33, 0f));
	}

	/*
	 *  ...
	 *  ...
	 *  +++
	 */
	public void Horizontal3(){
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 0, -distanceSpan33, -distanceSpan33));
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 1, 0f, -distanceSpan33));
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 2, distanceSpan33, -distanceSpan33));
	}

	/*
	 *  +..
	 *  .+.
	 *  ..+
	 */
	public void BackSlash(){
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 0, -distanceSpan33, distanceSpan33));
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 1, 0f, 0f));
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 2, distanceSpan33, -distanceSpan33));
	}

	/*
	 *  ..+
	 *  .+.
	 *  +..
	 */
	public void Slash(){
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 0, distanceSpan33, distanceSpan33));
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 1, 0f, 0f));
		StartCoroutine (CreateEnemyAttack (timeSpan33 * 2, -distanceSpan33, -distanceSpan33));
	}



}
