using CVVTuber.VRM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmileSlider : MonoBehaviour
{
    Slider smileSlider;
    public VRMFaceBlendShapeController vrmFaceBlendShapeController;

    // Start is called before the first frame update
    void Start()
    {
        smileSlider = GetComponent<Slider>(); // Sliderコンポーネントを取得
        smileSlider.value = vrmFaceBlendShapeController.smileVal;

        smileSlider.onValueChanged.AddListener(OnSliderValueChanged); // スライダーの値が変更されたときに呼び出されるリスナーを追加
    }

    // スライダーの値が変更されたときに呼び出されるメソッド
    void OnSliderValueChanged(float value)
    {
        vrmFaceBlendShapeController.smileVal = value;
    }
    // Update is called once per frame

}
