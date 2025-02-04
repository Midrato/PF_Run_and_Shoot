using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BasketPoint : MonoBehaviour
{
    private int my_score = 3;
    [SerializeField] private TextMeshPro score_disp;
    [SerializeField] private GameObject efBall; //シュート演出用のボール
    [SerializeField] private int[] scoreRange = {2, 8};     //スコアの範囲
    
    

    
    [Header("ConditionSetting")]
    public bool isSetCondition = false;      //得点条件を設定する
    public int needBalls = 3;   //得点に必要な最低ボール数
    [SerializeField] private TextMeshPro needBall_disp;     //必要ボール数表示
    


    private void Start() {
        my_score = Random.Range(scoreRange[0], scoreRange[1] + 1);      //スコアを範囲内からランダムに決定
        score_disp.text = my_score.ToString();
        if(isSetCondition){
            needBall_disp.text = "×" + needBalls.ToString();    //必要ボール表示
        }
    }

    public void Pointed() {
        GameManager.inst.AddScore(my_score);    //スコアを加算
        GameManager.inst.GetEffect(new Vector3(this.transform.position.x, this.transform.position.y+2, this.transform.position.z), 1);  //バスケットの位置にParticle
        if(isSetCondition && needBalls >= 10){
            GameManager.inst.PlaySE(4);     //難易度の高いゴールはファンファーレ
        }
        efBall.SetActive(true);
        efBall.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 10f, 0f));
        Destroy(this.gameObject);
    }
}
