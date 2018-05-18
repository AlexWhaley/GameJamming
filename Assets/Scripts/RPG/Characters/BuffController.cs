using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{

    [SerializeField]
    private GameObject _shieldBuff;
    [SerializeField]
    private GameObject _attackBuff;

    public void SetShieldBuffActive(bool isActive)
    {
        _shieldBuff.SetActive(isActive);
    }

    public void SetAttackBuffActive(bool isActive)
    {
        _attackBuff.SetActive(isActive);
    }
}
