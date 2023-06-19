using UnityEngine;
using TMPro;

public class UIDistanceText : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] DistanceMeter _distanceMeter;

    void Update()
    {
        var distance = _distanceMeter.GetDistance();
        _text.text = ((int)distance).ToString();
    }
}
