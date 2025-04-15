using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EndCanvas : MonoBehaviour
{
    [SerializeField] private CanvasGroup backgroundCanvasGroup;
    [SerializeField] private CanvasGroup panelCanvasGroup;
    [SerializeField] private CanvasGroup textCanvasGroup;
    
    [SerializeField] private Transform panelTransform;
    [SerializeField] private Transform textTransform;
    
    [SerializeField] private TMP_Text text;
    
    [SerializeField] private Image image;

    
    
    public void RoundEnd(int winnerID)
    {
        backgroundCanvasGroup.DOFade(1, .2f);
        panelCanvasGroup.DOFade(1, .2f);
        textCanvasGroup.DOFade(1, .2f);
        
        panelTransform.DOScale(1, .2f).SetEase(Ease.OutBack);
        textTransform.DOScale(1, .2f).SetEase(Ease.OutBack);
        
        NewText(winnerID);
    }

    public void RoundStart()
    {
        backgroundCanvasGroup.alpha = 0;
        panelCanvasGroup.alpha = 0;
        textCanvasGroup.alpha = 0;
        
        panelTransform.localScale = new Vector3(1, 0, 1);
        textTransform.localScale = Vector3.zero;
    }

    void NewText(int winnerID)
    {
        string winnerColor = "Blue";
        string loserColor = "Red";
        string message = "";

        if (winnerID == 1)
        {
            winnerColor = "Blue";
            loserColor = "Red";
            image.color = new Color(0, .5f ,1 , 1f);
        }
        else
        {
            winnerColor = "Red";
            loserColor = "Blue";
            image.color = new Color(1, .2f ,.2f , 1f);
        }

        switch (Random.Range(0, 7))
        {
            case 0:
                message = winnerColor + " takes the W!";
                break;
            case 1:
                message = loserColor + " got pwned!";
                break;
            case 2:
                message = loserColor + " got smoked!";
                break;
            case 3:
                message = loserColor + " takes the L!";
                break;
            case 4:
                message = loserColor + " just couldn't hang...";
                break;
            case 5:
                message = winnerColor + " clutches the win!";
                break;
            case 6:
                message = winnerColor + " is on FIRE!";
                break;
            case 7:
                message = winnerColor + " cooked the round to perfection!!";
                break;
        }

        text.text = message;
    }

    void Update()
    {
        panelTransform.eulerAngles = new Vector3(0, 0, Mathf.Sin(Time.time * 2) - 4);
        textTransform.eulerAngles = new Vector3(0, 0, Mathf.Sin(Time.time * 2) * 4);
    }
}
