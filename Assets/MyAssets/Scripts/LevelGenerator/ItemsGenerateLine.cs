using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsGenerateLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            GameManager.inst.levelGen.Generate_ItemSet();
            //Debug.Log(other.gameObject.name);
            Destroy(gameObject);
        }
    }
}
