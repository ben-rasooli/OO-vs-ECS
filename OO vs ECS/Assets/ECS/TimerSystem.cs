using Unity.Entities;
using Unity.Jobs;

public class TimerSystem : SystemBase
{
  protected override void OnCreate()
  {
    _ECBSys = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
  }

  protected override void OnUpdate()
  {
    var ECB = _ECBSys.CreateCommandBuffer();
    float deltaTime = Time.DeltaTime;

    Entities
      .ForEach((Entity entity, ref DynamicBuffer<Timer> timers) =>
      {
        for (int i = timers.Length - 1; i >= 0; i--)
        {
          var timer = timers[i];
          timer.Remaining -= deltaTime;
          if (timer.Remaining <= 0)
          {
            ECB.AddComponent(entity, timer.CompletionTag);
            timers.RemoveAtSwapBack(i);
          }
          else
            timers[i] = timer;
        }
      }).Schedule();

    _ECBSys.AddJobHandleForProducer(Dependency);
  }

  EntityCommandBufferSystem _ECBSys;
}

public struct Timer : IBufferElementData
{
  public float Remaining;
  public ComponentType CompletionTag;
}
