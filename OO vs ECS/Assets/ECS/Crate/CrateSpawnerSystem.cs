using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class CrateSpawnerSystem : SystemBase
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

    Entities.ForEach((ref SpawnCrate_command command) =>
    {
      Entity cratePrefab = default;
      switch (command.CrateType)
      {
        case CrateType.Yellow:
          cratePrefab = sceneSettings.YellowCratePrefab;
          break;
        case CrateType.Red:
          cratePrefab = sceneSettings.RedCratePrefab;
          break;
        case CrateType.Blue:
          cratePrefab = sceneSettings.BlueCratePrefab;
          break;
      }
      Entity crate = ECB.Instantiate(cratePrefab);
      ECB.SetComponent(crate, new Crate { Type = command.CrateType });
      ECB.SetComponent(crate, new Translation { Value = command.Position });
      ECB.SetComponent(crate, new Health { Value = UnityEngine.Random.Range(4, 10) });
    }).WithoutBurst().Run();

    _ECBSys.AddJobHandleForProducer(Dependency);
  }
}

public struct SpawnCrate_command : IComponentData
{
  public float3 Position;
  public CrateType CrateType;
}