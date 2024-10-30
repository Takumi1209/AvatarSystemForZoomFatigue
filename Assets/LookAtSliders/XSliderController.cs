using CVVTuber.VRM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XSliderController : MonoBehaviour
{
    Slider XSlider;
    public Transform obj;

    void Start()
    {
        XSlider = GetComponent<Slider>(); // Sliderコンポーネントを取得
        XSlider.value = obj.position.z;

        XSlider.onValueChanged.AddListener(OnSliderValueChanged); // スライダーの値が変更されたときに呼び出されるリスナーを追加
    }

    void OnSliderValueChanged(float value)
    {
        obj.position = new Vector3(value, obj.position.y, obj.position.z);
    }

}
