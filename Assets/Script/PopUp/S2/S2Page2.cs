using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S2Page2 : BasePopUp
{
    public string name;
    public Toggle[] allToggles;
    public GameObject submit;
    public void Start()
    {
        startTime = Time.time;
    }

    public void OpenCount(GameObject obj)
    {
        StartCoroutine(Count(obj));
    }

    IEnumerator Count(GameObject obj)
    {
        yield return new WaitForSeconds(3);
        obj.SetActive(true);
    }
   

    public void Back()
    {
        manager.OpenChat(name);
        TimeRecord.Instance.SaveRecord(ID, "กลับแชท", startTime);
        gameObject.SetActive(false);
    }

    public void Update()
    {
        int countT=0;
        foreach(Toggle t in allToggles)
        {
            if (t.isOn)
            {
                countT++;
            }
        }

        if (countT >= 7)
        {
            submit.SetActive(true);
        }
        else
        {
            submit.SetActive(false);
        }
    }
}
