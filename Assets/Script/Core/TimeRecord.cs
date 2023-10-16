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
        CreatePlayerCsv(csv_name);
    }

    public void AddTaskNumber()
    {
        taskNumber++;
    }

   /* void Confirm()
    {
        seen.SetActive(false);
        Back(true);
        taskNumber++;
        manager.Confirm();
        manager.SetPhase(QuestionPhase.Is_Fake);
    }

    void Back(bool ok)
    {
        select.SetActive(ok);
        
        score.SetActive(!ok);
    }*/

    public void CreatePlayerCsv(string name)
    {
        List<string[]> rowData = new List<string[]>();

        // Creating First row of titles manually..
        string[] rowDataTemp = new string[9];
        rowDataTemp[0] = "Date";
        rowDataTemp[1] = "TimeFromStart";
        rowDataTemp[2] = "TaskNumber";
        rowDataTemp[3] = "PostID";
        rowDataTemp[4] = "ClickTarget";
        rowDataTemp[5] = "QuestionType";
        rowDataTemp[6] = "TestType";
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


        string filePath = Application.dataPath + "/CSV/" + csv_name + "test" + ".csv";

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.Write(sb);
        outStream.Close();

    }

    private string[] rowDataTemp = new string[7];

    public void SaveRecord(string clickTarget)
    {
        rowData = new List<string[]>();

        // Creating First row of titles manually..

        DateTime serverTime = DateTime.Now; // gives you current Time in server timeZone
        long unixTime = ((DateTimeOffset)serverTime).ToUnixTimeMilliseconds();

        rowDataTemp[0] = unixTime.ToString();
        rowDataTemp[1] = Time.time.ToString();
        rowDataTemp[2] = taskNumber.ToString();
        rowDataTemp[3] = manager.CurrentPostPopupData.ID;
        rowDataTemp[4] = clickTarget;
        rowDataTemp[5] = manager.Phase.ToString();
        rowDataTemp[6] = testType;
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


        string filePath = Application.dataPath + "/CSV/" + csv_name + "test" + ".csv";

        StreamWriter outStream = System.IO.File.AppendText(filePath);
        outStream.Write(sb);
        outStream.Close();

    }




    // Following method is used to retrive the relative path as device platform
    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + csv_name + "test" + ".csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath + "/Saved_data.csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath + "/"+"Saved_data.csv";
#else
        return "/"+"Saved_data.csv";
#endif
        Debug.Log("get path leido");
    }
}
