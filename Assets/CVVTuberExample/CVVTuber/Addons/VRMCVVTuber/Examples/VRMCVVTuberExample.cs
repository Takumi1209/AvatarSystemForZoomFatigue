using CVVTuber;
using CVVTuber.VRM;
using SFB;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CVVTuberExample
{
    public class VRMCVVTuberExample : MonoBehaviour
    {
        public Button loadVRMButton;

        public RuntimeVRMMetaLoader runtimeVRMMetaLoader;

        [Space]

        public VRMCVVTuberControllManager vRMControllManager;

        /// <summary>
        /// The webcam texture mat source getter.
        /// </summary>
        public WebCamTextureMatSourceGetter webCamTextureMatSourceGetter;

        /// <summary>
        /// The dlib face landmark getter.
        /// </summary>
        public DlibFaceLandmarkGetter dlibFaceLandmarkGetter;

        // Use this for initialization
        void Start()
        {
            // Load global settings.
            dlibFaceLandmarkGetter.dlibShapePredictorFileName = CVVTuberExample.dlibShapePredictorFileName;
            dlibFaceLandmarkGetter.dlibShapePredictorMobileFileName = CVVTuberExample.dlibShapePredictorFileName;

            // The “Load VRM” button is only enabled on editor runs where the file selection dialog is available.
            // To enable this feature on other platforms, consider adding a plugin such as “UnityStandaloneFileBrowser”.
#if !UNITY_EDITOR
            if (loadVRMButton != null)
                loadVRMButton.interactable = false;
#endif
        }

        /// <summary>
        /// Raises the back button click event.
        /// </summary>
        public void OnBackButtonClick()
        {
            SceneManager.LoadScene("CVVTuberExample");
        }

        /// <summary>
        /// Raises the change camera button click event.
        /// </summary>
        public void OnChangeCameraButtonClick()
        {
            webCamTextureMatSourceGetter.ChangeCamera();
        }

        /// <summary>
        /// Raises the load VRM button click event.
        /// </summary>
        public void OnLoadVRMButtonClick()
        {
            StartCoroutine(LoadVRM());
        }

        private IEnumerator LoadVRM()
        {
            ExtensionFilter[] extensions = new[] {
               new ExtensionFilter("VRM Files", "vrm", "VRM"),
            };

            if (runtimeVRMMetaLoader != null)
            {
                var path = UnityEditor.EditorUtility.OpenFilePanel("Open VRM", "", "vrm");
                runtimeVRMMetaLoader.vrmFilePath = path;
                //var path = runtimeVRMMetaLoader.vrmFilePath;
                if (string.IsNullOrEmpty(path))
                {
                    yield break;
                }

                Debug.Log("VRM file path: " + path);

                yield return runtimeVRMMetaLoader.LoadVRMMetaAsync();

                if (vRMControllManager != null)
                {
                    vRMControllManager.loadVRMMeta(runtimeVRMMetaLoader.GetMeta());
                }
            }
        }
    }
}