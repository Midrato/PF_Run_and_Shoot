using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    public List<Level> Levels = new List<Level>();  //レベルを保存するリスト
    [SerializeField] private GameObject lastLeveltmp;  //最終レベル複製用
    private int cashLevel = 0;

    private float roadPoint = -1f;     //道生成の初期値
    private float roadDistance = 10f;     //道同士の間隔

    private float itemPoint = 8f;     //アイテムセットの初期位置
    private float itemDistance = 9f;    //アイテムセットの間隔

    public int generated_levels = 0;    //生成した足場の数
    public int generated_items = 0;    //生成したアイテムセットの数
    private int itemSetLength = 0;      //アイテムセット生成数

    private int listGenerateCounter = 0;    //レベルプレハブの何番目を出したかを保存する

    [SerializeField] private GameObject player;     //プレイヤーのオブジェクト
    [SerializeField] private Slider progress;       //ステージの進行度表記バー

    


    // Start is called before the first frame update
    void Start()
    {
        GameManager.inst.levelGen = this;
        cashLevel = GameManager.inst.now_level;    //現在のレベルを代入

        GameManager.inst.reproductionLevel = (cashLevel >= Levels.Count);   //最終レベル到達後なら複製スタイルへ変更
        LevelReproduction(GameManager.inst.reproductionLevel);      //最終レベル到達後なら複製
        
        Level_Startup(5);
        itemSetLength = (int)((progress.maxValue - itemPoint) / itemDistance);     //アイテム生成数
        //Debug.Log("itemSetLength:" + itemSetLength);
        Level_Startup_Item(8);

    }

    void Update()
    {
        MoveProgress();
    }

    private void Level_Startup(int setblocknumber){         //レベルの初期生成を行う。引数で描画ブロック数を設定
        for(int i=0; i<setblocknumber;i++){
            Generate_Level();
        }
        progress.maxValue = (Levels[cashLevel].stage_Length_in_block - 1) * roadDistance;       //プログレスバーの初期値
    }

    public void Generate_Level(){
        if(generated_levels < Levels[cashLevel].stage_Length_in_block){        //生成ブロック数がまだステージ長になっていないなら
            //道とアイテム等オブジェクト類を生成
            Generate_Road(Levels[cashLevel].RoadPrefabs);
            //Generate_Road(ObjectSetPrefabs);

            roadPoint += roadDistance;      //次生成位置を設定
            
            //Debug.Log("Level Generated");
            Debug.Log("block数:" + generated_levels.ToString());
        }else if(generated_levels == Levels[cashLevel].stage_Length_in_block){     //既定の数レベルを置いたなら
            Vector3 pos = new Vector3(0f, 0f, roadPoint);
            var newobj = Instantiate(Levels[cashLevel].endPoint, pos, Quaternion.identity);     //最後の足場を配置
            newobj.transform.SetParent(this.transform);
        }
        generated_levels++;     //生成した道の数をカウント
    }

    private void Generate_Road(List<GameObject> ObjectSetList){      //順番に地形セットを設置
        Vector3 pos = new Vector3(0f, 0f, roadPoint);
        var newobj = Instantiate(ObjectSetList[listGenerateCounter], pos, Quaternion.identity);     //番号順にプレハブを既定の位置に配置
        listGenerateCounter = (listGenerateCounter+1) % ObjectSetList.Count;    //counterを次の添え字に
        newobj.transform.SetParent(this.transform);
    }

     private void Level_Startup_Item(int setitemnumber){         //アイテムの初期生成を行う。引数で描画ブロック数を設定
        for(int i=0; i<setitemnumber;i++){
            Generate_ItemSet();
        }
    }

    public void Generate_ItemSet(){
        if(generated_items < itemSetLength){        //生成アイテムセット数がまだステージ長になっていないなら
            //道とアイテム等オブジェクト類を生成
            _Generate_ItemSet(Levels[cashLevel].ItemSetPrefabs);
            //Generate_Road(ObjectSetPrefabs);

            itemPoint += itemDistance;      //次生成位置を設定
            
            //Debug.Log("Level Generated");
            Debug.Log("itemSet数:" + generated_items.ToString());
        }
        generated_items++;     //生成した道の数をカウント
    }

    private void _Generate_ItemSet(List<GameObject> ObjectSetList){      //ランダムなアイテムセットを設置
        Vector3 pos = new Vector3(0f, 0f, itemPoint);
        var newobj = Instantiate(ObjectSetList[Random.Range(0, Levels[cashLevel].ItemSetPrefabs.Count)], pos, Quaternion.identity);     //番号順にプレハブを既定の位置に配置

        newobj.transform.SetParent(this.transform);
    }

    private void MoveProgress(){        //プログレスバーを動かす
        progress.value = player.transform.position.z;
    }

    private void LevelReproduction(bool isActive){  //最終レベルを複製しだんだん難しくしていくスタイルに
        Levels[Levels.Count -1] = Instantiate(lastLeveltmp).GetComponent<Level>();      //Prefabを実体化し、それを最終レベルとすることでプレハブの変化を防ぐ
        if(isActive && GameManager.inst.now_level!=0){     //使用する、かつ現在がレベル1の時のみ起動
            cashLevel = Levels.Count - 1;
            Levels[Levels.Count -1].stage_Length_in_block += (GameManager.inst.now_level - (Levels.Count-1)) * 1;   //レベル上昇で少し道を長く
            Levels[Levels.Count -1].needScore += (GameManager.inst.now_level - (Levels.Count-1)) * 10;
        }
    }
}
