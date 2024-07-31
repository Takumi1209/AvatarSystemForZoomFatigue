using AddonScripts;
using Mebiustos.BreathController;
using System;
using System.Collections;
using UnityEngine;
using VRM;

namespace CVVTuber.VRM
{
    public class RuntimeVRMMetaLoader : MonoBehaviour
    {
        [Tooltip("Set the VRM file path, relative to the starting point of the \"StreamingAssets\" folder, or absolute path.")]
        public string vrmFilePath = "C:/Users/aise-member/Desktop/VRMAvatars";

        [Space(5)]

        GameObject m_model;

        public virtual bool isDone { get; protected set; }

        public virtual bool isError { get; protected set; }

        public virtual void Dispose()
        {
            isDone = isError = false;

            GameObject.Destroy(m_model);
        }

        public virtual VRMMeta GetMeta()
        {
            if (isDone == false || isError == true)
                return null;

            return m_model.GetComponent<VRMMeta>();
        }

        public virtual IEnumerator LoadVRMMetaAsync()
        {
            Uri uri;
            if (Uri.TryCreate(vrmFilePath, UriKind.Absolute, out uri))
            {
                ImportVRMAsync(uri.OriginalString);
            }
            else
            {
                yield return OpenCVForUnity.UnityUtils.Utils.getFilePathAsync(vrmFilePath, (result) =>
                {
                    ImportVRMAsync(result);
                });
            }

            while (!isDone)
            {
                yield return null;
            }
        }

        protected async void ImportVRMAsync(string path)
        {
            var loaded = await VrmUtility.LoadAsync(path);
            loaded.ShowMeshes();
            loaded.EnableUpdateWhenOffscreen();

            if (m_model != null)
            {
                GameObject.Destroy(m_model.gameObject);
            }

            m_model = loaded.gameObject;

            var meta = loaded.gameObject.GetComponent<VRMMeta>();
            if (meta == null)
            {
                isDone = isError = true;
            }
            else
            {
                var metaObject = meta.Meta;
                /*
                Debug.LogFormat("meta: title:{0}", metaObject.Title);
                Debug.LogFormat("meta: version:{0}", metaObject.Version);
                Debug.LogFormat("meta: author:{0}", metaObject.Author);
                Debug.LogFormat("meta: exporterVersion:{0}", metaObject.ExporterVersion);
                */
                isDone = true;
            }

            var animator = m_model.GetComponent<Animator>();
            var leftEye = animator.GetBoneTransform(HumanBodyBones.LeftEye);
            var rightEye = animator.GetBoneTransform(HumanBodyBones.RightEye);

            VRMBlendShapeProxy vrmBlendShapePloxy = m_model.GetComponent<VRMBlendShapeProxy>();
            VRMLookAtHead vrmLookAtHead = m_model.GetComponent<VRMLookAtHead>();

            //AutoMoving script Attach
            EyeJitter eyeJitter = m_model.AddComponent<EyeJitter>();
            eyeJitter.rightEye = rightEye;
            eyeJitter.leftEye = leftEye;

            vrmAutoController vrmautoController = m_model.AddComponent<vrmAutoController>();
            vrmautoController.blendShapeProxy = vrmBlendShapePloxy;
            vrmautoController.VRMLookAtHead = vrmLookAtHead;

            m_model.AddComponent<BreathController>();
        }
    }
}
