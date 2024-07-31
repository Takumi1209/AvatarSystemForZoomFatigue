using UnityEngine;
using System.Collections;

/**
BreathController

Copyright (c) 2015 Toshiaki Aizawa (https://twitter.com/xflutexx)

This software is released under the MIT License.
 http://opensource.org/licenses/mit-license.php …
*/
namespace Mebiustos.BreathController {
    public class BreathController : MonoBehaviour {
        public const float InitialDurationInhale = 1.2f; // 1.3
        public const float InitialDurationExhale = 2.4f; // 2.7
        public const float InitialDurationRest = 0.2f;
        public const float InitialAngleSpineInhale = 2f;
        public const float InitialAngleSpineExhale = -2f;
        public const float InitialAngleChestInhale = -3f;
        public const float InitialAngleChestExhale = 3f;
        public const float InitialAngleNeckInhale = 0.5f;
        public const float InitialAngleNeckExhale = -0.5f;
        public const float InitialAngleHeadInhale = 0.5f;
        public const float InitialAngleHeadExhale = -0.5f;
        public const HalingMethod InitialMethodInhale = HalingMethod.EaseOutSine;

        [System.Serializable]
        public class Segment {
            public HumanBodyBones Bone;

            public Angle x = new Angle();
            public Angle y = new Angle();
            public Angle z = new Angle();

            [System.NonSerialized]
            public Transform transform;
        }

        [System.Serializable]
        public class Angle {
            public float max;
            public float min;
            public float maxDuration;
            public float minDuration;

            float startTime;
            float startValue;
            float changeInValue;
            float durationTime;

            public float lastEaseValue;

            public void SetEase(float startValue, float changeInValue, float durationTime) {
                this.startTime = Time.time;
                this.startValue = startValue;
                this.changeInValue = changeInValue;
                this.durationTime = durationTime;
            }

            public float UpdateEase(Phase status, HalingMethod inhalingMethod, float durationRate) {
                if (status == Phase.Inhaling) {
                    if (inhalingMethod == HalingMethod.EaseOutSine)
                        this.lastEaseValue = easeOutSine(Time.time - this.startTime, this.startValue, this.changeInValue, this.durationTime * durationRate);
                    else
                        this.lastEaseValue = easeInOutQuad(Time.time - this.startTime, this.startValue, this.changeInValue, this.durationTime * durationRate);
                    return this.lastEaseValue;
                } else {
                    this.lastEaseValue = easeInOutQuad(Time.time - this.startTime, this.startValue, this.changeInValue, this.durationTime * durationRate);
                    return this.lastEaseValue;
                }
            }

            public bool IsFinishEase(float durationRate) {
                if (this.durationTime == 0) return true;
                return Time.time - this.startTime >= this.durationTime * durationRate;
            }

            /// <summary>
            /// </summary>
            /// <param name="t">current time</param>
            /// <param name="b">start value</param>
            /// <param name="c">change in value</param>
            /// <param name="d">duration</param>
            /// <returns></returns>
            float easeInOutQuad(float t, float b, float c, float d) {
                if (t >= d) return c + b;
                t /= d / 2;
                if (t < 1) return c / 2 * t * t + b;
                t--;
                return -c / 2 * (t * (t - 2) - 1) + b;
            }
            float easeOutCubic(float t, float b, float c, float d) {
                if (t >= d) return c + b;
                t /= d;
                t--;
                return c * (t * t * t + 1) + b;
            }
            float easeOutQuart(float t, float b, float c, float d) {
                if (t >= d) return c + b;
                t /= d;
                t--;
                return -c * (t * t * t * t - 1) + b;
            }
            float easeInOutQuart(float t, float b, float c, float d) {
                if (t >= d) return c + b;
                t /= d / 2;
                if (t < 1) return c / 2 * t * t * t * t + b;
                t -= 2;
                return -c / 2 * (t * t * t * t - 2) + b;
            }
            float easeOutSine(float t, float b, float c, float d) {
                if (t >= d) return c + b;
                return c * Mathf.Sin(t / d * (Mathf.PI / 2)) + b;
            }
            float easeInOutSine(float t, float b, float c, float d) {
                if (t >= d) return c + b;
                return -c / 2 * (Mathf.Cos(Mathf.PI * t / d) - 1) + b;
            }
            float easeOutExpo(float t, float b, float c, float d) {
                if (t >= d) return c + b;
                return c * (-Mathf.Pow(2, -10 * t / d) + 1) + b;
            }
        }

        [Header("Basic Config")]
        public float durationRate = 1;
        public float effectRate = 1;

        Segment[] Segments;
        Transform LeftShoulder;
        Transform RightShoulder;
        public enum Phase {
            Inhaling,
            Exhaling,
            Rest
        }
        Phase phase;
        float restEndTime;
        bool hasController;

        void Start() {
            var anim = GetComponent<Animator>();
            this.hasController = anim.runtimeAnimatorController != null;
            if (!this.hasController)
                Debug.LogWarning("Not found 'Animator Controller' : " + this.gameObject.name);

            this.phase = Phase.Inhaling;

            this.InitializeSegments(anim);
            this.InitializeSoulders(anim);

            this.SetEase();
        }

        void LateUpdate() {
            if (this.hasController)
                switch (phase) {
                    case Phase.Inhaling: OnInhaling(); break;
                    case Phase.Exhaling: OnExhaling(); break;
                    case Phase.Rest: OnRest(); break;
                }
        }

        void OnInhaling() {
            if (this.RotateBone()) {
                this.phase = Phase.Exhaling;
                this.SetEase();
            }
        }

        void OnExhaling() {
            if (this.RotateBone()) {
                this.phase = Phase.Rest;
                this.restEndTime = Time.time + (this.restDuration * this.durationRate);
            }
        }

        void OnRest() {
            this.RotateBone();
            if (this.restEndTime <= Time.time) {
                this.phase = Phase.Inhaling;
                this.SetEase();
            }
        }

        /// <summary>
        /// Bone Rotate
        /// </summary>
        /// <returns>IsReadyToNextPhase</returns>
        bool RotateBone() {
            // Backup Shoulder(or UpperArm) rotation.
            var originLeftShoulderRotation = this.LeftShoulder.rotation;
            var originRightShoulderRotation = this.RightShoulder.rotation;

            // Rotate Spine, Cheast, Neck, Head
            int finishCnt = 0;
            for (int i = 0; i < this.Segments.Length; i++) {
                var seg = this.Segments[i];

                if (this.hasController) {
                    seg.transform.Rotate(new Vector3(
                        seg.x.UpdateEase(this.phase, this.InhalingMethod, this.durationRate),
                        seg.y.UpdateEase(this.phase, this.InhalingMethod, this.durationRate),
                        seg.z.UpdateEase(this.phase, this.InhalingMethod, this.durationRate)
                        ));
                } else {
                    var lastEaseValueX = seg.x.lastEaseValue;
                    var lastEaseValueY = seg.y.lastEaseValue;
                    var lastEaseValueZ = seg.z.lastEaseValue;
                    seg.transform.Rotate(new Vector3(
                        seg.x.UpdateEase(this.phase, this.InhalingMethod, this.durationRate) - lastEaseValueX,
                        seg.y.UpdateEase(this.phase, this.InhalingMethod, this.durationRate) - lastEaseValueY,
                        seg.z.UpdateEase(this.phase, this.InhalingMethod, this.durationRate) - lastEaseValueZ)
                        );
                }

                if (seg.x.IsFinishEase(this.durationRate) &&
                    seg.y.IsFinishEase(this.durationRate) &&
                    seg.z.IsFinishEase(this.durationRate)) {
                    finishCnt++;
                }
            }

            // Rotate Shoulder or UpperArm
            this.LeftShoulder.rotation = originLeftShoulderRotation;
            this.RightShoulder.rotation = originRightShoulderRotation;

            // return IsReadyToNextPhase
            return finishCnt >= Segments.Length;
        }

        void SetEase() {
            for (int i = 0; i < this.Segments.Length; i++) {
                var seg = this.Segments[i];
                if (this.phase == Phase.Inhaling) {
                    seg.x.SetEase(seg.x.lastEaseValue, (seg.x.max * this.effectRate) - seg.x.lastEaseValue, seg.x.maxDuration);
                    seg.y.SetEase(seg.y.lastEaseValue, (seg.y.max * this.effectRate) - seg.y.lastEaseValue, seg.y.maxDuration);
                    seg.z.SetEase(seg.z.lastEaseValue, (seg.z.max * this.effectRate) - seg.z.lastEaseValue, seg.z.maxDuration);
                    //Debug.Log("duration:" + seg.z.maxDuration);
                } else {
                    seg.x.SetEase(seg.x.lastEaseValue, (seg.x.min * this.effectRate) - seg.x.lastEaseValue, seg.x.minDuration);
                    seg.y.SetEase(seg.y.lastEaseValue, (seg.y.min * this.effectRate) - seg.y.lastEaseValue, seg.y.minDuration);
                    seg.z.SetEase(seg.z.lastEaseValue, (seg.z.min * this.effectRate) - seg.z.lastEaseValue, seg.z.minDuration);
                    //Debug.Log("duration:" + seg.z.minDuration);
                }
            }
        }

        [Header("Advanced Config")]
        public float maxDuration = BreathController.InitialDurationInhale;
        public float minDuration = BreathController.InitialDurationExhale;
        public float restDuration = BreathController.InitialDurationRest;

        public float SpineInhaleAngle = BreathController.InitialAngleSpineInhale;
        public float SpineExhaleAngle = BreathController.InitialAngleSpineExhale;
        public float ChestInhaleAngle = BreathController.InitialAngleChestInhale;
        public float ChestExhaleAngle = BreathController.InitialAngleChestExhale;
        public float NeckInhaleAngle = BreathController.InitialAngleNeckInhale;
        public float NeckExhaleAngle = BreathController.InitialAngleNeckExhale;
        public float HeadInhaleAngle = BreathController.InitialAngleHeadInhale;
        public float HeadExhaleAngle = BreathController.InitialAngleHeadExhale;
        public enum HalingMethod {
            EaseOutSine,
            EaseInOutQuad
        }
        public HalingMethod InhalingMethod = BreathController.InitialMethodInhale;

        void InitializeSegments(Animator anim) {
            this.Segments = new BreathController.Segment[4];
            BreathController.Segment seg;

            // spine
            seg = new BreathController.Segment();
            seg.Bone = HumanBodyBones.Spine;
            seg.transform = anim.GetBoneTransform(seg.Bone);
            this.Segments[0] = seg;

            // chest
            seg = new BreathController.Segment();
            seg.Bone = HumanBodyBones.Chest;
            seg.transform = anim.GetBoneTransform(seg.Bone);
            this.Segments[1] = seg;

            // neck
            seg = new BreathController.Segment();
            seg.Bone = HumanBodyBones.Neck;
            seg.transform = anim.GetBoneTransform(seg.Bone);
            this.Segments[2] = seg;

            // head
            seg = new BreathController.Segment();
            seg.Bone = HumanBodyBones.Head;
            seg.transform = anim.GetBoneTransform(seg.Bone);
            this.Segments[3] = seg;

            var originRotation = this.transform.rotation;
            this.transform.rotation = Quaternion.identity;

            InitAngleConfig(anim, this.Segments[0], this.SpineInhaleAngle, this.SpineExhaleAngle);
            InitAngleConfig(anim, this.Segments[1], this.ChestInhaleAngle, this.ChestExhaleAngle);
            InitAngleConfig(anim, this.Segments[2], this.NeckInhaleAngle, this.NeckExhaleAngle);
            InitAngleConfig(anim, this.Segments[3], this.HeadInhaleAngle, this.HeadExhaleAngle);

            this.transform.rotation = originRotation;
        }

        enum vect {forward, right, up};
        void InitAngleConfig(Animator anim, Segment segment, float inhaleAngle, float exhaleAngle) {
            var btra = anim.GetBoneTransform(segment.Bone);

            var forwardDot = Vector3.Dot(transform.right, transform.InverseTransformDirection(btra.forward));
            var rightDot = Vector3.Dot(transform.right, transform.InverseTransformDirection(btra.right));
            var upDot = Vector3.Dot(transform.right, transform.InverseTransformDirection(btra.up));

            //Debug.Log("---- " + this.gameObject.name + " (" + btra.gameObject.name + ")");
            //Debug.Log("Forward Dot:" + forwardDot);
            //Debug.Log("Right   Dot:" + rightDot);
            //Debug.Log("Up      Dot:" + upDot);

            float min = 1;
            vect bestvec = 0;
            float machv;

            machv = 1 - Mathf.Abs(forwardDot);
            if (machv < min) {
                bestvec = vect.forward;
                min = machv;
            }

            machv = 1 - Mathf.Abs(rightDot);
            if (machv < min) {
                bestvec = vect.right;
                min = machv;
            }

            machv = 1 - Mathf.Abs(upDot);
            if (machv < min) {
                bestvec = vect.up;
                min = machv;
            }

            switch (bestvec) {
                case vect.forward:
                    segment.z.max = inhaleAngle * Mathf.Sign(forwardDot);
                    segment.z.min = exhaleAngle * Mathf.Sign(forwardDot);
                    segment.z.maxDuration = this.maxDuration;
                    segment.z.minDuration = this.minDuration;
                    break;
                case vect.right:
                    segment.x.max = inhaleAngle * Mathf.Sign(rightDot);
                    segment.x.min = exhaleAngle * Mathf.Sign(rightDot);
                    segment.x.maxDuration = this.maxDuration;
                    segment.x.minDuration = this.minDuration;
                    break;
                case vect.up:
                    segment.y.max = inhaleAngle * Mathf.Sign(upDot);
                    segment.y.min = exhaleAngle * Mathf.Sign(upDot);
                    segment.y.maxDuration = this.maxDuration;
                    segment.y.minDuration = this.minDuration;
                    break;
            }
        }

        private void InitializeSoulders(Animator anim) {
            this.LeftShoulder = anim.GetBoneTransform(HumanBodyBones.LeftShoulder);
            this.RightShoulder = anim.GetBoneTransform(HumanBodyBones.RightShoulder);

            if (LeftShoulder == null)
                this.LeftShoulder = anim.GetBoneTransform(HumanBodyBones.LeftUpperArm);
            
            if (RightShoulder == null)
                this.RightShoulder = anim.GetBoneTransform(HumanBodyBones.RightUpperArm);
        }
    }
}