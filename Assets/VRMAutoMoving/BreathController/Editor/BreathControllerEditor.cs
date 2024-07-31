using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;

/**
BreathController

Copyright (c) 2015 Toshiaki Aizawa (https://twitter.com/xflutexx)

This software is released under the MIT License.
 http://opensource.org/licenses/mit-license.php …
*/
namespace Mebiustos.BreathController {
    [CustomEditor(typeof(BreathController))]
    public class BreathControllerEditor : Editor {
        [System.Obsolete]
        public override void OnInspectorGUI() {
            var breath = (BreathController)target;
            bool change = false;

            breath.durationRate = EditorGUILayout.FloatField("Duration Rate", breath.durationRate);
            if (breath.durationRate < 0) breath.durationRate = 0;
            breath.effectRate = EditorGUILayout.FloatField("Effect Rate", breath.effectRate);
            if (breath.effectRate < 0) breath.effectRate = 0;

            ///System.Environment.NewLine
            float cycledurate = (breath.maxDuration + breath.minDuration + breath.restDuration) * breath.durationRate;
            var cycleInfo = new StringBuilder().AppendLine("BREATH SPEED").AppendFormat("{0} cycle / min  ( {1} sec / cycle )", (60f / cycledurate).ToString("F2"), cycledurate.ToString("F2")).ToString();
            EditorGUILayout.HelpBox(cycleInfo, MessageType.Info);

            var anim = breath.GetComponent<Animator>();
            if (anim == null || anim.runtimeAnimatorController == null)
                EditorGUILayout.HelpBox("Not found 'Animator Controller'.", MessageType.Warning);

            change |= GUI.changed;
            change |= AdvancedConfig(breath);

            if (change) {
                EditorUtility.SetDirty(target);
                EditorApplication.MarkSceneDirty();
            }
        }

        bool isFoldoutAdv = false;
        bool isFoldoutAdvDurtion = false;
        bool isFoldoutAdvAngle = false;
        bool isFoldoutAdvEtc = false;
        bool AdvancedConfig(BreathController breath) {
            bool change = false;
            EditorGUI.indentLevel++;
            this.isFoldoutAdv = EditorGUILayout.Foldout(this.isFoldoutAdv, "Advanced Config");
            if (this.isFoldoutAdv) {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Restore default advanced configs", GUILayout.ExpandWidth(false))) {
                    if (EditorUtility.DisplayDialog(
                        "Restore advanced configs?",
                        "Are you sure you want to restore default advanced configs ?",
                        "Restore", "Cancel")) {
                            breath.maxDuration = BreathController.InitialDurationInhale;
                            breath.minDuration = BreathController.InitialDurationExhale;
                            breath.restDuration = BreathController.InitialDurationRest;

                            breath.SpineInhaleAngle = BreathController.InitialAngleSpineInhale;
                            breath.SpineExhaleAngle = BreathController.InitialAngleSpineExhale;
                            breath.ChestInhaleAngle = BreathController.InitialAngleChestInhale;
                            breath.ChestExhaleAngle = BreathController.InitialAngleChestExhale;
                            breath.NeckInhaleAngle = BreathController.InitialAngleNeckInhale;
                            breath.NeckExhaleAngle = BreathController.InitialAngleNeckExhale;
                            breath.HeadInhaleAngle = BreathController.InitialAngleHeadInhale;
                        
                            breath.HeadExhaleAngle = BreathController.InitialAngleHeadExhale;

                            change = true;
                            EditorGUI.FocusTextInControl(null);
                            EditorUtility.DisplayDialog("Restore Completed", "Restore Completed.", "OK");
                    }
                }
                GUILayout.EndHorizontal();

                EditorGUI.indentLevel++;
                this.isFoldoutAdvDurtion = EditorGUILayout.Foldout(this.isFoldoutAdvDurtion, "Duration (sec)");
                if (this.isFoldoutAdvDurtion) {
                    GUI.changed = false;
                    EditorGUI.indentLevel++;
                    breath.maxDuration = EditorGUILayout.FloatField("Inhale", breath.maxDuration);
                    breath.minDuration = EditorGUILayout.FloatField("Exhale", breath.minDuration);
                    breath.restDuration = EditorGUILayout.FloatField("Rest", breath.restDuration);
                    EditorGUI.indentLevel--;
                    change |= GUI.changed;
                }
                this.isFoldoutAdvAngle = EditorGUILayout.Foldout(this.isFoldoutAdvAngle, "Angle");
                if (this.isFoldoutAdvAngle) {
                    GUI.changed = false;
                    EditorGUI.indentLevel++;
                    breath.SpineInhaleAngle = EditorGUILayout.FloatField("Spine Inhale", breath.SpineInhaleAngle);
                    breath.SpineExhaleAngle = EditorGUILayout.FloatField("Spine Exhale", breath.SpineExhaleAngle);
                    breath.ChestInhaleAngle = EditorGUILayout.FloatField("Chest Inhale", breath.ChestInhaleAngle);
                    breath.ChestExhaleAngle = EditorGUILayout.FloatField("Chest Exhale", breath.ChestExhaleAngle);
                    breath.NeckInhaleAngle = EditorGUILayout.FloatField("Neck Inhale", breath.NeckInhaleAngle);
                    breath.NeckExhaleAngle = EditorGUILayout.FloatField("Neck Exhale", breath.NeckExhaleAngle);
                    breath.HeadInhaleAngle = EditorGUILayout.FloatField("Head Inhale", breath.HeadInhaleAngle);
                    breath.HeadExhaleAngle = EditorGUILayout.FloatField("Head Exhale", breath.HeadExhaleAngle);
                    float inTotalAngle = breath.SpineInhaleAngle + breath.ChestInhaleAngle + breath.NeckInhaleAngle + breath.HeadInhaleAngle;
                    float exTotalAngle = breath.SpineExhaleAngle + breath.ChestExhaleAngle + breath.NeckExhaleAngle + breath.HeadExhaleAngle;
                    var angleInfo = new StringBuilder()
                        .AppendFormat("Inhale final head angle : origin {0} {1}", Mathf.Sign(inTotalAngle) != -1 ? "+" : "-", Mathf.Abs(inTotalAngle).ToString("F2")).AppendLine()
                        .AppendFormat("Exhale final head angle : origin {0} {1}", Mathf.Sign(exTotalAngle) != -1 ? "+" : "-", Mathf.Abs(exTotalAngle).ToString("F2")).ToString();
                    EditorGUILayout.HelpBox(angleInfo, MessageType.Info);
                    EditorGUI.indentLevel--;
                    change |= GUI.changed;
                }
                this.isFoldoutAdvEtc = EditorGUILayout.Foldout(this.isFoldoutAdvEtc, "Etc");
                if (this.isFoldoutAdvEtc) {
                    GUI.changed = false;
                    EditorGUI.indentLevel++;
                    breath.InhalingMethod = (BreathController.HalingMethod)EditorGUILayout.EnumPopup("Inhale Method", breath.InhalingMethod);
                    EditorGUI.indentLevel--;
                    change |= GUI.changed;
                }
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
            return change;
        }

        // Debug angle
        void xyz(BreathController.Angle angle) {
            angle.max = EditorGUILayout.FloatField("Max", angle.max);
            angle.min = EditorGUILayout.FloatField("Min", angle.min);
            angle.maxDuration = EditorGUILayout.FloatField("Max Duration", angle.maxDuration);
            angle.minDuration = EditorGUILayout.FloatField("Min Duration", angle.minDuration);

            var range = Mathf.Abs(angle.max - angle.min);
            var halfRange = range / 2f;

            range = EditorGUILayout.Slider("range", range, -45, +45);

            if (angle.max > angle.min) {
                var bias = -halfRange + angle.max;
                bias = EditorGUILayout.Slider("bias", bias, -45, +45);
                angle.max = (bias + halfRange);
                angle.min = (bias - halfRange);
                EditorGUILayout.LabelField("range:" + range + " halfRange:" + halfRange + " bias: " + bias + " max:" + (bias + halfRange) + " min:" + (bias - halfRange));
            } else {
                var bias = halfRange + angle.max;
                bias = EditorGUILayout.Slider("bias", bias, -45, +45);
                angle.max = (bias - halfRange);
                angle.min = (bias + halfRange);
                EditorGUILayout.LabelField("range:" + range + " halfRange:" + halfRange + " bias: " + bias + " max:" + (-bias + halfRange) + " min:" + (bias + halfRange));
            }
        }
    }
}