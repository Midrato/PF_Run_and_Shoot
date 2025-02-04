using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsController : MonoBehaviour
{
    private Animator anim;          //アタッチされるアニメーターへの参照

    private Rigidbody rb;

    public int having_balls = 1;  //現在のボールの所持数

    private PlayerController plControl;
    [SerializeField] private StackBall stBall;  //スタックボール内の関数を使用する

    private bool canGetBall = true;
    private bool canShoot = true;

    private void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        plControl = GetComponent<PlayerController>();
        for(int i = 0;i < having_balls;i++){
            stBall.AddBall();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("ball")){    //触れたものがボール
            if(canGetBall){         //ボール取得可なら
                GameManager.inst.GetEffect(other.transform.position, 0);    //ボールゲット演出
                Destroy(other.gameObject);
                having_balls++;    //持っているボールを一つ増やす
                stBall.AddBall();
                StartCoroutine(StopShoot(0.2f));    //僅かな時間シュート不可(左右擦り対策)
            }
            
        }else if(other.gameObject.CompareTag("basket")){    //触れたものがバスケットゴール
            if(having_balls >= 1 && canShoot){      //ボールを持っているかつ、シュート可なら
                //Destroy(other.gameObject);
                BasketPoint otherBasP= other.gameObject.GetComponent<BasketPoint>();
                if(!(otherBasP.isSetCondition && having_balls < otherBasP.needBalls)){   //条件付きならば、条件を満たしていない場合無視
                    otherBasP.Pointed();     //触れたオブジェクトのBasketPointの、ポイント取得処理関数を実行する
                    having_balls--;     //持っているボールを一つ無くす
                    anim.SetBool("shoot", true);        //シュートアニメーションを起動
                    stBall.RemoveBall();
                    StartCoroutine(StopGetBall(0.2f));  //僅かな時間ボール取得不可(左右擦り対策)
                }
            } 
        }
    }

    private IEnumerator StopGetBall(float stopTime){
        canGetBall = false;
        yield return new WaitForSeconds(stopTime);
        canGetBall = true;
    }
    private IEnumerator StopShoot(float stopTime){
        canShoot = false;
        yield return new WaitForSeconds(stopTime);
        canShoot = true;
    }

    public void EndShoot(){     //Shootアニメーションのアニメーションイベント用
        anim.SetBool("shoot", false);
    }
}
