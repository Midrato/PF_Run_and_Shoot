using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Update()
    {
        // カメラの位置を取得
        Vector3 cameraPosition = Camera.main.transform.position;
        // カメラの方向を向く
        Vector3 direction = cameraPosition - transform.position;
        direction.x = 0; // X軸で固定
        

        // 反対を向かないように180度回転させる
        transform.rotation = Quaternion.LookRotation(-direction);
    }
}