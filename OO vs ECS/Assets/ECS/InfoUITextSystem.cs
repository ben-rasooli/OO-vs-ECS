using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;

public class InfoUITextSystem : SystemBase
{
  protected override void OnCreate()
  {
    _mainCamera = Camera.main;
    _ECBSys = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
  }

  protected override void OnUpdate()
  {
    var ECB = _ECBSys.CreateCommandBuffer();
    // showing health texts
    Entities
      .WithChangeFilter<Health>()
      .ForEach((Entity entity, in Health health, in InfoUIText infoUIText) =>
      {
        UIManager.Instance.SetUIText(entity.Index, health.Value.ToString());
      }).WithoutBurst().Run();

    // showing timer texts
    Entities
      .ForEach((Entity entity, in DynamicBuffer<Timer> timers, in InfoUIText infoUIText) =>
      {
        if (timers.Length > 0)
        {
          string textToShow = string.Empty;
          foreach (var timer in timers)
            textToShow += $"{timer.Remaining.ToString("0.0")}\n";
          UIManager.Instance.SetUIText(entity.Index, textToShow);
        }
      }).WithoutBurst().Run();

    // set UIText positions
    Entities
      .WithChangeFilter<Translation>()
      .ForEach((Entity entity, ref Translation translation, in InfoUIText infoUI) =>
      {
        UIManager.Instance.SetUITextPosition(
          entity.Index,
          (float3)_mainCamera.WorldToScreenPoint(translation.Value) + infoUI.Offset);
      }).WithoutBurst().Run();

    // clean up UITexts
    Entities
      .WithAll<InfoUIText>()
      .WithNone<InfoUITextStateComponent>()
      .ForEach((Entity entity) =>
      {
        ECB.AddComponent<InfoUITextStateComponent>(entity);
      }).Schedule();

    Entities
      .WithNone<InfoUIText>()
      .WithAll<InfoUITextStateComponent>()
      .ForEach((Entity entity) =>
      {
        UIManager.Instance.RemoveUIText(entity.Index);
        ECB.RemoveComponent<InfoUITextStateComponent>(entity);
      }).WithoutBurst().Run();

    _ECBSys.AddJobHandleForProducer(Dependency);
  }

  Camera _mainCamera;
  EntityCommandBufferSystem _ECBSys;
}

public struct InfoUIText : IComponentData
{
  public float3 Offset;
}

public struct InfoUITextStateComponent : ISystemStateComponentData { }
