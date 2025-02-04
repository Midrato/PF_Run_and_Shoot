using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Arg_GameManager : MonoBehaviour
{
    //  レベル更新時にリセットされる変数に代入する物たち
    [SerializeField] private AudioSource audioSource;     //(外部代入)
    [SerializeField] private GameObject displayScore;      //現スコアの表示(強調演出用にオブジェクトを取得)   (外部代入)
    [SerializeField] private LevelGenerator levelGen;     //使用するlevelgenerator    (外部代入)
    [SerializeField] private TextMeshProUGUI UIDispLevel;   //UIに表示する現レベル    (外部代入)

    void Start()
    {
        GameManager.inst.audioSource = audioSource;
        GameManager.inst.displayScore = displayScore;
        GameManager.inst.levelGen = levelGen;
        GameManager.inst.UIDispLevel = UIDispLevel;
    }
}
