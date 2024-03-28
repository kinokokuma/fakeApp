using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selectlone : BasePopUp
{
    public string route1;
    public string route2;
    public string route1Name;
    public string route2Name;
    public Button go1;
    public Button go2;
    public GameObject[] next;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        go1.onClick.AddListener(() => {
            back(route1);
            save($"Select {route1Name}", true);
        });
        go2.onClick.AddListener(() => {
            back(route2);
            save($"Select {route2Name}", true);
        });
    }

    public void back(string name)
    {
        manager.OpenChat(name);
        gameObject.SetActive(false);
    }
}
