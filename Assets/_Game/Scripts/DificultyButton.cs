using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DificultyButton : MonoBehaviour
{
    Toggle toggle;
    public Vector2 CellSize;
    public int CellsNumber;
    public int ClmnsNumber;
    private void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }
    void OnToggleChanged(bool isOn)
    {
        if (isOn)
        {
            Root.instance.levelManager.CountModify(CellsNumber);
            Root.instance.levelManager.CreateCards();

        }
        else
        {
            
        }
    }

}
