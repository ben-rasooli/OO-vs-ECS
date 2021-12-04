using UnityEngine;

public class BombController : MonoBehaviour
{
  [SerializeField] Vector3 _infoTextOffset;

  public void Init(BombType type, float explosionDelay, float explosionRadius, int damage)
  {
    _myType = type;
    _explosionRadius = explosionRadius;
    _damage = damage;
    _timer = explosionDelay;
    _UIManager.SetUIText(gameObject.GetInstanceID(), _timer.ToString("0.0"));
  }

  public void SetPosition(Vector3 value)
  {
    transform.position = value;
    _UIManager.SetUITextPosition(gameObject.GetInstanceID(), _mainCamera.WorldToScreenPoint(value) + _infoTextOffset);
  }

  void Update()
  {
    _timer -= Time.deltaTime;
    _UIManager.SetUIText(gameObject.GetInstanceID(), _timer.ToString("0.0"));
    if (_timer <= 0f)
      explode();
  }

  void OnEnable()
  {
    _UIManager = FindObjectOfType<UIManager>();
    _mainCamera = Camera.main;
  }

  void OnDisable()
  {
    _UIManager.RemoveUIText(gameObject.GetInstanceID());
  }

  void explode()
  {
    var crates = FindObjectsOfType<CrateController>();
    foreach (var crate in crates)
      if (Vector3.Distance(transform.position, crate.Position) <= _explosionRadius)
        crate.TakeDamage(_myType, _damage);
    Destroy(gameObject);
  }

  UIManager _UIManager;
  Camera _mainCamera;
  BombType _myType;
  float _timer;
  float _explosionRadius;
  int _damage;
}
