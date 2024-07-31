using CVVTuber;
using CVVTuber.VRM;
using UnityEngine;
using UnityEngine.UI;

public class EmotionalToggleController : MonoBehaviour
{
    public Toggle EmotionalToggle; // UI��Toggle�R���|�[�l���g�ւ̎Q��
    public FaceAnimationController faceAnimationController; // ���̃X�N���v�g�ւ̎Q��

    void Start()
    {
        // �g�O���̏�����Ԃ�ݒ�
        EmotionalToggle.isOn = faceAnimationController.enableQuestion;

        // �g�O���̒l���ύX���ꂽ�Ƃ��ɌĂяo����郊�X�i�[��ǉ�
        EmotionalToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    // �g�O���̒l���ύX���ꂽ�Ƃ��ɌĂяo����郁�\�b�h
    void OnToggleValueChanged(bool isOn)
    {
        faceAnimationController.enableBrow = isOn;
    }
}
