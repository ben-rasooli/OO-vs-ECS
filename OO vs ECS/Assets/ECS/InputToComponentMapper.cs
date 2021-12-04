using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class InputToComponentMapper : MonoBehaviour
{
  [SerializeField] Button _bombButton;
  [SerializeField] Button _crateButton;

  void OnEnable()
  {
    _bombButton.onClick.AddListener(createSpawnBombCommand);
    _crateButton.onClick.AddListener(createSpawnCrateCommand);
  }

  void OnDisable()
  {
    _bombButton.onClick.RemoveAllListeners();
    _crateButton.onClick.RemoveAllListeners();
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

  void createSpawnCrateCommand()
  {
    Vector2 randomPosition = UnityEngine.Random.insideUnitCircle * _groundExtend;
    var spawnCrate_command = new SpawnCrate_command
    {
      Position = new float3(randomPosition.x, 0, randomPosition.y)
    };
    var ECB =
      World.DefaultGameObjectInjectionWorld
      .GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>()
      .CreateCommandBuffer();
    ECB.CreateSingleFrameComponent(spawnCrate_command);
  }

  float _groundExtend = 7f;
}
