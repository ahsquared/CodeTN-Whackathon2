using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Timers;
using UnityEngine.UI;

public class GameManager : AudioEvents
{

    private int _byronScore = 0;
    private int _smokeyScore = 0;
    private int _clockTime = 0;
    private bool _gameRunning;
    private Text _instructionsTimerText;
    private KinectManager _kinectManager;

    [Range(0, 60)]
    public int GameDuration = 30;

    public int Countdown = 15;
    public GameObject ByronWinner;
    public GameObject SmokeyWinner;
    public GameObject Tie;
    public GameObject Instructions;
    public GameObject Sponsors;
    public bool ByronActive;
    public bool SmokeyActive;

    // Use this for initialization
    void Start ()
    {
        _kinectManager = GameObject.Find("KinectController").GetComponent<KinectManager>();
        _instructionsTimerText = GameObject.Find("InstructionsTimer").GetComponent<Text>();
        _instructionsTimerText.text = Countdown.ToString(CultureInfo.InvariantCulture);
        PlayAmbientSound();
        //StartGame();
    }

    private void PlayAmbientSound()
    {
        PlayEvent("Crickets");
    }

    // Update is called once per frame
	void Update () {
       
    }

    void StartGame()
    {
        _byronScore = 0;
        _smokeyScore = 0;
        _clockTime = Countdown;
        TimersManager.SetLoopableTimer(this, 1.0f, UpdateClock);
        _gameRunning = true;
        _kinectManager.displayUserMap = false;
    }

    void UpdateClock()
    {
        _clockTime -= 1;
        if (_clockTime == 0 )
        {
            PlayGame();
        }
        else if (_clockTime > 0)
        {
            _instructionsTimerText.text = _clockTime.ToString(CultureInfo.InvariantCulture);
        }
        //Debug.Log("Clock Tick: " + _clockTime);
    }

    bool GetPlayerReadyStatus()
    {
        return ByronActive && SmokeyActive;
    }

    public void UpdatePlayer(string player, bool playerState)
    {
        if (player == "Byron")
        {
            ByronActive = playerState;
        }
        if (player == "Smokey")
        {
            SmokeyActive = playerState;
        }
        if (GetPlayerReadyStatus() && !_gameRunning)
        {
            StartGame();
        }
    }

    void PlayGame()
    {
        TimersManager.ClearTimer(UpdateClock);
        ByronWinner.SetActive(false);
        SmokeyWinner.SetActive(false);
        Tie.SetActive(false);
        Sponsors.SetActive(false);
        _byronScore = 0;
        _smokeyScore = 0;
        ResetScores();
        Instructions.SetActive(false);
        SetRTPCValue("GameTime", 0);
        PlayEvent("Play_Bear_Facts_30_SEC_LOOP", true);
        GameObject.Find("ByronEmitter").GetComponent<BallEmitter>().StartEmitting();
        GameObject.Find("SmokeyEmitter").GetComponent<BallEmitter>().StartEmitting();
        TimersManager.SetTimer(this, GameDuration, EndGame);
    }

    void ResetGame()
    {
        _clockTime = Countdown;
        Instructions.SetActive(true);
        _instructionsTimerText.text = Countdown.ToString(CultureInfo.InvariantCulture);
        _kinectManager.displayUserMap = true;
        if (GetPlayerReadyStatus())
        {
            StartGame();
        }
    }

    void EndGame()
    {
        _gameRunning = false;
        GameObject.Find("ByronEmitter").GetComponent<BallEmitter>().StopEmitting();
        GameObject.Find("SmokeyEmitter").GetComponent<BallEmitter>().StopEmitting();
        foreach (var acorn in GameObject.FindGameObjectsWithTag("Acorn"))
        {
            Destroy(acorn);
        }
        Debug.Log("End Game (B vs S) " + _byronScore + " to " + _smokeyScore);
        ShowWinner();
    }

    void ShowWinner()
    {
        if (_byronScore > _smokeyScore)
        {
            ByronWinner.SetActive(true);
        } else if (_smokeyScore > _byronScore)
        {
            SmokeyWinner.SetActive(true);
        }
        else
        {
            Tie.SetActive(true);
        }
        PlayEvent("BearWin");
        TimersManager.SetTimer(this, 5f, ResetGame);
    }

    public void UpdateScore(string playerName)
    {
        if (playerName == "Byron")
        {
            _byronScore += 1;
            GameObject.Find("ByronText").GetComponent<Text>().text = _byronScore.ToString();
        } else if (playerName == "Smokey")
        {
            _smokeyScore += 1;
            GameObject.Find("SmokeyText").GetComponent<Text>().text = _smokeyScore.ToString();
        }

    }

    public void ResetScores()
    {
        GameObject.Find("ByronText").GetComponent<Text>().text = "0";
        GameObject.Find("SmokeyText").GetComponent<Text>().text = "0";
    }
}
