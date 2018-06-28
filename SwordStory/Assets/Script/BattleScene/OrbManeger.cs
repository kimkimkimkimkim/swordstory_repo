using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbManeger : MonoBehaviour {

	private GameObject gameManager;
	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("OrbManager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TouchOrb(){
		if(Input.GetMouseButton(0) == false){
			return;
		}
		Debug.Log("orbtouched");
		Destroy(this.gameObject);
	}
}
