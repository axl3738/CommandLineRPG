using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public abstract class Spell
    {
        string name;
        public int attunementSlots, castTimes, damg;
        public Spell() { }
        public Spell(int attunementSlots, int castTimes, int damg)
        {
            this.attunementSlots = attunementSlots;
            this.castTimes = castTimes;
            this.damg = damg;
        }

        public int getDamg() { return this.damg; }
        public void SetDamg(int damg){this.damg = damg;}
        public int getCastTimes() { return this.castTimes; }
        public void setCastTimes(int castTimes) { this.castTimes = castTimes; }
        public int getAttunementSlots() { return this.attunementSlots; }
        public void setAttunementSlots(int attunementSlots) { this.attunementSlots = attunementSlots; }
        public void decreaseCastTimes() { this.castTimes--; }
        public abstract void resetCastTimes();
        public abstract int use();
        public abstract string GetName();
    }

    class MagicMissile : Spell
    {
        string name;
        int attunementSlots, castTimes, damg, maxCastTimes;
        public MagicMissile(int attunementSlots, int castTimes, int damg)
        {
            base.attunementSlots = attunementSlots;
            base.castTimes = castTimes;
            base.damg = damg;
            maxCastTimes = 10;
            name = "Magic Missile";
        }

        public override void resetCastTimes()
        {
            castTimes = maxCastTimes;
        }

        public override string GetName()
        {
            return this.name;
        }

        public override int use()
        {
            return 0;
        }

        public override string ToString()
        {
            return (name + ", Attunement slots: " + getAttunementSlots() + ", casts: " + getCastTimes() + ", damg: " +
                        getDamg());
        }
    }

    class IronArmor : Spell
    {
        string name;
        int attunementSlots, castTimes, damg, maxCastTimes;
        public IronArmor(int attunementSlots, int castTimes, int damg)
        {
            base.attunementSlots = attunementSlots;
            base.castTimes = castTimes;
            base.damg = damg;
            maxCastTimes = 5;
            name = "Iron Armor";
        }

        public override void resetCastTimes()
        {
            castTimes = maxCastTimes;
        }

        public override string GetName()
        {
            return this.name;
        }

        // Returns the amount of turns the spell is active for
        public override int use()
        {
            return 10;
        }

        public override string ToString()
        {
            return (name + ", Attunement slots: " + getAttunementSlots() + ", casts: " + getCastTimes());
        }
    }

    class SwiftEvasion : Spell
    {
        string name;
        int attunementSlots, castTimes, damg, maxCastTimes;
        public SwiftEvasion(int attunementSlots, int castTimes, int damg)
        {
            base.attunementSlots = attunementSlots;
            base.castTimes = castTimes;
            base.damg = damg;
            maxCastTimes = 5;
            name = "Swift Evasion";
        }

        public override void resetCastTimes()
        {
            castTimes = maxCastTimes;
        }

        public override string GetName()
        {
            return this.name;
        }

        // Returns the amount of turns the spell is active for
        public override int use()
        {
            return 10;
        }

        public override string ToString()
        {
            return (name + ", Attunement slots: " + getAttunementSlots() + ", casts: " + getCastTimes());
        }
    }
}
