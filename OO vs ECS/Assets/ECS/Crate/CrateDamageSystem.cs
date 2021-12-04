#region usings
using Unity.Entities;
using Unity.Jobs;
#endregion

public class CrateDamageSystem : SystemBase
{
  protected override void OnCreate()
  {
    _ECBSys = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
  }

  protected override void OnUpdate()
  {
    var ECB = _ECBSys.CreateCommandBuffer();
    Entities
      .ForEach((Entity entity, ref Health health, in Damage damage) =>
      {
        health.Value -= damage.Value;
        if (health.Value <= 0)
          ECB.AddComponent<Destroy_tag>(entity);
      }).WithoutBurst().Schedule();
    _ECBSys.AddJobHandleForProducer(Dependency);
  }
  EntityCommandBufferSystem _ECBSys;
}