using SFB;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageFileDialog : MonoBehaviour
{
    public RawImage displayImage;  // UI element to display the image
    public string defaultDirectory = "C:/Users/aise-member/Desktop/BackgroundImages";

    // Function to open the file dialog
    public void OpenFileDialog()
    {
        var extensions = new[] {
            new ExtensionFilter("Image Files", "png", "jpg"),
            new ExtensionFilter("All Files", "*" ),
        };

        // Open the file dialog and get the selected file path(s)
        var paths = StandaloneFileBrowser.OpenFilePanel("Open Image File", defaultDirectory, extensions, false);


        if (paths.Length > 0)
        {
            string path = paths[0];
            LoadAndDisplayImage(path);
        }
    }

    // Function to load and display the selected image
    private void LoadAndDisplayImage(string path)
    {
        if (File.Exists(path))
        {
            byte[] fileData = File.ReadAllBytes(path);
            Texture2D tex = new Texture2D(2, 2);
            if (tex.LoadImage(fileData))
            {
                // Create a sprite from the texture
                Sprite newSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                SpriteManager.Instance.SetSelectedSprite(newSprite);

                // Adjust the RawImage size to match the texture's aspect ratio
                displayImage.texture = tex;
                RectTransform rt = displayImage.GetComponent<RectTransform>();
                float aspectRatio = (float)tex.width / tex.height;

                // Assume a maximum width and height for the RawImage
                float maxWidth = 100f;
                float maxHeight = 80f;

                if (aspectRatio > 1) // Wider than tall
                {
                    rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxWidth);
                    rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, maxWidth / aspectRatio);
                }
                else // Taller than wide
                {
                    rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxHeight * aspectRatio);
                    rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, maxHeight);
                }

                // Optionally, load the next scene
                // SceneManager.LoadScene("NextSceneName"); // Replace "NextSceneName" with the name of your target scene
            }
            else
            {
                Debug.LogError("Failed to load texture from " + path);
            }
        }
        else
        {
            Debug.LogError("File does not exist at " + path);
        }
    }
}
