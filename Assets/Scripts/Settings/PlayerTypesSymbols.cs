using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PlayerTypesSymbols", menuName = "Installers/PlayerTypesSymbols")]
public class PlayerTypesSymbols : ScriptableObjectInstaller<PlayerTypesSymbols>
{
    public Model[] Models;
    public override void InstallBindings()
    {
        Container.BindInstance(Models);
    }
}