using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Group1Popup : BasePopUp
{
    [SerializeField]
    private GameObject Page1;
    [SerializeField]
    private Button button;
    private int index = 0;
    private float startTime;

    public GameObject[] male;
    public GameObject[] female;
    public GameObject[] backbutt;
    void Start()
    {
        startTime = Time.time;
        button.onClick.AddListener(() => {
            Page1.SetActive(true);
            TimeRecord.Instance.SaveRecord("Group-1", "กดเพื่อโพสต์", startTime);
            startTime = Time.time;
        }
        );

        if(UserData.UserSex == "ชาย")
        {
            foreach (GameObject obj in male)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in female)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject obj in male)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in female)
            {
                obj.SetActive(true);
            }
        }
    }

    public void saveChoiceRecord(string comment)
    {
        TimeRecord.Instance.SaveRecord("Group-1", comment, startTime);
    }

    public void ShowPose(GameObject pose)
    {
        pose.SetActive(true);
        StartCoroutine(CountToshowBack());
    }

    IEnumerator CountToshowBack()
    {
        yield return new WaitForSeconds(3);

        foreach (GameObject obj in backbutt)
        {
            obj.SetActive(true);
        }
    }

    public void SaveIndex(int index)
    {
        UserData.Story1PostIndex = index;
    }

    public void update()
    {
        if (index == 1)
        {
            print("xxxx");
        }
    }

    public void Back()
    {

        //manager.OpenChat("Route1/story1-4");
        manager.CreatePopup("oldchat");
        gameObject.SetActive(false);
        index++;
    }
}
