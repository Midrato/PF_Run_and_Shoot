using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;  //dotween使用


public class GameManager : MonoBehaviour
{
    public static GameManager inst = null;      //どこからでもアクセスできる唯一のゲームマネージャーを宣言
    
    void Awake()
    {
        if(inst == null){   //一つ目の物だったら保持、破壊されないようにする
            inst = this;
            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this);      //複数ゲームマネージャーが出ようとしたら破壊する
        }
    }

    public enum SITUATION{      //クリア状況の列挙体
        TITLE,
        INGAME,
        WIN,
        DEATH
    }

    public int now_level = 0;   //現在のレベル
    public int level_score = 0;     //今レベルの得点

    [SerializeField] private GameObject get_particle;

    [SerializeField] private List<AudioClip> soundEffects;
    public AudioSource audioSource;     //(外部代入)

    public GameObject displayScore;      //現スコアの表示(強調演出用にオブジェクトを取得)   (外部代入)
    [SerializeField] private TextMeshPro displayScore_tmpro;      //現スコアの表示

    public LevelGenerator levelGen;     //使用するlevelgenerator    (外部代入)
    public bool reproductionLevel = true;    //レベル複製スタイルにするか

    public SITUATION nowSituation = SITUATION.TITLE;    //現在のゲームの状況

    public TextMeshProUGUI UIDispLevel;   //UIに表示する現レベル    (外部代入)

    

    void Start()
    {
        SceneManager.sceneLoaded += _SceneStart;    //シーン読み込み時に実行

        SceneManager.LoadScene("Level");        //一度リロード

    }
    
    private void _SceneStart(Scene nextScene, LoadSceneMode mode){   
        StartCoroutine(_start());
    }

    private IEnumerator _start(){//シーン読み込み時に実行される関数
        yield return null;
        //audioSource = GetComponent<AudioSource>();
        displayScore_tmpro = displayScore.GetComponent<TextMeshPro>();
        UIDispLevel.text = "LEVEL " + (now_level + 1).ToString();
    }
    


    public void AddScore(int score){
        level_score+=score;
        //一瞬大きく
        displayScore.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0.1f).SetEase(Ease.InQuart).OnComplete(() =>  
        {
            displayScore.transform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), 0.1f).SetEase(Ease.OutQuart);
        });
        
        displayScore_tmpro.text = level_score.ToString();
    }


    public void GetEffect(Vector3 pos, int seNum){
        Instantiate(get_particle, pos, Quaternion.identity);    //パーティクルオブジェクト生成
        PlaySE(seNum);
    }

    public void PlaySE(int num){        //SEを鳴らす
        audioSource.PlayOneShot(soundEffects[num]);
    }

    public void GoNextLevel(){      //次レベル移行処理
        level_score = 0;
        now_level++;    //次のレベルへ
        nowSituation = SITUATION.TITLE;
        DOTween.KillAll();  //dotweenを用いたアニメーションをキル
        SceneManager.LoadScene("Level");
    }

    public void Retry(){      //次レベル移行処理
        level_score = 0;
        nowSituation = SITUATION.TITLE;
        DOTween.KillAll();  //dotweenを用いたアニメーションをキル
        SceneManager.LoadScene("Level");
    }

}
