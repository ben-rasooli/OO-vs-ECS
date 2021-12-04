using UnityEngine;

public class BombSpawner : MonoBehaviour
{
  [SerializeField] GameObject _chillyBombPrefab;
  [SerializeField] GameObject _pepperBombPrefab;

  public void Spawn(Vector3 position, float explosionDelay, BombType bombType)
  {
    float explosionRadius = UnityEngine.Random.Range(3f, 5f);
    int damage = UnityEngine.Random.Range(1, 4);
    GameObject bomb = default;
    if (bombType == BombType.Chilly)
      bomb = Instantiate(_chillyBombPrefab, position, Quaternion.identity);
    else
      bomb = Instantiate(_pepperBombPrefab, position, Quaternion.identity);
    var bombController = bomb.GetComponent<BombController>();
    bombController.Init(bombType, explosionDelay, explosionRadius, damage);
    bombController.SetPosition(position);
  }
}
