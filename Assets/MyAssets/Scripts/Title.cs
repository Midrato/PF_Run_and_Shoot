using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.inst.nowSituation != GameManager.SITUATION.TITLE){   //タイトル画面でないなら非表示
            this.gameObject.SetActive(false);
        }
    }

    private void Update(){
        var platform = Application.platform;
        if(platform == RuntimePlatform.WindowsEditor || platform == RuntimePlatform.WindowsPlayer){
            if(Input.GetMouseButtonDown(0)){
                Debug.Log("タイトル画面タッチ");
                touchToIngame();
            }
        }else{
            if (Input.touchCount > 0){
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began) {
                    Debug.Log("タイトル画面タッチ");
                    touchToIngame();
                }
            }
        }
        
        
    }
    private void touchToIngame() {
        if(GameManager.inst.nowSituation == GameManager.SITUATION.TITLE){
            GameManager.inst.nowSituation = GameManager.SITUATION.INGAME;   //タッチしたならインゲームへ
            this.gameObject.SetActive(false);
        }
    }
}
