using System.Collections.Generic;
using UnityEngine;
using VRM;
using System;
using System.Runtime.CompilerServices;
using OpenCVForUnityExample;

namespace CVVTuber.VRM
{
    public class VRMFaceBlendShapeController : FaceAnimationController
    {
        [Header("[Target]")]

        public VRMBlendShapeProxy blendShapeProxy;

        GameObject vrmObject;

        #region CVVTuberProcess

        public override string GetDescription()
        {
            return "Update face BlendShape of VRM using FaceLandmarkGetter.";
        }

        public override void LateUpdateValue()
        {
            if (blendShapeProxy == null)
                return;

            blendShapeProxy.Apply();
        }

        #endregion


        #region FaceAnimationController

        public override void Setup()
        {
            base.Setup();

            NullCheck(blendShapeProxy, "blendShapeProxy");
        }

        public override void UpdateValue()
        {
            if (blendShapeProxy == null)
                return;

            base.UpdateValue();
        }

      

        public float browHightVal = 0.85f;
        public float jawAngleVal = 65.0f;
        public float smileVal = 0.3f;
        public float nodVal = 16f;


        private float timeElapsed;
        public float timeOut = 1.0f;
        public float prevNose = 0;
        public float prevChin = 0;

        protected override void UpdateFaceAnimation(List<Vector2> points)
        {

            if (enableQuestion)
            {
                float jawangle = angleTilt(points);
                if (jawangle > jawAngleVal + 50.0f || jawangle < jawAngleVal)
                {

                    blendShapeProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Relaxed), 1.0f);

                }
                else
                {
                    blendShapeProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Relaxed), 0.0f);
                }


            }

            if (enableBrow)
            {
                float browHeight = (GetLeftEyebrowUPRatio(points) + GetRightEyebrowUPRatio(points)) / 2.0f;
                // Debug.Log("browHeight " + browHeight);

                if (browHeight >= browHightVal)
                {
                    browHeight = 1.0f;

                }
                else
                {
                    browHeight = 0.0f;
                }
                BrowParam = Mathf.Lerp(BrowParam, browHeight, browLeapT);

                blendShapeProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Surprised), BrowParam);

            }

            if (enableEye)
            {
                float eyeOpen = (GetLeftEyeOpenRatio(points) + GetRightEyeOpenRatio(points)) / 2.0f;
                //Debug.Log("eyeOpen " + eyeOpen);

                if (eyeOpen >= 0.2f)
                {
                    eyeOpen = 1.0f;
                }
                else
                {
                    eyeOpen = 0.0f;
                }
                EyeParam = Mathf.Lerp(EyeParam, 1.0f - eyeOpen, eyeLeapT);

                blendShapeProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink), EyeParam);
            }

            if (enableMouth)
            {
                float mouthOpen = GetMouthOpenYRatio(points);
                //Debug.Log("mouthOpen " + mouthOpen);

                if (mouthOpen >= 0.65f)
                {
                    mouthOpen = 1.0f;
                }
                else if (mouthOpen >= 0.25f)
                {
                    mouthOpen = 0.5f;
                }
                else
                {
                    mouthOpen = 0.0f;
                }
                MouthOpenParam = Mathf.Lerp(MouthOpenParam, mouthOpen, mouthLeapT);

                blendShapeProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.A), MouthOpenParam);
                blendShapeProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.O), MouthOpenParam * 0.7f);


                float mouthSize = GetMouthOpenXRatio(points);
                //Debug.Log("mouthSize " + mouthSize);

                if (mouthSize >= smileVal)
                {
                    mouthSize = 1.0f;
                }
                /*
                else if (mouthSize >= 0.6f)
                {
                    mouthSize = 0.5f;
                }
                */
                else
                {
                    mouthSize = 0.0f;
                }
                MouthSizeParam = Mathf.Lerp(MouthSizeParam, mouthSize, mouthLeapT);

                if (enableSmile)
                {
                    blendShapeProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.Joy), MouthSizeParam);
                }
                else
                {
                    blendShapeProxy.AccumulateValue(BlendShapeKey.CreateFromPreset(BlendShapePreset.I), MouthSizeParam);
                }
                float mouthRatio = (GetMouthOpenXRatio(points) + GetMouthOpenYRatio(points)) / 2;
                // Debug.Log("mouthWidth " + mouthRatio);
                float EyeRatio = (GetLeftEyeOpenRatio(points) + GetRightEyeOpenRatio(points)) / 2;
                // Debug.Log("EyeRatio " + EyeRatio);
             
            }

            
            if (enableNod)
            {
                timeElapsed += Time.deltaTime;
                float Nose = noseDitect(points);
                float Chin = chinDitect(points);

                vrmObject = GameObject.Find("VRM");
                Transform transform = vrmObject.transform;
                Vector3 originalPos = new Vector3(0, 0, 0);
                Vector3 lowerPos = new Vector3(0, -0.05f, 0);

                if (timeElapsed >= timeOut)
                {
               
                   if (Mathf.Abs(Nose - prevNose) > nodVal && Mathf.Abs(Chin - prevChin) > nodVal)
                    {
                        Debug.Log("Nod");
                        transform.position = Vector3.Lerp(originalPos, lowerPos, 0.5f);

                    }
                   else
                    {
                       // transform.position = Vector3.Lerp(lowerPos, originalPos, 0.5f);
                    }
                    transform.position = Vector3.Lerp(lowerPos, originalPos, 0.5f);
                    prevNose = Nose;
                    prevChin = Chin;
                    timeElapsed = 0.0f;
                }
               
            }
            
        }

        #endregion
    }
}