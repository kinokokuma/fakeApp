using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page1Popup : BasePopUp
{
    public string name;
    public RectTransform chatParent;
    public GameObject next;

    public void chatClick1()
    {
        manager.OpenChat(name);
        TimeRecord.Instance.SaveRecord(ID, "กลับแชท", startTime);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if ((int)chatParent.anchoredPosition.y >= - 20)
        {
            startTime = Time.time;
            next.gameObject.SetActive(true);

        }
    }
}
