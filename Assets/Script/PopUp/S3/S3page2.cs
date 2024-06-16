using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S3page2 : BasePopUp
{
    public string name;
    public RectTransform chatParent;
    public GameObject Choice, next;

    public void Start()
    {
        startTime = Time.time;
        StartCoroutine(StartChoice());
    }

    IEnumerator StartChoice()
    {
        yield return new WaitForSeconds(3);
        startTime = Time.time;
        Choice.SetActive(true);
    }

    public void ClickChoice(int index)
    {
        TimeRecord.Instance.SaveRecord(ID, "เลือก Choice No:" + index, startTime,true);
    }

    public void Back(string chatID)
    {
        manager.OpenChat(chatID);
        TimeRecord.Instance.SaveRecord(chatID, "กลับแชท", startTime);
        gameObject.SetActive(false);
    }


    IEnumerator ShowBack()
    {
        yield return new WaitForSeconds(3);
        startTime = Time.time;
       // next.gameObject.SetActive(true);
    }

    public void Open(GameObject obj)
    {
        obj.SetActive(true);
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
}
