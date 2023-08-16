using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera aimVirtualCamera;

    [SerializeField]
    GameObject aimPanel;

    [SerializeField]
    float normalSensitivity = 1.0F;

    [SerializeField]
    float aimSentitivity = 0.5F;

    [SerializeField]
    GunController gunController;

    ThirdPersonController _thirdPersonController;

    StarterAssetsInputs _starterAssetsInputs;

    void Awake()
    {
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        if (_starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            _thirdPersonController.SetSensitivity(aimSentitivity);
            _thirdPersonController.SetRotateOnMove(false);
            aimPanel.SetActive(true);

            Vector3 mouseWorldPosition = Vector3.zero;
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2.0F, Screen.height / 2.0F);
            Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, 1000.0F))
            {
                mouseWorldPosition = raycastHit.point;
            }

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;

            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            //transform.forward = aimDirection;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 15.0F);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            _thirdPersonController.SetSensitivity(normalSensitivity);
            _thirdPersonController.SetRotateOnMove(true);
            aimPanel.SetActive(false);
        }
    }

    public void shoot() {
        gunController._nextFireTime = Time.time + 1.0F / gunController.fireRate;
        gunController.animator.SetTrigger("shoot");
        gunController.Shoot();
        gunController.animator.ResetTrigger("shoot");
    }
}
