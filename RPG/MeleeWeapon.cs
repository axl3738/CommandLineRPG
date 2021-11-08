using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public abstract class MeleeWeapon
    {
        public int damg;
        public int upgradeLevel;
        public double upgradeMultiplier;
        public MeleeWeapon() { }
        public MeleeWeapon(int damg)
        {
            this.damg = damg;
            upgradeLevel = 0;
            upgradeMultiplier = 1;
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

        public void SetDamg(int damg) { this.damg = damg; }
        public int GetDamg() { return (int)(this.damg * GetUpgradeMultiplier()); }
        abstract public int CalcDamg(int str);
        public int GetUpgradeLevel() { return this.upgradeLevel; }
        public void SetUpgradeLevel(int upgradeLevel) { this.upgradeLevel = upgradeLevel; }
    }

    class LongSword : MeleeWeapon
    {
        int upgradeLevel;
        public LongSword() { }
        public LongSword(int damg)
        {
            base.damg = damg;
            upgradeLevel = 0;
        }

        public override int CalcDamg(int str)
        {
            return GetDamg() + (int)(2.5 * str);
        }

        public override string ToString()
        {
            return "Longsword +" + GetUpgradeLevel() + " (" + GetDamg() + ")";
        }
    }

    class ShortSword : MeleeWeapon
    {
        int upgradeLevel;
        public ShortSword() { }
        public ShortSword(int damg)
        {
            base.damg = damg;
            upgradeLevel = 0;
        }

        public override int CalcDamg(int str)
        {
            return GetDamg() + (int)(str * 2);
        }

        public override string ToString()
        {
            return "Shortsword +" + GetUpgradeLevel() + " (" + GetDamg() + ")";
        }
    }

    class Dagger : MeleeWeapon
    {
        int upgradeLevel;
        public Dagger() { }
        public Dagger(int damg)
        {
            base.damg = damg;
            upgradeLevel = 0;
        }

        public override int CalcDamg(int dex)
        {
            return GetDamg() + (2 * dex);
        }


        public override string ToString()
        {
            return "Dagger +" + GetUpgradeLevel() + " (" + GetDamg() + ")";
        }
    }

    class TwinDaggers : MeleeWeapon
    {
        int upgradeLevel;
        public TwinDaggers() { }
        public TwinDaggers(int damg)
        {
            base.damg = damg;
            upgradeLevel = 0;
        }

        public override int CalcDamg(int dex)
        {
            return GetDamg() + (int)(2.5 * dex);
        }

        public override string ToString()
        {
            return "Twindaggers +" + GetUpgradeLevel() + " (" + GetDamg() + ")";
        }
    }

    class LegendaryLongSword : LongSword
    {
        
        int upgradeLevel;
        public LegendaryLongSword(int damg)
        {
            base.damg = damg;
            upgradeLevel = 0;
        }

        public override int CalcDamg(int str)
        {
            return GetDamg() + (int)(5 * str);
        }

        public override string ToString()
        {
            return "Devastator +" + GetUpgradeLevel() + " (" + GetDamg() + ")";
        }
    }

    class LegendaryDagger : Dagger
    {
        
        int upgradeLevel;
        public LegendaryDagger(int damg)
        {
            base.damg = damg;
            upgradeLevel = 0;
        }

        public override int CalcDamg(int dex)
        {
            return GetDamg() + (int)(4.5 * dex);
        }

        public override string ToString()
        {
            return "Shadow Knife +" + GetUpgradeLevel() + " (" + GetDamg() + ")";
        }
    }

    class LegendaryTwinDaggers : TwinDaggers
    {
        
        int upgradeLevel;
        public LegendaryTwinDaggers(int damg)
        {
            base.damg = damg;
            upgradeLevel = 0;
        }

        public override int CalcDamg(int dex)
        {
            return GetDamg() + (int)(6.0 * dex);
        }

        public override string ToString()
        {
            return "Twin Assassins +" + GetUpgradeLevel() + " (" + GetDamg() + ")";
        }
    }

    class LegendaryShortSword : ShortSword
    {
        
        int upgradeLevel;
        public LegendaryShortSword(int damg)
        {
            base.damg = damg;
            upgradeLevel = 0;
        }

        public override int CalcDamg(int str)
        {
            return GetDamg() + (int)(4.5 * str);
        }

        public override string ToString()
        {
            return "Short Devastation +" + GetUpgradeLevel() + " (" + GetDamg() + ")";
        }
    }

}
