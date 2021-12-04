using UnityEngine;

public class CrateController : MonoBehaviour
{
  [SerializeField] Vector3 _infoTextOffset;

  public void TakeDamage(int value)
  {
    int damage = 0;
    switch (_myType)
    {
      case CrateType.Yellow:
        damage = value;
        break;
      case CrateType.Red:
        damage = Mathf.CeilToInt(value / 2f);
        break;
      case CrateType.Blue:
        damage = value * 2;
        break;
    }
    setHealth(_health - damage);
    if (_health <= 0)
      Destroy(gameObject);
    Debug.Log($"{_myType} crate took {damage} damage. Actual bomb's damage was {value}");
  }

  public Vector3 Position => transform.position;

  public void Init(int health, CrateType type)
  {
    setHealth(health);
    _myType = type;
  }

  public void SetPosition(Vector3 value)
  {
    transform.position = value;
    _UIManager.SetUITextPosition(gameObject.GetInstanceID(), _mainCamera.WorldToScreenPoint(value) + _infoTextOffset);
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

  void setHealth(int value)
  {
    _health = value;
    _UIManager.SetUIText(gameObject.GetInstanceID(), _health.ToString());
  }

  UIManager _UIManager;
  Camera _mainCamera;
  int _health;
  CrateType _myType;
}
