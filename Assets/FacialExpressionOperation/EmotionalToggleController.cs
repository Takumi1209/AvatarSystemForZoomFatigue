using CVVTuber;
using CVVTuber.VRM;
using UnityEngine;
using UnityEngine.UI;

public class EmotionalToggleController : MonoBehaviour
{
    public Toggle EmotionalToggle; // UIのToggleコンポーネントへの参照
    public FaceAnimationController faceAnimationController; // 元のスクリプトへの参照

    void Start()
    {
        // トグルの初期状態を設定
        EmotionalToggle.isOn = faceAnimationController.enableQuestion;

        // トグルの値が変更されたときに呼び出されるリスナーを追加
        EmotionalToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    // トグルの値が変更されたときに呼び出されるメソッド
    void OnToggleValueChanged(bool isOn)
    {
        faceAnimationController.enableBrow = isOn;
    }
}
