using CVVTuber;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class NodToggleController : MonoBehaviour
{
    public Toggle NodToggle; // UI��Toggle�R���|�[�l���g�ւ̎Q��
    public FaceAnimationController faceAnimationController; // ���̃X�N���v�g�ւ̎Q��
    // Start is called before the first frame update
    void Start()
    {
        // �g�O���̏�����Ԃ�ݒ�
        NodToggle.isOn = faceAnimationController.enableQuestion;

        // �g�O���̒l���ύX���ꂽ�Ƃ��ɌĂяo����郊�X�i�[��ǉ�
        NodToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    // Update is called once per frame
    void OnToggleValueChanged(bool isOn)
    {
        faceAnimationController.enableNod = isOn;
    }
}
