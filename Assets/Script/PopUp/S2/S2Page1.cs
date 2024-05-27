using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2Page1 : BasePopUp
{
    public string name;
    public RectTransform chatParent;
    public GameObject Choice,next;

    public void Start()
    {
        startTime = Time.time;
        //StartCoroutine(StartChoice());
    }
    
    IEnumerator StartChoice()
    {
        yield return new WaitForSeconds(3);
        startTime = Time.time;
        Choice.SetActive(true);
    }

    public void ClickChoice(GameObject index)
    {
        TimeRecord.Instance.SaveRecord(ID, "เลือก Choice No:"+index.name, startTime);
        Choice.SetActive(false);
        index.SetActive(true);
        StartCoroutine(ShowBack());
    }

    public void Back()
    {
        manager.OpenChat(name);
        TimeRecord.Instance.SaveRecord(ID, "กลับแชท", startTime);
        gameObject.SetActive(false);
    }


    IEnumerator ShowBack()
    {
        yield return new WaitForSeconds(3);
        startTime = Time.time;
        next.gameObject.SetActive(true);
    }
}
