using UnityEngine;
using UnityEngine.UI;

public class ZPositionControl : MonoBehaviour
{
    public Scrollbar scrollbar;
    public GameObject targetObject;

    // Z���W�̍ŏ��l�ƍő�l
    public float minZ = -14.0f;
    public float maxZ = -2.0f;

    void Start()
    {
        // Scrollbar�̒l���ύX���ꂽ�Ƃ��ɌĂ΂��C�x���g��ݒ�
        scrollbar.onValueChanged.AddListener(delegate { OnScrollbarValueChanged(); });

        // �����l��GameObject��Z���W��ݒ�
        UpdateObjectPosition(scrollbar.value);
    }

    void OnScrollbarValueChanged()
    {
        // Scrollbar�̒l�Ɋ�Â���GameObject��Z���W���X�V
        UpdateObjectPosition(scrollbar.value);
    }

    void UpdateObjectPosition(float scrollbarValue)
    {
        // GameObject��Z���W��Scrollbar�̒l�ɉ����čX�V
        float z = Mathf.Lerp(minZ, maxZ, scrollbarValue);
        Vector3 newPosition = targetObject.transform.position;
        newPosition.z = z;
        targetObject.transform.position = newPosition;
    }
}
