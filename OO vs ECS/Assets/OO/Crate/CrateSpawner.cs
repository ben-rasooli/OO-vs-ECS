using UnityEngine;

public class CrateSpawner : MonoBehaviour
{
  [SerializeField] GameObject _yellowCratePrefab;
  [SerializeField] GameObject _redCratePrefab;
  [SerializeField] GameObject _blueCratePrefab;

  public void Spawn(Vector3 position, CrateType crateType)
  {
    GameObject cratePrefab = default;
    switch (crateType)
    {
      case CrateType.Yellow:
        cratePrefab = _yellowCratePrefab;
        break;
      case CrateType.Red:
        cratePrefab = _redCratePrefab;
        break;
      case CrateType.Blue:
        cratePrefab = _blueCratePrefab;
        break;
    }

    var crateController = Instantiate(cratePrefab).GetComponent<CrateController>();
    crateController.Init(Random.Range(4, 10), crateType);
    crateController.SetPosition(position);
  }
}
