using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UI_Creator : MonoBehaviour
{
    [Inject(Id = "sizeBtn")] GameObject sizeBtn;
    [Inject(Id = "playerCountBtn")] GameObject playerBtn;
    [Inject(Id = "playerIcon")] GameObject playerIcon;
    public GridLayoutGroup containerBoardSize;
    public HorizontalLayoutGroup containerPlayerCount;
    public VerticalLayoutGroup containerPlayerIcons;

    [HideInInspector] public List<Image> iconsP = new List<Image>();
    [Inject] Model[] Models;

    [HideInInspector] public int size = 3;
    [HideInInspector] public int playerCount = 2;
    Dictionary<Button, int> dictSize = new Dictionary<Button, int>();
    Dictionary<Button, int> dictPlayerCount = new Dictionary<Button, int>();

    public void PlayerIconContainer()
    {
        for (int i = 0; i < playerCount; i++)
        {
            GameObject obj = Instantiate(playerIcon, containerPlayerIcons.transform);
            Image image = obj.GetComponent<Image>();

            image.sprite = Models[i].sprite;
            image.color = Color.black;
            iconsP.Add(image);
        }
    }
    public void PlayerCountContainer()
    {
        int index = 2;
        int len = size > 6 ? 6 : size;
        for (int i = index; i < len; i++)
        {
            GameObject obj = Instantiate(playerBtn, containerPlayerCount.transform);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = $"{i}P";
            obj.name = $"{i}P";
            Button btn = obj.GetComponent<Button>();
            dictPlayerCount.Add(btn, i);
            btn.onClick.AddListener(() => SetPcount(btn));
        }
    }
    private void SetPcount(Button button)
    {
        int index = dictPlayerCount[button];
        playerCount = index;
    }
    private void SetSize(Button button)
    {
        int index = dictSize[button];
        size = index;
    }
    public void BoardSizeContainer()
    {
        int index = 3;
        for (int i = 0; i < 9; i++)
        {
            GameObject obj = Instantiate(sizeBtn, containerBoardSize.transform);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = $"{index}x{index}";
            obj.name = $"{index}x{index}";
            Button btn = obj.GetComponent<Button>();
            dictSize.Add(btn, index);
            btn.onClick.AddListener(() => SetSize(btn));
            index++;
        }
    }
}

