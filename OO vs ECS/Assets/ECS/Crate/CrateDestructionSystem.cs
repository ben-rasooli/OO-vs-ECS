#region usings
using Unity.Entities;
using Unity.Jobs;
#endregion

public class CrateDestructionSystem : SystemBase
{
  protected override void OnCreate()
  {
    _ECBSys = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
  }

  protected override void OnUpdate()
  {
    var ECB = _ECBSys.CreateCommandBuffer();
    Entities
      .WithAll<Crate, Destroy_tag>()
      .ForEach((Entity entity) =>
      {
        ECB.DestroyEntity(entity);
      }).Schedule();
    _ECBSys.AddJobHandleForProducer(Dependency);
  }
  EntityCommandBufferSystem _ECBSys;
}

public struct Destroy_tag : IComponentData { }