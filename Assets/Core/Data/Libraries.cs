using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public static class Libraries
{
    public static Skill[] skills = new Skill[]
    {
            new Skill("ship_management", "Ship Management", 0, 10),
            new Skill("adv_ship_management", "Adv. Ship Management", 0, 100),
            new Skill("cap_ship_management", "Capital Ship Management", 0, 1000),
            new Skill("hyperdrive_control", "Hyperdrive Control", 0, 10),
            new Skill("power_management", "Power Management", 0, 10),
            new Skill("railgun_control", "Railgun Control", 0, 100)
    };
}
