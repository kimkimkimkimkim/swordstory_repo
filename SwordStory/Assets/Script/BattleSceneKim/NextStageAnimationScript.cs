using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextStageAnimationScript : MonoBehaviour {

	//オブジェクト参照
	public GameObject imageQuestStart; //クエストスタートするときにクエスト名とか表示するビュー
	public GameObject textQuestName; //クエストネームテキスト
	public GameObject textQuestLevel; //クエストレベルテキスト
	public GameObject imageEnemy; //敵画像
	public GameObject sliderPlayerHp; //プレイヤーのHPスライダー
	public GameObject sliderEnemyHp; //敵のHPスライダー
	public GameObject textEnemyName; //敵の名前を表示するテキスト
	public GameObject textFight; //ファイトテキスト

	// Use this for initialization
	void Start () {

		//imageQuestStartをフェードアウト
		iTween.ValueTo (gameObject, iTween.Hash("from",1f,"to",0f,"time",2f,
			"easeType",iTween.EaseType.linear,"onupdate","FadeOut","onupdatetarget",gameObject,
			"oncomplete","Inactivate","oncompletetarget",gameObject));

		//HPスライダーを左からフレームイン
		iTween.MoveFrom(sliderPlayerHp, iTween.Hash("x",-15, "y",-3,"time",1f, 
			"delay", 0.5f,"easeType",iTween.EaseType.linear));

	}

	//敵出現アニメーション
	public void goNextStage(Vector2 size, Vector3 posi){

		imageEnemy.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 1f);
		imageEnemy.GetComponent<RectTransform>().sizeDelta = size; //画像のサイズ
		imageEnemy.transform.localPosition = posi; //画像の位置

		//敵を上から落としてくる
		iTween.MoveFrom(imageEnemy.gameObject, iTween.Hash("x",0, "y",4000,"time",1f, "isLocal",true,
			"delay", 0.5f,"easeType",iTween.EaseType.linear));

		//HPスライダーを左からフレームイン
		iTween.MoveFrom(sliderEnemyHp, iTween.Hash("x",-15, "y",3,"time",1f, 
			"delay", 0.5f,"easeType",iTween.EaseType.linear));

		//テキスト類のアニメーション
		Invoke("ShowEnemyNameText", 2.5f);


	}
	
	//フェードアウト処理
	void FadeOut(float alfa){
		imageQuestStart.GetComponent<Image>().color = new Color(0f, 0f, 0f, alfa);
		textQuestName.GetComponent<Text> ().color = new Color (1f, 1f, 1f, alfa);
		textQuestLevel.GetComponent<Text> ().color = new Color (1f, 1f, 1f, alfa);
	}

	//非アクティブにする
	void Inactivate(){
		imageQuestStart.SetActive (false);
	}

	//EnemyNameTextの表示
	void ShowEnemyNameText(){
		textEnemyName.SetActive (true);
		Invoke ("ShowFightText",1.5f);
	}

	//FightTextの表示
	void ShowFightText(){
		textFight.SetActive (true);
		textEnemyName.SetActive (false);
		Invoke ("BattleStart",1.5f);
	}

	//バトル開始
	void BattleStart(){
		textFight.SetActive (false);
	}

}
