using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject StartPanel;
    [SerializeField] GameObject PlayPanel;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public void Init()
    {
        StartPanel.SetActive(true);
        PlayPanel.SetActive(false);
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
}
