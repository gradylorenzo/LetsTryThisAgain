using System;

namespace Core.Data.Stats
{
    #region Stat Data
    [System.Serializable]
    public struct StatSet
    {
        #region Fields
        public Defense defense;
        public Offense offense;
        public Power power;
        public Mobility mobility;
        public Cargo cargo;
        #endregion
        #region Constructors
        public StatSet(Defense d, Offense o, Power p, Mobility m, Cargo c)
        {
            defense = d;
            offense = o;
            power = p;
            mobility = m;
            cargo = c;
        }
        #endregion
        #region Operators
        public static StatSet operator +(StatSet a, StatSet b)
        {
            StatSet c = new StatSet();
            c.defense = a.defense + b.defense;
            c.offense = a.offense + b.offense;
            c.power = a.power + b.power;
            c.mobility = a.mobility + b.mobility;
            c.cargo = a.cargo + b.cargo;
            return c;
        }
        public static StatSet operator -(StatSet a, StatSet b)
        {
            StatSet c = new StatSet();
            c.defense = a.defense - b.defense;
            c.offense = a.offense - b.offense;
            c.power = a.power - b.power;
            c.mobility = a.mobility - b.mobility;
            c.cargo = a.cargo - b.cargo;
            return c;
        }
        public static StatSet operator *(StatSet a, StatSet b)
        {
            StatSet c = new StatSet();
            c.defense = a.defense * b.defense;
            c.offense = a.offense * b.offense;
            c.power = a.power * b.power;
            c.mobility = a.mobility * b.mobility;
            c.cargo = a.cargo * b.cargo;
            return c;
        }
        public static StatSet operator /(StatSet a, StatSet b)
        {
            StatSet c = new StatSet();
            c.defense = a.defense / b.defense;
            c.offense = a.offense / b.offense;
            c.power = a.power / b.power;
            c.mobility = a.mobility / b.mobility;
            c.cargo = a.cargo / b.cargo;
            return c;
        }
        #endregion
    }
    [System.Serializable]
    public struct Defense
    {
        #region Fields
        public float shields;
        public float rechargeRate;
        public float rechargeDelay;
        public float armor;
        #endregion
        #region Constructor
        public Defense(float s, float r, float d, float a)
        {
            shields = s;
            rechargeRate = r;
            rechargeDelay = d;
            armor = a;
        }
        #endregion
        #region Operators
        public static Defense operator +(Defense a, Defense b)
        {
            Defense c = new Defense();
            c.shields = a.shields + b.shields;
            c.rechargeRate = a.rechargeRate + b.rechargeRate;
            c.rechargeDelay = a.rechargeDelay + b.rechargeDelay;
            c.armor = a.armor + b.armor;
            return c;
        }
        public static Defense operator -(Defense a, Defense b)
        {
            Defense c = new Defense();
            c.shields = a.shields - b.shields;
            c.rechargeRate = a.rechargeRate - b.rechargeRate;
            c.rechargeDelay = a.rechargeDelay - b.rechargeDelay;
            c.armor = a.armor - b.armor;
            return c;
        }
        public static Defense operator *(Defense a, Defense b)
        {
            Defense c = new Defense();
            c.shields = a.shields * b.shields;
            c.rechargeRate = a.rechargeRate * b.rechargeRate;
            c.rechargeDelay = a.rechargeDelay * b.rechargeDelay;
            c.armor = a.armor * b.armor;
            return c;
        }
        public static Defense operator /(Defense a, Defense b)
        {
            Defense c = new Defense();
            c.shields = a.shields / b.shields;
            c.rechargeRate = a.rechargeRate / b.rechargeRate;
            c.rechargeDelay = a.rechargeDelay / b.rechargeDelay;
            c.armor = a.armor / b.armor;
            return c;
        }

        public static Defense operator +(Defense a, float b)
        {
            Defense c = new Defense();
            c.shields = a.shields + b;
            c.rechargeRate = a.rechargeRate + b;
            c.rechargeDelay = a.rechargeDelay + b;
            c.armor = a.armor + b;
            return c;
        }
        public static Defense operator -(Defense a, float b)
        {
            Defense c = new Defense();
            c.shields = a.shields - b;
            c.rechargeRate = a.rechargeRate - b;
            c.rechargeDelay = a.rechargeDelay - b;
            c.armor = a.armor - b;
            return c;
        }
        public static Defense operator *(Defense a, float b)
        {
            Defense c = new Defense();
            c.shields = a.shields * b;
            c.rechargeRate = a.rechargeRate * b;
            c.rechargeDelay = a.rechargeDelay * b;
            c.armor = a.armor * b;
            return c;
        }
        public static Defense operator /(Defense a, float b)
        {
            Defense c = new Defense();
            c.shields = a.shields / b;
            c.rechargeRate = a.rechargeRate / b;
            c.rechargeDelay = a.rechargeDelay / b;
            c.armor = a.armor / b;
            return c;
        }
        #endregion
    }
    [System.Serializable]
    public struct Offense
    {
        #region Fields
        public float gaussDamage;
        public float laserDamage;
        public float missileDamage;
        #endregion
        #region Constructors
        public Offense(float g, float l, float m)
        {
            gaussDamage = g;
            laserDamage = l;
            missileDamage = m;
        }
        #endregion
        #region Operators
        public static Offense operator +(Offense a, Offense b)
        {
            Offense c = new Offense();
            c.gaussDamage = a.gaussDamage + b.gaussDamage;
            c.laserDamage = a.laserDamage + b.laserDamage;
            c.missileDamage = a.missileDamage + b.missileDamage;
            return c;
        }
        public static Offense operator -(Offense a, Offense b)
        {
            Offense c = new Offense();
            c.gaussDamage = a.gaussDamage - b.gaussDamage;
            c.laserDamage = a.laserDamage - b.laserDamage;
            c.missileDamage = a.missileDamage - b.missileDamage;
            return c;
        }
        public static Offense operator *(Offense a, Offense b)
        {
            Offense c = new Offense();
            c.gaussDamage = a.gaussDamage * b.gaussDamage;
            c.laserDamage = a.laserDamage * b.laserDamage;
            c.missileDamage = a.missileDamage * b.missileDamage;
            return c;
        }
        public static Offense operator /(Offense a, Offense b)
        {
            Offense c = new Offense();
            c.gaussDamage = a.gaussDamage / b.gaussDamage;
            c.laserDamage = a.laserDamage / b.laserDamage;
            c.missileDamage = a.missileDamage / b.missileDamage;
            return c;
        }

        public static Offense operator +(Offense a, float b)
        {
            Offense c = new Offense();
            c.gaussDamage = a.gaussDamage + b;
            c.laserDamage = a.laserDamage + b;
            c.missileDamage = a.missileDamage + b;
            return c;
        }
        public static Offense operator -(Offense a, float b)
        {
            Offense c = new Offense();
            c.gaussDamage = a.gaussDamage - b;
            c.laserDamage = a.laserDamage - b;
            c.missileDamage = a.missileDamage - b;
            return c;
        }
        public static Offense operator *(Offense a, float b)
        {
            Offense c = new Offense();
            c.gaussDamage = a.gaussDamage * b;
            c.laserDamage = a.laserDamage * b;
            c.missileDamage = a.missileDamage * b;
            return c;
        }
        public static Offense operator /(Offense a, float b)
        {
            Offense c = new Offense();
            c.gaussDamage = a.gaussDamage / b;
            c.laserDamage = a.laserDamage / b;
            c.missileDamage = a.missileDamage / b;
            return c;
        }
        #endregion
    }
    [System.Serializable]
    public struct Power
    {
        #region Fields
        public float capacity;
        public float output;
        #endregion
        #region Constructors
        public Power(float c, float o)
        {
            capacity = c;
            output = o;
        }
        #endregion
        #region Operators
        public static Power operator +(Power a, Power b)
        {
            Power c = new Power();
            c.capacity = a.capacity + b.capacity;
            c.output = a.output + b.output;
            return c;
        }
        public static Power operator -(Power a, Power b)
        {
            Power c = new Power();
            c.capacity = a.capacity - b.capacity;
            c.output = a.output - b.output;
            return c;
        }
        public static Power operator *(Power a, Power b)
        {
            Power c = new Power();
            c.capacity = a.capacity * b.capacity;
            c.output = a.output * b.output;
            return c;
        }
        public static Power operator /(Power a, Power b)
        {
            Power c = new Power();
            c.capacity = a.capacity / b.capacity;
            c.output = a.output / b.output;
            return c;
        }

        public static Power operator +(Power a, float b)
        {
            Power c = new Power();
            c.capacity = a.capacity + b;
            c.output = a.output + b;
            return c;
        }
        public static Power operator -(Power a, float b)
        {
            Power c = new Power();
            c.capacity = a.capacity - b;
            c.output = a.output - b;
            return c;
        }
        public static Power operator *(Power a, float b)
        {
            Power c = new Power();
            c.capacity = a.capacity * b;
            c.output = a.output * b;
            return c;
        }
        public static Power operator /(Power a, float b)
        {
            Power c = new Power();
            c.capacity = a.capacity / b;
            c.output = a.output / b;
            return c;
        }
        #endregion
    }
    [System.Serializable]
    public struct Mobility
    {
        #region Fields
        public float mass;
        public float thrust;
        public float torque;
        public float warp;
        #endregion
        #region Constructors
        public Mobility(float m, float t, float q, float w)
        {
            mass = m;
            thrust = t;
            torque = q;
            warp = w;
        }
        #endregion
        #region Operators
        public static Mobility operator +(Mobility a, Mobility b)
        {
            Mobility c = new Mobility();
            c.mass = a.mass + b.mass;
            c.thrust = a.thrust + b.thrust;
            c.torque = a.torque + b.torque;
            c.warp = a.warp + b.warp;
            return c;
        }
        public static Mobility operator -(Mobility a, Mobility b)
        {
            Mobility c = new Mobility();
            c.mass = a.mass - b.mass;
            c.thrust = a.thrust - b.thrust;
            c.torque = a.torque - b.torque;
            c.warp = a.warp - b.warp;
            return c;
        }
        public static Mobility operator *(Mobility a, Mobility b)
        {
            Mobility c = new Mobility();
            c.mass = a.mass * b.mass;
            c.thrust = a.thrust * b.thrust;
            c.torque = a.torque * b.torque;
            c.warp = a.warp * b.warp;
            return c;
        }
        public static Mobility operator /(Mobility a, Mobility b)
        {
            Mobility c = new Mobility();
            c.mass = a.mass / b.mass;
            c.thrust = a.thrust / b.thrust;
            c.torque = a.torque / b.torque;
            c.warp = a.warp / b.warp;
            return c;
        }

        public static Mobility operator +(Mobility a, float b)
        {
            Mobility c = new Mobility();
            c.mass = a.mass + b;
            c.thrust = a.thrust + b;
            c.torque = a.torque + b;
            c.warp = a.warp + b;
            return c;
        }
        public static Mobility operator -(Mobility a, float b)
        {
            Mobility c = new Mobility();
            c.mass = a.mass - b;
            c.thrust = a.thrust - b;
            c.torque = a.torque - b;
            c.warp = a.warp - b;
            return c;
        }
        public static Mobility operator *(Mobility a, float b)
        {
            Mobility c = new Mobility();
            c.mass = a.mass * b;
            c.thrust = a.thrust * b;
            c.torque = a.torque * b;
            c.warp = a.warp * b;
            return c;
        }
        public static Mobility operator /(Mobility a, float b)
        {
            Mobility c = new Mobility();
            c.mass = a.mass / b;
            c.thrust = a.thrust / b;
            c.torque = a.torque / b;
            c.warp = a.warp / b;
            return c;
        }
        #endregion
    }
    [System.Serializable]
    public struct Cargo
    {
        #region Fields
        public float capacity;
        #endregion
        #region Constructors
        public Cargo(float c)
        {
            capacity = c;
        }
        #endregion
        #region Operators
        public static Cargo operator +(Cargo a, Cargo b)
        {
            Cargo c = new Cargo();
            c.capacity = a.capacity + b.capacity;
            return c;
        }
        public static Cargo operator -(Cargo a, Cargo b)
        {
            Cargo c = new Cargo();
            c.capacity = a.capacity - b.capacity;
            return c;
        }
        public static Cargo operator *(Cargo a, Cargo b)
        {
            Cargo c = new Cargo();
            c.capacity = a.capacity * b.capacity;
            return c;
        }
        public static Cargo operator /(Cargo a, Cargo b)
        {
            Cargo c = new Cargo();
            c.capacity = a.capacity / b.capacity;
            return c;
        }

        public static Cargo operator +(Cargo a, float b)
        {
            Cargo c = new Cargo();
            c.capacity = a.capacity + b;
            return c;
        }
        public static Cargo operator -(Cargo a, float b)
        {
            Cargo c = new Cargo();
            c.capacity = a.capacity - b;
            return c;
        }
        public static Cargo operator *(Cargo a, float b)
        {
            Cargo c = new Cargo();
            c.capacity = a.capacity * b;
            return c;
        }
        public static Cargo operator /(Cargo a, float b)
        {
            Cargo c = new Cargo();
            c.capacity = a.capacity / b;
            return c;
        }
        #endregion
    }
    #endregion

    #region Stat Modifiers
    [Serializable]
    public struct ModifierSet
    {
        public DefenseModifier[] defense;
        public OffenseModifier[] offense;
        public PowerModifier[] power;
        public MobilityModifier[] mobility;
        public CargoModifier[] cargo;
    }
    [Serializable]
    public struct DefenseModifier
    {
        public ModifierType type;
        public Defense bonus;
        public string skillAssociation;
    }
    [Serializable]
    public struct OffenseModifier
    {
        public ModifierType type;
        public Offense bonus;
        public string skillAssociation;
    }
    [Serializable]
    public struct PowerModifier
    {
        public ModifierType type;
        public Power bonus;
        public string skillAssociation;
    }
    [Serializable]
    public struct MobilityModifier
    {
        public ModifierType type;
        public Mobility bonus;
        public string skillAssociation;
    }
    [Serializable]
    public struct CargoModifier
    {
        public ModifierType type;
        public Cargo bonus;
        public string skillAssociation;
    }
    [Serializable]
    public enum ModifierType
    {
        Flat,
        Percentage
    }
    #endregion
}
