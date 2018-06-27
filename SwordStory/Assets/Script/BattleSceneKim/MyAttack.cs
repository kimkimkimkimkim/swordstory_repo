using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyAttack : MonoBehaviour {

	//オブジェクト参照
	public GameObject canvas; //キャンバス
	public GameObject imageSlash; //スラッシュ画像
	public GameObject imageEnemy; //敵画像
	public GameObject imageDamage; //ダメージ画像
	public GameObject createEnemyAttack; //CreateEnemyAttack
	public GameObject defenseManager; //DefenseManager
	public GameObject sliderEnemyHp; //敵のHPスライダー
	public GameObject textWinOrLoss; //勝敗テキスト
	public GameObject textComment; //コメントテキスト

	//メンバ変数
	private Vector3 touchStartPos; //タップし始めの位置
	private Vector3 touchEndPos;   //離した位置
	private float dig; //フリックの角度
	private float seconds = 0; //経過時間（秒）
	private float maxTime = 8; //攻撃可能時間

	void Update()
	{
		seconds += Time.deltaTime;
		Flick ();
		Debug.Log(seconds);
		if (seconds >= maxTime) {
			//敵の攻撃に移る
			Debug.Log("敵のターン");
			seconds = 0;
			this.gameObject.SetActive (false);
			createEnemyAttack.SetActive (true);
			defenseManager.SetActive (true);

			textComment.GetComponent<Text> ().text = "敵の攻撃だ！\nシールドで防ぐんだ！";

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
		Debug.Log (dig);

		/*
		if (dig >= 0 && dig < 22.5) {
			//右
		}else if (dig >= 22.5 && dig < 67.5) {
			//右上
		}else if (dig >= 67.5 && dig < 112.5) {
			//上
		}else if (dig >= 112.5 && dig < 157.5) {
			//左上
		}else if (dig >= 157.5 && dig <= 180) {
			//左
		}else if (dig >= -22.5 && dig < 0) {
			//右
		}else if (dig >= -67.5 && dig < -22.5) {
			//右下
		}else if (dig >= -112.5 && dig < -67.5) {
			//下
		}else if (dig >= -157.5 && dig < -112.5) {
			//左下
		}else if (dig > -180 && dig < -157.5) {
			//左
		}
		*/
		if (directionX > 30 || directionY > 30) {
			Attack ();
		}

	}

	//攻撃
	void Attack(){
		RectTransform CanvasRect = canvas.GetComponent<RectTransform>();
		//マウスのポジション
		Vector3 worldStartPos = Camera.main.ScreenToWorldPoint(touchStartPos);
		Vector3 screenStartPos = Camera.main.WorldToViewportPoint (worldStartPos);
		Vector2 targetPosStart = new Vector2(
			((screenStartPos.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
			((screenStartPos.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));
		Vector3 worldEndPos = Camera.main.ScreenToWorldPoint(touchEndPos);
		Vector3 screenEndPos = Camera.main.WorldToViewportPoint (worldEndPos);
		Vector2 targetPosEnd = new Vector2(
			((screenEndPos.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
			((screenEndPos.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));


		GameObject slash = (GameObject)Instantiate (imageSlash);
		slash.transform.SetParent (canvas.transform, false);
		slash.transform.localPosition = new Vector3 (
			(targetPosEnd.x + targetPosStart.x)/2,
			(targetPosEnd.y + targetPosStart.y)/2,
			0f);
		slash.transform.Rotate (new Vector3 (0, 0, dig));
		slash.transform.SetSiblingIndex (5);

		//アニメーションの再生
		imageEnemy.GetComponent<Animation>().Play();
		imageDamage.GetComponent<Animation>().Play();

		//敵のHP減少
		sliderEnemyHp.GetComponent<Slider>().value -= 0.02f;

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
}
