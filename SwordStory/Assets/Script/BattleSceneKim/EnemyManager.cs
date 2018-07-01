using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	//メンバ変数
	private Vector3[] path = new Vector3[8];
	private Vector3 initMyPos; //最初の敵の座標
	private float factor; //動きの大きさの係数

	// Use this for initialization
	void Start () {
		initMyPos = this.gameObject.transform.position;
		factor = 0.3f;
		path[0] = new Vector3(initMyPos.x - 1f * factor,initMyPos.y - 1f * factor,initMyPos.z);
		path[1] = new Vector3(initMyPos.x - 2f * factor,initMyPos.y              ,initMyPos.z);
		path[2] = new Vector3(initMyPos.x - 1f * factor,initMyPos.y + 1f * factor,initMyPos.z);
		path[3] = new Vector3(initMyPos.x              ,initMyPos.y              ,initMyPos.z);
		path[4] = new Vector3(initMyPos.x + 1f * factor,initMyPos.y - 1f * factor,initMyPos.z);
		path[5] = new Vector3(initMyPos.x + 2f * factor,initMyPos.y              ,initMyPos.z);
		path[6] = new Vector3(initMyPos.x + 1f * factor,initMyPos.y + 1f * factor,initMyPos.z);
		path[7] = new Vector3(initMyPos.x              ,initMyPos.y              ,initMyPos.z);
		iTween.MoveTo(this.gameObject,iTween.Hash("path",path,"time",4,
			"easetype",iTween.EaseType.linear,"looptype",iTween.LoopType.loop));
	}

	//敵画像を初期位置に戻す
	public void RevertToInitPos(){
		this.gameObject.transform.position = initMyPos;
		//iTweenを止める
		iTween.Stop(this.gameObject);
	}

	//敵画像を動かす
	public void MoveEnemyImage(){
		iTween.MoveTo(this.gameObject,iTween.Hash("path",path,"time",4,
			"easetype",iTween.EaseType.linear,"looptype",iTween.LoopType.loop));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
