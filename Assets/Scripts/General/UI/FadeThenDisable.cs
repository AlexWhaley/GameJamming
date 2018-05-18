using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class FadeThenDisable : MonoBehaviour
{
    public float Duration = 3f;
    private bool fade = false;
    private float time = 0;

    public void StartFade()
    {
        fade = true;
        time = Duration;
    }

    private void Update()
    {
        if (fade)
        {
            time -= Time.deltaTime;

            GetComponent<Image>().color = new Color(1, 1, 1, time / Duration);

            if (time <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}