using TMPro;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[DisallowMultipleComponent]
public class Bomb_authoring : MonoBehaviour, IConvertGameObjectToEntity
{
  [SerializeField] Vector2 _infoTextOffset;

  public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
  {
    dstManager.AddBuffer<SingleFrameComponent>(entity);
    dstManager.AddBuffer<Timer>(entity);
    dstManager.AddComponent<Bomb>(entity);
    dstManager.AddComponentData(entity, new InfoUIText { Offset = new float3(_infoTextOffset, 0) });
  }
}

public struct Bomb : IComponentData
{
  public BombType Type;
  public float ExplosionRadius;
  public int Damage;
}
