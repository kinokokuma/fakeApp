using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelOfConfidentButton : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private Image image;
    private int index;

    public int Index => index;
    public Image Image => image;
    public Button Button => button;

    public void Initialized(int index)
    {
        this.index = index;
    }
    
}
