using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectShopPopup : BasePopUp
{
    public string route = "Route1";
    public Button go1;
    public Button go2;
    public GameObject[] next;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        go1.onClick.AddListener(() => { back($"{route}/story1-10-A");
            save("Select BagShop");
        });
        go2.onClick.AddListener(() => {
            back($"{route}/story1-10-B");
            save("Select ForyouBag");
        });
    }

    public void back(string name)
    {
        manager.OpenChat(name);
        gameObject.SetActive(false);
    }
}
