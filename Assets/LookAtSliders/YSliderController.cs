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
        YSlider = GetComponent<Slider>(); // Slider�R���|�[�l���g���擾
        YSlider.value = obj.position.y;

        YSlider.onValueChanged.AddListener(OnSliderValueChanged); // �X���C�_�[�̒l���ύX���ꂽ�Ƃ��ɌĂяo����郊�X�i�[��ǉ�
    }

    void OnSliderValueChanged(float value)
    {
        obj.position = new Vector3(obj.position.x, value, obj.position.z);
    }
}
