using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
using UnityEngine.UI;

public class BattleSceneManager : MonoBehaviour {

	// Use this for initialization
	public GameObject obj;
	public GameObject imageClear;
	private float life_time = 1.0f;
	public GameObject canvas;
	public GameObject ballEffectPrefab;
	private GameObject ballEffect;
    float time = 0f;
	float scale = 0f;
	Slider _slider;
	float _hp = 1;
	GameObject[] tagObjects;

	// 処理が終わったどうかを示すフラグ
    bool iTweenMoving = false;

	void Start () {
		_slider = GameObject.Find("SliderEnemy").GetComponent<Slider>();
		obj.SetActive(false);
		imageClear.SetActive(false);
		Generation();
	}
	
	void OnCompleteHandler(){
        iTweenMoving = false;
    }

	void Generation(){
		ballEffect = (GameObject)Instantiate(ballEffectPrefab);
		ballEffect.transform.SetParent(canvas.transform, false);
		ballEffect.transform.localPosition = new Vector3(
			UnityEngine.Random.Range(-300.0f,300.0f),
			UnityEngine.Random.Range(-300.0f,300.0f),
			0f);
		iTweenMoving = true;
		iTween.ScaleTo(ballEffect, iTween.Hash ("x", 3, "y", 3, "time", 5, "easeType", iTween.EaseType.linear, "oncomplete", "OnCompleteHandler","oncompletetarget",gameObject));

	}	// Update is called once per frame

	//シーン上のBallEffectタグが付いたオブジェクトを数える
    void Check(string tagname){
		if(!iTweenMoving){
			Destroy(ballEffect);
		}
        tagObjects = GameObject.FindGameObjectsWithTag(tagname);
        if(tagObjects.Length == 0){
			Generation();
        }
    }

	void Update () {
		_slider.value = _hp;
		time += Time.deltaTime;

		Check("BallEffect");
        if(time>life_time){
            obj.SetActive(false);
        }
		if(_hp <= 0){
			imageClear.SetActive(true);

		}

	}

	void OnEnable(){
    	// FlickGestureのdelegateに登録
    	GameObject.FindGameObjectWithTag("FullScreenLayer").GetComponent<FlickGesture>().Flicked += FlickedHandle;
	}

	void OnDisable(){
    	UnsubscribeEvent();
	}

	void OnDestroy(){
    	UnsubscribeEvent();
	}

	void UnsubscribeEvent(){
    	// 登録を解除
    	GameObject.FindGameObjectWithTag("FullScreenLayer").GetComponent<FlickGesture>().Flicked -= FlickedHandle;
	}

	void FlickedHandle(object sender, System.EventArgs e){
    	var gesture = sender as FlickGesture;
    	// ジェスチャが適切かチェック
    	if (gesture.State != FlickGesture.GestureState.Recognized)return;
    	// 処理したい内容
    	Debug.Log("Flicked");
		_hp -= 0.1f;        
		obj.SetActive(true);
		time = 0;
	}
}
