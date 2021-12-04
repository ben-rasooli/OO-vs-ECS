using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

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
    var bombPrefab = GetSingleton<SceneSettings>().BombPrefab;

    Entities.ForEach((ref SpawnBomb_command command) =>
    {
      Entity bomb = ECB.Instantiate(bombPrefab);
      ECB.SetComponent(bomb, new Translation { Value = command.Position });
      ECB.AppendToBuffer(bomb, new Timer { Remaining = command.ExplosionDelay, CompletionTag = new ComponentType(typeof(Ready_tag)) });
      ECB.SetComponent(bomb, new Bomb
      {
        ExplosionRadius = UnityEngine.Random.Range(3f, 5f),
        Damage = UnityEngine.Random.Range(1, 4)
      });
    }).WithoutBurst().Run();

    _ECBSys.AddJobHandleForProducer(Dependency);
  }
}

public struct SpawnBomb_command : IComponentData
{
  public float3 Position;
  public float ExplosionDelay;
}
