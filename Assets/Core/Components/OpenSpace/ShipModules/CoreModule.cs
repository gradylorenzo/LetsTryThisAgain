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
        public Rigidbody rb                 { get; private set; }

        [Header("Stats")]
        [SerializeField]
        private StatSet _baseStats;
        private StatSet _modifiedStats;
        private StatSet _currentStats;
        public StatSet baseStats
        {
            get { return _baseStats; }
        }
        public StatSet modifiedStats
        {
            get { return _modifiedStats; }
        }
        public StatSet currentStats
        {
            get { return _currentStats; }
        }

        public ModifierSet modifiers;

        private void Start()
        {
            equipment = GetComponent<EquipmentModule>();
            integrity = GetComponent<IntegrityModule>();
            effects =   GetComponent<EffectsModule>();
            mobility =  GetComponent<MobilityModule>();
            rb =        GetComponent<Rigidbody>();

            equipment.SetCoreModule(this);
            integrity.SetCoreModule(this);
            effects.SetCoreModule(this);
            mobility.SetCoreModule(this);

            CalculateStats();
        }

        private void CalculateStats()
        {
            CalculateDefense();
            CalculateOffense();
            CalculatePower();
            CalculateMobility();
            CalculateCargo();
            ApplyStatsToCurrent();
        }
        private void CalculateDefense()
        {
            Defense allPercentage = new Defense();
            Defense allFlat = new Defense();
            foreach (DefenseModifier dm in modifiers.defense)
            {
                if (dm.type == ModifierType.Percentage)
                {
                    allPercentage += dm.bonus;
                }
                else
                {
                    allFlat += dm.bonus;
                }
            }
            _modifiedStats.defense = _baseStats.defense + (_baseStats.defense * allPercentage);
            _modifiedStats.defense += allFlat;
        }
        private void CalculateOffense()
        {
            Offense allPercentage = new Offense();
            Offense allFlat = new Offense();
            foreach(OffenseModifier om in modifiers.offense)
            {
                if(om.type == ModifierType.Percentage)
                {
                    allPercentage += om.bonus;
                }
                else
                {
                    allFlat += om.bonus;
                }
            }
            _modifiedStats.offense = _baseStats.offense + (_baseStats.offense * allPercentage);
            _modifiedStats.offense += allFlat;
        }
        private void CalculatePower()
        {
            Power allPercentage = new Power();
            Power allFlat = new Power();
            foreach (PowerModifier pm in modifiers.power)
            {
                if(pm.type == ModifierType.Percentage)
                {
                    allPercentage += pm.bonus;
                }
                else
                {
                    allFlat += pm.bonus;
                }
            }

            _modifiedStats.power = _baseStats.power + (_baseStats.power * allPercentage);
            _modifiedStats.power += allFlat;
        }
        private void CalculateMobility()
        {
            Mobility allPercentage = new Mobility();
            Mobility allFlat = new Mobility();
            foreach(MobilityModifier mm in modifiers.mobility)
            {
                if(mm.type == ModifierType.Percentage)
                {
                    allPercentage += mm.bonus;
                }
                else
                {
                    allFlat += mm.bonus;
                }
            }

            _modifiedStats.mobility = _baseStats.mobility + (_baseStats.mobility * allPercentage);
            _modifiedStats.mobility += allFlat;
        }
        private void CalculateCargo()
        {
            Cargo allPercentage = new Cargo();
            Cargo allFlat = new Cargo();

            foreach(CargoModifier cm in modifiers.cargo)
            {
                if(cm.type == ModifierType.Percentage)
                {
                    allPercentage += cm.bonus;
                }
                else
                {
                    allFlat += cm.bonus;
                }
            }

            _modifiedStats.cargo = _baseStats.cargo + (_baseStats.cargo * allPercentage);
            _modifiedStats.cargo += allFlat;
        }
        private void ApplyStatsToCurrent()
        {
            _currentStats = _modifiedStats;
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