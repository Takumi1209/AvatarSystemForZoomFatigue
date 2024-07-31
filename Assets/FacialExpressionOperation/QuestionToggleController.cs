using CVVTuber;
using CVVTuber.VRM;
using UnityEngine;
using UnityEngine.UI;

public class QuestionToggleController : MonoBehaviour
{
    public Toggle QuestionToggle; // UI��Toggle�R���|�[�l���g�ւ̎Q��
    public FaceAnimationController faceAnimationController; // ���̃X�N���v�g�ւ̎Q��

    void Start()
    {
        // �g�O���̏�����Ԃ�ݒ�
        QuestionToggle.isOn = faceAnimationController.enableQuestion;

        // �g�O���̒l���ύX���ꂽ�Ƃ��ɌĂяo����郊�X�i�[��ǉ�
        QuestionToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    // �g�O���̒l���ύX���ꂽ�Ƃ��ɌĂяo����郁�\�b�h
    void OnToggleValueChanged(bool isOn)
    {
        faceAnimationController.enableQuestion = isOn;
    }
}
