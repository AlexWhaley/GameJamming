using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Rotor : MonoBehaviour
{
    public float Speed;

    public void Update()
    {
        transform.Rotate(new Vector3(0, 0, Speed * Time.deltaTime));
    }
}
