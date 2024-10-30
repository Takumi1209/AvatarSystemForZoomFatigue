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
        XSlider = GetComponent<Slider>(); // Slider�R���|�[�l���g���擾
        XSlider.value = obj.position.z;

        XSlider.onValueChanged.AddListener(OnSliderValueChanged); // �X���C�_�[�̒l���ύX���ꂽ�Ƃ��ɌĂяo����郊�X�i�[��ǉ�
    }

    void OnSliderValueChanged(float value)
    {
        obj.position = new Vector3(value, obj.position.y, obj.position.z);
    }

}
