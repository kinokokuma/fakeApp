using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserData : MonoSingleton<UserData>
{
    public string ID;
    public string TaskIndex;
    public bool IsTutorial;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(1);
    }

}
