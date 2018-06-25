using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GoToShop : MonoBehaviour {

	//ショップシーンに移動
	public void OnClick(){
		SceneManager.LoadScene ("ShopScene");
	}
}
