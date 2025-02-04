using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;  //dotween使用

public class WinPanelEffect : MonoBehaviour
{
    [SerializeField] private GameObject nextButton;    //次画面遷移ポタン
    private Image panelImage;
    [SerializeField] private TextMeshProUGUI winText;   //勝利文

    void OnEnable()
    {
        panelImage = GetComponent<Image>();
        StartCoroutine(_onEnable());
        winText.text = "LEVEL " + (GameManager.inst.now_level+1) + "\nCOMPLETE!";
    }

    private IEnumerator _onEnable(){
        panelImage.DOFade(0.2f, 0.8f);     //フェードイン
        //Debug.Log("WIN!");
        yield return new WaitForSeconds(0.8f);
        nextButton.SetActive(true);
    }
}
