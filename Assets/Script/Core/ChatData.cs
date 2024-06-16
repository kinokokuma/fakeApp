using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChatData
{
    public string ID;
    public string ChatName;
    public string[] Icon;
    public string Header;
    public ChatDataDetail[] DataDetail;

}

[System.Serializable]
public class ChatDataDetail
{
    public string ID;
    public string OnwerName;
    public string Icon;
    public string ChatType;
    public float DelayTime;
    public string Content;
    public string PostImage;
    public ChoiceImage[] ChoiceImage;
    public ChoiceText[] Choice;
    public string LinkToPageName;
    public string LinkType;
    public string FileName;
}

[System.Serializable]
public class ChoiceImage
{
    public string ID;
    public bool CanClick = true;
    public string Path;
    public string LinkType;
    public string FileName;
    public bool IsSignificant;
}

[System.Serializable]
public class ChoiceText
{
    public string ID;
    public string Path;
    public string LinkType;
    public string FileName;
    public bool IsSignificant;
}

