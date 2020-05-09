using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Core.Modules;
using System;
using Core.Data.Stats;

namespace Core.Modules
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(EquipmentModule))]
    [RequireComponent(typeof(IntegrityModule))]
    [RequireComponent(typeof(EffectsModule))]
    [RequireComponent(typeof(MobilityModule))]
    public class CoreModule : MonoBehaviour
    {
        public EquipmentModule equipment    { get; private set; }
        public IntegrityModule integrity    { get; private set; }
        public EffectsModule effects        { get; private set; }
        public MobilityModule mobility      { get; private set; }

        public StatSet baseStats;
        public StatSet modifiedStats        { get; private set; }
        public StatSet currentStats         { get; private set; }

        private void Start()
        {
            equipment = GetComponent<EquipmentModule>();
            integrity = GetComponent<IntegrityModule>();
            effects =   GetComponent<EffectsModule>();
            mobility =  GetComponent<MobilityModule>();

            equipment.SetCoreModule(this);
            integrity.SetCoreModule(this);
            effects.SetCoreModule(this);
            mobility.SetCoreModule(this);
        }

        #region Module Calls
        public void SetMobilityVectors(Vector3 d, float r)
        {
            mobility.SetVectors(d, r);
            effects.SetVectors(d, r);
        }
        #endregion
    }
}