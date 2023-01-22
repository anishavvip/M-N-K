using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "UI", menuName = "Installers/UI")]
public class UI : ScriptableObjectInstaller<UI>
{
    public GameObject sizeBtn, playerBtn, playerIcon;
    public override void InstallBindings()
    {
        Container.BindInstance(playerIcon).WithId("playerIcon");
        Container.BindInstance(sizeBtn).WithId("sizeBtn");
        Container.BindInstance(playerBtn).WithId("playerCountBtn");
    }

}