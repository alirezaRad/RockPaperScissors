using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Record : MonoBehaviour
{
    [SerializeField] private Text round;
    [SerializeField] private Text playerScore;
    [SerializeField] private Text cpuScore;
    [SerializeField] private Text status;

    public void DataInput(int roundText, int playerScoreText, int cpuScoreText, string statusText)
    {
        round.text ="Round : " + roundText;
        playerScore.text = "You : " + playerScoreText;
        cpuScore.text = "Cpu : " + cpuScoreText;
        status.text = "Status : " + statusText;
        if(statusText == "Win")
            status.color = Color.green;
        else
            status.color = Color.red;
    }
}
