using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour {

	//メンバ変数
	private GameObject battleSceneManager; //BattleSceneKimManager
	private GameObject imageDamage; //ダメージを食らった時に表示する赤い画像
	private Animation anim; //ダメージを食らった時のアニメーショ
	private float attackDicisionOn = 0.8f; //ColliderがOnし始めるScale
	private float scaleMax = 1.0f;   //敵の攻撃の最大の大きさ

	// Use this for initialization
	void Start () {

		battleSceneManager = GameObject.Find ("BattleSceneKimManager");
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
		//守備成功の処理
		Destroy(this.gameObject);

		//必殺技ゲージ上昇
		battleSceneManager.GetComponent<BattleSceneKimManager>().AscentSpecialMoveGauge(0.01f);
	}


	//敵の攻撃判定
	void AnimationEnd(){
		//攻撃を食らう処理
		anim.Play();
		Destroy(this.gameObject);
		battleSceneManager.GetComponent<BattleSceneKimManager> ().PlayerReceiveAttack (0.1f);
	}
}
