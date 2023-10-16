using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChatData
{
    public string ID;
    public string OnwerName;
    public string Icon;
    public string ChatType;
    public float DelayTime;
    public string Content;
    public string PostImage;
    public ChoiceImage[] ChoiceImage;
    public string[] ChoiceString;
    public string LinkToPageName; 

}

[System.Serializable]
public class ChoiceImage
{
    public string ID;
    public string Path;
}
