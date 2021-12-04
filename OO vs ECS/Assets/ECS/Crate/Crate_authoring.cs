using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[DisallowMultipleComponent]
public class Crate_authoring : MonoBehaviour, IConvertGameObjectToEntity
{
  [SerializeField] Vector2 _infoTextOffset;

  public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
  {
    dstManager.AddBuffer<SingleFrameComponent>(entity);
    dstManager.AddBuffer<Timer>(entity);
    dstManager.AddComponentData(entity, new Crate());
    dstManager.AddComponentData(entity, new Health { Value = 1 });
    dstManager.AddComponentData(entity, new InfoUIText { Offset = new float3(_infoTextOffset, 0) });
  }
}

public struct Crate : IComponentData { }
