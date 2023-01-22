using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class UI_Manager : MonoBehaviour
{
    UI_Creator UI_Creator;
    CommandHandler CommandHandler;
    [Inject(Id = "undoBtn")] Button undoBtn;
    [Inject(Id = "redoBtn")] Button redoBtn;
    [Inject(Id = "homeBtn")] Button homeBtn;
    [Inject(Id = "nextBtn")] Button nextBtn;

    [Inject(Id = "inGameScreen")] GameObject inGameScreen;
    [Inject(Id = "homeScreen")] GameObject homeScreen;

    bool isReadyForMainScene = false;

    public bool playerEnteredDetails { get; set; }
    public bool isPlayerVsPlayer { get; set; }
    public int size { get; set; }
    public int playerCount { get; set; }
    GameManager GameManager;
    ITurn Player;

    [Inject]
    public void Construct(ITurn Player, UI_Creator UI_Creator, CommandHandler CommandHandler, GameManager GameManager)
    {
        this.Player = Player;
        this.GameManager = GameManager;
        this.CommandHandler = CommandHandler;
        this.UI_Creator = UI_Creator;
    }
    public void NextScene()
    {
        if (isReadyForMainScene)
            JumpToMainScene();
        else
            JumpToPlayerSelectionScreen();
    }

    private void JumpToPlayerSelectionScreen()
    {
        UI_Creator.containerPlayerCount.gameObject.SetActive(true);
        UI_Creator.containerBoardSize.gameObject.SetActive(false);

        UI_Creator.PlayerCountContainer();
        isReadyForMainScene = true;
    }

    private void JumpToMainScene()
    {
        UI_Creator.PlayerIconContainer();
        homeScreen.SetActive(false);
        inGameScreen.SetActive(true);
        MainScene(UI_Creator.playerCount, UI_Creator.size, true);
    }

    void MainScene(int pCount, int sizeXsize, bool PvsP)
    {
        playerCount = pCount;
        size = sizeXsize;
        isPlayerVsPlayer = PvsP;
        playerEnteredDetails = true;
    }
    void HomeScene()
    {
        SceneManager.LoadScene(0);
    }
    void Start()
    {
        UI_Creator.BoardSizeContainer();
        UI_Creator.containerPlayerCount.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        nextBtn.onClick.AddListener(() => NextScene());
        homeBtn.onClick.AddListener(() => HomeScene());
        if (CommandHandler != null && GameManager != null)
        {
            redoBtn.onClick.AddListener(() => CommandHandler.RedoCommand(GameManager, Player, this));
            undoBtn.onClick.AddListener(() => CommandHandler.UndoCommand(GameManager, Player, this));
        }
    }
    private void OnDisable()
    {
        nextBtn.onClick.RemoveListener(() => NextScene());
        homeBtn.onClick.RemoveListener(() => HomeScene());
        redoBtn.onClick.RemoveListener(() => CommandHandler.RedoCommand(GameManager, Player, this));
        undoBtn.onClick.RemoveListener(() => CommandHandler.UndoCommand(GameManager, Player, this));
    }
}
