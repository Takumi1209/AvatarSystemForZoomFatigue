using CVVTuber;
using CVVTuber.VRM;
using UnityEngine;
using UnityEngine.UI;

public class QuestionToggleController : MonoBehaviour
{
    public Toggle QuestionToggle; // UIのToggleコンポーネントへの参照
    public FaceAnimationController faceAnimationController; // 元のスクリプトへの参照

    void Start()
    {
        // トグルの初期状態を設定
        QuestionToggle.isOn = faceAnimationController.enableQuestion;

        // トグルの値が変更されたときに呼び出されるリスナーを追加
        QuestionToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    // トグルの値が変更されたときに呼び出されるメソッド
    void OnToggleValueChanged(bool isOn)
    {
        faceAnimationController.enableQuestion = isOn;
    }
}
