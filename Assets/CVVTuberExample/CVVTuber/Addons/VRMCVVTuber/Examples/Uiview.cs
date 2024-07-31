using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uiview : MonoBehaviour
{

    public Canvas Canvas; // 表示させたいCanvasをここで指定
    // Start is called before the first frame update
    void Start()
    {
        Canvas.enabled = false; // 最初は非表示にする
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        bool isMouseInsideWindow =
            mousePosition.x >= 0 &&
            mousePosition.x <= Screen.width &&
            mousePosition.y >= 0 &&
            mousePosition.y <= Screen.height;

        if (isMouseInsideWindow)
        {
            Canvas.enabled = true; // マウスがウィンドウ内にある場合、Canvasを表示する
        }
        else
        {
            Canvas.enabled = false; // マウスがウィンドウ外にある場合、Canvasを非表示にする
        }
    }
}
