using UnityEngine;
using UnityEngine.UI;

public class ZPositionControl : MonoBehaviour
{
    public Scrollbar scrollbar;
    public GameObject targetObject;

    // Z座標の最小値と最大値
    public float minZ = -14.0f;
    public float maxZ = -2.0f;

    void Start()
    {
        // Scrollbarの値が変更されたときに呼ばれるイベントを設定
        scrollbar.onValueChanged.AddListener(delegate { OnScrollbarValueChanged(); });

        // 初期値でGameObjectのZ座標を設定
        UpdateObjectPosition(scrollbar.value);
    }

    void OnScrollbarValueChanged()
    {
        // Scrollbarの値に基づいてGameObjectのZ座標を更新
        UpdateObjectPosition(scrollbar.value);
    }

    void UpdateObjectPosition(float scrollbarValue)
    {
        // GameObjectのZ座標をScrollbarの値に応じて更新
        float z = Mathf.Lerp(minZ, maxZ, scrollbarValue);
        Vector3 newPosition = targetObject.transform.position;
        newPosition.z = z;
        targetObject.transform.position = newPosition;
    }
}
