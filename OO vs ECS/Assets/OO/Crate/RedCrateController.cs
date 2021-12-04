using UnityEngine;

public class RedCrateController : CrateController
{
  public override void TakeDamage(BombType bombType, int value)
  {
    if (bombType == BombType.Chilly)
    {
      _alreadyAffectedByChillyBomb = true;
      Debug.Log("Red crate got a sticky Chilly bomb");
    }
    else if (bombType == BombType.Pepper && _alreadyAffectedByChillyBomb)
    {
      _alreadyAffectedByChillyBomb = false;
      var damage = Mathf.CeilToInt(value / 2f);
      setHealth(_health - damage);
      if (_health <= 0)
        Destroy(gameObject);
      Debug.Log($"{_myType} crate took {damage} damage. Actual bomb's damage was {value}");
    }
  }

  bool _alreadyAffectedByChillyBomb;
}