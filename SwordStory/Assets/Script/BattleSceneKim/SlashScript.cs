using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashScript : MonoBehaviour {

	//グローバル変数
	public float disappearanceTime; //slashの消滅時間

	//
	void Start () {
		iTween.ScaleTo (gameObject, iTween.Hash ("x", 1, "y", 0, "time", disappearanceTime,"EaseType", iTween.EaseType.easeOutCubic
			,"oncomplete", "Delete","oncompletetarget", this.gameObject));
	}

	void Delete(){
		Destroy (this.gameObject);
	}
}
