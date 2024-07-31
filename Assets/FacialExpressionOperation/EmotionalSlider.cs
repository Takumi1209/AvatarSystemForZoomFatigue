using CVVTuber.VRM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionalSlider : MonoBehaviour
{
    Slider emotionalSlider;
    public VRMFaceBlendShapeController vrmFaceBlendShapeController;

    // Start is called before the first frame update
    void Start()
    {
        emotionalSlider = GetComponent<Slider>(); // Sliderコンポーネントを取得
        emotionalSlider.value = vrmFaceBlendShapeController.browHightVal;
    }
    void Update()
    {
        if (vrmFaceBlendShapeController.blendShapeProxy != null)
        {
            emotionalSlider.onValueChanged.AddListener(OnSliderValueChanged); // スライダーの値が変更されたときに呼び出されるリスナーを追加
        }
    }

    // スライダーの値が変更されたときに呼び出されるメソッド
    void OnSliderValueChanged(float value)
    {
        vrmFaceBlendShapeController.browHightVal = value;
        // Update is called once per frame

    }
}
