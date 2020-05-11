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
                    if (dm.skillAssociation == "")
                    {
                        allPercentage += dm.bonus;
                    }
                    else
                    {
                        allPercentage += dm.bonus * GameManager.gameData.skillsData.GetSkillIndex(dm.skillAssociation);
                    }
                }
                else
                {
                    if (dm.skillAssociation == "")
                    {
                        allFlat += dm.bonus;
                    }
                    else
                    {
                        allFlat += dm.bonus * GameManager.gameData.skillsData.GetSkillIndex(dm.skillAssociation);
                    }
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
                    if (om.skillAssociation == "")
                    {
                        allPercentage += om.bonus;
                    }
                    else
                    {
                        allPercentage += om.bonus * GameManager.gameData.skillsData.GetSkillIndex(om.skillAssociation);
                    }
                }
                else
                {
                    if (om.skillAssociation == "")
                    {
                        allFlat += om.bonus;
                    }
                    else
                    {
                        allFlat += om.bonus * GameManager.gameData.skillsData.GetSkillIndex(om.skillAssociation);
                    }
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
                    if (pm.skillAssociation == "")
                    {
                        allPercentage += pm.bonus;
                    }
                    else
                    {
                        allPercentage += pm.bonus * GameManager.gameData.skillsData.GetSkillIndex(pm.skillAssociation);
                    }
                }
                else
                {
                    if (pm.skillAssociation == "")
                    {
                        allFlat += pm.bonus;
                    }
                    else
                    {
                        allFlat += pm.bonus * GameManager.gameData.skillsData.GetSkillIndex(pm.skillAssociation);
                    }
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
                    if (mm.skillAssociation == "")
                    {
                        allPercentage += mm.bonus;
                    }
                    else
                    {
                        allPercentage += mm.bonus * GameManager.gameData.skillsData.GetSkillIndex(mm.skillAssociation);
                    }
                }
                else
                {
                    if (mm.skillAssociation == "")
                    {
                        allFlat += mm.bonus;
                    }
                    else
                    {
                        allFlat += mm.bonus * GameManager.gameData.skillsData.GetSkillIndex(mm.skillAssociation);
                    }
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
                    if (cm.skillAssociation == "")
                    {
                        allPercentage += cm.bonus;
                    }
                    else
                    {
                        allPercentage += cm.bonus * GameManager.gameData.skillsData.GetSkillIndex(cm.skillAssociation);
                    }
                }
                else
                {
                    if (cm.skillAssociation == "")
                    {
                        allFlat += cm.bonus;
                    }
                    else
                    {
                        allFlat += cm.bonus * GameManager.gameData.skillsData.GetSkillIndex(cm.skillAssociation);
                    }
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