using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [HideInInspector]public Gun gun;
    [SerializeField] private TextMeshProUGUI ammoUI;


    // Update is called once per frame
    void Update()
    {
        if (gun == null)
            return;
        ammoUI.text = gun.currentBullets + "/" + gun.bulletsPerMag;
    }
}
