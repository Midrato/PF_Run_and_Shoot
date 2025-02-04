using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //dotween使用

public class Bounce_tween : MonoBehaviour
{
    [SerializeField] private float big_scale = 1f;  //スケールをどこまで拡大するか
    [SerializeField] private float bounceTime = 1f; //何秒で1ループするか

    private void OnEnable(){
        transform.DOScale(big_scale, bounceTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic);  //でかくなったり小さくなったりアニメーション
    }
    private void OnDestroy() {
        transform.DOKill();
    }
    private void OnDisable() {
        transform.DOKill();
    }
}
