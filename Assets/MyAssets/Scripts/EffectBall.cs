using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBall : MonoBehaviour
{
    void Update()
    {
        if(transform.localPosition.y <= -3){
            Destroy(this.gameObject);
        }
    }
}
