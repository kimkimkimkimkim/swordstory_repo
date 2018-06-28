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
	public GameObject battleSceneManager; //BattleSceneKimManager

	//メンバ変数
	private Vector3 touchStartPos; //タップし始めの位置
	private Vector3 touchEndPos;   //離した位置
	private float dig; //フリックの角度
	private float seconds = 0; //経過時間（秒）
	private float maxTime = 8; //攻撃可能時間

	void Update()
	{
		//seconds += Time.deltaTime;
		Flick ();
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

		if (Mathf.Abs(directionX) > 30 || Mathf.Abs(directionY) > 30) {
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

		//必殺技ゲージ上昇
		battleSceneManager.GetComponent<BattleSceneKimManager>().AscentSpecialMoveGauge(0.05f);

		//敵にダメージを与える
		battleSceneManager.GetComponent<BattleSceneKimManager> ().EnemyReceiveAttack (0.02f);

	}
}
