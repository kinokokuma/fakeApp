using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputID;
    [SerializeField]
    private TMP_Dropdown inputGame;
    [SerializeField]
    private GameObject instruction;

    public void Start()
    {
        if (UserData.Instance.IsTutorial)
        {
            instruction.SetActive(true);
        }
    }
    void Update()
    {
        if (!instruction.active)
        {
            UserData.Instance.ID = inputID.text;
            UserData.Instance.TaskIndex = inputGame.options[inputGame.value].text;
        }
    }

    public void LoadScene(int inedx)
    {
        SceneManager.LoadScene(inedx);
    }
    public void LoadObject(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void IsTutorial(bool isTutorial)
    {
            UserData.Instance.IsTutorial = isTutorial;
    }

}
