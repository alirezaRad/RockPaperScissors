using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RpsGameManager : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator cpuAnimator;
    
    [SerializeField] private Text playerScoreText;
    [SerializeField] private Text cpuScoreText;
    
    [SerializeField] private Image playerImage;  
    [SerializeField] private Image cpuImage;     
    
    [SerializeField] private Sprite rockSpritePlayer;
    [SerializeField] private Sprite paperSpritePlayer;
    [SerializeField] private Sprite scissorsSpritePlayer;

    [SerializeField] private Sprite rockSpriteCpu;
    [SerializeField] private Sprite paperSpriteCpu;
    [SerializeField] private Sprite scissorsSpriteCpu;
    
    [SerializeField] GameObject[]  activeButtons;
    [SerializeField] GameObject[]  deActiveButtons;

    [SerializeField] private GameObject finalPanel;
    [SerializeField] private Text result;
    
    [SerializeField] private Record record;
    [SerializeField] private RectTransform contentForRecord;
    
    private int _playerScore = 0;
    private int _cpuScore = 0;
    
    private RpsChoice _playerChoice;
    private RpsChoice _cpuChoice;

    private int recordNumber = 0;
    private string gameStatus;

    public void PlayerChoose(int choiceIndex)
    {
        DeActivateButtons();
        _playerChoice = (RpsChoice)choiceIndex;
        ResetStateOfHands();
        TriggerAnimation();
    }

    private void TriggerAnimation()
    {
        playerAnimator.SetTrigger("ShakeHand");
        cpuAnimator.SetTrigger("ShakeHand");
    }

    private void ResetStateOfHands()
    {
        playerImage.sprite = rockSpritePlayer;
        cpuImage.sprite = rockSpriteCpu;
    }

    public void OnAnimationEnd()
    {
        CpuChoose();
        UpdateImages(_playerChoice, _cpuChoice);
        string roundResult = CompareChoices(_playerChoice, _cpuChoice);
        UpdateScores(roundResult);
        UpdateTextUI();
        ResetAnimation();
    }

    private void ResetAnimation()
    {
        playerAnimator.ResetTrigger("ShakeHand");
        cpuAnimator.ResetTrigger("ShakeHand");
        playerAnimator.Play("Idle");
        cpuAnimator.Play("Idle");
    }

    private void CpuChoose()
    {
        _cpuChoice = (RpsChoice)Random.Range(0, 3);
    }

    private void UpdateTextUI()
    {
        playerScoreText.text = $"You : {_playerScore}";
        cpuScoreText.text = $"CPU : {_cpuScore}";
    }
    
    void UpdateImages(RpsChoice player, RpsChoice cpu)
    {
        playerImage.sprite = GetChoiceSprite(player,rockSpritePlayer,paperSpritePlayer,scissorsSpritePlayer);
        cpuImage.sprite = GetChoiceSprite(cpu,rockSpriteCpu,paperSpriteCpu,scissorsSpriteCpu);
    }
    Sprite GetChoiceSprite(RpsChoice choice,Sprite rockSprite,Sprite paperSprite,Sprite scissorsSprite)
    {
        switch (choice)
        {
            case RpsChoice.Rock:
                return rockSprite;
            case RpsChoice.Paper:
                return paperSprite;
            case RpsChoice.Scissors:
                return scissorsSprite;
            default:
                return null; 
        }
    }

    string CompareChoices(RpsChoice player, RpsChoice cpu)
    {
        if (player == cpu)
        {
            return "Draw";
        }
        else if ((player == RpsChoice.Rock && cpu == RpsChoice.Scissors) ||
                 (player == RpsChoice.Paper && cpu == RpsChoice.Rock) ||
                 (player == RpsChoice.Scissors && cpu == RpsChoice.Paper))
        {
            return "Player Wins!";
        }
        else
        {
            return "CPU Wins!";
        }
    }

    void UpdateScores(string result)
    {
        if (result == "Player Wins!")
        {
            CalculateScoreAndActToit(nameof(WinTheGame), ref _playerScore);
            AudioManager.Instance.PlayWinSound();
        }
        else if (result == "CPU Wins!")
        {
            CalculateScoreAndActToit(nameof(LoseTheGame), ref _cpuScore);
            AudioManager.Instance.PlayLoseSound();
        }
        else
        {
            ActivateButtons();
            AudioManager.Instance.PlayEqualSound();
        }
    }

    private void CalculateScoreAndActToit(string functionName,ref int score)
    {
        score++;
        if (score == 5)
            Invoke(functionName,1f);
        else
            ActivateButtons();
    }

    private void LoseTheGame()
    {
        gameStatus = "Lose";
        AudioManager.Instance.PlayGameOverSound();
        finalPanel.SetActive(true);
        UpdateResultState("You Lose",Color.red);
    }

    private void UpdateResultState(string text , Color color)
    {
        result.text = text;
        result.color = color;
    }

    private void WinTheGame()
    {
        gameStatus = "Win";
        AudioManager.Instance.PlayVictorySound();
        finalPanel.SetActive(true);
        UpdateResultState("You Win",Color.green);
    }

    public void DeActivateButtons()
    {
        foreach (var item in activeButtons)
        {
            item.SetActive(false);
        }
        foreach (var item in deActiveButtons)
        {
            item.SetActive(true);
        }
    }
    public void ActivateButtons()
    {
        foreach (var item in activeButtons)
        {
            item.SetActive(true);
        }
        foreach (var item in deActiveButtons)
        {
            item.SetActive(false);
        }
    }

    public void RestartGame()
    {
        CreateNewRecordInScoreBord();
        _playerScore = 0;
        _cpuScore = 0;
        finalPanel.SetActive(false);
        UpdateTextUI();
        ActivateButtons();
        ResetStateOfHands();
    }

    private void CreateNewRecordInScoreBord()
    {
        recordNumber++;
        GameObject newRecord = Instantiate(record.gameObject);
        newRecord.GetComponent<Record>().DataInput(recordNumber,_playerScore,_cpuScore,gameStatus);
        newRecord.GetComponent<RectTransform>().SetParent(contentForRecord);
    }
}
