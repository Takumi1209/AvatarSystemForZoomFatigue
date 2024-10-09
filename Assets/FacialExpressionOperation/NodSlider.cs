using CVVTuber.VRM;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NodSlider : MonoBehaviour
{
    Slider nodSlider;
    public VRMFaceBlendShapeController vrmFaceBlendShapeController;
    // Start is called before the first frame update
    void Start()
    {
        nodSlider = GetComponent<Slider>(); // Sliderコンポーネントを取得
        nodSlider.value = vrmFaceBlendShapeController.nodVal;

        nodSlider.onValueChanged.AddListener(OnSliderValueChanged); // スライダーの値が変更されたときに呼び出されるリスナーを追加
    }

    void OnSliderValueChanged(float value)
    {
        vrmFaceBlendShapeController.nodVal = value;
    }

}
