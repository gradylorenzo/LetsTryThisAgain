using System;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Core.Modules;

namespace Core.Modules
{
    public class EquipmentModule : MonoBehaviour
    {
        #region Fields
        [Range(1.0f, 100.0f)]
        public float ArcSize = 10.0f;
        public bool showArcs = false;
        [Tooltip("The default weapon to spawn on the hardpoints")]
        public GameObject defaultWeapon;
        public HardpointModule[] hardpoints;
        public GameObject projectile;
        #endregion
        #region Cache
        private TurretController[] weapons;
        private CoreModule core;
        private Camera mainCamera;
        private Vector3 hitPoint;
        #endregion
        #region MB methods
        private void Start()
        {
            BuildWeaponLoadout(defaultWeapon);
        }
        private void Update()
        {
            GET_CAMERA_TAGGED_MAINCAMERA();
            UPDATE_TURRETS_FROM_CAMERA_SPHERECAST();
        }
        #endregion
        #region Public methods
        public void Fire()
        {
            foreach(TurretController tc in weapons)
            {
                if (tc.IsAimed)
                {
                    GameObject proj = Instantiate(projectile, tc.turretBarrels.position, tc.turretBarrels.rotation);
                    proj.GetComponent<ProjectileController>().SetTargetPoint(hitPoint);
                }
            }
        }

        public void SetCoreModule(CoreModule mod)
        {
            core = mod;
        }

        public void BuildWeaponLoadout(GameObject w)
        {
            List<TurretController> newWeapons = new List<TurretController>();
            foreach (HardpointModule hp in hardpoints)
            {
                GameObject newWeapon = Instantiate(w, hp.transform.position, hp.transform.rotation);
                TurretController tc = newWeapon.GetComponent<TurretController>();
                tc.limitBearing = hp.config.limitBearing;
                tc.minBearing = hp.config.minBearing;
                tc.maxBearing = hp.config.maxBearing;
                tc.maxAzimuth = hp.config.maxAzimuth;
                tc.minAzimuth = hp.config.minAzimuth;
                newWeapons.Add(tc);
                tc.transform.parent = hp.transform;
            }
            weapons = newWeapons.ToArray();
        }
        #endregion
        #region Editor methods
        public void AutoPopulate()
        {
            hardpoints = GetComponentsInChildren<HardpointModule>();
        }
        public void ClearHardpoints()
        {
            hardpoints = new HardpointModule[0];
        }
        #endregion
        #region Private methods
        private void GET_CAMERA_TAGGED_MAINCAMERA()
        {
            if (!mainCamera)
            {
                mainCamera = Camera.main;
            }
        }
        private void UPDATE_TURRETS_FROM_CAMERA_SPHERECAST()
        {
            if (mainCamera)
            {
                //Run raycast from main camera
                RaycastHit hit;
                if (Physics.SphereCast(mainCamera.transform.position, 3.0f, mainCamera.transform.forward, out hit, 500))
                {
                    hitPoint = hit.point;
                    foreach (TurretController tc in weapons)
                    {
                        tc.SetTargetPoint(hitPoint + mainCamera.transform.position);
                    }
                }
                else
                {
                    hitPoint = mainCamera.transform.forward * 500;
                    foreach (TurretController tc in weapons)
                    {
                        tc.SetTargetPoint(hitPoint + mainCamera.transform.position);
                    }
                }
            }
        }
        #endregion
    }
}