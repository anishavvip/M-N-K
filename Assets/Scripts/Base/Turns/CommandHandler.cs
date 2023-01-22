using System.Collections.Generic;
using Zenject;
using UnityEngine;
using System;
using TMPro;

public class CommandHandler : MonoBehaviour
{
    public List<IPlayedTurn> moves = new List<IPlayedTurn>();
    public List<List<string>> movesDict = new List<List<string>>();
    [Inject] TextMeshProUGUI gameStatusText;
    public void AddCommand(IPlayedTurn move, GameManager GameManager, UI_Manager UI_Manager, ITurn Player)
    {
        if (Player.turnIndex < moves.Count)
            moves.RemoveRange(Player.turnIndex, moves.Count - Player.turnIndex);

        if (movesDict.Count != UI_Manager.playerCount)
            for (int i = movesDict.Count; i < UI_Manager.playerCount; i++)
                movesDict.Add(new List<string>());

        moves.Add(move);
        move.Execute();

        int currentTurn = Player.turnIndex % UI_Manager.playerCount;
        movesDict[currentTurn].Add($"{move.GetPosition()[0]},{move.GetPosition()[1]}");

        CheckVictory(GameManager, UI_Manager, currentTurn);
        Player.turnIndex++;
        GameManager.ColorizeIconsForPlayerTurn(Player.turnIndex);
    }

    private void CheckVictory(GameManager GameManager, UI_Manager UI_Manager, int currentTurn)
    {
        int wonPlayer;
        int win = IsPlayerWin.IsWinning(UI_Manager, this, currentTurn, out wonPlayer);
        if (win > -1)
        {
            GameManager.win = win > 0;
            if (GameManager.win)
            {
                if (wonPlayer < 0)
                    gameStatusText.text = "Draw!";
                else
                {
                    GameManager.playerWinIndex = wonPlayer;
                    gameStatusText.text = $"Player {wonPlayer} won!";
                }
            }
        }
    }

    public void UndoCommand(GameManager GameManager, ITurn Player, UI_Manager UI_Manager)
    {
        if (moves.Count == 0)
            return;
        if (Player.turnIndex > 0)
        {
            Player.turnIndex--;
            moves[Player.turnIndex].Undo();

            int[] key = moves[Player.turnIndex].GetPosition();
            string keyStr = $"{key[0]},{key[1]}";
            int currentTurn = Player.turnIndex % UI_Manager.playerCount;
            movesDict[currentTurn].Remove(keyStr);

            GameManager.ColorizeIconsForPlayerTurn(Player.turnIndex);
        }
    }
    public void RedoCommand(GameManager GameManager, ITurn Player, UI_Manager UI_Manager)
    {
        if (moves.Count == 0)
            return;
        if (Player.turnIndex < moves.Count)
        {
            GameManager.ColorizeIconsForPlayerTurn(Player.turnIndex);
            Player.turnIndex++;

            moves[Player.turnIndex - 1].Execute();
            int currentTurn = (Player.turnIndex - 1) % UI_Manager.playerCount;
            int[] key = moves[Player.turnIndex - 1].GetPosition();
            string keyStr = $"{key[0]},{key[1]}";
            movesDict[currentTurn].Add(keyStr);
        }
    }
}
