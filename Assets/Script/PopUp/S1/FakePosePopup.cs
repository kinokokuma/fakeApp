using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePosePopup : BasePopUp
{
    // Start is called before the first frame update
    public GameObject comment;
    void Start()
    {
        StartCoroutine(Run(comment));

    }

    private IEnumerator Run(GameObject obj)
    {
        yield return new WaitForSeconds(5);
        startTime = Time.time;
        obj.SetActive(true);
    }

    public void chatClick1()
    {
        TimeRecord.Instance.SaveRecord(ID, "กลับแชท", startTime);
        manager.OpenChat("Route1/story1-15-A");
        gameObject.SetActive(false);
    }
}
