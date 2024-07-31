using CVVTuber;
using CVVTuber.VRM;
using DlibFaceLandmarkDetector;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VRM;

namespace AddonScripts{
    public class vrmAutoController : MonoBehaviour
    {
        public VRMBlendShapeProxy blendShapeProxy;

        public float BlinkParam;
        public float MIniBlinkParam = 15f;
        public float blinkSpeed = 0.5f;
        [Range(0, 1)]
        public float eyeLeapT = 0.4f;

        public float timeOut = 1.0f;
        private float timeElapsed;

        public VRMLookAtHead VRMLookAtHead;
        public float BeforeYaw;
        public float BeforePitch;

        private Animator anim = null;

      
        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
            var target = GameObject.Find("Main Camera").transform;
            VRMLookAtHead.Target = target;
          
            anim.SetInteger("RandomState", 0);

        }
       


        // Update is called once per frame
        void Update()
        {

            timeElapsed += Time.deltaTime;
            float Yaw = VRMLookAtHead.Yaw;
            float Pitch = VRMLookAtHead.Pitch;
            
            
         

           // Debug.Log("Yaw: " + Math.Abs(Yaw - BeforeYaw) + " Pitch: " + Math.Abs(Pitch - BeforePitch));

            if (timeElapsed >= timeOut)
            {
                if (Math.Abs(Yaw - BeforeYaw) < 0.05f && Math.Abs(Pitch - BeforePitch) < 0.05f)
                {
                   
                    float randomin = UnityEngine.Random.Range(0.0f, 1.0f);
                    if(randomin < 0.6f)
                    {
                        //ƒ‰ƒ“ƒ_ƒ€‚É–Ú‚ð•Â‚¶‚é
                        BlinkParam = Mathf.Lerp(0.9f, 1.0f, 0.2f);
                        blendShapeProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink), BlinkParam);
                    }
                    // 1‚©‚ç3‚ÌŠÔ‚Åƒ‰ƒ“ƒ_ƒ€‚È®”‚ðŽæ“¾
                    int RandomState = UnityEngine.Random.Range(1, 3);
                    anim.SetInteger("RandomState", RandomState);
                }
                else
                {
                    anim.SetInteger("RandomState", 0);
                }
                BeforeYaw = Yaw;
                BeforePitch = Pitch;

                timeElapsed = 0.0f;
            }


        }
    }

}
