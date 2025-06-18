using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    public static Root instance;
    public  GameManager gameManager;
    public UIManager uiManager;
    public LevelManager levelManager;
    public SoundManager soundManager;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);          
        }
        else
        {
            instance = this;
        }
    }
   
}
