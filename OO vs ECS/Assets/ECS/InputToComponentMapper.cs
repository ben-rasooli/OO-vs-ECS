using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class InputToComponentMapper : MonoBehaviour
{
  [SerializeField] Button _bombButton;
  [SerializeField] Button _yellowCrateButton;
  [SerializeField] Button _redCrateButton;
  [SerializeField] Button _blueCrateButton;

  void OnEnable()
  {
    _bombButton.onClick.AddListener(createSpawnBombCommand);
    _yellowCrateButton.onClick.AddListener(() => createSpawnCrateCommand(CrateType.Yellow));
    _redCrateButton.onClick.AddListener(() => createSpawnCrateCommand(CrateType.Red));
    _blueCrateButton.onClick.AddListener(() => createSpawnCrateCommand(CrateType.Blue));
  }

  void OnDisable()
  {
    _bombButton.onClick.RemoveAllListeners();
    _yellowCrateButton.onClick.RemoveAllListeners();
    _redCrateButton.onClick.RemoveAllListeners();
    _blueCrateButton.onClick.RemoveAllListeners();
  }

  void createSpawnBombCommand()
  {
    Vector2 randomPosition = UnityEngine.Random.insideUnitCircle * _groundExtend;
    SpawnBomb_command spawnBomb_command = new SpawnBomb_command
    {
      Position = new float3(randomPosition.x, 0, randomPosition.y),
      ExplosionDelay = 2f
    };
    var ECB =
      World.DefaultGameObjectInjectionWorld
      .GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>()
      .CreateCommandBuffer();
    ECB.CreateSingleFrameComponent(spawnBomb_command);
  }

  void createSpawnCrateCommand(CrateType crateType)
  {
    Vector2 randomPosition = UnityEngine.Random.insideUnitCircle * _groundExtend;
    var spawnCrate_command = new SpawnCrate_command
    {
      Position = new float3(randomPosition.x, 0, randomPosition.y),
      CrateType = crateType
    };
    var ECB =
      World.DefaultGameObjectInjectionWorld
      .GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>()
      .CreateCommandBuffer();
    ECB.CreateSingleFrameComponent(spawnCrate_command);
  }

  float _groundExtend = 7f;
}
