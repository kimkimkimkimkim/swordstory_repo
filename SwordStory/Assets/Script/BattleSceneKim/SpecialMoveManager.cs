using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialMoveManager : MonoBehaviour {

	//オブジェクト参照
	public GameObject containerOrbImage; //オーブ画像コンテナ
	public Sprite[] spriteOrb = new Sprite[9]; //オーブの画像
	public GameObject imageTiming; //タイミング画像
	public GameObject imageBack; //背景画像
	public GameObject textSpecialMove; //必殺技テキスト
	public GameObject imageEnemy; //敵画像
	public GameObject imageBackSpecialMove; //必殺技の時の背景
	public GameObject textBaku;
	public GameObject textRetsu;
	public GameObject textGiri;
	public GameObject battleSceneManager; //BattleSceneKimMabnager
	public GameObject createEnemyAttack;
	public GameObject defenseManager;
	public GameObject myAttack;

	//メンバ変数
	private Vector3 touchStartPos; //タップし始めの位置
	private Vector3 touchEndPos;   //離した位置
	private float dig; //フリックの角度
	private int numOrbImage; //オーブ画像の番号
	private float speedTimingImage = 0.06f; //タイミング画像の移動速度
	private Animation animSpecialMove; //必殺技アニメーション
	private float timeElapsed = 0.0f; //時間を蓄積させる
	private Vector3 initialTimingImagePos; //タイミング画像の最初のx座標

	//グローバル変数
	public bool isCollision; //オーブとタイミング画像が衝突しているかどうか
	public GameObject whichOrb; //衝突しているオーブ

	// Use this for initialization
	void Start () {
		initialTimingImagePos = imageTiming.transform.localPosition;
		animSpecialMove = imageEnemy.GetComponent<Animation> ();
		imageBack.SetActive (false);
		containerOrbImage.SetActive (true);
		imageTiming.SetActive (true);
	}

	void OnEnable(){
		imageBack.SetActive (false);
		containerOrbImage.SetActive (true);
		imageTiming.SetActive (true);
		for (int i = 0; i < 5; i++) {
			containerOrbImage.transform.GetChild (i).GetComponent<Image> ().sprite = spriteOrb [0];
		}
	}
	
	// Update is called once per frame
	void Update () {
		//フリック感知
		Flick ();

		//タイミング画像の移動
		imageTiming.transform.position += new Vector3(speedTimingImage, 0.0f);
		if(imageTiming.transform.position.x >= 3.0f){
			Debug.Log ("コマンド終了");
			//必殺技コマンド終了
			timeElapsed += Time.deltaTime;
			//背景を黒に変更
			imageBackSpecialMove.GetComponent<Image>().color = Color.black;
			//爆表示
			if (timeElapsed >= 1.0f) {
				textBaku.SetActive (true);
			}
			//烈表示
			if (timeElapsed >= 2.0f) {
				textRetsu.SetActive (true);
			}
			//斬表示
			if (timeElapsed >= 3.0f) {
				textGiri.SetActive (true);
			}
			//爆発アニメーション
			if (timeElapsed >= 4.0f) {
				animSpecialMove.Play ();
			}
			//必殺技アニメーション終了
			if (timeElapsed >= 6.0f) {
				timeElapsed = 0;
				imageBack.SetActive (true);
				textBaku.SetActive (false);
				textRetsu.SetActive (false);
				textGiri.SetActive (false);
				imageBackSpecialMove.GetComponent<Image> ().color = new Color (47.0f / 255f, 117f / 255f, 214f / 255f, 1f);
				containerOrbImage.SetActive (false);
				imageTiming.transform.localPosition = initialTimingImagePos;
				imageTiming.SetActive (false);

				battleSceneManager.SetActive (true);
				createEnemyAttack.SetActive (true);
				myAttack.SetActive (true);
				defenseManager.SetActive (true);
				this.gameObject.SetActive (false);


				//ダメージ計算
				battleSceneManager.GetComponent<BattleSceneKimManager>().EnemyReceiveAttack(0.1f);

			}

		}

	}

	void Flick(){
		if (Input.GetMouseButtonDown(0)){
			touchStartPos = new Vector3(Input.mousePosition.x,
				Input.mousePosition.y,
				Input.mousePosition.z);
		}

		if (Input.GetMouseButtonUp(0)){
			touchEndPos = new Vector3(Input.mousePosition.x,
				Input.mousePosition.y,
				Input.mousePosition.z);
			GetDirection();
		}
	}

	void GetDirection(){

		float directionX = touchEndPos.x - touchStartPos.x;
		float directionY = touchEndPos.y - touchStartPos.y;

		float rad = Mathf.Atan2 (directionY, directionX);
		dig = rad * Mathf.Rad2Deg;


		if (dig >= 0 && dig < 22.5) {
			//右
			numOrbImage = 1;
		}else if (dig >= 22.5 && dig < 67.5) {
			//右上
			numOrbImage = 2;
		}else if (dig >= 67.5 && dig < 112.5) {
			//上
			numOrbImage = 3;
		}else if (dig >= 112.5 && dig < 157.5) {
			//左上
			numOrbImage = 4;
		}else if (dig >= 157.5 && dig <= 180) {
			//左
			numOrbImage = 5;
		}else if (dig >= -22.5 && dig < 0) {
			//右
			numOrbImage = 1;
		}else if (dig >= -67.5 && dig < -22.5) {
			//右下
			numOrbImage = 8;
		}else if (dig >= -112.5 && dig < -67.5) {
			//下
			numOrbImage = 7;
		}else if (dig >= -157.5 && dig < -112.5) {
			//左下
			numOrbImage = 6;
		}else if (dig > -180 && dig < -157.5) {
			//左
			numOrbImage = 5;
		}

		if (Mathf.Abs(directionX) > 30 || Mathf.Abs(directionY) > 30) {
			if (isCollision) {
				whichOrb.GetComponent<Image>().sprite = spriteOrb [numOrbImage];
			}
		}


	}
}
