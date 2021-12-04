using UnityEngine;

public class YellowCrateController : CrateController
{
  public override void TakeDamage(BombType bombType, int value)
  {
    if (bombType == BombType.Pepper)
    {
      _alreadyAffectedByPepperBomb = true;
      Debug.Log("Yellow crate got a sticky Pepper bomb");
    }
    else if (bombType == BombType.Chilly && _alreadyAffectedByPepperBomb)
    {
      _alreadyAffectedByPepperBomb = false;
      var damage = value;
      setHealth(_health - damage);
      if (_health <= 0)
        Destroy(gameObject);
      Debug.Log($"{_myType} crate took {damage} damage. Actual bomb's damage was {value}");
    }
  }

  bool _alreadyAffectedByPepperBomb = false;
}