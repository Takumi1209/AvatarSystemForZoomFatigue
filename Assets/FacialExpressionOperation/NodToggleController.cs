using CVVTuber;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class NodToggleController : MonoBehaviour
{
    public Toggle NodToggle; // UIのToggleコンポーネントへの参照
    public FaceAnimationController faceAnimationController; // 元のスクリプトへの参照
    // Start is called before the first frame update
    void Start()
    {
        // トグルの初期状態を設定
        NodToggle.isOn = faceAnimationController.enableQuestion;

        // トグルの値が変更されたときに呼び出されるリスナーを追加
        NodToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    // Update is called once per frame
    void OnToggleValueChanged(bool isOn)
    {
        faceAnimationController.enableNod = isOn;
    }
}
