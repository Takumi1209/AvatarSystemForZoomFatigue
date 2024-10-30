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
        ZSlider = GetComponent<Slider>(); // Slider�R���|�[�l���g���擾
        ZSlider.value = obj.position.x;

        ZSlider.onValueChanged.AddListener(OnSliderValueChanged); // �X���C�_�[�̒l���ύX���ꂽ�Ƃ��ɌĂяo����郊�X�i�[��ǉ�
    }

    void OnSliderValueChanged(float value)
    {
        obj.position = new Vector3(obj.position.x, obj.position.y, value);
    }
}
