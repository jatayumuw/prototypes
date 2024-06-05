using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;

public class ReloadTexture : MonoBehaviour
{
    string path;
    public MeshRenderer mRenderer;
    public bool canLoadTexture;

    private void Awake()
    {
        path = @"D:\PycharmProjects\RnD_ProjectHalo\Data\gakki.jpg";
        UpdateImage();

        canLoadTexture = false;
    }

    void Update()
    {
        if (canLoadTexture)
        {
            Debug.Log("!? Refreshing File Path");
            path = @"D:\PycharmProjects\RnD_ProjectHalo\Data\_userSelfie_unwrapped.jpg";
            UpdateImage();
        }
    }

    public void TextureLoader()
    {
        Debug.Log("!? Texture Loaded");
        canLoadTexture = true;
    }

    void UpdateImage()
    {
        if (path != null)
        {
            byte[] imgByte = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imgByte);

            mRenderer.material.mainTexture = texture;

            Debug.Log("!? Image is Updated: {}");
            Debug.Log($"!? Texture Image Using: {path}");

            canLoadTexture = false;
        }
    }

}
