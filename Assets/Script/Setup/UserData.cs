using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserData : MonoBehaviour
{
    public static string UserID;
    public static string Story;
    public static string Solution;
    public static string UserName;
    public static string UserSex;

    public static int Story1PostIndex;

    public Button next;
    public TMP_InputField inputID;
    public TMP_Dropdown dropdownStory;
    public TMP_Dropdown dropdownSolution;

    public Button nextPage;
    public TMP_InputField inputName;
    public TMP_Dropdown dropdownSex;

    public TMP_Text type;

    public GameObject User;
    public void Start()
    {
        next.onClick.AddListener(() => { User.SetActive(true); });
    }

    public void Update()
    {

        if (inputID.text == string.Empty)
        {
            next.interactable = false;
        }
        else
        {
            next.interactable = true;
        }
        UserID = inputID.text;
        Solution = dropdownSolution.captionText.text;
        Story = dropdownStory.captionText.text;
        type.text = Solution.Split('_')[1];
        if (inputName.text == string.Empty)
        {
            nextPage.interactable = false;
        }
        else
        {
            nextPage.interactable = true;
        }
        UserName = inputName.text;
        UserSex = dropdownSex.captionText.text;
        print(UserData.Story + " " + UserData.Solution + " " + UserData.UserID + " " + UserData.UserSex);
    }
}
