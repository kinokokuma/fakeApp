using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ChatNormal : ChatObjectBase
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Image iconMask;
    [SerializeField]
    private Image postImage;
    [SerializeField]
    private AspectRatioFitter postImageRatio;
    [SerializeField]
    private GameObject nameParent;
    [SerializeField]
    private TMP_Text name;
    [SerializeField]
    private Image contentParent;
    [SerializeField]
    private TMP_Text content;
    [SerializeField]
    private ContentSizeFitter contentSize;
    [SerializeField]
    private Button button;
    [SerializeField]
    private HorizontalLayoutGroup layOutGroup;
    [SerializeField]
    private GameObject guildLineTag;
    [SerializeField]
    private TMP_Text time;

    public GameObject textImage;
    public TMP_Text URLText;
    public Image URLImage;
    public AspectRatioFitter URLImageRatio;

    public override void Initialized(ChatDataDetail data, ChatPopup chatPopup, PopUpManager manager, bool muteSound)
    {
        base.Initialized(data, chatPopup, manager, muteSound);
        if (data.Icon != string.Empty)
        {
            iconMask.color = new Color32(255,255,255,255);
            print(data.Icon);
            icon.sprite = ImageManager.Instance.LoadImage(data.Icon);
        }
        else
        {
            iconMask.color = new Color32(255, 255, 255, 0);
        }

        

        if (data.OnwerName == "my")
        {
            //content.alignment = TextAlignmentOptions.MidlineRight;
            layOutGroup.childAlignment = TextAnchor.UpperRight;
            contentParent.color = new Color32(85, 144, 255,255);
            if (!muteSound)
            {
                SoundManager.Instance.PlaySound(SoundID.chatPop, 0.5f);
            }
        }
        else
        {
            if (!muteSound)
            {
                SoundManager.Instance.PlaySound(SoundID.chatPop);
            }
        }

      

        if(data.OnwerName != string.Empty && data.OnwerName!="my")
        {
            nameParent.SetActive(true);
            name.text = data.OnwerName;
        }
        else
        {
            nameParent.SetActive(false);
        }

      

        if(data.ChatType == "Button")
        {
            button.enabled = data.ChatType == "Button-N"? false:true;
            button.onClick.AddListener(() => ChatButton());
            guildLineTag.SetActive(true);
            postImage.gameObject.SetActive(false);
            contentParent.gameObject.SetActive(false);
            textImage.SetActive(true);
            URLText.text = data.Content;
            URLImage.sprite = ImageManager.Instance.LoadImage(data.PostImage);
            if ((float)URLImage.sprite.texture.width / URLImage.sprite.texture.height > 0)
            {

                print((float)URLImage.sprite.texture.width / (float)URLImage.sprite.texture.height);
                URLImageRatio.aspectRatio = (float)URLImage.sprite.texture.width / (float)URLImage.sprite.texture.height;
            }
        }
        else if (data.ChatType == "Button-N")
        {
            textImage.SetActive(true);
            URLText.text = data.Content;
            URLImage.sprite = ImageManager.Instance.LoadImage(data.PostImage);
            if ((float)URLImage.sprite.texture.width / URLImage.sprite.texture.height > 0)
            {
                print((float)URLImage.sprite.texture.width / (float)URLImage.sprite.texture.height);
                URLImageRatio.aspectRatio = (float)URLImage.sprite.texture.width / (float)URLImage.sprite.texture.height;
            }
        }
        else if (data.ChatType == "Time")
        {
            icon.gameObject.SetActive(false);
            contentParent.gameObject.SetActive(false);
            postImage.gameObject.SetActive(false);
            time.gameObject.SetActive(true);
            time.text = data.Content;
            muteSound = true;
        }
        else
        {
            if (data.PostImage != string.Empty)
            {
                postImage.gameObject.SetActive(true);
                postImage.sprite = ImageManager.Instance.LoadImage(data.PostImage);

                if ((float)postImage.sprite.texture.width / postImage.sprite.texture.height > 0)
                {
                    postImageRatio.aspectRatio = (float)postImage.sprite.texture.width / postImage.sprite.texture.height;
                }

                if (data.PostImage == "Image/Like")
                {
                    postImage.rectTransform.sizeDelta = postImage.rectTransform.sizeDelta / 3.0f;
                }
            }
            else
            {
                postImage.gameObject.SetActive(false);
            }

            if (data.Content == string.Empty)
            {
                contentParent.gameObject.SetActive(false);
            }
            else
            {
                contentParent.gameObject.SetActive(true);
                if (data.Content.Length < 45)
                {
                    contentSize.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                }
                else
                {
                    contentSize.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                }
                string text = data.Content.Replace("{Player}", UserData.UserName);
                content.text = text;
            }
        }

        //StartCoroutine(UpdateLayoutGroup(,2));
    }


    private void ChatButton()
    {

        if (data.LinkType == "page")
        {
            chatPopup.gameObject.SetActive(false);
        }
        else if(data.LinkType == "chat")
        {

        }
        else
        {
            manager.CreatePopup(data.FileName);
        }
        TimeRecord.Instance.SaveRecord(data.ID,$"go to ID : {data.FileName}", timeFromStart);
        button.enabled = false;
        guildLineTag.SetActive(false);
    }
}
