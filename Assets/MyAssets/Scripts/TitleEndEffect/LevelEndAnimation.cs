using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //dotween使用

public class LevelEndAnimation : MonoBehaviour
{
    private Animator anim;          //アタッチされるアニメーターへの参照

    [SerializeField] private GameObject StackBall;   //頭の上のボールのオブジェクト

    [SerializeField] private GameObject winUIPanel;     //勝利時表示のUIパネル
    [SerializeField] private GameObject deathUIPanel;     //死時表示のUIパネル
    [SerializeField] private GameObject ProgressLevelUI;   //プログレスバー・レベルのUI

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update(){
        if(!anim.GetBool("run") && GameManager.inst.nowSituation == GameManager.SITUATION.INGAME){
            anim.SetBool("run", true);      //インゲームで走りでないなら走りにする
        }
    }

    public void Win(){
        StartCoroutine(_win());
    }

    public void Death(){
        StartCoroutine(_death());
    }

    private IEnumerator _win(){
        transform.DOMoveZ(transform.position.z + 4, 2f);
        yield return new WaitForSeconds(2f);
        anim.SetBool("victory", true);
        anim.SetBool("run", false);
        yield return new WaitForSeconds(1.4f);
        ProgressLevelUI.SetActive(false);   //プログレスバー非表示に
        GameManager.inst.PlaySE(2);     //勝利SEを鳴らす
        winUIPanel.SetActive(true);
    }

    private IEnumerator _death(){
        anim.SetBool("death", true);
        anim.SetBool("run", false);
        yield return new WaitForSeconds(0.2f);
        
        StackBall.transform.DOLocalJump(new Vector3(1f, 0f, -1f), jumpPower: 1f, numJumps: 2, duration: 1f);
        StackBall.transform.DORotate(new Vector3(90f, 0f, 90f), 1f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(1f);
        ProgressLevelUI.SetActive(false);   //プログレスバー非表示に
        GameManager.inst.PlaySE(3);     //死SEを鳴らす
        deathUIPanel.SetActive(true);
    }

    public void _GoNextLevel(){         //ゲームマネージャーの代わりにボタンに入れるメソッド
        GameManager.inst.GoNextLevel();
    }

    public void _Retry(){
        GameManager.inst.Retry();
    }
}
