using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	//メンバ変数
	private float attackDicisionOn = 0.8f; //ColliderがOnし始めるScale
	private float scaleMax = 1.0f;   //敵の攻撃の最大の大きさ

	// Use this for initialization
	void Start () {
		//徐々に大きくしていき終わったらColliderOnを呼ぶ
		iTween.ScaleTo (this.gameObject, iTween.Hash ("x", attackDicisionOn , "y",attackDicisionOn, "easetype", iTween.EaseType.linear,"time",3
			,"oncomplete", "AttackDicisionOn","oncompletetarget", this.gameObject));
	}

	//敵の攻撃判定が開始する
	void AttackDicisionOn() {

		this.GetComponent<CircleCollider2D> ().enabled = true;

		//徐々に大きくしていき終わったらAnimationEndを呼ぶ
		iTween.ScaleTo (this.gameObject, iTween.Hash ("x", scaleMax , "y",scaleMax, "easetype", iTween.EaseType.linear,"time",0.8
			,"oncomplete", "AnimationEnd","oncompletetarget", this.gameObject));

	}

	//シールド成功した時
	void OnCllisionEnter(Collision collision) {
		Debug.Log ("衝突");
		if (Input.GetMouseButton (0)) {
			Destroy (this.gameObject);
			Debug.Log ("守備成功");
		}
	}


	//敵の攻撃判定
	void AnimationEnd(){
		Debug.Log ("敵の攻撃成功");
	}
}
