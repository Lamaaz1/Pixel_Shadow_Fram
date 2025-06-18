using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DificultyButton : MonoBehaviour
{
    Toggle toggle;
    public int LevelIndex;
    public int CellsNumber;
    
    private void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }
    void OnToggleChanged(bool isOn)
    {
        if (isOn)
        {
            LoadL();
            PlayerPrefs.SetInt("CurrentLevel", LevelIndex);

        }
        else
        {
            
        }
    }
    public void LoadL()
    {
        Root.instance.levelManager.CountModify(CellsNumber);
        Root.instance.levelManager.CreateCards();
    }
}
