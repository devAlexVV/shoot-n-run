using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitchController : MonoBehaviour
{
    [SerializeField]
    Transform[] weapons;

    int _selectedWeapon;

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0.0F)
        {
            _selectedWeapon = _selectedWeapon < weapons.Length - 1 ? _selectedWeapon + 1 : 0;

            SelectWeapon();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0.0F)
        {
            _selectedWeapon = _selectedWeapon > 0 ? _selectedWeapon - 1 : weapons.Length - 1;

            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int index = 0;
        foreach (var weapon in weapons)
        {
            bool isActive = index == _selectedWeapon;
            weapon.gameObject.SetActive(isActive);
            index++;
        }
    }
}
