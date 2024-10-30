using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class ZSliderController : MonoBehaviour
{
    Slider ZSlider;
    public Transform obj;

    void Start()
    {
        ZSlider = GetComponent<Slider>(); // Sliderコンポーネントを取得
        ZSlider.value = obj.position.x;

        ZSlider.onValueChanged.AddListener(OnSliderValueChanged); // スライダーの値が変更されたときに呼び出されるリスナーを追加
    }

    void OnSliderValueChanged(float value)
    {
        obj.position = new Vector3(obj.position.x, obj.position.y, value);
    }
}
