using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Core.Modules;
using System;

namespace Core.Modules
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(EquipmentModule))]
    [RequireComponent(typeof(IntegrityModule))]
    [RequireComponent(typeof(InputModule))]
    [RequireComponent(typeof(EffectsModule))]
    public class CoreModule : MonoBehaviour
    {
        public EquipmentModule equipmentModule;
        public IntegrityModule integrityModule;
        public InputModule inputModule;
        public EffectsModule effectsModule;

        public Camera mainCamera { get; private set; }

        private void Start()
        {
            FIND_MAIN_CAMERA();
            equipmentModule = GetComponent<EquipmentModule>();
            integrityModule = GetComponent<IntegrityModule>();
            inputModule = GetComponent<InputModule>();
            effectsModule = GetComponent<EffectsModule>();

            equipmentModule.SetCoreModule(this);
            integrityModule.SetCoreModule(this);
            inputModule.SetCoreModule(this);
            effectsModule.SetCoreModule(this);
        }

        private void Update()
        {
            FIND_MAIN_CAMERA();
        }

        private void FIND_MAIN_CAMERA()
        {
            if (!mainCamera)
            {
                mainCamera = Camera.main;
            }
        }
    }
}