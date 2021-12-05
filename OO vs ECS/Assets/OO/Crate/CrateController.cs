using UnityEngine;

public abstract class CrateController : MonoBehaviour
{
  [SerializeField] Vector3 _infoTextOffset;

  public abstract void TakeDamage(BombType _myType, int value);

  public Vector3 Position => transform.position;

  public CrateType Type => _myType;

  public int Health => _health;

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
    _crateTotalHealthDisplay = FindObjectOfType<CrateTotalHealthDisplay>();
    _crateTotalHealthDisplay.AddCrateController(this);
    _mainCamera = Camera.main;
  }

  void OnDisable()
  {
    _UIManager.RemoveUIText(gameObject.GetInstanceID());
    _crateTotalHealthDisplay.RemoveCrateController(this);
  }

  protected void setHealth(int value)
  {
    _health = value;
    _UIManager.SetUIText(gameObject.GetInstanceID(), _health.ToString());
  }

  UIManager _UIManager;
  CrateTotalHealthDisplay _crateTotalHealthDisplay;
  Camera _mainCamera;
  protected int _health;
  protected CrateType _myType;
}
