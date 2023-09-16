using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CostPricerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void SetMoney(int money) => _text.SetText(money.ToString());
}
