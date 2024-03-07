using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OldStoryChat : BasePopUp
{
    public GameObject read;
    public GameObject next;
    public TMP_Text text;
    void Start()
    {
        if (UserData.UserSex == "ชาย")
        {
            text.text += "คะ";
        }
        else
        {
            text.text += "ครับ";
        }
        StartCoroutine(count());
    }

    IEnumerator count()
    {
        yield return new WaitForSeconds(10);
        read.SetActive(false);
        startTime = Time.time;
        next.SetActive(true);
    }

    public void GotoChat()
    {
        save("เล่นแชทแฟน");
        manager.OpenChat("Route1/story1-4");
        gameObject.SetActive(false);
    }


}
