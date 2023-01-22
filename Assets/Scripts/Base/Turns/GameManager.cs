using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameManager : MonoBehaviour, ITaskManager
{
    IBaseGridFactory baseGridFactory;
    public bool IsCompleted { get; private set; }
    Model[] models;
    ITurn turn;
    IInputManager inputManager;
    [HideInInspector] public bool win = false;
    [HideInInspector] public int playerWinIndex = -1;
    UI_Creator UI_Creator;
    UI_Manager UI_Manager;
    [Inject(Id = "inGameScreen")] GameObject inGameScreen;
    [Inject]
    public void Construct(UI_Manager UI_Manager, IBaseGridFactory baseGridFactory, ITurn turn, IInputManager inputManager, Model[] models, UI_Creator UI_Creator)
    {
        this.UI_Manager = UI_Manager;
        this.UI_Creator = UI_Creator;
        this.baseGridFactory = baseGridFactory;
        this.models = models;
        this.turn = turn;
        this.inputManager = inputManager;
    }
    public void CreateGrid(IBaseGridFactory baseGridFactory)
    {
        baseGridFactory.CreateGrid(UI_Manager.size);
    }
    private void Init()
    {
        UI_Manager.playerCount = UI_Manager.playerCount > models.Length ? models.Length : UI_Manager.playerCount;
        turn.turnIndex = 0;
        CreateGrid(baseGridFactory);
        ColorizeIconsForPlayerTurn(0);
    }
    private void Update()
    {
        Tick();
    }
    public void Tick()
    {
        if (win) { inGameScreen.SetActive(false); return; }
        if (UI_Manager == null) return;
        if (UI_Manager.playerEnteredDetails)
        {
            Init();
            UI_Manager.playerEnteredDetails = false;
        }

        if (inputManager.leftClick)
            if (UI_Manager.isPlayerVsPlayer)
            {
                int currentTurn = turn.turnIndex % UI_Manager.playerCount;

                turn.PlayTurn(inputManager.InputPosition, UI_Manager.playerCount, models[currentTurn], this, UI_Manager);
            }
    }
    public void ColorizeIconsForPlayerTurn(int turnIndex)
    {
        int currentTurn = turnIndex % UI_Manager.playerCount;
        foreach (Image image in UI_Creator.iconsP)
        {
            if (image != UI_Creator.iconsP[currentTurn])
                image.color = Color.black;
            else
                image.color = models[currentTurn].color;
        }
    }
}
