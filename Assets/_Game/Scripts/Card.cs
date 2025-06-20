using DG.Tweening;
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
        Root.instance.soundManager.PlayFlipSound();
        StartCoroutine(FlipCoroutine());
       
    }
    IEnumerator FlipCoroutine()
    {
        // Step 1: Scale X → shrink (flip start)
        float duration = 0.2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float scale = Mathf.Lerp(1f, 0f, elapsed / duration);
            transform.localScale = new Vector3(scale, 1f, 1f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Set scale to 0 exactly
        transform.localScale = new Vector3(0f, 1f, 1f);

        // Step 2: Change image (reveal)
        frontImage.gameObject.SetActive(true);
        isRevealed = true;
        //backImage.gameObject.SetActive(false);

        // Step 3: Scale X → grow back
        elapsed = 0f;
        while (elapsed < duration)
        {
            float scale = Mathf.Lerp(0f, 1f, elapsed / duration);
            transform.localScale = new Vector3(scale, 1f, 1f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Final scale 1
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
    // This is called when player clicks on the card
    public void OnCardClicked()
    {
        if (isRevealed) return; // already revealed → do nothing
        if (!Root.instance.gameManager.StartPlay) return; // already revealed → do nothing
        SetRevealed();

        // Tell the GameManager that this card was clicked
        Root.instance.gameManager.OnCardRevealed(this);
        //addTurn
        Root.instance.uiManager.AddTurn();
    }
   

    public void matched()
    {
        gameObject.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), .4f).OnComplete(() =>
        {
            frontImage.gameObject.SetActive(false );
            GetComponent<Image>().enabled = false;
        });
      
    }

}
