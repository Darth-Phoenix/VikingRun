using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text timeText;
    public Text coinsText;

    public void Setup(float time, int coin)
    {
        gameObject.SetActive(true);
        timeText.text = (Mathf.Round(time * 100) / 100.0f).ToString() + "  SECONDS";
        if (coin <= 1)
        {
            coinsText.text = coin.ToString() + "  COIN";
        }
        else
        {
            coinsText.text = coin.ToString() + "  COINS";
        }
    }
}
