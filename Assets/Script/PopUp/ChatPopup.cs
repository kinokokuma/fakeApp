using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class ChatPopup : BasePopUp
{
    private const int chatSizeOffset = 285;
    private ChatData data;
    private int chatIndex;
    [SerializeField]
    private Button allChatButton;
    [SerializeField]
    private GameObject allChatObject;
    [SerializeField]
    private TMP_Text userInputText;
    [SerializeField]
    private TMP_Text headerName;
    [SerializeField]
    private Image[] headerIcon;
    [SerializeField]
    private Image[] headerIconMask;
    [SerializeField]
    private ChatObjectBase chatobject;
    [SerializeField]
    private RectTransform chatViewPoint;
    [SerializeField]
    private RectTransform chatParent;
    [SerializeField]
    private Transform imageChoiceParent;
    [SerializeField]
    private Transform QuestionObject;
    [SerializeField]
    private ChatChoice imageChoice;
    [SerializeField]
    private ScrollRect chatParentScrollRect;
    [SerializeField]
    private GameObject reloadObj;
    [SerializeField]
    private RectTransform bottomChat;

    [SerializeField]
    public List<ChatObjectBase> chatObject;

    private bool isReload;
    [SerializeField]
    private string ChatID = "";
    private List<ChatChoice> choiceList = new List<ChatChoice>();
    private float timeToShowQuestion;
    private bool isFirstTime = true;
    private int oldIndex = 0;

    public void Start()
    {
        chatObject = new List<ChatObjectBase>();
        allChatButton.onClick.AddListener(ShowChatList);
        //ReadData();
    }

    private void ShowChatList()
    {
        allChatObject.SetActive(!allChatObject.active);

        if (allChatObject.active)
        {
            bottomChat.sizeDelta = new Vector2(635, bottomChat.sizeDelta.y);
        }
        else
        {
            bottomChat.sizeDelta = new Vector2(1024, bottomChat.sizeDelta.y);
        }
    }



    public IEnumerator ShowChat(ChatData data)
    {
        oldIndex = 0;
        chatIndex = 0;
        this.data = data;
        int contextCharecterIndex = 0;

        if (isFirstTime)
        {
            print("xxxxxx");
            headerName.text = data.ChatName;
            print(data.Icon[0]);
            print(data.ChatName);
            if (data.Icon.Length >=2)
            {
                headerIcon[0].sprite = ImageManager.Instance.LoadImage(data.Icon[0]);
                headerIcon[1].sprite = ImageManager.Instance.LoadImage(data.Icon[1]);
            }
            else if(data.Icon != null)
            {
                headerIcon[1].sprite = ImageManager.Instance.LoadImage(data.Icon[0]);
                headerIconMask[0].gameObject.SetActive(false);
            }
            isFirstTime = false;
        }

        bool haveQuestion =false;

        while (chatIndex < data.DataDetail.Length)
        {
            if (oldIndex != chatIndex || oldIndex == 0)
            {
                if (data.DataDetail[chatIndex].ChoiceImage != null)
                {
                    if (data.DataDetail[chatIndex].ChoiceImage.Length > 0)
                    {
                        for (int i = 0; i < data.DataDetail[chatIndex].ChoiceImage.Length; i++)
                        {
                            timeToShowQuestion = Time.time;

                            ChatChoice choice = Instantiate(imageChoice, imageChoiceParent);
                            choice.gameObject.SetActive(true);
                            choice.InitializedImage(i, ChoiceType.Image, data.DataDetail[chatIndex].ChoiceImage[i]);
                            choice.Button.onClick.AddListener(() => OnClickImageChoice(choice.DataImage));
                            choiceList.Add(choice);
                        }
                        QuestionObject.gameObject.SetActive(true);
                        chatViewPoint.sizeDelta = new Vector2(chatViewPoint.sizeDelta.x, chatViewPoint.sizeDelta.y - chatSizeOffset);
                        haveQuestion = true;
                    }
                }
                else if (data.DataDetail[chatIndex].Choice != null)
                {
                    if (data.DataDetail[chatIndex].Choice.Length > 0)
                    {
                        for (int i = 0; i < data.DataDetail[chatIndex].Choice.Length; i++)
                        {
                            timeToShowQuestion = Time.time;

                            ChatChoice choice = Instantiate(imageChoice, imageChoiceParent);
                            choice.gameObject.SetActive(true);
                            choice.InitializedText(i, ChoiceType.String, data.DataDetail[chatIndex].Choice[i]);
                            choice.Button.onClick.AddListener(() => OnClickTextChoice(choice.DataText, data.DataDetail[data.DataDetail.Length - 1]));

                            choiceList.Add(choice);
                        }
                        QuestionObject.gameObject.SetActive(true);
                        chatViewPoint.sizeDelta = new Vector2(chatViewPoint.sizeDelta.x, chatViewPoint.sizeDelta.y - chatSizeOffset);
                        haveQuestion = true;
                    }
                }
                else
                {

                    if (data.DataDetail[chatIndex].OnwerName == "my" && data.DataDetail[chatIndex].DelayTime !=0)
                    {

                        if (oldIndex != chatIndex || oldIndex == 0)
                        {
                            for (contextCharecterIndex = 0; contextCharecterIndex < data.DataDetail[chatIndex].Content.Length; contextCharecterIndex++)
                            {
                                userInputText.text += data.DataDetail[chatIndex].Content[contextCharecterIndex];
                                yield return new WaitForSeconds(0.05f);
                            }
                            

                            ChatObjectBase chat = Instantiate(chatobject);
                            chat.gameObject.SetActive(true);
                            chat.Initialized(data.DataDetail[chatIndex], this, manager);
                            chatObject.Add(chat);
                            chat.gameObject.transform.SetParent(chatParent);
                            oldIndex = chatIndex;
                            userInputText.text = string.Empty;
                            Reload();
                        }
                    }
                    else
                    {
                        ChatObjectBase chat = Instantiate(chatobject, chatParent);
                        chat.gameObject.SetActive(true);
                        chat.Initialized(data.DataDetail[chatIndex], this, manager);
                        chatObject.Add(chat);
                        oldIndex = chatIndex;
                        oldIndex = chatIndex;
                        Reload();
                    }

                }
                StartCoroutine(UpdateLayoutGroup(reloadObj));
                Reload();





            }
                yield return new WaitForSeconds(data.DataDetail[chatIndex++].DelayTime);
        }

        if (!haveQuestion && data.DataDetail[chatIndex-1].ChatType == "Normal")
        {
            if (data.DataDetail[chatIndex - 1].LinkType == "chat")
            {
                manager.OpenChat(data.DataDetail[data.DataDetail.Length - 1].FileName);
            }
            else
            {
                manager.CreatePopup(data.DataDetail[data.DataDetail.Length - 1].FileName);
            }
        }
    }
    int countLoad = 0;

    private void Reload()
    {
      /*  print((int)chatParent.sizeDelta.y + " sizr " + (int)chatParent.anchoredPosition.y);
        if ((int)chatParent.sizeDelta.y >= (int)chatParent.anchoredPosition.y)
        {
            chatParent.anchoredPosition = new Vector2(chatParent.anchoredPosition.x, chatParent.sizeDelta.y);
        }*/
    }

    private void LateUpdate()
    {

        if (chatParent.sizeDelta.y <= 1134)
        {
            chatParent.pivot = new Vector2(chatParent.pivot.x, 1);
        }
        else
        {
            chatParent.pivot = new Vector2(chatParent.pivot.x, 0);
        }
       /* if (isReload)
        {
            chatParent.anchoredPosition = new Vector2(chatParent.anchoredPosition.x, chatParent.sizeDelta.y);
            countLoad++;
            //if (countLoad > 4)
            {
                isReload = false;
                countLoad = 0;
            }
        }*/
    }

    private void OnClickTextChoice(ChoiceText choiceText, ChatDataDetail data)
    {
        HintChoice();
        ChatObjectBase chat = Instantiate(chatobject, chatParent);
        chat.gameObject.SetActive(true);
        ChatDataDetail dataDetail = new ChatDataDetail();
        chatObject.Add(chat);
        dataDetail.OnwerName = "my";
        dataDetail.Icon = string.Empty;
        dataDetail.Content = data.Content.Replace("{0}", choiceText.Path);
        dataDetail.PostImage = string.Empty;
        dataDetail.LinkType = choiceText.LinkType;
        dataDetail.FileName = choiceText.FileName;

        TimeRecord.Instance.SaveRecord(choiceText.ID, choiceText.Path, timeToShowQuestion);

        chat.Initialized(dataDetail, this, manager);
        QuestionObject.gameObject.SetActive(false);


        chatViewPoint.sizeDelta = new Vector2(chatViewPoint.sizeDelta.x, chatViewPoint.sizeDelta.y + chatSizeOffset);

        if ((int)chatParent.sizeDelta.y >= (int)chatParent.anchoredPosition.y)
        {
            isReload = true;
        }
        Canvas.ForceUpdateCanvases();
        print(dataDetail.FileName);
        if (choiceText.LinkType == "chat")
        {
            manager.OpenChat(dataDetail.FileName);
        }
        else if(choiceText.LinkType != null)
        {
            manager.CreatePopup(choiceText.FileName);
        }
        StartCoroutine(UpdateLayoutGroup(reloadObj));
    }

    private void HintChoice()
    {
        foreach(var choice in choiceList)
        {
            choice.gameObject.SetActive(false);
        }
    }

    private void OnClickImageChoice(ChoiceImage choiceImage)
    {
        HintChoice();
        ChatObjectBase chat = Instantiate(chatobject, chatParent);
        chat.gameObject.SetActive(true);
        ChatDataDetail dataDetail = new ChatDataDetail();
        chatObject.Add(chat);
        dataDetail.OnwerName = "my";
        dataDetail.Icon = string.Empty;
        dataDetail.Content = string.Empty;
        dataDetail.PostImage = choiceImage.Path;
        dataDetail.LinkType = choiceImage.LinkType;
        dataDetail.FileName = choiceImage.FileName;
        chat.Initialized(dataDetail, this, manager);
        QuestionObject.gameObject.SetActive(false);

        TimeRecord.Instance.SaveRecord(choiceImage.ID, Path.GetFileName(choiceImage.Path), timeToShowQuestion);

        chatIndex = 0;

        chatViewPoint.sizeDelta = new Vector2(chatViewPoint.sizeDelta.x, chatViewPoint.sizeDelta.y + chatSizeOffset);

        if ((int)chatParent.sizeDelta.y >= (int)chatParent.anchoredPosition.y)
        {
            isReload = true;
        }
        Canvas.ForceUpdateCanvases();
        manager.OpenChat(choiceImage.FileName);
        StartCoroutine(UpdateLayoutGroup(reloadObj));
    }

    public void ClearChat()
    {
        print("Button");
        if (chatObject.Count > 0)
        {
            print("Button");
            foreach (var chat in chatObject)
            {
                Destroy(chat.gameObject);
            }
            chatObject.Clear();
        }
    }
}
