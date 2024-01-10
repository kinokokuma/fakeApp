using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectShopPopup : BasePopUp
{
    public Button go1;
    public Button go2;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        go1.onClick.AddListener(() => { back("Route1/story1-10-A");
            save("Select BagShop");
        });
        go2.onClick.AddListener(() => {
            back("Route1/story1-10-B");
            save("Select ForyouBag");
        });
    }

    public void back(string name)
    {
        manager.OpenChat(name);
        gameObject.SetActive(false);
    }
}
