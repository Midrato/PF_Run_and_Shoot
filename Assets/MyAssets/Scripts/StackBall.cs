using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackBall : MonoBehaviour
{
    [SerializeField] private GameObject HeadStackBall;  //頭に乗せるボールのプレハブ
    private Stack<GameObject> balls = new Stack<GameObject>();    //ボールのスタック

    [SerializeField] private float yTrans;  //ボール間の変位

    public void AddBall(){
        var tmp = Instantiate(HeadStackBall, this.transform.position, Quaternion.identity);  //ボールを出現
        tmp.transform.SetParent(this.transform);    //自身を親に
        tmp.transform.localPosition = new Vector3(0f, yTrans*balls.Count, 0f);    //ボール数×変位だけyをずらす
        balls.Push(tmp);    //スタックにプッシュ

    }

    public void RemoveBall(){
        Destroy(balls.Pop());   //最後=最高点のボールを除去
    }
}
