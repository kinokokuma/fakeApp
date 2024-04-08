using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageManager : MonoSingleton<ImageManager>
{
    private Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();
    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(1);
    }
    public Sprite LoadImage(string path)
    {
        print(path);
        if (!imageDic.ContainsKey(path))
        {
            var poseTexture = Resources.Load<Texture2D>(path);
            Sprite sprite = Sprite.Create(poseTexture, new Rect(0.0f, 0.0f, poseTexture.width, poseTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
            imageDic[path] = sprite;

            return sprite;
        }
        else
        {
            return imageDic[path];
        }
    }
}
