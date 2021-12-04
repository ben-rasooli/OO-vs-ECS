using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
  [SerializeField] GameObject _UITextPrefab;

  public void SetUIText(int ID, string value)
  {
    getUIText(ID).text = value;
  }

  public void SetUITextPosition(int ID, Vector3 value)
  {
    if (_UITexts.ContainsKey(ID))
      _UITexts[ID].rectTransform.position = value;
  }

  public void RemoveUIText(int ID)
  {
    if (_UITexts.ContainsKey(ID))
    {
      if (_UITexts[ID] != null)
        Destroy(_UITexts[ID].gameObject);
    }
    _UITexts.Remove(ID);
  }

  public static UIManager Instance { get; private set; }

  private void Awake()
  {
    if (Instance == null)
      Instance = this;
  }

  TextMeshProUGUI getUIText(int ID)
  {
    if (_UITexts.ContainsKey(ID))
      return _UITexts[ID];

    TextMeshProUGUI UIText = Instantiate(_UITextPrefab, transform).GetComponent<TextMeshProUGUI>();
    _UITexts.Add(ID, UIText);
    return UIText;
  }

  Dictionary<int, TextMeshProUGUI> _UITexts = new Dictionary<int, TextMeshProUGUI>();
}
