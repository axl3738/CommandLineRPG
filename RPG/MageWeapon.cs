using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public abstract class MageWeapon
    {
        public int damg;
        public int upgradeLevel;
        double upgradeMultiplier;
        public MageWeapon() { }
        public MageWeapon(int damg)
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
        abstract public int CalcDamg(int mag);
        public void SetUpgradeLevel(int upgradeLevel) { this.upgradeLevel = upgradeLevel; }
        public int GetUpgradeLevel() { return this.upgradeLevel; }
    }

    class Staff : MageWeapon
    {
        
        int upgradeLevel;
        public Staff() { }
        public Staff(int damg)
        {
            base.damg = damg;
            upgradeLevel = 0;
        }

        public override int CalcDamg(int mag)
        {
            return GetDamg() + (int)(2.5 * mag);
        }

        public override string ToString()
        {
            return "Staff +" + GetUpgradeLevel() + " (" + GetDamg() + ")";
        }
    }

    class LegendaryStaff : Staff
    {
        
        int upgradeLevel;
        public LegendaryStaff(int damg)
        {
            base.damg = damg;
            upgradeLevel = 0;
        }

        public override int CalcDamg(int mag)
        {
            return GetDamg() + (int)(5.0 * mag);
        }

        public override string ToString()
        {
            return "Wizards Eye +" + GetUpgradeLevel() + " (" + GetDamg() + ")";
        }
    }
}
