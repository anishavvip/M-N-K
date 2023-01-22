
using System;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerWin
{
    public static int IsWinning(UI_Manager UI_Manager, CommandHandler CommandHandler, int currentTurn, out int wonPlayer)
    {
        int winIndex = -1;
        List<string> movesList = CommandHandler.movesDict[currentTurn];
        int n = UI_Manager.size;
        if (movesList.Count >= n)
        {
            //Cols
            for (int i = 0; i < n; i++)
            {
                winIndex = 1;
                for (int j = 0; j < n; j++)
                {
                    string key1 = $"{i},{j}";
                    if (!movesList.Contains(key1))
                    {
                        winIndex = 0;
                        break;
                    }
                }
                if (winIndex == 1)
                {
                    wonPlayer = currentTurn;
                    return winIndex;
                }
            }
            //Rows
            for (int i = 0; i < n; i++)
            {
                winIndex = 1;
                for (int j = 0; j < n; j++)
                {
                    string key1 = $"{j},{i}";
                    if (!movesList.Contains(key1))
                    {
                        winIndex = 0;
                        break;
                    }
                }
                if (winIndex == 1)
                {
                    wonPlayer = currentTurn;
                    return winIndex;
                }
            }
            //Diagonals
            winIndex = 1;
            for (int i = 0; i < n; i++)
            {
                string key1 = $"{i},{i}";
                if (!movesList.Contains(key1))
                {
                    winIndex = 0;
                    break;
                }
            }
            if (winIndex == 1)
            {
                wonPlayer = currentTurn;
                return winIndex;
            }
            winIndex = 1;
            for (int i = 0; i < n; i++)
            {
                string key1 = $"{i},{n - 1 - i}";
                if (!movesList.Contains(key1))
                {
                    winIndex = 0;
                    break;
                }
            }
            if (winIndex == 1)
            {
                wonPlayer = currentTurn;
                return winIndex;
            }

            //Filled
            int count = 0;
            foreach (var item in CommandHandler.movesDict)
                count += item.Count;
            if (count == (n * n))
            {
                wonPlayer = -1;
                return 1;
            }
        }

        wonPlayer = -1;
        return 0;
    }
}
