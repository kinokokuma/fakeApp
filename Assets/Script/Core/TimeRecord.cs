using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine.UI;

public class TimeRecord : MonoSingleton<TimeRecord>
{
    private string csv_name = "Observer_";
    private int taskNumber = 1;
    private string id = "1";
    private List<string[]> rowData = new List<string[]>();
    private string testType = "PreTest";

    public PopUpManager manager;

    public void Start()
    {
        csv_name += UserData.Instance.ID + "_" + UserData.Instance.TaskIndex;
        if (!UserData.Instance.IsTutorial)
        {
            CreatePlayerCsv(csv_name);
        }
    }

    public void AddTaskNumber()
    {
        taskNumber++;
    }


    public void CreatePlayerCsv(string name)
    {
        List<string[]> rowData = new List<string[]>();

        // Creating First row of titles manually..
        string[] rowDataTemp = new string[6];
        rowDataTemp[0] = "Date";
        rowDataTemp[1] = "TimeFromStart";
        rowDataTemp[2] = "TaskNumber";
        rowDataTemp[3] = "PostID";
        rowDataTemp[4] = "QuestionType";
        rowDataTemp[5] = "ClickTarget";
        rowData.Add(rowDataTemp);

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));

        StreamWriter outStream = System.IO.File.CreateText(getPath());
        outStream.Write(sb);
        outStream.Close();

    }

    private string[] rowDataTemp = new string[6];

    public void SaveRecord(string clickTarget)
    {
        if (!UserData.Instance.IsTutorial)
        {
            rowData = new List<string[]>();

            // Creating First row of titles manually..

            DateTime serverTime = DateTime.Now; // gives you current Time in server timeZone
            long unixTime = ((DateTimeOffset)serverTime).ToUnixTimeMilliseconds();

            rowDataTemp[0] = unixTime.ToString();
            rowDataTemp[1] = Time.time.ToString();
            rowDataTemp[2] = taskNumber.ToString();
            rowDataTemp[3] = manager.CurrentPostPopupData.PostImage;
            rowDataTemp[4] = manager.Phase.ToString();
            rowDataTemp[5] = clickTarget;
            rowData.Add(rowDataTemp);

            string[][] output = new string[rowData.Count][];

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = rowData[i];
            }

            int length = output.GetLength(0);
            string delimiter = ",";

            StringBuilder sb = new StringBuilder();

            for (int index = 0; index < length; index++)
                sb.AppendLine(string.Join(delimiter, output[index]));

            StreamWriter outStream = System.IO.File.AppendText(getPath());
            outStream.Write(sb);
            outStream.Close();
        }
    }
    
    // Following method is used to retrive the relative path as device platform
    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + csv_name + ".csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath + "/"+ csv_name + ".csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath + "/"+ csv_name + ".csv";
#else
        return "/"+ csv_name + ".csv";
#endif
        Debug.Log("get path leido");
    }
}
