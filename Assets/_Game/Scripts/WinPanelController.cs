using UnityEngine;
using DG.Tweening; // don't forget this!

public class WinPanelController : MonoBehaviour
{
    public GameObject winPanel;
    public RectTransform playButton;

    public void ShowWinPanel()
    {
        winPanel.SetActive(true);

        // Reset button position off-screen (Y = 800, adjust for your canvas)
        playButton.anchoredPosition = new Vector2(0, 800);

        // Animate: move to center (Y = 0), with bounce
        playButton.DOAnchorPosY(0, 1f).SetEase(Ease.OutBounce);
    }

    public void OnPlayButtonClicked()
    {
        // Load next level or restart logic here
        Debug.Log("Play Next Level");
    }
}