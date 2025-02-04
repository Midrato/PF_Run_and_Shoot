using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerateLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            GameManager.inst.levelGen.Generate_Level();
            //Debug.Log(other.gameObject.name);
            Destroy(gameObject);
        }
    }
}
