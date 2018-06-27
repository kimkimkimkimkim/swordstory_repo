using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
using UnityEngine.UI;

public class BattleSceneManager : MonoBehaviour {

	// Use this for initialization
	public GameObject obj;
	public GameObject imageClear;
	public GameObject imageFail;
	private float life_time = 1.0f;
	public GameObject canvas;
	public GameObject ballEffectPrefab;
	private GameObject ballEffect;
	private float[] skillGauge; 

	public Vector2 startPos;
    public Vector2 direction;

    private Text m_Text;
    string message;

    float time = 0f;
	float scale = 0f;
	Slider _slider;
	float _hp = 1;
	int remainingHp = 3;
	GameObject[] tagObjects;
	GameObject[] fillHearts;

	// 処理が終わったどうかを示すフラグ
    bool iTweenMoving = false;

	void Start () {
		skillGauge = new float[4] {1,1,1,1};
		fillHearts = GameObject.FindGameObjectsWithTag("FillHearts");
		_slider = GameObject.Find("SliderEnemy").GetComponent<Slider>();
		obj.SetActive(false);
		imageClear.SetActive(false);
		imageFail.SetActive(false);
		Generation();
	}
	
	void OnCompleteHandler(){
        iTweenMoving = false;
    }

	void Generation(){
		ballEffect = (GameObject)Instantiate(ballEffectPrefab);
		ballEffect.transform.SetParent(canvas.transform, false);
		ballEffect.transform.SetSiblingIndex(2); 
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
			if(remainingHp > 0){
				Destroy(fillHearts[remainingHp-1]);
				remainingHp--;
			}
			if(remainingHp == 0 && _hp > 0){
				imageFail.SetActive(true);
			}
        }
    }

	public void OnBallClick () {
		Destroy(ballEffect);
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

        // Track a single touch as a direction control.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on TouchPhase
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    startPos = touch.position;
                    message = "Begun ";
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    // Determine direction by comparing the current touch position with the initial one
                    direction = touch.position - startPos;
                    message = "Moving ";
                    break;

                case TouchPhase.Ended:
                    // Report that the touch has ended when it ends
                    message = "Ending ";
                    break;
            }
					//Update the Text on the screen depending on current TouchPhase, and the current direction vector
       		m_Text.text = "Touch : " + message + "in direction" + direction;
			Debug.Log(m_Text);
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

		skillGauge[0] -= 0.1f;
		skillGauge[1] -= 0.1f;
		skillGauge[2] -= 0.1f;
		skillGauge[3] -= 0.1f;

		_hp -= 0.1f;

		obj.transform.eulerAngles = new Vector3(0,0,gesture.ScreenFlickVector.y);
		obj.SetActive(true);
		time = 0;
	}
}
