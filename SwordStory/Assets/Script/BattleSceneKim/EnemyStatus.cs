using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu( menuName = "MyGame/Create ParameterTable",fileName = "ParameterTable")]
public class ParameterTable : ScriptableObject
{
	//ListステータスのList
	public List<EnemyStatusData> EnemyStatusList = new List<EnemyStatusData>();
}

[System.Serializable]
public class EnemyStatusData{

	//設定したいデータの変数
	public string name = "名前"; //モンスター名
	public Sprite image; //敵画像
	public int num; //図鑑ナンバー
	public int hp; //HP
	public Vector2 size; //画像サイズ
	public Vector3 posi; //画像の位置

}