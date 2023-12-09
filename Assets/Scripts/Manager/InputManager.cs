using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;

    public DirSwitch dirSwitch;

    [SerializeField]
    private float swipeThreshold = 100f;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (GameManager.instance.stateGame != GameState.Playing)
            return;
        DetectTouchDirection();
    }

    void DetectTouchDirection()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touchEndPos = touch.position;

                float swipeX = touchEndPos.x - touchStartPos.x;
                float swipeY = touchEndPos.y - touchStartPos.y;

                if (Mathf.Abs(swipeX) > Mathf.Abs(swipeY))
                {
                    // Horizontal swipe
                    if (swipeX > swipeThreshold)
                    {
                        dirSwitch = DirSwitch.right;
                        GameManager.instance.Shift(Vector2.right);
                    }
                    else if (swipeX < -swipeThreshold)
                    {
                        dirSwitch = DirSwitch.left;
                        GameManager.instance.Shift(Vector2.left);
                    }
                    //BoardController.instance.CellsCanMove(dirSwitch);
                }
                else
                {
                    // Vertical swipe
                    if (swipeY > swipeThreshold)
                    {
                        dirSwitch = DirSwitch.up;
                        GameManager.instance.Shift(Vector2.up);

                    }
                    else if (swipeY < -swipeThreshold)
                    {
                        dirSwitch = DirSwitch.down;
                        GameManager.instance.Shift(Vector2.down);

                    }
                }

            }
        }
    }
}
public enum DirSwitch
{
    up,
    down,
    left,
    right
}
