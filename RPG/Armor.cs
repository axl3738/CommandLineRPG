using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum Set
{
    Turtle,
    Raider,
    None
}

namespace RPG
{
    public abstract class Armor
    {
        public double def;
        public Set set;
        string name;
        public int upgradeLevel;
        double upgradeMultiplier;
        public Armor() { }
        public Armor(double def)
        {
            this.def = def;
            upgradeLevel = 0;
            upgradeMultiplier = 1;
            this.set = Set.None;
        }

        public double GetUpgradeMultiplier()
        {
            switch (this.upgradeLevel)
            {
                case 0:
                    upgradeMultiplier = 1;
                    break;

                case 1:
                    upgradeMultiplier = 1.1;
                    break;

                case 2:
                    upgradeMultiplier = 1.2;
                    break;

                case 3:
                    upgradeMultiplier = 1.3;
                    break;

                case 4:
                    upgradeMultiplier = 1.4;
                    break;

                case 5:
                    upgradeMultiplier = 1.6;
                    break;

                case 6:
                    upgradeMultiplier = 1.8;
                    break;

                case 7:
                    upgradeMultiplier = 2.0;
                    break;

                case 8:
                    upgradeMultiplier = 2.3;
                    break;

                case 9:
                    upgradeMultiplier = 2.6;
                    break;

                case 10:
                    upgradeMultiplier = 3;
                    break;
            }

            return upgradeMultiplier;
        }
        public double GetDef(){return (int)(this.def * GetUpgradeMultiplier());}
        public void SetDef(double def){this.def = def;}
        public double DamgReduct(){return (this.def / 1000);}
        public void SetName(string name){this.name = name;}
        public int GetUpgradeLevel() { return this.upgradeLevel; }
        public void SetUpgradeLevel(int upgradeLevel) { this.upgradeLevel = upgradeLevel; }
        public Set GetSet() { return this.set; }
    }

    class LightArmor : Armor
    {  
        public LightArmor(double def)
        {
            base.def = def;
            base.upgradeLevel = 0;
            base.set = Set.None;
        }

        public override string ToString()
        {
            return "Lightarmor +" + GetUpgradeLevel() + " (" + GetDef() + ")";
        }
    }

    class MediumArmor : Armor
    {
        public MediumArmor(double def)
        {
            base.def = def;
            base.upgradeLevel = 0;
            base.set = Set.None;
        }

        public override string ToString()
        {
            return "MediumArmor +" + GetUpgradeLevel() + " (" + GetDef() + ")";
        }
    }

    class HeavyArmor : Armor
    {
        public HeavyArmor(double def)
        {
            base.def = def;
            base.upgradeLevel = 0;
            base.set = Set.None;
        }

        public override string ToString()
        {
            return "HeavyArmor +" + GetUpgradeLevel() + " (" + GetDef() + ")";
        }
    }

    class Shield : Armor
    {
        
        string name = "wooden plank shield";
        public Shield(double def)
        {
            base.def = def;
            base.upgradeLevel = 0;
            base.set = Set.None;
        }

        public override string ToString()
        {
            return name + " +" + GetUpgradeLevel() + " (" + GetDef() + ")";
        }
    }

    class LightPants : Armor
    {
        public LightPants(double def)
        {
            base.def = def;
            base.upgradeLevel = 0;
            base.set = Set.None;
        }

        public override string ToString()
        {
            return "Light Pants +" + GetUpgradeLevel() + " (" + GetDef() + ")";
        }
    }

    class MediumPants : Armor
    {
        public MediumPants(double def)
        {
            base.def = def;
            base.upgradeLevel = 0;
            base.set = Set.None;
        }

        public override string ToString()
        {
            return "Medium Pants +" + GetUpgradeLevel() + " (" + GetDef() + ")";
        }
    }

    class HeavyPants : Armor
    {
        public HeavyPants(double def)
        {
            base.def = def;
            base.upgradeLevel = 0;
            base.set = Set.None;
        }

        public override string ToString()
        {
            return "Heavy Pants +" + GetUpgradeLevel() + " (" + GetDef() + ")";
        }
    }

    class Boots : Armor
    {
        public Boots(double def)
        {
            base.def = def;
            base.upgradeLevel = 0;
            base.set = Set.None;
        }

        public override string ToString()
        {
            return "Boots +" + GetUpgradeLevel() + " (" + GetDef() + ")";
        }
    }

    class LightHelm : Armor
    {
        public LightHelm(double def)
        {
            base.def = def;
            base.upgradeLevel = 0;
            base.set = Set.None;
        }

        public override string ToString()
        {
            return "Light Helm +" + GetUpgradeLevel() + " (" + GetDef() + ")";
        }
    }

    class MediumHelm : Armor
    {
        public MediumHelm(double def)
        {
            base.def = def;
            base.upgradeLevel = 0;
            base.set = Set.None;
        }

        public override string ToString()
        {
            return "Medium Helm +" + GetUpgradeLevel() + " (" + GetDef() + ")";
        }
    }

    class HeavyHelm : Armor
    {
        public HeavyHelm(double def)
        {
            base.def = def;
            base.upgradeLevel = 0;
            base.set = Set.None;
        }

        public override string ToString()
        {
            return "Heavy Helm +" + GetUpgradeLevel() + " (" + GetDef() + ")";
        }
    }

    class Gloves : Armor
    {
        public Gloves(double def)
        {
            base.def = def;
            base.upgradeLevel = 0;
            base.set = Set.None;
        }

        public override string ToString()
        {
            return "Gloves +" + GetUpgradeLevel() + " (" + GetDef() + ")";
        }
    }

    class DamgSetHelm : Armor
    {
        Set set;
        public DamgSetHelm(double def)
        {
            base.def = def;
            base.upgradeLevel = 0;
            base.set = Set.Raider;
        }

        public override string ToString()
        {
            return "Raiders Helm +" + GetUpgradeLevel() + " (" + GetDef() + ")";
        }
    }

    class DamgSetArmor : Armor
    {
        Set set;
        public DamgSetArmor(double def)
        {
            base.def = def;
            base.upgradeLevel = 0;
            base.set = Set.Raider;
        }

        public override string ToString()
        {
            return "Raiders Armor +" + GetUpgradeLevel() + " (" + GetDef() + ")";
        }
    }

    class DamgSetPants : Armor
    {
        Set set;
        public DamgSetPants(double def)
        {
            base.def = def;
            base.upgradeLevel = 0;
            base.set = Set.Raider;
        }

        public override string ToString()
        {
            return "Raiders Pants +" + GetUpgradeLevel() + " (" + GetDef() + ")";
        }
    }

    class DamgSetBoots : Armor
    {
        Set set;
        public DamgSetBoots(double def)
        {
            base.def = def;
            base.upgradeLevel = 0;
            base.set = Set.Raider;
        }

        public override string ToString()
        {
            return "Raiders Boots +" + GetUpgradeLevel() + " (" + GetDef() + ")";
        }
    }

    class DamgSetGloves : Armor
    {
        Set set;
        public DamgSetGloves(double def)
        {
            base.def = def;
            base.upgradeLevel = 0;
            base.set = Set.Raider;
        }

        public override string ToString()
        {
            return "Raiders Gloves +" + GetUpgradeLevel() + " (" + GetDef() + ")";
        }
    }

    class DefSetHelm : Armor
    {
        Set set;
        public DefSetHelm(double def)
        {
            base.def = def;
            base.upgradeLevel = 0;
            base.set = Set.Turtle;
        }

        public override string ToString()
        {
            return "Turtle Helm +" + GetUpgradeLevel() + " (" + GetDef() + ")";
        }
    }

    class DefSetArmor : Armor
    {
        Set set;
        public DefSetArmor(double def)
        {
            base.def = def;
            base.upgradeLevel = 0;
            base.set = Set.Turtle;
        }

        public override string ToString()
        {
            return "Turtle Armor +" + GetUpgradeLevel() + " (" + GetDef() + ")";
        }
    }

    class DefSetPants : Armor
    {
        Set set;
        public DefSetPants(double def)
        {
            base.def = def;
            base.upgradeLevel = 0;
            base.set = Set.Turtle;
        }

        public override string ToString()
        {
            return "Turtle Pants +" + GetUpgradeLevel() + " (" + GetDef() + ")";
        }
    }

    class DefSetBoots : Armor
    {
        Set set;
        public DefSetBoots(double def)
        {
            base.def = def;
            base.upgradeLevel = 0;
            base.set = Set.Turtle;
        }

        public override string ToString()
        {
            return "Turtle Boots +" + GetUpgradeLevel() + " (" + GetDef() + ")";
        }
    }

    class DefSetGloves : Armor
    {
        Set set;
        public DefSetGloves(double def)
        {
            base.def = def;
            base.upgradeLevel = 0;
            base.set = Set.Turtle;
        }

        public override string ToString()
        {
            return "Turtle Gloves +" + GetUpgradeLevel() + " (" + GetDef() + ")";
        }
    }
}
