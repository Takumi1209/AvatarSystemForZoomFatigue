using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uiview : MonoBehaviour
{

    public Canvas Canvas; // �\����������Canvas�������Ŏw��
    // Start is called before the first frame update
    void Start()
    {
        Canvas.enabled = false; // �ŏ��͔�\���ɂ���
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
            Canvas.enabled = true; // �}�E�X���E�B���h�E���ɂ���ꍇ�ACanvas��\������
        }
        else
        {
            Canvas.enabled = false; // �}�E�X���E�B���h�E�O�ɂ���ꍇ�ACanvas���\���ɂ���
        }
    }
}
