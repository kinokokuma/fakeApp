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

    void Start()
    {
        button.onClick.AddListener(() => Page1.SetActive(true));
    }

    public void ShowPose(GameObject pose)
    {
        pose.SetActive(true);
        StartCoroutine(Back());

    }

    public void update()
    {
        if (index == 1)
        {
            print("xxxx");
        }
    }

    public IEnumerator Back()
    {
        yield return new WaitForSeconds(5);
        manager.OpenChat("Route1/story1-4");
        gameObject.SetActive(false);
        index++;
    }
}
