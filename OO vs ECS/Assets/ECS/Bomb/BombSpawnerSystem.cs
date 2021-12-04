using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class BombSpawnerSystem : SystemBase
{
  protected override void OnCreate()
  {
    _ECBSys = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
  }
  EntityCommandBufferSystem _ECBSys;

  protected override void OnUpdate()
  {
    var ECB = _ECBSys.CreateCommandBuffer();
    var sceneSettings = GetSingleton<SceneSettings>();

    Entities.ForEach((ref SpawnBomb_command command) =>
    {
      Entity bombPrefab = default;
      switch (command.BombType)
      {
        case BombType.Chilly:
          bombPrefab = sceneSettings.ChillyBombPrefab;
          break;
        case BombType.Pepper:
          bombPrefab = sceneSettings.PepperBombPrefab;
          break;
      }
      Entity bomb = ECB.Instantiate(bombPrefab);
      var bombData = new Bomb
      {
        Type = command.BombType,
        ExplosionRadius = UnityEngine.Random.Range(3f, 5f),
        Damage = UnityEngine.Random.Range(1, 4)
      };
      ECB.SetComponent(bomb, bombData);
      ECB.SetComponent(bomb, new Translation { Value = command.Position });
      ECB.AppendToBuffer(bomb, new Timer { Remaining = command.ExplosionDelay, CompletionTag = new ComponentType(typeof(Ready_tag)) });
      Debug.Log($"A {bombData.Type} bomb with a damage of {bombData.Damage} and radius of {bombData.ExplosionRadius.ToString("0.0")} was spawned");
    }).WithoutBurst().Run();

    _ECBSys.AddJobHandleForProducer(Dependency);
  }
}

public struct SpawnBomb_command : IComponentData
{
  public float3 Position;
  public float ExplosionDelay;
  public BombType BombType;
}
