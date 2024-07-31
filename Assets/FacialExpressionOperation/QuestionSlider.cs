using CVVTuber.VRM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionSlider : MonoBehaviour
{
    Slider questionSlider;
    public VRMFaceBlendShapeController vrmFaceBlendShapeController;

    // Start is called before the first frame update
    void Start()
    {
        questionSlider = GetComponent<Slider>(); // Sliderコンポーネントを取得
        questionSlider.value = vrmFaceBlendShapeController.jawAngleVal;

        questionSlider.onValueChanged.AddListener(OnSliderValueChanged); // スライダーの値が変更されたときに呼び出されるリスナーを追加
    }

    // スライダーの値が変更されたときに呼び出されるメソッド
    void OnSliderValueChanged(float value)
    {
        vrmFaceBlendShapeController.jawAngleVal = value;
    }
    // Update is called once per frame
    
}
