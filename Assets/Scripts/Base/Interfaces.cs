using UnityEngine;
public interface IUI_Manager
{
    bool playerEnteredDetails { get; set; }
    bool isPlayerVsPlayer { get; set; }
    int size { get; set; }
    int playerCount { get; set; }
}
public interface ITaskManager
{
    void CreateGrid(IBaseGridFactory baseGridFactory);
    bool IsCompleted { get; }
}
public interface IPlayedTurn
{
    int playerIndex { get; }
    int[] GetPosition();
    bool CheckPosition(int[] intList);
    void Undo();
    void Execute();
}
public interface ITurn
{
    int turnIndex { get; set; }
    void PlayTurn(Vector3 pos, int playerCount, Model model, GameManager GameManager, UI_Manager UI_Manager);
}
public interface IInputManager
{
    public bool leftClick { get; }
    Vector3 InputPosition { get; }
}
public interface IBaseGridFactory
{
    Color color { get; }
    void CreateGrid(int size);
}

