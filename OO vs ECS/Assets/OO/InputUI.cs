using UnityEngine;
using UnityEngine.UI;

public class InputUI : MonoBehaviour
{
  [SerializeField] Button _bombButton;
  [SerializeField] Button _crateButton;

  void OnEnable()
  {
    _bombButton.onClick.AddListener(spawnBomb);
    _crateButton.onClick.AddListener(spawnCrate);
  }

  void OnDisable()
  {
    _bombButton.onClick.RemoveAllListeners();
    _crateButton.onClick.RemoveAllListeners();
  }

  void Awake()
  {
    _bombSpawner = FindObjectOfType<BombSpawner>();
    _crateSpawner = FindObjectOfType<CrateSpawner>();
  }

  void spawnBomb()
  {
    Vector2 randomPosition = Random.insideUnitCircle * _groundExtend;
    _bombSpawner.Spawn(
      position: new Vector3(randomPosition.x, 0, randomPosition.y),
      explosionDelay: 2f);
  }

  void spawnCrate()
  {
    Vector2 randomPosition = Random.insideUnitCircle * _groundExtend;
    _crateSpawner.Spawn(new Vector3(randomPosition.x, 0, randomPosition.y));
  }

  float _groundExtend = 7f;
  BombSpawner _bombSpawner;
  CrateSpawner _crateSpawner;
}
