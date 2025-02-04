using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class LevelDestroyer : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Level")){
            Destroy(other.gameObject);
        }
    }
}
