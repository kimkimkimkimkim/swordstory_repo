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
	public GameObject imageEnemy; //敵画像
	public GameObject imageSpecialMoveGauge; //必殺技ゲージ画像
	public Sprite[] spriteSpecialMoveGauge = new Sprite[2]; //必殺技ゲージが満タンになった時の画像
	public GameObject imageShield; //シールド画像
	public GameObject specialMoveManager; //SpecialMoveManager
	public ParameterTable parameterTable; //ScriptableObjectを継承したクラス

	//メンバ変数
	private float timeEnemyBreak = 10.0f; //敵の休憩時間
	private float timeElapsed = 0.0f; //時間を蓄積させる
	private bool isFinishedEnemyAttack = false; //敵の攻撃が終わったかどうか
	private bool isSpecialMove = false; //この攻撃で必殺技になるかどうか
	private int enemyHp = 100; //敵のHP
	private int playerHp = 10; //プレイヤーのHP
	private EnemyStatusData enemyStatusData; //敵のステータス情報が入ったクラス
	private int stageNum = 0;


	void Start(){
		//敵の生成
		EnemyInit(0);
	}

	/*
	void OnEnable(){
		timeElapsed = timeEnemyBreak;
	}

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
			imageEnemy.GetComponent<EnemyManager> ().MoveEnemyImage ();

		}
	}

	//敵の攻撃ターンが終わったら受け取る
	public void EndEnemyAttack(){
		isFinishedEnemyAttack = true;
		imageEnemy.GetComponent<EnemyManager> ().RevertToInitPos ();
	}
	*/

	//プレイヤーが攻撃を受ける
	public void PlayerReceiveAttack(int damage){
		//自分のHP減少
		Hashtable hash = new Hashtable () {
			{ "from", playerHp },
			{ "to", playerHp - damage },
			{ "time", 1f },
			{ "easeType",iTween.EaseType.easeOutCubic },
			{ "loopType",iTween.LoopType.none },
			{ "onupdate", "OnUpdateMyHp" },
			{ "onupdatetarget", gameObject },
		};
		iTween.ValueTo (sliderEnemyHp, hash);
		playerHp -= damage;
		Debug.Log (playerHp);
		if (playerHp <= 0) {
			//負け処理
			sliderMyHp.GetComponent<Slider> ().value = 0;
			this.gameObject.SetActive (false);
			createEnemyAttack.SetActive (false);
			defenseManager.SetActive (false);
			myAttack.SetActive (false);
			imageShield.SetActive (false);

			textWinOrLoss.GetComponent<Text> ().text = "Lose...";
			textWinOrLoss.GetComponent<Text> ().color = Color.blue;
			imageEnemy.GetComponent<EnemyManager> ().RevertToInitPos ();

		}
	}

	//敵にダメージを与える
	public void EnemyReceiveAttack(int damage){
		if (isSpecialMove) {
			//必殺技発動
			specialMoveManager.SetActive(true);
			createEnemyAttack.SetActive (false);
			defenseManager.SetActive (false);
			myAttack.SetActive (false);
			this.gameObject.SetActive (false);

			isSpecialMove = false;

			imageSpecialMoveGauge.GetComponent<Image> ().fillAmount = 0.0f;
			imageSpecialMoveGauge.GetComponent<Image>().sprite = spriteSpecialMoveGauge[0];
		} else {
			//敵のHP減少
			Hashtable hash = new Hashtable(){
				{"from", enemyHp},
				{"to", enemyHp - damage},
				{"time", 1f},
				{"easeType",iTween.EaseType.easeOutCubic},
				{"loopType",iTween.LoopType.none},
				{"onupdate", "OnUpdateEnemyHp"},
				{"onupdatetarget", gameObject},
			};
			enemyHp -= damage;
			if (enemyHp > 0) {
				iTween.ValueTo (sliderEnemyHp, hash);
			}

		}

		if (enemyHp <= 0) {

			sliderEnemyHp.GetComponent<Slider> ().value = 0;

			if (stageNum != 2) {

				stageNum++;
				EnemyInit (stageNum);

			} else {

				//勝ち処理
				this.gameObject.SetActive (false);
				createEnemyAttack.SetActive (false);
				defenseManager.SetActive (false);
				myAttack.SetActive (false);
				imageShield.SetActive (false);

				textWinOrLoss.GetComponent<Text> ().text = "WIN!!";
				textWinOrLoss.GetComponent<Text> ().color = Color.red;
				imageEnemy.GetComponent<EnemyManager> ().RevertToInitPos ();

			}
			
		}

	}

	//必殺技ゲージ上昇
	public void AscentSpecialMoveGauge(float quantity){
		//必殺技ゲージ上昇
		imageSpecialMoveGauge.GetComponent<Image> ().fillAmount += quantity;

		if (imageSpecialMoveGauge.GetComponent<Image> ().fillAmount >= 1.0f) {
			//必殺技発動可能
			isSpecialMove = true;

			//必殺技ゲージの画像変更
			imageSpecialMoveGauge.GetComponent<Image>().sprite = spriteSpecialMoveGauge[1];

		}
	}

	//敵のHPを減少させる
	void OnUpdateEnemyHp(float value){
		sliderEnemyHp.GetComponent<Slider> ().value = value;
	}

	//自分のHPを減少させる
	void OnUpdateMyHp(float value){
		sliderMyHp.GetComponent<Slider> ().value = value;
	}

	//敵の初期設定を行う
	void EnemyInit(int num){

		//num番目のモンスター情報をenemyStatusData
		enemyStatusData = parameterTable.EnemyStatusList [num];

		//画像の設定
		imageEnemy.GetComponent<Image>().sprite = enemyStatusData.image; //画像を設定
		imageEnemy.GetComponent<RectTransform>().sizeDelta = enemyStatusData.size; //画像のサイズ
		imageEnemy.transform.localPosition = enemyStatusData.posi; //画像の位置

		//敵のステータスの設定
		enemyHp = enemyStatusData.hp; //HPを保存
		sliderEnemyHp.GetComponent<Slider>().maxValue = enemyStatusData.hp; //敵のHPスライダーの最大値を設定
		sliderEnemyHp.GetComponent<Slider>().value = enemyStatusData.hp; //敵のHPスライダーの値も変更


		//敵の攻撃を生成
		createEnemyAttack.GetComponent<CreateEnemyAttack>().GenerateEnemyAttack(enemyStatusData.enemyAttackPatternList);
	}

}
