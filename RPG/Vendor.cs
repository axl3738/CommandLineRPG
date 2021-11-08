using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace RPG
{
    abstract class Vendor
    {
        string name;
        int rarity, numOfItems;

        public Vendor() { }
        public Vendor(int rarity, int numOfItems)
        {
            this.rarity = rarity;
            this.numOfItems = numOfItems;
        }

        public abstract void generateItems();
        public abstract ArrayList getItems();
    }

    class ArmorSeller : Vendor
    {
        string name;
        int rarity, numOfItems, choice;
        Armor armor;
        Random r = new Random();
        ArrayList items = new ArrayList();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rarity">0-50: common, 51-80: uncommon, 81-95 rare, 96-100: legendary, continues in groups of 100</param>
        /// <param name="numOfItems"></param>
        public ArmorSeller(int rarity, int numOfItems)
        {
            name = "Armor Seller";
            this.rarity = rarity;
            this.numOfItems = numOfItems;
            generateItems();
        }

        public override void generateItems()
        {
            for (int i = 0; i < numOfItems; i++)
            {
                choice = r.Next(0, 12);
                switch (choice)
                {

                    case 0:
                        if (rarity >= 0 && rarity <= 50)
                        {
                            armor = new LightArmor(r.Next(1, 10));
                        }
                        else if (rarity >= 51 && rarity <= 80)
                        {
                            armor = new LightArmor(r.Next(5, 15));
                        }
                        else if (rarity >= 81 && rarity <= 95)
                        {
                            armor = new LightArmor(r.Next(10, 20));
                        }
                        else if (rarity >= 96 && rarity <= 100)
                        {
                            armor = new LightArmor(r.Next(30, 40));
                        }
                        else if (rarity >= 101 && rarity <= 150)
                        {
                            armor = new LightArmor(r.Next(40, 50));
                        }
                        else if (rarity >= 151 && rarity <= 180)
                        {
                            armor = new LightArmor(r.Next(50, 60));
                        }
                        else if (rarity >= 181 && rarity <= 195)
                        {
                            armor = new LightArmor(r.Next(60, 70));
                        }
                        else if(rarity >= 196 && rarity <= 200)
                        {
                            armor = new LightArmor(r.Next(70, 85));
                        }
                        else if (rarity >= 201 && rarity <= 250)
                        {
                            armor = new LightArmor(r.Next(100, 110));
                        }
                        else if (rarity >= 251 && rarity <= 280)
                        {
                            armor = new LightArmor(r.Next(125, 135));
                        }
                        else if (rarity >= 281 && rarity <= 295)
                        {
                            armor = new LightArmor(r.Next(145, 155));
                        }
                        else if (rarity >= 296 && rarity <= 300)
                        {
                            armor = new LightArmor(r.Next(170, 180));
                        }
                        break;

                    case 1:
                        if (rarity >= 0 && rarity <= 50)
                        {
                            armor = new MediumArmor(r.Next(5, 15));
                        }
                        else if (rarity >= 51 && rarity <= 80)
                        {
                            armor = new MediumArmor(r.Next(10, 20));
                        }
                        else if (rarity >= 81 && rarity <= 95)
                        {
                            armor = new MediumArmor(r.Next(20, 30));
                        }
                        else if (rarity >= 96 && rarity <= 100)
                        {
                            armor = new MediumArmor(r.Next(40, 50));
                        }
                        else if (rarity >= 101 && rarity <= 150)
                        {
                            armor = new MediumArmor(r.Next(50, 55));
                        }
                        else if (rarity >= 151 && rarity <= 180)
                        {
                            armor = new MediumArmor(r.Next(55, 65));
                        }
                        else if (rarity >= 181 && rarity <= 195)
                        {
                            armor = new MediumArmor(r.Next(65, 75));
                        }
                        else if (rarity >= 196 && rarity <= 200)
                        {
                            armor = new MediumArmor(r.Next(80, 95));
                        }
                        else if (rarity >= 201 && rarity <= 250)
                        {
                            armor = new MediumArmor(r.Next(115, 125));
                        }
                        else if (rarity >= 251 && rarity <= 280)
                        {
                            armor = new MediumArmor(r.Next(145, 155));
                        }
                        else if (rarity >= 281 && rarity <= 295)
                        {
                            armor = new MediumArmor(r.Next(165, 185));
                        }
                        else if (rarity >= 296 && rarity <= 300)
                        {
                            armor = new MediumArmor(r.Next(200, 210));
                        }
                        break;

                    case 2:
                        if (rarity >= 0 && rarity <= 50)
                        {
                            armor = new HeavyArmor(r.Next(10, 20));
                        }
                        else if (rarity >= 51 && rarity <= 80)
                        {
                            armor = new HeavyArmor(r.Next(15, 25));
                        }
                        else if (rarity >= 81 && rarity <= 95)
                        {
                            armor = new HeavyArmor(r.Next(25, 35));
                        }
                        else if (rarity >= 96 && rarity <= 100)
                        {
                            armor = new HeavyArmor(r.Next(45, 55));
                        }
                        else if (rarity >= 101 && rarity <= 150)
                        {
                            armor = new HeavyArmor(r.Next(55, 65));
                        }
                        else if (rarity >= 151 && rarity <= 180)
                        {
                            armor = new HeavyArmor(r.Next(70, 80));
                        }
                        else if (rarity >= 181 && rarity <= 195)
                        {
                            armor = new HeavyArmor(r.Next(80, 90));
                        }
                        else if (rarity >= 196 && rarity <= 200)
                        {
                            armor = new HeavyArmor(r.Next(100, 120));
                        }
                        else if (rarity >= 201 && rarity <= 250)
                        {
                            armor = new HeavyArmor(r.Next(140, 150));
                        }
                        else if (rarity >= 251 && rarity <= 280)
                        {
                            armor = new HeavyArmor(r.Next(165, 175));
                        }
                        else if (rarity >= 281 && rarity <= 295)
                        {
                            armor = new HeavyArmor(r.Next(185, 200));
                        }
                        else if (rarity >= 296 && rarity <= 300)
                        {
                            armor = new HeavyArmor(r.Next(225, 250));
                        }
                        break;

                    case 3:
                        if (rarity >= 0 && rarity <= 50)
                        {
                            armor = new Shield(r.Next(1, 5));
                        }
                        else if (rarity >= 51 && rarity <= 80)
                        {
                            armor = new Shield(r.Next(3, 8));
                        }
                        else if (rarity >= 81 && rarity <= 95)
                        {
                            armor = new Shield(r.Next(5, 10));
                        }
                        else if (rarity >= 96 && rarity <= 100)
                        {
                            armor = new Shield(r.Next(10, 15));
                        }
                        else if (rarity >= 101 && rarity <= 150)
                        {
                            armor = new Shield(r.Next(20, 25));
                        }
                        else if (rarity >= 151 && rarity <= 180)
                        {
                            armor = new Shield(r.Next(30, 35));
                        }
                        else if (rarity >= 181 && rarity <= 195)
                        {
                            armor = new Shield(r.Next(40, 45));
                        }
                        else if (rarity >= 196 && rarity <= 200)
                        {
                            armor = new Shield(r.Next(55, 65));
                        }
                        else if (rarity >= 201 && rarity <= 250)
                        {
                            armor = new Shield(r.Next(65, 75));
                        }
                        else if (rarity >= 251 && rarity <= 280)
                        {
                            armor = new Shield(r.Next(80, 90));
                        }
                        else if (rarity >= 281 && rarity <= 295)
                        {
                            armor = new Shield(r.Next(90, 100));
                        }
                        else if (rarity >= 296 && rarity <= 300)
                        {
                            armor = new Shield(r.Next(125, 140));
                        }
                        break;

                    case 4:
                        if (rarity >= 0 && rarity <= 50)
                        {
                            armor = new LightHelm(r.Next(1, 10));
                        }
                        else if (rarity >= 51 && rarity <= 80)
                        {
                            armor = new LightHelm(r.Next(5, 15));
                        }
                        else if (rarity >= 81 && rarity <= 95)
                        {
                            armor = new LightHelm(r.Next(10, 20));
                        }
                        else if (rarity >= 96 && rarity <= 100)
                        {
                            armor = new LightHelm(r.Next(30, 40));
                        }
                        else if (rarity >= 101 && rarity <= 150)
                        {
                            armor = new LightHelm(r.Next(40, 50));
                        }
                        else if (rarity >= 151 && rarity <= 180)
                        {
                            armor = new LightHelm(r.Next(50, 60));
                        }
                        else if (rarity >= 181 && rarity <= 195)
                        {
                            armor = new LightHelm(r.Next(60, 70));
                        }
                        else if (rarity >= 196 && rarity <= 200)
                        {
                            armor = new LightHelm(r.Next(70, 85));
                        }
                        else if (rarity >= 201 && rarity <= 250)
                        {
                            armor = new LightHelm(r.Next(100, 110));
                        }
                        else if (rarity >= 251 && rarity <= 280)
                        {
                            armor = new LightHelm(r.Next(125, 135));
                        }
                        else if (rarity >= 281 && rarity <= 295)
                        {
                            armor = new LightHelm(r.Next(145, 155));
                        }
                        else if (rarity >= 296 && rarity <= 300)
                        {
                            armor = new LightHelm(r.Next(170, 180));
                        }
                        break;

                    case 5:
                        if (rarity >= 0 && rarity <= 50)
                        {
                            armor = new MediumHelm(r.Next(5, 15));
                        }
                        else if (rarity >= 51 && rarity <= 80)
                        {
                            armor = new MediumHelm(r.Next(10, 20));
                        }
                        else if (rarity >= 81 && rarity <= 95)
                        {
                            armor = new MediumHelm(r.Next(20, 30));
                        }
                        else if (rarity >= 96 && rarity <= 100)
                        {
                            armor = new MediumHelm(r.Next(40, 50));
                        }
                        else if (rarity >= 101 && rarity <= 150)
                        {
                            armor = new MediumHelm(r.Next(50, 55));
                        }
                        else if (rarity >= 151 && rarity <= 180)
                        {
                            armor = new MediumHelm(r.Next(55, 65));
                        }
                        else if (rarity >= 181 && rarity <= 195)
                        {
                            armor = new MediumHelm(r.Next(65, 75));
                        }
                        else if (rarity >= 196 && rarity <= 200)
                        {
                            armor = new MediumHelm(r.Next(80, 95));
                        }
                        else if (rarity >= 201 && rarity <= 250)
                        {
                            armor = new MediumHelm(r.Next(115, 125));
                        }
                        else if (rarity >= 251 && rarity <= 280)
                        {
                            armor = new MediumHelm(r.Next(145, 155));
                        }
                        else if (rarity >= 281 && rarity <= 295)
                        {
                            armor = new MediumHelm(r.Next(165, 185));
                        }
                        else if (rarity >= 296 && rarity <= 300)
                        {
                            armor = new MediumHelm(r.Next(200, 210));
                        }
                        break;

                    case 6:
                        if (rarity >= 0 && rarity <= 50)
                        {
                            armor = new HeavyHelm(r.Next(10, 20));
                        }
                        else if (rarity >= 51 && rarity <= 80)
                        {
                            armor = new HeavyHelm(r.Next(15, 25));
                        }
                        else if (rarity >= 81 && rarity <= 95)
                        {
                            armor = new HeavyHelm(r.Next(25, 35));
                        }
                        else if (rarity >= 96 && rarity <= 100)
                        {
                            armor = new HeavyHelm(r.Next(45, 55));
                        }
                        else if (rarity >= 101 && rarity <= 150)
                        {
                            armor = new HeavyHelm(r.Next(55, 65));
                        }
                        else if (rarity >= 151 && rarity <= 180)
                        {
                            armor = new HeavyHelm(r.Next(70, 80));
                        }
                        else if (rarity >= 181 && rarity <= 195)
                        {
                            armor = new HeavyHelm(r.Next(80, 90));
                        }
                        else if (rarity >= 196 && rarity <= 200)
                        {
                            armor = new HeavyHelm(r.Next(100, 120));
                        }
                        else if (rarity >= 201 && rarity <= 250)
                        {
                            armor = new HeavyHelm(r.Next(140, 150));
                        }
                        else if (rarity >= 251 && rarity <= 280)
                        {
                            armor = new HeavyHelm(r.Next(165, 175));
                        }
                        else if (rarity >= 281 && rarity <= 295)
                        {
                            armor = new HeavyHelm(r.Next(185, 200));
                        }
                        else if (rarity >= 296 && rarity <= 300)
                        {
                            armor = new HeavyHelm(r.Next(225, 250));
                        }
                        break;

                    case 7:
                        if (rarity >= 0 && rarity <= 50)
                        {
                            armor = new LightPants(r.Next(1, 10));
                        }
                        else if (rarity >= 51 && rarity <= 80)
                        {
                            armor = new LightPants(r.Next(5, 15));
                        }
                        else if (rarity >= 81 && rarity <= 95)
                        {
                            armor = new LightPants(r.Next(10, 20));
                        }
                        else if (rarity >= 96 && rarity <= 100)
                        {
                            armor = new LightPants(r.Next(30, 40));
                        }
                        else if (rarity >= 101 && rarity <= 150)
                        {
                            armor = new LightPants(r.Next(40, 50));
                        }
                        else if (rarity >= 151 && rarity <= 180)
                        {
                            armor = new LightPants(r.Next(50, 60));
                        }
                        else if (rarity >= 181 && rarity <= 195)
                        {
                            armor = new LightPants(r.Next(60, 70));
                        }
                        else if (rarity >= 196 && rarity <= 200)
                        {
                            armor = new LightPants(r.Next(70, 85));
                        }
                        else if (rarity >= 201 && rarity <= 250)
                        {
                            armor = new LightPants(r.Next(100, 110));
                        }
                        else if (rarity >= 251 && rarity <= 280)
                        {
                            armor = new LightPants(r.Next(125, 135));
                        }
                        else if (rarity >= 281 && rarity <= 295)
                        {
                            armor = new LightPants(r.Next(145, 155));
                        }
                        else if (rarity >= 296 && rarity <= 300)
                        {
                            armor = new LightPants(r.Next(170, 180));
                        }
                        break;

                    case 8:
                        if (rarity >= 0 && rarity <= 50)
                        {
                            armor = new MediumPants(r.Next(5, 15));
                        }
                        else if (rarity >= 51 && rarity <= 80)
                        {
                            armor = new MediumPants(r.Next(10, 20));
                        }
                        else if (rarity >= 81 && rarity <= 95)
                        {
                            armor = new MediumPants(r.Next(20, 30));
                        }
                        else if (rarity >= 96 && rarity <= 100)
                        {
                            armor = new MediumPants(r.Next(40, 50));
                        }
                        else if (rarity >= 101 && rarity <= 150)
                        {
                            armor = new MediumPants(r.Next(50, 55));
                        }
                        else if (rarity >= 151 && rarity <= 180)
                        {
                            armor = new MediumPants(r.Next(55, 65));
                        }
                        else if (rarity >= 181 && rarity <= 195)
                        {
                            armor = new MediumPants(r.Next(65, 75));
                        }
                        else if (rarity >= 196 && rarity <= 200)
                        {
                            armor = new MediumPants(r.Next(80, 95));
                        }
                        else if (rarity >= 201 && rarity <= 250)
                        {
                            armor = new MediumPants(r.Next(115, 125));
                        }
                        else if (rarity >= 251 && rarity <= 280)
                        {
                            armor = new MediumPants(r.Next(145, 155));
                        }
                        else if (rarity >= 281 && rarity <= 295)
                        {
                            armor = new MediumPants(r.Next(165, 185));
                        }
                        else if (rarity >= 296 && rarity <= 300)
                        {
                            armor = new MediumPants(r.Next(200, 210));
                        }
                        break;

                    case 9:
                        if (rarity >= 0 && rarity <= 50)
                        {
                            armor = new HeavyPants(r.Next(10, 20));
                        }
                        else if (rarity >= 51 && rarity <= 80)
                        {
                            armor = new HeavyPants(r.Next(15, 25));
                        }
                        else if (rarity >= 81 && rarity <= 95)
                        {
                            armor = new HeavyPants(r.Next(25, 35));
                        }
                        else if (rarity >= 96 && rarity <= 100)
                        {
                            armor = new HeavyPants(r.Next(45, 55));
                        }
                        else if (rarity >= 101 && rarity <= 150)
                        {
                            armor = new HeavyPants(r.Next(55, 65));
                        }
                        else if (rarity >= 151 && rarity <= 180)
                        {
                            armor = new HeavyPants(r.Next(70, 80));
                        }
                        else if (rarity >= 181 && rarity <= 195)
                        {
                            armor = new HeavyPants(r.Next(80, 90));
                        }
                        else if (rarity >= 196 && rarity <= 200)
                        {
                            armor = new HeavyPants(r.Next(100, 120));
                        }
                        else if (rarity >= 201 && rarity <= 250)
                        {
                            armor = new HeavyPants(r.Next(140, 150));
                        }
                        else if (rarity >= 251 && rarity <= 280)
                        {
                            armor = new HeavyPants(r.Next(165, 175));
                        }
                        else if (rarity >= 281 && rarity <= 295)
                        {
                            armor = new HeavyPants(r.Next(185, 200));
                        }
                        else if (rarity >= 296 && rarity <= 300)
                        {
                            armor = new HeavyPants(r.Next(225, 250));
                        }
                        break;

                    case 10:
                        if (rarity >= 0 && rarity <= 50)
                        {
                            armor = new Boots(r.Next(1, 5));
                        }
                        else if (rarity >= 51 && rarity <= 80)
                        {
                            armor = new Boots(r.Next(3, 8));
                        }
                        else if (rarity >= 81 && rarity <= 95)
                        {
                            armor = new Boots(r.Next(5, 10));
                        }
                        else if (rarity >= 96 && rarity <= 100)
                        {
                            armor = new Boots(r.Next(10, 15));
                        }
                        else if (rarity >= 101 && rarity <= 150)
                        {
                            armor = new Boots(r.Next(20, 25));
                        }
                        else if (rarity >= 151 && rarity <= 180)
                        {
                            armor = new Boots(r.Next(30, 35));
                        }
                        else if (rarity >= 181 && rarity <= 195)
                        {
                            armor = new Boots(r.Next(40, 45));
                        }
                        else if (rarity >= 196 && rarity <= 200)
                        {
                            armor = new Boots(r.Next(55, 65));
                        }
                        else if (rarity >= 201 && rarity <= 250)
                        {
                            armor = new Boots(r.Next(65, 75));
                        }
                        else if (rarity >= 251 && rarity <= 280)
                        {
                            armor = new Boots(r.Next(80, 90));
                        }
                        else if (rarity >= 281 && rarity <= 295)
                        {
                            armor = new Boots(r.Next(90, 100));
                        }
                        else if (rarity >= 296 && rarity <= 300)
                        {
                            armor = new Boots(r.Next(125, 140));
                        }
                        break;

                    case 11:
                        if (rarity >= 0 && rarity <= 50)
                        {
                            armor = new Gloves(r.Next(1, 5));
                        }
                        else if (rarity >= 51 && rarity <= 80)
                        {
                            armor = new Gloves(r.Next(3, 8));
                        }
                        else if (rarity >= 81 && rarity <= 95)
                        {
                            armor = new Gloves(r.Next(5, 10));
                        }
                        else if (rarity >= 96 && rarity <= 100)
                        {
                            armor = new Gloves(r.Next(10, 15));
                        }
                        else if (rarity >= 101 && rarity <= 150)
                        {
                            armor = new Gloves(r.Next(20, 25));
                        }
                        else if (rarity >= 151 && rarity <= 180)
                        {
                            armor = new Gloves(r.Next(30, 35));
                        }
                        else if (rarity >= 181 && rarity <= 195)
                        {
                            armor = new Gloves(r.Next(40, 45));
                        }
                        else if (rarity >= 196 && rarity <= 200)
                        {
                            armor = new Gloves(r.Next(55, 65));
                        }
                        else if (rarity >= 201 && rarity <= 250)
                        {
                            armor = new Gloves(r.Next(65, 75));
                        }
                        else if (rarity >= 251 && rarity <= 280)
                        {
                            armor = new Gloves(r.Next(80, 90));
                        }
                        else if (rarity >= 281 && rarity <= 295)
                        {
                            armor = new Gloves(r.Next(90, 100));
                        }
                        else if (rarity >= 296 && rarity <= 300)
                        {
                            armor = new Gloves(r.Next(125, 140));
                        }
                        break;
                }
                items.Add(armor);
            }
        }


        public override ArrayList getItems()
        {
            return items;
        }

        public override string ToString()
        {
            return this.name;
        }
    }

    /// <summary>
    /// Need to add in more spells
    /// </summary>
    class SpellsSeller : Vendor
    {
        string name;
        int rarity, numOfItems, choice;
        Spell spell;
        Random r = new Random();
        ArrayList items = new ArrayList();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rarity">0-50: common, 51-80: uncommon, 81-95 rare, 96-100: legendary, continues in groups of 100</param>
        /// <param name="numOfItems"></param>
        public SpellsSeller(int rarity, int numOfItems)
        {
            name = "Spells Seller";
            this.rarity = rarity;
            this.numOfItems = numOfItems;
            generateItems();
        }

        public override void generateItems()
        {
            for (int i = 0; i < numOfItems; i++)
            {
                choice = r.Next(0, 3);
                switch (choice)
                {

                    case 0:
                        if (rarity >= 0 && rarity <= 50)
                        {
                            spell = new MagicMissile(3, 10, r.Next(1, 10));
                        }
                        else if (rarity >= 51 && rarity <= 80)
                        {
                            spell = new MagicMissile(4, 10, r.Next(5, 15));
                        }
                        else if (rarity >= 81 && rarity <= 95)
                        {
                            spell = new MagicMissile(5, 10, r.Next(10, 20));
                        }
                        else if (rarity >= 96 && rarity <= 100)
                        {
                            spell = new MagicMissile(5, 10, r.Next(20, 30));
                        }
                        else if (rarity >= 101 && rarity <= 150)
                        {
                            spell = new MagicMissile(5, 10, r.Next(40, 50));
                        }
                        else if (rarity >= 151 && rarity <= 180)
                        {
                            spell = new MagicMissile(5, 10, r.Next(55, 60));
                        }
                        else if (rarity >= 181 && rarity <= 195)
                        {
                            spell = new MagicMissile(5, 10, r.Next(60, 70));
                        }
                        else if (rarity >= 196 && rarity <= 200)
                        {
                            spell = new MagicMissile(5, 10, r.Next(85, 100));
                        }
                        else if (rarity >= 201 && rarity <= 250)
                        {
                            spell = new MagicMissile(5, 10, r.Next(125, 140));
                        }
                        else if (rarity >= 251 && rarity <= 280)
                        {
                            spell = new MagicMissile(5, 10, r.Next(150, 175));
                        }
                        else if (rarity >= 281 && rarity <= 295)
                        {
                            spell = new MagicMissile(5, 10, r.Next(180, 200));
                        }
                        else if (rarity >= 296 && rarity <= 300)
                        {
                            spell = new MagicMissile(5, 10, r.Next(215, 235));
                        }
                        items.Add(spell);
                        break;

                    case 1:
                        if (rarity >= 0 && rarity <= 50)
                        {
                            spell = new IronArmor(5, r.Next(1, 3), 0);
                        }
                        else if (rarity >= 51 && rarity <= 80)
                        {
                            spell = new IronArmor(5, r.Next(2, 4), 0);
                        }
                        else if (rarity >= 81 && rarity <= 95)
                        {
                            spell = new IronArmor(6, r.Next(3, 5), 0);
                        }
                        else if (rarity >= 96 && rarity <= 100)
                        {
                            spell = new IronArmor(6, r.Next(4, 6), 0);
                        }
                        else if (rarity >= 101 && rarity <= 150)
                        {
                            spell = new IronArmor(7, r.Next(4, 6), 0);
                        }
                        else if (rarity >= 151 && rarity <= 180)
                        {
                            spell = new IronArmor(7, r.Next(5, 7), 0);
                        }
                        else if (rarity >= 181 && rarity <= 195)
                        {
                            spell = new IronArmor(8, r.Next(6, 8), 0);
                        }
                        else if (rarity >= 196 && rarity <= 200)
                        {
                            spell = new IronArmor(8, r.Next(7, 9), 0);
                        }
                        else if (rarity >= 201 && rarity <= 250)
                        {
                            spell = new IronArmor(9, r.Next(8, 10), 0);
                        }
                        else if (rarity >= 251 && rarity <= 280)
                        {
                            spell = new IronArmor(9, r.Next(9, 11), 0);
                        }
                        else if (rarity >= 281 && rarity <= 295)
                        {
                            spell = new IronArmor(10, r.Next(10, 12), 0);
                        }
                        else if (rarity >= 296 && rarity <= 300)
                        {
                            spell = new IronArmor(10, r.Next(11, 13), 0);
                        }
                        items.Add(spell);
                        break;

                    case 2:
                        if (rarity >= 0 && rarity <= 50)
                        {
                            spell = new SwiftEvasion(5, r.Next(1, 3), 0);
                        }
                        else if (rarity >= 51 && rarity <= 80)
                        {
                            spell = new SwiftEvasion(5, r.Next(2, 4), 0);
                        }
                        else if (rarity >= 81 && rarity <= 95)
                        {
                            spell = new SwiftEvasion(6, r.Next(3, 5), 0);
                        }
                        else if (rarity >= 96 && rarity <= 100)
                        {
                            spell = new SwiftEvasion(6, r.Next(4, 6), 0);
                        }
                        else if (rarity >= 101 && rarity <= 150)
                        {
                            spell = new SwiftEvasion(7, r.Next(4, 6), 0);
                        }
                        else if (rarity >= 151 && rarity <= 180)
                        {
                            spell = new SwiftEvasion(7, r.Next(5, 7), 0);
                        }
                        else if (rarity >= 181 && rarity <= 195)
                        {
                            spell = new SwiftEvasion(8, r.Next(6, 8), 0);
                        }
                        else if (rarity >= 196 && rarity <= 200)
                        {
                            spell = new SwiftEvasion(8, r.Next(7, 9), 0);
                        }
                        else if (rarity >= 201 && rarity <= 250)
                        {
                            spell = new SwiftEvasion(9, r.Next(8, 10), 0);
                        }
                        else if (rarity >= 251 && rarity <= 280)
                        {
                            spell = new SwiftEvasion(9, r.Next(9, 11), 0);
                        }
                        else if (rarity >= 281 && rarity <= 295)
                        {
                            spell = new SwiftEvasion(10, r.Next(10, 12), 0);
                        }
                        else if (rarity >= 296 && rarity <= 300)
                        {
                            spell = new SwiftEvasion(10, r.Next(11, 13), 0);
                        }
                        items.Add(spell);
                        break;
                    

                }
            }
        }


        public override ArrayList getItems()
        {
            return items;
        }

        public override string ToString()
        {
            return this.name;
        }
    }

    class MeleeWeaponSeller : Vendor
    {
        string name;
        int rarity, numOfItems, choice;
        MeleeWeapon meleeWeapon;
        Random r = new Random();
        ArrayList items = new ArrayList();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rarity">0-50: common, 51-80: uncommon, 81-95 rare, 96-100: legendary, continues in groups of 100</param>
        /// <param name="numOfItems"></param>
        public MeleeWeaponSeller(int rarity, int numOfItems)
        {
            name = "Melee Weapon Seller";
            this.rarity = rarity;
            this.numOfItems = numOfItems;
            generateItems();
        }

        public override void generateItems()
        {
            for (int i = 0; i < numOfItems; i++)
            {
                choice = r.Next(0, 4);
                switch (choice)
                {

                    case 0:
                        if (rarity >= 0 && rarity <= 50)
                        {
                            meleeWeapon = new ShortSword(r.Next(1, 8));
                        }
                        else if (rarity >= 51 && rarity <= 80)
                        {
                            meleeWeapon = new ShortSword(r.Next(5, 15));
                        }
                        else if (rarity >= 81 && rarity <= 95)
                        {
                            meleeWeapon = new ShortSword(r.Next(20, 30));
                        }
                        else if (rarity >= 96 && rarity <= 100)
                        {
                            meleeWeapon = new ShortSword(r.Next(35, 45));
                        }
                        else if (rarity >= 101 && rarity <= 150)
                        {
                            meleeWeapon = new ShortSword(r.Next(55, 65));
                        }
                        else if (rarity >= 151 && rarity <= 180)
                        {
                            meleeWeapon = new ShortSword(r.Next(65, 75));
                        }
                        else if (rarity >= 181 && rarity <= 195)
                        {
                            meleeWeapon = new ShortSword(r.Next(80, 90));
                        }
                        else if (rarity >= 196 && rarity <= 200)
                        {
                            meleeWeapon = new ShortSword(r.Next(100, 110));
                        }
                        else if (rarity >= 201 && rarity <= 250)
                        {
                            meleeWeapon = new ShortSword(r.Next(125, 135));
                        }
                        else if (rarity >= 251 && rarity <= 280)
                        {
                            meleeWeapon = new ShortSword(r.Next(145, 165));
                        }
                        else if (rarity >= 281 && rarity <= 295)
                        {
                            meleeWeapon = new ShortSword(r.Next(170, 190));
                        }
                        else if (rarity >= 296 && rarity <= 300)
                        {
                            meleeWeapon = new ShortSword(r.Next(215, 225));
                        }
                        break;

                    case 1:
                        if (rarity >= 0 && rarity <= 50)
                        {
                            meleeWeapon = new LongSword(r.Next(1, 10));
                        }
                        else if (rarity >= 51 && rarity <= 80)
                        {
                            meleeWeapon = new LongSword(r.Next(8, 20));
                        }
                        else if (rarity >= 81 && rarity <= 95)
                        {
                            meleeWeapon = new LongSword(r.Next(25, 35));
                        }
                        else if (rarity >= 96 && rarity <= 100)
                        {
                            meleeWeapon = new LongSword(r.Next(35, 45));
                        }
                        else if (rarity >= 101 && rarity <= 150)
                        {
                            meleeWeapon = new LongSword(r.Next(55, 65));
                        }
                        else if (rarity >= 151 && rarity <= 180)
                        {
                            meleeWeapon = new LongSword(r.Next(65, 75));
                        }
                        else if (rarity >= 181 && rarity <= 195)
                        {
                            meleeWeapon = new LongSword(r.Next(80, 90));
                        }
                        else if (rarity >= 196 && rarity <= 200)
                        {
                            meleeWeapon = new LongSword(r.Next(110, 125));
                        }
                        else if (rarity >= 201 && rarity <= 250)
                        {
                            meleeWeapon = new LongSword(r.Next(140, 150));
                        }
                        else if (rarity >= 251 && rarity <= 280)
                        {
                            meleeWeapon = new LongSword(r.Next(160, 175));
                        }
                        else if (rarity >= 281 && rarity <= 295)
                        {
                            meleeWeapon = new LongSword(r.Next(180, 195));
                        }
                        else if (rarity >= 296 && rarity <= 300)
                        {
                            meleeWeapon = new LongSword(r.Next(235, 250));
                        }
                        break;

                    case 2:
                        if (rarity >= 0 && rarity <= 50)
                        {
                            meleeWeapon = new Dagger(r.Next(1, 8));
                        }
                        else if (rarity >= 51 && rarity <= 80)
                        {
                            meleeWeapon = new Dagger(r.Next(5, 15));
                        }
                        else if (rarity >= 81 && rarity <= 95)
                        {
                            meleeWeapon = new Dagger(r.Next(20, 30));
                        }
                        else if (rarity >= 96 && rarity <= 100)
                        {
                            meleeWeapon = new Dagger(r.Next(35, 45));
                        }
                        else if (rarity >= 101 && rarity <= 150)
                        {
                            meleeWeapon = new Dagger(r.Next(50, 60));
                        }
                        else if (rarity >= 151 && rarity <= 180)
                        {
                            meleeWeapon = new Dagger(r.Next(65, 70));
                        }
                        else if (rarity >= 181 && rarity <= 195)
                        {
                            meleeWeapon = new Dagger(r.Next(80, 90));
                        }
                        else if (rarity >= 196 && rarity <= 200)
                        {
                            meleeWeapon = new Dagger(r.Next(100, 110));
                        }
                        else if (rarity >= 201 && rarity <= 250)
                        {
                            meleeWeapon = new Dagger(r.Next(125, 135));
                        }
                        else if (rarity >= 251 && rarity <= 280)
                        {
                            meleeWeapon = new Dagger(r.Next(145, 165));
                        }
                        else if (rarity >= 281 && rarity <= 295)
                        {
                            meleeWeapon = new Dagger(r.Next(170, 190));
                        }
                        else if (rarity >= 296 && rarity <= 300)
                        {
                            meleeWeapon = new Dagger(r.Next(215, 225));
                        }
                        break;

                    case 3:
                        if (rarity >= 0 && rarity <= 50)
                        {
                            meleeWeapon = new TwinDaggers(r.Next(1, 10));
                        }
                        else if (rarity >= 51 && rarity <= 80)
                        {
                            meleeWeapon = new TwinDaggers(r.Next(8, 18));
                        }
                        else if (rarity >= 81 && rarity <= 95)
                        {
                            meleeWeapon = new TwinDaggers(r.Next(25, 35));
                        }
                        else if (rarity >= 96 && rarity <= 100)
                        {
                            meleeWeapon = new TwinDaggers(r.Next(35, 45));
                        }
                        else if (rarity >= 101 && rarity <= 150)
                        {
                            meleeWeapon = new TwinDaggers(r.Next(55, 65));
                        }
                        else if (rarity >= 151 && rarity <= 180)
                        {
                            meleeWeapon = new TwinDaggers(r.Next(65, 75));
                        }
                        else if (rarity >= 181 && rarity <= 195)
                        {
                            meleeWeapon = new TwinDaggers(r.Next(80, 90));
                        }
                        else if (rarity >= 196 && rarity <= 200)
                        {
                            meleeWeapon = new TwinDaggers(r.Next(110, 125));
                        }
                        else if (rarity >= 201 && rarity <= 250)
                        {
                            meleeWeapon = new TwinDaggers(r.Next(140, 150));
                        }
                        else if (rarity >= 251 && rarity <= 280)
                        {
                            meleeWeapon = new TwinDaggers(r.Next(160, 175));
                        }
                        else if (rarity >= 281 && rarity <= 295)
                        {
                            meleeWeapon = new TwinDaggers(r.Next(180, 195));
                        }
                        else if (rarity >= 296 && rarity <= 300)
                        {
                            meleeWeapon = new TwinDaggers(r.Next(235, 250));
                        }
                        break;
                }
                items.Add(meleeWeapon);
            }
        }


        public override ArrayList getItems()
        {
            return items;
        }

        public override string ToString()
        {
            return this.name;
        }
    }

    /// <summary>
    /// Will have to add in other mage and ranged weapons eventually
    /// </summary>
    class RangedMageSeller : Vendor
    {
        string name;
        int rarity, numOfItems, choice;
        RangedWeapon rangedWep;
        MageWeapon mageWep;
        Random r = new Random();
        ArrayList items = new ArrayList();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rarity">0-50: common, 51-80: uncommon, 81-95 rare, 96-100: legendary</param>
        /// <param name="numOfItems"></param>
        public RangedMageSeller(int rarity, int numOfItems)
        {
            name = "Ranged/Mage Seller";
            this.rarity = rarity;
            this.numOfItems = numOfItems;
            generateItems();
        }

        public override void generateItems()
        {
            for (int i = 0; i < numOfItems; i++)
            {
                choice = r.Next(0, 2);
                switch (choice)
                {

                    case 0:
                        if (rarity >= 0 && rarity <= 50)
                        {
                            rangedWep = new Bow(r.Next(1, 8));
                        }
                        else if (rarity >= 51 && rarity <= 80)
                        {
                            rangedWep = new Bow(r.Next(5, 15));
                        }
                        else if (rarity >= 81 && rarity <= 95)
                        {
                            rangedWep = new Bow(r.Next(20, 30));
                        }
                        else if(rarity >= 96 && rarity <= 100)
                        {
                            rangedWep = new Bow(r.Next(35, 45));
                        }
                        else if (rarity >= 101 && rarity <= 150)
                        {
                            rangedWep = new Bow(r.Next(60, 70));
                        }
                        else if(rarity >= 151 && rarity <= 180)
                        {
                            rangedWep = new Bow(r.Next(70, 80));
                        }
                        else if (rarity >= 181 && rarity <= 195)
                        {
                            rangedWep = new Bow(r.Next(85, 100));
                        }
                        else if (rarity >= 196 && rarity <= 200)
                        {
                            rangedWep = new Bow(r.Next(115, 125));
                        }
                        else if (rarity >= 201 && rarity <= 250)
                        {
                            rangedWep = new Bow(r.Next(140, 150));
                        }
                        else if (rarity >= 251 && rarity <= 280)
                        {
                            rangedWep = new Bow(r.Next(165, 180));
                        }
                        else if (rarity >= 281 && rarity <= 295)
                        {
                            rangedWep = new Bow(r.Next(190, 200));
                        }
                        else if (rarity >= 296 && rarity <= 300)
                        {
                            rangedWep = new Bow(r.Next(225, 240));
                        }
                        items.Add(rangedWep);
                        break;

                    case 1:
                        if (rarity >= 0 && rarity <= 50)
                        {
                            mageWep = new Staff(r.Next(1, 8));
                        }
                        else if (rarity >= 51 && rarity <= 80)
                        {
                            mageWep = new Staff(r.Next(5, 15));
                        }
                        else if (rarity >= 81 && rarity <= 95)
                        {
                            mageWep = new Staff(r.Next(20, 30));
                        }
                        else if(rarity >= 96 && rarity <= 100)
                        {
                            mageWep = new Staff(r.Next(35, 45));
                        }
                        else if (rarity >= 101 && rarity <= 150)
                        {
                            mageWep = new Staff(r.Next(60, 70));
                        }
                        else if (rarity >= 151 && rarity <= 180)
                        {
                            mageWep = new Staff(r.Next(70, 80));
                        }
                        else if (rarity >= 181 && rarity <= 195)
                        {
                            mageWep = new Staff(r.Next(85, 100));
                        }
                        else if (rarity >= 196 && rarity <= 200)
                        {
                            mageWep = new Staff(r.Next(115, 125));
                        }
                        else if (rarity >= 201 && rarity <= 250)
                        {
                            mageWep = new Staff(r.Next(140, 150));
                        }
                        else if (rarity >= 251 && rarity <= 280)
                        {
                            mageWep = new Staff(r.Next(165, 180));
                        }
                        else if (rarity >= 281 && rarity <= 295)
                        {
                            mageWep = new Staff(r.Next(190, 200));
                        }
                        else if (rarity >= 296 && rarity <= 300)
                        {
                            mageWep = new Staff(r.Next(225, 240));
                        }
                        items.Add(mageWep);
                        break;

                }
            }
        }


        public override ArrayList getItems()
        {
            return items;
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
