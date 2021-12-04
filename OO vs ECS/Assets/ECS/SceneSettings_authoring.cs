using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[DisallowMultipleComponent]
public class SceneSettings_authoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
  [SerializeField] GameObject _chillyBombPrefab;
  [SerializeField] GameObject _pepperBombPrefab;
  [SerializeField] GameObject _yellowCratePrefab;
  [SerializeField] GameObject _redCratePrefab;
  [SerializeField] GameObject _blueCratePrefab;

  public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
  {
    dstManager.AddComponentData(entity, new SceneSettings
    {
      ChillyBombPrefab = conversionSystem.GetPrimaryEntity(_chillyBombPrefab),
      PepperBombPrefab = conversionSystem.GetPrimaryEntity(_pepperBombPrefab),
      YellowCratePrefab = conversionSystem.GetPrimaryEntity(_yellowCratePrefab),
      RedCratePrefab = conversionSystem.GetPrimaryEntity(_redCratePrefab),
      BlueCratePrefab = conversionSystem.GetPrimaryEntity(_blueCratePrefab)
    });
  }

  public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
  {
    referencedPrefabs.Add(_chillyBombPrefab);
    referencedPrefabs.Add(_pepperBombPrefab);
    referencedPrefabs.Add(_yellowCratePrefab);
    referencedPrefabs.Add(_redCratePrefab);
    referencedPrefabs.Add(_blueCratePrefab);
  }
}

public struct SceneSettings : IComponentData
{
  public Entity ChillyBombPrefab;
  public Entity PepperBombPrefab;
  public Entity YellowCratePrefab;
  public Entity RedCratePrefab;
  public Entity BlueCratePrefab;
}
