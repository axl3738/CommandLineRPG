using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using ConsoleApplication1;

namespace RPG
{
    public class Player
    {
        private int health, str, dex, mag, vit, end, atn, exp, level, mapLevel, attunementSlots, heals, arrows, bagSpace;
        private MageWeapon mageWep;
        private RangedWeapon rangedWep;
        private MeleeWeapon mWep;
        private Armor armor, shield, helm, pants, boots, gloves;
        int maxHealth;
        int vitMult = 5;
        ArrayList equipmentBag = new ArrayList();
        ArrayList itemBag = new ArrayList();
        ArrayList spells = new ArrayList();
        ArrayList spellsEquipped = new ArrayList();

        /* Keeping if I want to change to properties instead of get/set methods
        public Armor Armor
        {
            get { return this.armor; }
            set { this.armor = value; }
        }

        public Armor Shield
        {
            get { return this.shield; }
            set { this.shield = value; }
        }

        public Armor Helm
        {
            get { return this.helm; }
            set { this.helm = value; }
        }

        public Armor Pants
        {
            get { return this.pants; }
            set { this.pants = value; }
        }

        public Armor Boots
        {
            get { return this.boots; }
            set { this.boots = value; }
        }

        public Armor Gloves
        {
            get { return this.gloves; }
            set { this.gloves = value; }
        }*/

        public Player(int health, int str, int dex, int mag, int vit, int end, int atn, int exp, int level, int mapLevel)
        {
            this.health = health;
            this.str = str;
            this.dex = dex;
            this.mag = mag;
            this.vit = vit;
            this.end = end;
            this.atn = atn;
            this.exp = exp;
            this.level = level;
            this.mapLevel = mapLevel;
            maxHealth = vit * vitMult;
            attunementSlots = atn;
            arrows = 10;
            bagSpace = end * 5;
        }

        public int GetArrows()
        {
            return this.arrows;
        }

        public int GetMaxHealth()
        {
            return this.maxHealth;
        }

        public int GetHealth()
        {
            return this.health;
        }

        public int GetStr()
        {
            return this.str;
        }

        public void IncreaseStr()
        {
            this.str++;
        }

        public int GetDex()
        {
            return this.dex;
        }

        public void IncreaseDex()
        {
            this.dex++;
        }

        public int GetMag()
        {
            return this.mag;
        }

        public void IncreaseMag()
        {
            this.mag++;
        }

        public int GetVit()
        {
            return this.vit;
        }

        public void IncreaseVit()
        {
            this.vit++;
        }

        public int GetEnd()
        {
            return this.end;
        }

        public void IncreaseEnd()
        {
            this.end++;
        }

        public int GetAttunement()
        {
            return this.atn;
        }

        public void IncreaseAttunement()
        {
            this.atn++;
        }

        public int GetExp()
        {
            return this.exp;
        }

        public void SetExp(int exp)
        {
            this.exp = exp;
        }

        public int GetLevel()
        {
            return this.level;
        }

        public void SetLevel(int level)
        {
            this.level = level;
        }

        public int GetMapLevel()
        {
            return this.mapLevel;
        }

        public int GetHeals()
        {
            return this.heals;
        }

        public void SetHeals(int heals)
        {
            this.heals = heals;
        }

        public int GetBagSpace()
        {
            this.bagSpace = this.end * 5;
            return this.bagSpace;
        }

        public void AddBagSpace(int amount)
        {
            this.bagSpace = bagSpace + amount;
        }

        public void DecreaseHealth(int damg)
        {
            this.health = health - damg;
        }

        public void IncreaseHealth(int heal)
        {
            this.health = health + heal;
        }

        /// <summary>
        /// Used to set health:
        /// 0. Update health to new vit
        /// 1. Update health to max health
        /// </summary>
        /// <param name="verb"></param>
        public void SetHealth(int verb)
        {
            if (verb == 1)
            {
                health = maxHealth;
            }
            else
            {
                maxHealth = vit * vitMult;
                health = maxHealth;
            }
        }

        public void DecreaseArrows(int num)
        {
            this.arrows = arrows - num;
        }

        public void IncreaseArrows(int num)
        {
            this.arrows = arrows + num;
        }

        public void SetArrows(int num)
        {
            this.arrows = num;
        }

        public void IncreaseAttunementSlots(int num)
        {
            this.attunementSlots = attunementSlots + num;
        }

        public void DecreaseAttunementSlots(int num)
        {
            this.attunementSlots = attunementSlots - num;
        }

        public int GetAttunementSlots()
        {
            return this.attunementSlots;
        }

        public void SetMageWeapon(MageWeapon mageWep)
        {
            this.mageWep = mageWep;
        }

        public void SetRangedWeapon(RangedWeapon rangedWep)
        {
            this.rangedWep = rangedWep;
        }

        public void Status()
        {
            if (armor != null)
            {
                Console.WriteLine("\nArmor: " + armor);
            }
            else
            {
                Console.WriteLine("\nArmor: " + armor);
            }
            Console.WriteLine("Melee weapon: " + mWep);
            if (mageWep != null)
            {
                Console.WriteLine("Mage weapon: " + mageWep);
            }
            else
            {
                Console.WriteLine("Mage weapon: " + mageWep);
            }
            if (rangedWep != null)
            {
                Console.WriteLine("Ranged weapon: " + rangedWep);
            }
            else
            {
                Console.WriteLine("Ranged weapon: " + rangedWep);
            }
            if (shield != null)
            {
                Console.WriteLine("Shield: " + shield);
            }
            else
            {
                Console.WriteLine("Shield: " + shield);
            }
            if (helm != null)
            {
                Console.WriteLine("Helm: " + helm);
            }
            else
            {
                Console.WriteLine("Helm: " + helm);
            }
            if (pants != null)
            {
                Console.WriteLine("Pants: " + pants);
            }
            else
            {
                Console.WriteLine("Pants: " + pants);
            }
            if (boots != null)
            {
                Console.WriteLine("Boots: " + boots);
            }
            else
            {
                Console.WriteLine("Boots: " + boots);
            }
            if (gloves != null)
            {
                Console.WriteLine("Gloves: " + gloves);
            }
            else
            {
                Console.WriteLine("Gloves: " + gloves);
            }
            Console.WriteLine();

        }

        public void SetMWeapon(MeleeWeapon mWeapon)
        {
            this.mWep = mWeapon;
        }

        public MeleeWeapon GetMWeapon()
        {
            return this.mWep;
        }

        public void SetArmor(Armor armor)
        {
            this.armor = armor;
        }

        public Armor GetArmor()
        {
            return this.armor;
        }

        public void SetShield(Armor shield)
        {
            this.shield = shield;
        }

        public Armor GetShield()
        {
            return this.shield;
        }

        public void SetHelm(Armor helm)
        {
            this.helm = helm;
        }

        public Armor GetHelm()
        {
            return this.helm;
        }

        public void SetPants(Armor pants)
        {
            this.pants = pants;
        }

        public Armor GetPants()
        {
            return this.pants;
        }

        public void SetBoots(Armor boots)
        {
            this.boots = boots;
        }

        public Armor GetBoots()
        {
            return this.boots;
        }

        public void SetGloves(Armor gloves)
        {
            this.gloves = gloves;
        }

        public Armor GetGloves()
        {
            return this.gloves;
        }

        public void ChangeMWeapon(MeleeWeapon wep, int index)
        {
            if (wep != null)
            {
                equipmentBag.Add(wep);
            }
            SetMWeapon((MeleeWeapon)equipmentBag[index]);
            equipmentBag.RemoveAt(index);
        }

        public void ChangeArmor(Armor armor, int index)
        {
            if (equipmentBag[index] is Shield)
            {
                if (shield != null)
                {
                    equipmentBag.Add(shield);
                }
                SetShield((Armor)equipmentBag[index]);
            }
            else if (equipmentBag[index] is HeavyHelm || equipmentBag[index] is MediumHelm || equipmentBag[index] is LightHelm 
                                                      || equipmentBag[index] is DefSetHelm || equipmentBag[index] is DamgSetHelm)
            {
                if (helm != null)
                {
                    equipmentBag.Add(helm);
                }
                SetHelm((Armor)equipmentBag[index]);
            }
            else if (equipmentBag[index] is HeavyPants || equipmentBag[index] is MediumPants || equipmentBag[index] is LightPants 
                                                       || equipmentBag[index] is DefSetPants || equipmentBag[index] is DamgSetPants)
            {
                if (pants != null)
                {
                    equipmentBag.Add(pants);
                }
                SetPants((Armor)equipmentBag[index]);
            }
            else if (equipmentBag[index] is Boots || equipmentBag[index] is DefSetBoots || equipmentBag[index] is DamgSetBoots)
            {
                if (boots != null)
                {
                    equipmentBag.Add(boots);
                }
                SetBoots((Armor)equipmentBag[index]);
            }
            else if (equipmentBag[index] is Gloves || equipmentBag[index] is DefSetGloves || equipmentBag[index] is DamgSetGloves)
            {
                if (gloves != null)
                {
                    equipmentBag.Add(gloves);
                }
                SetGloves((Armor)equipmentBag[index]);
            }
            else
            {
                if (equipmentBag[index] != null)
                {
                    equipmentBag.Add(armor);
                }
                SetArmor((Armor)equipmentBag[index]);
            }
            equipmentBag.RemoveAt(index);
        }

        public RangedWeapon GetRangedWeapon()
        {
            return this.rangedWep;
        }

        public void ChangeRangedWeapon(RangedWeapon rangedWep, int index)
        {
            if (rangedWep != null)
            {
                equipmentBag.Add(rangedWep);
            }
            SetRangedWeapon((RangedWeapon)equipmentBag[index]);
            equipmentBag.RemoveAt(index);
        }

        public MageWeapon GetMageWeapon()
        {
            return this.mageWep;
        }

        public void ChangeMageWeapon(MageWeapon mageWep, int index)
        {
            if (mageWep != null)
            {
                equipmentBag.Add(mageWep);
            }
            SetMageWeapon((MageWeapon)equipmentBag[index]);
            equipmentBag.RemoveAt(index);

        }

        public ArrayList GetEquipmentBag()
        {
            return this.equipmentBag;
        }

        public void DisplayEquipmentBag()
        {
            string input;
            int index;
            for (int i = 0; i < equipmentBag.Count; i++)
            {
                Console.WriteLine("(" + i + ") " + equipmentBag[i]);
            }
            if (equipmentBag.Count != 0)
            {
                Console.WriteLine("Change Equipment Bag? (Type in the number to switch or type in no to exit)");
                input = Console.ReadLine();
                if (int.TryParse(input, out index) == true)
                {
                    if (index >= 0 && index < equipmentBag.Count)
                    {
                        if (equipmentBag[index] is MeleeWeapon)
                        {
                            ChangeMWeapon(this.mWep, index);
                        }
                        else if (equipmentBag[index] is Armor)
                        {
                            if (equipmentBag[index] is Shield)
                            {
                                ChangeArmor(this.shield, index);
                            }
                            else if (equipmentBag[index] is HeavyHelm || equipmentBag[index] is MediumHelm || equipmentBag[index] is LightHelm 
                                                                      || equipmentBag[index] is DefSetHelm || equipmentBag[index] is DamgSetHelm)
                            {
                                ChangeArmor(this.helm, index);
                            }
                            else if (equipmentBag[index] is HeavyPants || equipmentBag[index] is MediumPants || equipmentBag[index] is LightPants 
                                                                       || equipmentBag[index] is DefSetPants|| equipmentBag[index] is DamgSetPants)
                            {
                                ChangeArmor(this.pants, index);
                            }
                            else if (equipmentBag[index] is Boots || equipmentBag[index] is DefSetBoots|| equipmentBag[index] is DamgSetBoots)
                            {
                                ChangeArmor(this.boots, index);
                            }
                            else if (equipmentBag[index] is Gloves || equipmentBag[index] is DefSetGloves || equipmentBag[index] is DamgSetGloves)
                            {
                                ChangeArmor(this.gloves, index);
                            }
                            else
                            {
                                ChangeArmor(this.armor, index);
                            }
                        }
                        else if (equipmentBag[index] is MageWeapon)
                        {
                            ChangeMageWeapon(this.mageWep, index);
                        }
                        else if (equipmentBag[index] is RangedWeapon)
                        {
                            ChangeRangedWeapon(this.rangedWep, index);
                        }
                    }
                    else
                    {
                        Console.WriteLine("index entered not in range");
                    }
                }
            }
        }

        public void AddEquipment(int level)
        {
            int sSwordDamg = 0, lSwordDamg = 0, daggerDamg = 0, twinDaggerDamg = 0, staffDamg = 0, bowDamg = 0, var = 0;
            int hArmorDef = 0, mArmorDef = 0, lArmorDef = 0, shieldDef = 0, hHelmDef = 0, mHelmDef = 0, lHelmDef = 0;
            int hPantsDef = 0, mPantsDef = 0, lPantsDef = 0, bootsDef = 0, glovesDef = 0;
            Random rng = new Random();
            MeleeWeapon shortSword = new ShortSword(0);
            MeleeWeapon longSword = new LongSword(0);
            MeleeWeapon dagger = new Dagger(0);
            MeleeWeapon twinDaggers = new TwinDaggers(0);
            Armor hArmor = new HeavyArmor(0);
            Armor mArmor = new MediumArmor(0);
            Armor lArmor = new LightArmor(0);
            Armor shield = new Shield(0);
            Armor hHelm = new HeavyHelm(0);
            Armor mHelm = new MediumHelm(0);
            Armor lHelm = new LightHelm(0);
            Armor hPants = new HeavyPants(0);
            Armor mPants = new MediumPants(0);
            Armor lPants = new LightPants(0);
            Armor boots = new Boots(0);
            Armor gloves = new Gloves(0);
            MageWeapon staff = new Staff(0);
            RangedWeapon bow = new Bow(0);

            if (level == 0)
            {
                sSwordDamg = rng.Next(10, 25);
                lSwordDamg = rng.Next(12, 30);
                daggerDamg = rng.Next(8, 23);
                twinDaggerDamg = rng.Next(10, 25);
                hArmorDef = rng.Next(15, 35);
                mArmorDef = rng.Next(12, 25);
                lArmorDef = rng.Next(8, 23);
                shieldDef = rng.Next(2, 15);
                hHelmDef = rng.Next(11, 25);
                mHelmDef = rng.Next(8, 15);
                lHelmDef = rng.Next(4, 10);
                hPantsDef = rng.Next(20, 30);
                mPantsDef = rng.Next(10, 20);
                lPantsDef = rng.Next(5, 15);
                bootsDef = rng.Next(2, 15);
                glovesDef = rng.Next(2, 15);
                staffDamg = rng.Next(12, 30);
                bowDamg = rng.Next(12, 30);
            }
            else if (level == 1)
            {
                sSwordDamg = rng.Next(40, 60);
                lSwordDamg = rng.Next(40, 60);
                daggerDamg = rng.Next(40, 55);
                twinDaggerDamg = rng.Next(40, 55);
                hArmorDef = rng.Next(50, 70);
                mArmorDef = rng.Next(40, 60);
                lArmorDef = rng.Next(30, 50);
                shieldDef = rng.Next(20, 30);
                hHelmDef = rng.Next(30, 50);
                mHelmDef = rng.Next(20, 40);
                lHelmDef = rng.Next(15, 35);
                hPantsDef = rng.Next(40, 60);
                mPantsDef = rng.Next(25, 45);
                lPantsDef = rng.Next(20, 40);
                bootsDef = rng.Next(20, 30);
                glovesDef = rng.Next(20, 30);
                staffDamg = rng.Next(40, 60);
                bowDamg = rng.Next(40, 60);
            }
            else if (level == 2)
            {
                sSwordDamg = rng.Next(100, 110);
                lSwordDamg = rng.Next(110, 120);
                daggerDamg = rng.Next(100, 110);
                twinDaggerDamg = rng.Next(110, 120);
                hArmorDef = rng.Next(110, 120);
                mArmorDef = rng.Next(95, 105);
                lArmorDef = rng.Next(80, 90);
                shieldDef = rng.Next(50, 60);
                hHelmDef = rng.Next(90, 100);
                mHelmDef = rng.Next(80, 90);
                lHelmDef = rng.Next(60, 70);
                hPantsDef = rng.Next(100, 110);
                mPantsDef = rng.Next(90, 100);
                lPantsDef = rng.Next(70, 80);
                bootsDef = rng.Next(50, 60);
                glovesDef = rng.Next(50, 60);
                staffDamg = rng.Next(90, 110);
                bowDamg = rng.Next(90, 110);
            }
            else if (level == 3)
            {
                sSwordDamg = rng.Next(220, 240);
                lSwordDamg = rng.Next(235, 250);
                daggerDamg = rng.Next(220, 240);
                twinDaggerDamg = rng.Next(235, 250);
                hArmorDef = rng.Next(225, 250);
                mArmorDef = rng.Next(200, 210);
                lArmorDef = rng.Next(170, 180);
                shieldDef = rng.Next(125, 150);
                hHelmDef = rng.Next(200, 210);
                mHelmDef = rng.Next(170, 180);
                lHelmDef = rng.Next(150, 160);
                hPantsDef = rng.Next(205, 215);
                mPantsDef = rng.Next(185, 195);
                lPantsDef = rng.Next(160, 170);
                bootsDef = rng.Next(125, 150);
                glovesDef = rng.Next(125, 150);
                staffDamg = rng.Next(225, 250);
                bowDamg = rng.Next(225, 250);
            }

            shortSword.SetDamg(sSwordDamg);
            longSword.SetDamg(lSwordDamg);
            dagger.SetDamg(daggerDamg);
            twinDaggers.SetDamg(twinDaggerDamg);
            hArmor.SetDef(hArmorDef);
            mArmor.SetDef(mArmorDef);
            lArmor.SetDef(lArmorDef);
            shield.SetDef(shieldDef);
            hHelm.SetDef(hHelmDef);
            mHelm.SetDef(mHelmDef);
            lHelm.SetDef(lHelmDef);
            hPants.SetDef(hPantsDef);
            mPants.SetDef(mPantsDef);
            lPants.SetDef(lPantsDef);
            gloves.SetDef(glovesDef);
            boots.SetDef(bootsDef);
            staff.SetDamg(staffDamg);
            bow.SetDamg(bowDamg);

            var = rng.Next(0, 18);
            switch (var)
            {
                case 0:
                    equipmentBag.Add(shortSword);
                    break;

                case 1:
                    equipmentBag.Add(longSword);
                    break;

                case 2:
                    equipmentBag.Add(dagger);
                    break;

                case 3:
                    equipmentBag.Add(twinDaggers);
                    break;

                case 4:
                    equipmentBag.Add(hArmor);
                    break;

                case 5:
                    equipmentBag.Add(mArmor);
                    break;

                case 6:
                    equipmentBag.Add(lArmor);
                    break;

                case 7:
                    equipmentBag.Add(bow);
                    break;

                case 8:
                    equipmentBag.Add(staff);
                    break;

                case 9:
                    equipmentBag.Add(shield);
                    break;

                case 10:
                    equipmentBag.Add(hHelm);
                    break;

                case 11:
                    equipmentBag.Add(mHelm);
                    break;

                case 12:
                    equipmentBag.Add(lHelm);
                    break;

                case 13:
                    equipmentBag.Add(hPants);
                    break;

                case 14:
                    equipmentBag.Add(mPants);
                    break;

                case 15:
                    equipmentBag.Add(lPants);
                    break;

                case 16:
                    equipmentBag.Add(boots);
                    break;

                case 17:
                    equipmentBag.Add(gloves);
                    break;
            }
        }

        public void AddSetItem()
        {
            int var = 0;
            Random rng = new Random();
            Armor turtleHelm = new DefSetHelm(200);
            Armor turtleArmor = new DefSetArmor(200);
            Armor turtlePants= new DefSetPants(200);
            Armor turtleGloves = new DefSetGloves(140);
            Armor turtleBoots = new DefSetBoots(140);
            Armor raidersHelm = new DamgSetHelm(200);
            Armor raidersArmor = new DamgSetArmor(200);
            Armor raidersPants = new DamgSetPants(200);
            Armor raidersGloves = new DamgSetGloves(140);
            Armor raidersBoots = new DamgSetBoots(140);

            var = rng.Next(2);
            if (var == 0)
            {
                var = rng.Next(0, 5);
                switch (var)
                {
                    case 0:
                        equipmentBag.Add(turtleHelm);
                        break;

                    case 1:
                        equipmentBag.Add(turtleArmor);
                        break;

                    case 2:
                        equipmentBag.Add(turtlePants);
                        break;

                    case 3:
                        equipmentBag.Add(turtleGloves);
                        break;

                    case 4:
                        equipmentBag.Add(turtleBoots);
                        break;
                }
            }
            else
            {
                var = rng.Next(0, 5);
                switch (var)
                {
                    case 0:
                        equipmentBag.Add(raidersHelm);
                        break;

                    case 1:
                        equipmentBag.Add(raidersArmor);
                        break;

                    case 2:
                        equipmentBag.Add(raidersPants);
                        break;

                    case 3:
                        equipmentBag.Add(raidersGloves);
                        break;

                    case 4:
                        equipmentBag.Add(raidersBoots);
                        break;
                }
            }
        }

        public ArrayList GetItemBag()
        {
            return this.itemBag;
        }

        public void DisplayItemBag()
        {
            string input;
            int index;
            for (int i = 0; i < itemBag.Count; i++)
            {
                Console.WriteLine(itemBag[i]);
            }
            if (itemBag.Count != 0)
            {
       
            }
        }

        public ArrayList GetSpells()
        {
            return this.spells;
        }

        public void AddSpell(Spell spell)
        {
            spells.Add(spell);
        }

        public void RemoveSpell(int index)
        {
            spells.RemoveAt(index);
        }

        public ArrayList GetSpellsEquipped()
        {
            return this.spellsEquipped;
        }

        public void AddSpellsToEquipped(Spell spell)
        {
            spellsEquipped.Add(spell);
        }

        public void RemoveSpellsFromEquipped(int index)
        {
            spellsEquipped.RemoveAt(index);
        }

        public void ReplenishSpellCasts()
        {
            for (int i = 0; i < spellsEquipped.Count; i++)
            {
                ((Spell)spellsEquipped[i]).resetCastTimes();
            }
        }

    }
}
