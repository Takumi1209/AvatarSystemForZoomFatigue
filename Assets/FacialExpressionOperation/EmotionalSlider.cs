using CVVTuber.VRM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionalSlider : MonoBehaviour
{
    Slider emotionalSlider;
    public VRMFaceBlendShapeController vrmFaceBlendShapeController;

    // Start is called before the first frame update
    void Start()
    {
        emotionalSlider = GetComponent<Slider>(); // Slider�R���|�[�l���g���擾
        emotionalSlider.value = vrmFaceBlendShapeController.browHightVal;
    }
    void Update()
    {
        if (vrmFaceBlendShapeController.blendShapeProxy != null)
        {
            emotionalSlider.onValueChanged.AddListener(OnSliderValueChanged); // �X���C�_�[�̒l���ύX���ꂽ�Ƃ��ɌĂяo����郊�X�i�[��ǉ�
        }
    }

    // �X���C�_�[�̒l���ύX���ꂽ�Ƃ��ɌĂяo����郁�\�b�h
    void OnSliderValueChanged(float value)
    {
        vrmFaceBlendShapeController.browHightVal = value;
        // Update is called once per frame

    }
}
