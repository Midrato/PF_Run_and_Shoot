using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private bool isTouching = false;

    public float moveSpeed = 5.0f; // z方向の移動速度
    public float swipeSpeed = 0.1f; // x方向のスワイプ速度
    public float minX = -5.0f; // x方向の最小値
    public float maxX = 5.0f;  // x方向の最大値

    //public bool canMove = true;

    void Update()
    {
        
        if(GameManager.inst.nowSituation == GameManager.SITUATION.INGAME){  //インゲームなら操作可能
            MoveForward();
        }
        HandleTouchInput();
    }

    // z方向に一定速度で移動する処理
    private void MoveForward()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    // タッチ入力を処理する関数
    private void HandleTouchInput()
    {
        var platform = Application.platform;
        // Windows(PC環境)ならマウス操作
        if(platform == RuntimePlatform.WindowsEditor || platform == RuntimePlatform.WindowsPlayer){
                if(Input.GetMouseButtonDown(0)){
                    startTouchPosition = Input.mousePosition;
                    isTouching = true;
                }else if(Input.GetMouseButton(0)){
                    if (isTouching && GameManager.inst.nowSituation == GameManager.SITUATION.INGAME){
                        currentTouchPosition = Input.mousePosition;
                        Vector2 moveDirection = currentTouchPosition - startTouchPosition;
                        // 横方向のみの移動に制限
                        Vector3 newPosition = transform.position + new Vector3(moveDirection.x * swipeSpeed * Time.deltaTime, 0, 0);
                        // x方向の移動範囲を制限
                        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
                        transform.position = newPosition;
                        // スタート位置を更新
                        startTouchPosition = currentTouchPosition;
                    }
                }else if(Input.GetMouseButtonUp(0)){
                    isTouching = false;
                }
        }else{
            // スマホ環境ならタッチ操作
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startTouchPosition = touch.position;
                        isTouching = true;
                        break;

                    case TouchPhase.Moved:
                        if (isTouching && GameManager.inst.nowSituation == GameManager.SITUATION.INGAME)
                        {
                            currentTouchPosition = touch.position;
                            Vector2 moveDirection = currentTouchPosition - startTouchPosition;

                            // 横方向のみの移動に制限
                            Vector3 newPosition = transform.position + new Vector3(moveDirection.x * swipeSpeed * Time.deltaTime, 0, 0);

                            // x方向の移動範囲を制限
                            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
                            transform.position = newPosition;

                            // スタート位置を更新
                            startTouchPosition = currentTouchPosition;
                        }
                        break;

                    case TouchPhase.Ended:
                        isTouching = false;
                        break;
                }
            }
        }
        

        
    }
}
