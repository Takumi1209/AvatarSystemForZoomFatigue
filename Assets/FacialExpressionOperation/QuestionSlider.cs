using CVVTuber.VRM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionSlider : MonoBehaviour
{
    Slider questionSlider;
    public VRMFaceBlendShapeController vrmFaceBlendShapeController;

    // Start is called before the first frame update
    void Start()
    {
        questionSlider = GetComponent<Slider>(); // Slider�R���|�[�l���g���擾
        questionSlider.value = vrmFaceBlendShapeController.jawAngleVal;

        questionSlider.onValueChanged.AddListener(OnSliderValueChanged); // �X���C�_�[�̒l���ύX���ꂽ�Ƃ��ɌĂяo����郊�X�i�[��ǉ�
    }

    // �X���C�_�[�̒l���ύX���ꂽ�Ƃ��ɌĂяo����郁�\�b�h
    void OnSliderValueChanged(float value)
    {
        vrmFaceBlendShapeController.jawAngleVal = value;
    }
    // Update is called once per frame
    
}
