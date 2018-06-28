using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneKimManager : MonoBehaviour {

	//オブジェクト参照
	public GameObject createEnemyAttack; //CreateEnemyAttack
	public GameObject sliderMyHp; //自分のHPバー
	public GameObject sliderEnemyHp; //敵のHPバー
	public GameObject textWinOrLoss; //勝敗テキスト
	public GameObject myAttack; //スクリプト
	public GameObject defenseManager; //スクリプト
	public GameObject imageSpecialMoveGauge; //必殺技ゲージ画像
	public GameObject specialMoveManager; //SpecialMoveManager

	//メンバ変数
	private float timeEnemyBreak = 10.0f; //敵の休憩時間
	private float timeElapsed = 0.0f; //時間を蓄積させる
	private bool isFinishedEnemyAttack = false; //敵の攻撃が終わったかどうか

	void Update(){
		//敵が休憩に入ったら時間計測開始
		if (isFinishedEnemyAttack) {
			timeElapsed += Time.deltaTime;
		}
		//指定した時間が経ったら敵の攻撃開始
		if (timeElapsed >= timeEnemyBreak) {
			Debug.Log ("敵のターン");
			timeElapsed = 0.0f;
			//敵の攻撃開始
			isFinishedEnemyAttack = false;
			createEnemyAttack.GetComponent<CreateEnemyAttack> ().enableAttack = true;


		}
	}

	//敵の攻撃ターンが終わったら受け取る
	public void EndEnemyAttack(){
		isFinishedEnemyAttack = true;
	}

	//プレイヤーが攻撃を受ける
	public void PlayerReceiveAttack(float damage){
		//自分のHP減少
		sliderMyHp.GetComponent<Slider> ().value -= damage;

		if (sliderMyHp.GetComponent<Slider> ().value <= 0) {
			//負け処理
			createEnemyAttack.SetActive(false);
			defenseManager.SetActive (false);
			myAttack.SetActive (false);

			textWinOrLoss.GetComponent<Text>().text = "Lose...";
			textWinOrLoss.GetComponent<Text> ().color = Color.blue;

		}
	}

	//敵にダメージを与える
	public void EnemyReceiveAttack(float damage){
		//敵のHP減少
		sliderEnemyHp.GetComponent<Slider>().value -= damage;

		if (sliderEnemyHp.GetComponent<Slider> ().value <= 0) {
			//勝ち処理
			this.gameObject.SetActive(false);
			createEnemyAttack.SetActive (false);
			defenseManager.SetActive (false);

			textWinOrLoss.SetActive (true);
			textWinOrLoss.GetComponent<Text>().text = "WIN!!";
			textWinOrLoss.GetComponent<Text> ().color = Color.red;
		}

	}

	//必殺技ゲージ上昇
	public void AscentSpecialMoveGauge(float quantity){
		//必殺技ゲージ上昇
		imageSpecialMoveGauge.GetComponent<Image> ().fillAmount += quantity;

		if (imageSpecialMoveGauge.GetComponent<Image> ().fillAmount >= 1.0f) {
			//必殺技発動
			specialMoveManager.SetActive(true);
			createEnemyAttack.SetActive (false);
			defenseManager.SetActive (false);
			myAttack.SetActive (false);
			this.gameObject.SetActive (false);

			imageSpecialMoveGauge.GetComponent<Image> ().fillAmount = 0.0f;
		}
	}

}
