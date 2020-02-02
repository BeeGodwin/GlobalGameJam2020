﻿using System.Collections;
using System.Collections.Generic;
using controllers;
using UnityEngine;
using DG.Tweening;

public class LogicController : MonoBehaviour{

    public static LogicController instance;
    public CanvasGroup lobbyPanel, mainGamePanel;
    public int penaltyAmount = 3;
    [HideInInspector]
    public bool gameStart = false;

    /*
    public LogicController Instance() {
        if (_instance == null) _instance = this;
        return _instance;
    }*/

    private void Awake() {
        instance = instance ? instance : this;
        DontDestroyOnLoad(instance);
        lobbyPanel.gameObject.SetActive(true);
        mainGamePanel.gameObject.SetActive(false);
    }

    public void StartMainGame() {
        FindObjectOfType<AudioManager>().Play("interior_loop"); // abient music
        mainGamePanel.gameObject.SetActive(true);
        mainGamePanel.alpha = 0f;
        mainGamePanel.DOFade(1f, 0.5f);
        GameStatusController.instance.StartCountdown();
    }

    public void SymbolWasMatched() {
        GameStatusController.instance.remainingSlots--;
        ButtonSpawner.instance.ButtonPressed();

        // Win if no more remaining slots left
        if (GameStatusController.instance.remainingSlots <= 0)
            ReachedWinState();

        StoryController.Instance().SymbolWasMatched();
    }

    public void SymbolWasMisMatched() {
        GameStatusController.instance.remainingSlots += penaltyAmount;
        ButtonSpawner.instance.ButtonPressed();
        PenaltyController.instance.TriggerPenalty(penaltyAmount, 0.3f);
        CanvasShaker.instance.Shake(100f, 0.25f);

        StoryController.Instance().SymbolWasMisMatched();
    }

    public void ReachedWinState() {
        FindObjectOfType<AudioManager>().Play("winner_fanfare"); // win sound
        GameStatusController.instance.UpdateGameStatusText("You win!");
        GameStatusController.instance.StopCountdown();
        StoryController.Instance().ReachedWinState();
    }

    public void ReachedLoseState() {
        FindObjectOfType<AudioManager>().Play("game_over_retro"); // lose sound
        GameStatusController.instance.UpdateGameStatusText("You lose!");
        gameStart = false;
        StoryController.Instance().ReachedLoseState();
    }
}
