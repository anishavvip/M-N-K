using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

public class UI_Installer : MonoInstaller
{
    [Header("In-Game")]
    public Button undoBtn;
    public Button redoBtn;
    public Button homeBtn;
    public Button nextBtn;
    public TextMeshProUGUI gameStatusText;

    [Header("Screens")]
    public GameObject homeScreen;
    public GameObject inGameScreen;

    public override void InstallBindings()
    {
        InstallInstances();
    }
    private void InstallInstances()
    {
        Buttons();
        Screens();
    }
    private void Screens()
    {
        Container.BindInstance(gameStatusText);
        Container.BindInstance(homeScreen).WithId("homeScreen");
        Container.BindInstance(inGameScreen).WithId("inGameScreen");
    }

    private void Buttons()
    {
        Container.BindInstance(undoBtn).WithId("undoBtn");
        Container.BindInstance(redoBtn).WithId("redoBtn");
        Container.BindInstance(homeBtn).WithId("homeBtn");
        Container.BindInstance(nextBtn).WithId("nextBtn");
    }
}