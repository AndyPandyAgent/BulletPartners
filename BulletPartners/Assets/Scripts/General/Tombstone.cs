using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tombstone : MonoBehaviour
{
    public float timer;
    public TextMeshProUGUI timeText;

    public void SetTimer(float time)
    {
        timer = time;
    }

    private void Update()
    {
        timeText.text = Mathf.Round(timer).ToString();

        if (timer > 0) 
        {
            timer -= Time.deltaTime;
        }
    }
}
