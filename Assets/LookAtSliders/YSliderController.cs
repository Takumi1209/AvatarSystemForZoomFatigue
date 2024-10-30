using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YSliderController : MonoBehaviour
{
    Slider YSlider;
    public Transform obj;

    void Start()
    {
        YSlider = GetComponent<Slider>(); // Sliderコンポーネントを取得
        YSlider.value = obj.position.y;

        YSlider.onValueChanged.AddListener(OnSliderValueChanged); // スライダーの値が変更されたときに呼び出されるリスナーを追加
    }

    void OnSliderValueChanged(float value)
    {
        obj.position = new Vector3(obj.position.x, value, obj.position.z);
    }
}
