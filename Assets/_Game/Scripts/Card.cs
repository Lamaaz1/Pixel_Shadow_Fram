using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Image frontImage; // assign in Inspector
    public Image backImage;
    public bool isRevealed = false;
    Button mybtn;
    private void Start()
    {
        mybtn = GetComponent<Button>();
        mybtn.onClick.AddListener(OnCardClicked);
    }
    public void SetImage(Sprite sprite)
    {
        frontImage.sprite = sprite;
    }

    public void SetHidden()
    {
        frontImage.gameObject.SetActive(false);
        //backImage.gameObject.SetActive(true);
        isRevealed = false;
    }

    public void SetRevealed()
    {
        frontImage.gameObject.SetActive(true);
        isRevealed = true;
        //backImage.gameObject.SetActive(false);
    }
    // This is called when player clicks on the card
    public void OnCardClicked()
    {
        if (isRevealed) return; // already revealed → do nothing
        SetRevealed();

        // Tell the GameManager that this card was clicked
        Root.instance.gameManager.OnCardRevealed(this);
    }

}
