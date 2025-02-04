using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    //各レベルの情報構成のクラス
    public List<GameObject> RoadPrefabs = new List<GameObject>(); //道のPrefab
    public List<GameObject> ItemSetPrefabs = new List<GameObject>();    //道中のアイテム配置のPrefab
    public int stage_Length_in_block = 20;      //最低速で40秒弱?のはず
    public GameObject endPoint;       //最終地点に出すオブジェクト
    public int needScore = 100;      //ボスを倒すのに必要なパワー
    public float boss_distance = 5.0f;   //最後の足場からボスまでの距離
}
