using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject StartPanel;
    [SerializeField] GameObject PlayPanel;
    [SerializeField] TextMeshProUGUI MatchesText;
    [SerializeField] int MatchesNumber;
    [SerializeField] TextMeshProUGUI TurnsText;
    [SerializeField] int TurnsNumber;
        
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public void Init()
    {
        StartPanel.SetActive(true);
        PlayPanel.SetActive(false);
        ResetNumbers();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    public void ResetNumbers()
    {
        MatchesNumber = 0;
        MatchesText.text=MatchesNumber.ToString();
        TurnsNumber = 0;
        TurnsText.text = TurnsNumber.ToString();

    }
    public void AddMatch()
    {
        MatchesNumber++;
        MatchesText.text = MatchesNumber.ToString();

    } 
    public void AddTurn()
    {
        TurnsNumber++;
        TurnsText.text = TurnsNumber.ToString();

    }
}
