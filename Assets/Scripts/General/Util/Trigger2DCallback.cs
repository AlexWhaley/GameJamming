using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Trigger2DCallback : MonoBehaviour
{
    public System.Action<Collider2D> OnEnter;
    public System.Action<Collider2D> OnStay;
    public System.Action<Collider2D> OnExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnStay(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnExit(collision);
    }
}
