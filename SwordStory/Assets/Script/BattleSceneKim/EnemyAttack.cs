using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour {

	//メンバ変数
	private GameObject imageDamage; //ダメージを食らった時に表示する赤い画像
	private Animation anim; //ダメージを食らった時のアニメーショ
	private float attackDicisionOn = 0.8f; //ColliderがOnし始めるScale
	private float scaleMax = 1.0f;   //敵の攻撃の最大の大きさ
	private GameObject sliderMyHp; //自分のHPスライダー
	private GameObject textWinOrLoss; //勝敗テキスト
	GameObject eneata;
	GameObject defense;
	GameObject ata;


	// Use this for initialization
	void Start () {

		eneata = GameObject.Find ("CreateEnemyAttack");
		defense = GameObject.Find ("DefenseManager");
		//ata = GameObject.Find ("MyAttack");
		textWinOrLoss = GameObject.Find ("TextWinOrLoss");
		sliderMyHp = GameObject.Find ("SliderMyHP");
		imageDamage = GameObject.Find ("ImageDamageRed");
		anim = imageDamage.GetComponent<Animation> ();

		//徐々に大きくしていき終わったらColliderOnを呼ぶ
		iTween.ScaleTo (this.gameObject, iTween.Hash ("x", attackDicisionOn , "y",attackDicisionOn, "easetype", iTween.EaseType.linear,"time",0.4
			,"oncomplete", "AttackDicisionOn","oncompletetarget", this.gameObject));
	}


	//敵の攻撃判定が開始する
	void AttackDicisionOn() {

		this.GetComponent<CircleCollider2D> ().enabled = true;
		this.GetComponent<Image> ().color = Color.red;

		//徐々に大きくしていき終わったらAnimationEndを呼ぶ
		iTween.ScaleTo (this.gameObject, iTween.Hash ("x", scaleMax , "y",scaleMax, "easetype", iTween.EaseType.linear,"time",0.1
			,"oncomplete", "AnimationEnd","oncompletetarget", this.gameObject));

	}

	//シールド成功した時
	void OnCollisionEnter2D (Collision2D collision) {
		Debug.Log ("守備成功");
		//守備成功の処理
		Destroy(this.gameObject);
	}


	//敵の攻撃判定
	void AnimationEnd(){
		Debug.Log ("敵の攻撃成功");
		//攻撃を食らう処理
		anim.Play();
		Destroy(this.gameObject);

		//自分のHP減少
		sliderMyHp.GetComponent<Slider> ().value -= 0.1f;

		if (sliderMyHp.GetComponent<Slider> ().value <= 0) {
			//負け処理
			eneata.SetActive(false);
			defense.SetActive (false);
			//ata.SetActive (false);

			//textWinOrLoss.SetActive (true);
			textWinOrLoss.GetComponent<Text>().text = "Lose...";
			textWinOrLoss.GetComponent<Text> ().color = Color.blue;

		}
	}
}
