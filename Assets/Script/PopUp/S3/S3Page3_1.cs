using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class S3Page3_1 : BasePopUp
{
    public RectTransform pos;
    public float[] posAll;
    public GameObject[] click;
    public int index;
    public ScrollRect scroll;

    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        print(pos.position.y);
        if(posAll[index] <= pos.position.y)
        {
            click[index].SetActive(true);
            scroll.vertical = false;
        }
    }

    public void ReadComment(GameObject obj)
    {
        obj.SetActive(false);
        scroll.vertical = true;
        index++;
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

    public void end(GameObject obj)
    {
        TimeRecord.Instance.SaveRecord(ID, "จบเกม", startTime);
        obj.SetActive(true);
    }

    public void BackHome()
    {
        SceneManager.LoadScene(1);
    }
}
