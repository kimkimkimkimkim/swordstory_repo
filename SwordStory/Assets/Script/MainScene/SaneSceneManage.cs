using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaneSceneManage : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	public void OnClick (){
		SceneManager.LoadScene("BattleScene");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
