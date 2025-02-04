using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;  //dotween使用

public class EndEffect : MonoBehaviour
{
    [SerializeField] private GameObject endScore;   //最後のスコアを表示する部分のオブジェクト
    private TextMeshPro _endScore;

    [SerializeField] private GameObject fence;      //ゴール地点の柵

    private int cashEndScore = 0;

    private void Start() {
        _endScore = endScore.GetComponent<TextMeshPro>();
        cashEndScore = GameManager.inst.levelGen.Levels[GameManager.inst.now_level % GameManager.inst.levelGen.Levels.Count].needScore;   //必要スコアのキャッシュ
        
        //最終レベル複製の時、最終レベルの必要スコアに固定
        if(GameManager.inst.reproductionLevel){cashEndScore = GameManager.inst.levelGen.Levels[GameManager.inst.levelGen.Levels.Count -1].needScore;}
        // ゴール地点のテキストを今レベルの必要スコアに変更
        _endScore.text = cashEndScore.ToString();
        endScore.transform.rotation = Quaternion.Euler(20, 0, -3);
        endScore.transform.DORotate(new Vector3(0, 0, 3), 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }


    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){  //プレイヤーが触れたなら
            LevelEndAnimation _playerAnim = other.gameObject.GetComponent<LevelEndAnimation>();  //プレイヤーのゴール演出
            if(GameManager.inst.level_score >= cashEndScore){
                Debug.Log("win");
                GameManager.inst.nowSituation = GameManager.SITUATION.WIN;  //勝利モードへ
                _playerAnim.Win();

                endScore.transform.DOKill();    //アニメーション停止
                Destroy(endScore);     //文字破壊
                fence.transform.DORotate(new Vector3(-90,180,0), 1.5f).SetEase(Ease.OutBounce);
            }else{
                Debug.Log("Lose");
                GameManager.inst.nowSituation = GameManager.SITUATION.DEATH;  //死モードへ
                _playerAnim.Death();
            }
        }
    }
}
