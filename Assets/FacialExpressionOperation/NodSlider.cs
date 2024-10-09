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
        nodSlider = GetComponent<Slider>(); // Slider�R���|�[�l���g���擾
        nodSlider.value = vrmFaceBlendShapeController.nodVal;

        nodSlider.onValueChanged.AddListener(OnSliderValueChanged); // �X���C�_�[�̒l���ύX���ꂽ�Ƃ��ɌĂяo����郊�X�i�[��ǉ�
    }

    void OnSliderValueChanged(float value)
    {
        vrmFaceBlendShapeController.nodVal = value;
    }

}
