using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;  //dotween使用

public class DeathPanelEffect : MonoBehaviour
{
    [SerializeField] private GameObject nextButton;    //次画面遷移ポタン
    private Image panelImage;
    
    [SerializeField] private TextMeshProUGUI failText;   //失敗文
    
    void OnEnable()
    {
        panelImage = GetComponent<Image>();
        StartCoroutine(_onEnable());

        failText.text = "LEVEL " + (GameManager.inst.now_level+1) + "\nFailed\nTry Again!";
    }

    private IEnumerator _onEnable(){
        panelImage.DOFade(0.8f, 0.6f);     //フェードイン
        Debug.Log("Death");
        yield return new WaitForSeconds(0.8f);
        nextButton.SetActive(true);
    }
}
