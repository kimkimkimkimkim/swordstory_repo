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
	public ParameterTable parameterTable; //ParameterTable
	public GameObject textEnemyName; //勝負開始の時に最初に敵の名前を表示するテキスト
	public GameObject[] imagePillar = new GameObject[10]; //脇の柱

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
		//EnemyInit(0);

		//goForward ();
	}

	void Update(){
		if (Input.GetKey (KeyCode.Space)) {
			goForward();
		}
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

			//スライダーの値を0にしてactiveをfalseにする
			sliderEnemyHp.GetComponent<Slider> ().value = 0;
			sliderEnemyHp.SetActive (false);

			if (stageNum != 2) {

				stageNum++;
				goForward ();
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

		//enemyNameテキストを表示
		textEnemyName.GetComponent<Text>().text = "-VS-\n" + enemyStatusData.name;
		textEnemyName.SetActive(true);

		//画像の設定
		imageEnemy.GetComponent<Image>().sprite = enemyStatusData.image; //画像を設定
		imageEnemy.GetComponent<RectTransform>().sizeDelta = enemyStatusData.size; //画像のサイズ
		imageEnemy.transform.localPosition = enemyStatusData.posi; //画像の位置

		//敵のステータスの設定
		enemyHp = enemyStatusData.hp; //HPを保存
		sliderEnemyHp.SetActive(true); //activeをtrueに
		iTween.ValueTo (sliderEnemyHp, iTween.Hash("from",0,"to",enemyStatusData.hp,"time",2,
			"onupdate","OnUpdateEnemyHp","onupdatetarget", gameObject,"easeType",iTween.EaseType.linear));
		sliderEnemyHp.GetComponent<Slider>().maxValue = enemyStatusData.hp; //敵のHPスライダーの最大値を設定


		//delay秒後にStartBattleを呼ぶ
		StartCoroutine (StartBattle (2, num));

	}

	/// <summary>
	/// 敵の攻撃が始まる
	/// </summary>
	/// <param name="delay">何秒遅延させるか</param>
	/// <param name="num">EnemyStatudDataの何番目のキャラか</param>
	IEnumerator StartBattle(float delay, int num){

		//delay秒待つ
		yield return new WaitForSeconds(delay);

		/*処理*/
		//攻撃生成
		createEnemyAttack.GetComponent<CreateEnemyAttack>().GenerateEnemyAttack(enemyStatusData.enemyAttackPatternList);

		//textEnemyNameを非表示に
		textEnemyName.SetActive(false);

	}

	void goForward(){

		for (int i = 0; i < 10; i++) {

			//変数宣言
			float posX = imagePillar [i].transform.localPosition.x;
			float posY = imagePillar [i].transform.localPosition.x;
			float width = imagePillar [i].GetComponent<RectTransform> ().sizeDelta.x;
			float height = imagePillar [i].GetComponent<RectTransform> ().sizeDelta.y;
			float time = 3f;


			switch ((int)posX) {
			case -368:
				iTween.MoveTo (imagePillar [i].gameObject, 
					iTween.Hash ("position", new Vector3 (-450.0f, 515.0f, 0.0f),
						"isLocal",true,"time",time,"EaseType",iTween.EaseType.linear));
				iTween.ScaleTo (imagePillar [i].gameObject,
					iTween.Hash ("x", 0.34, "y", 0.34,"time",time,"EaseType",iTween.EaseType.linear));
				break;
			case 368:
				iTween.MoveTo (imagePillar [i].gameObject, 
					iTween.Hash ("position", new Vector3 (450.0f, 515.0f, 0.0f),"isLocal",true,"time",time,"EaseType",iTween.EaseType.linear));
				iTween.ScaleTo (imagePillar [i].gameObject,
					iTween.Hash ("x", 0.34, "y", 0.34,"time",time,"EaseType",iTween.EaseType.linear));
				break;
			case -450:
				iTween.MoveTo (imagePillar [i].gameObject, 
					iTween.Hash ("position", new Vector3 (-551.0f, 272.0f, 0.0f),"isLocal",true,"time",time,"EaseType",iTween.EaseType.linear));
				iTween.ScaleTo (imagePillar [i].gameObject,
					iTween.Hash ("x", 0.7, "y", 0.7,"time",time,"EaseType",iTween.EaseType.linear));
				break;
			case 450:
				iTween.MoveTo (imagePillar [i].gameObject, 
					iTween.Hash ("position", new Vector3 (551.0f, 272.0f, 0.0f),"isLocal",true,"time",time,"EaseType",iTween.EaseType.linear));
				iTween.ScaleTo (imagePillar [i].gameObject,
					iTween.Hash ("x", 0.7, "y", 0.7,"time",time,"EaseType",iTween.EaseType.linear));
				break;
			case -551:
				iTween.MoveTo (imagePillar [i].gameObject, 
					iTween.Hash ("position", new Vector3 (-735.0f, -46.0f, 0.0f),"isLocal",true,"time",time,"EaseType",iTween.EaseType.linear));
				iTween.ScaleTo (imagePillar [i].gameObject,
					iTween.Hash ("x", 1, "y", 1,"time",time,"EaseType",iTween.EaseType.linear));
				break;
			case 551:
				iTween.MoveTo (imagePillar [i].gameObject, 
					iTween.Hash ("position", new Vector3 (735.0f, -46.0f, 0.0f),"isLocal",true,"time",time,"EaseType",iTween.EaseType.linear));
				iTween.ScaleTo (imagePillar [i].gameObject,
					iTween.Hash ("x", 1, "y", 1,"time",time,"EaseType",iTween.EaseType.linear));
				break;
			case -735:
				iTween.MoveTo (imagePillar [i].gameObject, 
					iTween.Hash ("position", new Vector3 (-1063.0f, -527.0f, 0.0f),"isLocal",true,"time",time,"EaseType",iTween.EaseType.linear));
				iTween.ScaleTo (imagePillar [i].gameObject,
					iTween.Hash ("x", 1.4, "y", 1.4,"time",time,"EaseType",iTween.EaseType.linear));
				break;
			case 735:
				iTween.MoveTo (imagePillar [i].gameObject, 
					iTween.Hash ("position", new Vector3 (1063.0f, -527.0f, 0.0f),"isLocal",true,"time",time,"EaseType",iTween.EaseType.linear));
				iTween.ScaleTo (imagePillar [i].gameObject,
					iTween.Hash ("x", 1.4, "y", 1.4,"time",time,"EaseType",iTween.EaseType.linear));
				break;
			case -1063:
				imagePillar [i].transform.localPosition = new Vector3 (-368.0f, 667.0f, 0.0f);
				imagePillar [i].transform.localScale = new Vector3 (0.06f, 0.06f, 1f); 
				break;
			case 1063:
				imagePillar [i].transform.localPosition = new Vector3 (368.0f, 667.0f, 0.0f);
				imagePillar [i].transform.localScale = new Vector3 (0.06f, 0.06f, 1f); 
				break;
			}

		}




	}




}
