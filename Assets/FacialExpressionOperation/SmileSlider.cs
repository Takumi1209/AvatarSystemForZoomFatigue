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
        smileSlider = GetComponent<Slider>(); // Slider�R���|�[�l���g���擾
        smileSlider.value = vrmFaceBlendShapeController.smileVal;

        smileSlider.onValueChanged.AddListener(OnSliderValueChanged); // �X���C�_�[�̒l���ύX���ꂽ�Ƃ��ɌĂяo����郊�X�i�[��ǉ�
    }

    // �X���C�_�[�̒l���ύX���ꂽ�Ƃ��ɌĂяo����郁�\�b�h
    void OnSliderValueChanged(float value)
    {
        vrmFaceBlendShapeController.smileVal = value;
    }
    // Update is called once per frame

}
