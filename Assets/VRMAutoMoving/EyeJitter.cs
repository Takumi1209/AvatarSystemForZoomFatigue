using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeJitter : MonoBehaviour
{

    float timer = 0.0f;
    Quaternion rot;


    public float changeTime = 0.4f; // �ύX���鎞�ԍŏ��l
    public float changeTimeRange = 2.0f; // �ύX���鎞�ԕ��i�����j
    public Vector2 range = new Vector2(0.001f, 0.01f); // ���͈�

    public Transform rightEye;   // ex.) 93.!joint_RightEye
    public Transform leftEye;    // ex.) 95.!joint_LeftEye


    void Start()
    {
    }

    [System.Obsolete]
    void LateUpdate()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            timer += Random.Range(changeTime, changeTimeRange);

            Vector3 v = Vector3.zero;
            v.x = Random.Range(-range.x, +range.x);
            v.y = Random.Range(-range.y, +range.y);

            rot = Quaternion.EulerRotation(v);
        }

        leftEye.localRotation *= rot;
        rightEye.localRotation *= rot;
    }
}
