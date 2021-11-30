using RPG;
using System;
using System.Collections;
using System.Collections.Generic;
/* Currently Doing:
 * Working on boss (possibly add in some other mechanic)
 * Balancing different aspects (rng based things like legendary drop rate)
 * Work on spells (maybe change dodging chances, currently goes from 33% miss to 50% with Swift Evasion active)
 * Adding in sets (rang/mag damg bonus)
 * FIX BUG WITH GETS/SETS AND TOSTRING WITH WEAPONS/ARMORS/SPELLS <----- WORKING ON NOW
 * Seems to run into a StackOverflowException sometimes at the line: "Console.WriteLine("Merchant Options:");"
 * CheckList
 * ---------------------------------------------------------------------------------------------------------------
 * Add in special area with bosses for special equipment [X]
 * Possibly add in sets (either flat damg or % damg increase per set piece? will have to add in helm/pants/boots/gloves/rings etc.) [ ]
 * Add in upgrading of weapons/armor (add in upgrading material) [X]
 * Add in separate bag for non-combat items (consumables/upgrading material) [X]
 * Add in 4th level (Depths of Hell) [ ]
 * Add in 3rd level (Graveyard) [X]
 * Add in 2nd level (make sure to change all calls to forest to be case statements [X]
 * Add in legendary weapons (bows, staffs, general weapons) [X]
 * Add in sets, physical damg/def implemented (both sets found in graveyard normal monsters) [X]
 * Add in more Magic Spells with different effects [ ]
 * Maybe change over to weight instead of inventory space? [ ]
 * Add in basic Magic Spells and be able to use in combat (magic attack command) [X]
 * Add in bow usage [X]
 * Add in basic merchant at rest area for arrows/other misc. items (need to add other misc. items to shop [ ]
 * Add in vendors after 10 monsters defeated (sell spells and other items), working on armor [X]
 * Add in a boss at the end of the Forest [X]
 * Be able to sell equipment[X]
 * fix armor and shield switching places on other classes besides knight bug [X]
 * make end do something at some point (add in weight and bag space) [X]
 * Implement saving [ ]
 * Work on actual story? [ ]
 * ---------------------------------------------------------------------------------------------------------------
 */
namespace ConsoleApplication1
{
    class Program
    {
        //static var classes = new System.Collections.Generic.Dictionary<int, Player>();
        //classes[0] = knight;
        //classes[1] = mage;
        //classes[2] = archer;
        //classes[3] = bandit;
        //classes[4] = trash;
        static double levelMultiplier = 1;
        static string input;
        static bool playerDead = false;
        static int expRequired = 600;
        static int expIncreasePerLevel = 300;
        static int potionHealAmount = 30;
        static int potionPrice = 5000;
        static int titaniteShardPrice = 2000;
        static int largeTitaniteShardPrice = 10000;
        static int titaniteChunkPrice = 20000;
        static int titaniteSlabPrice = 100000;
        static int maxHeals = 5;
        static int healCounter = 0;
        static int numberOfStagesAvailable = 1;
        static int ironArmorTurns = 0;
        static int swiftEvasionTurns = 0;
        static int lastSpellIndex = -1;
        //static bool specialStagesAvailable = false;
        static ArrayList specialStages = new ArrayList{"Longsword Battlegrounds", "Dagger Battlegrounds", "Twindaggers Battlegrounds",
                                                         "Shortsword Battlegrounds", "Staff Battlegrounds", "Bow Battlegrounds"};
        static ArrayList specialStagesPlayerList = new ArrayList { };
        // Health, str, dex, mag, vit, end, atn, exp, level, maplevel 
        static Player knight = new Player(60, 15, 7, 5, 12, 10, 0, 0, 10, 0);
        static Player mage = new Player(50, 7, 6, 15, 10, 10, 5, 0, 10, 0);
        static Player archer = new Player(50, 8, 15, 5, 10, 10, 0, 0, 10, 0);
        static Player bandit = new Player(50, 8, 15, 5, 10, 10, 0, 0, 10, 0);
        static Player trash = new Player(40, 8, 8, 8, 8, 10, 0, 0, 8, 0);
        static Player player = new Player(0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        static MeleeWeapon longSword = new LongSword(20);
        static MeleeWeapon shortSword = new ShortSword(15);
        static MeleeWeapon dagger = new Dagger(12);
        static MeleeWeapon twinDaggers = new TwinDaggers(15);
        static MageWeapon staff = new Staff(15);
        static RangedWeapon bow = new Bow(15);
        static Armor lightArmor = new LightArmor(10);
        static Armor mediumArmor = new MediumArmor(15);
        static Armor heavyArmor = new HeavyArmor(25);
        static Armor shield = new Shield(5);
        static Spell magicMissile = new MagicMissile(3, 10, 15);
        static Spell ironArmor = new IronArmor(5, 5, 0);

        /* Test Code for sets */
        static Armor raidersHelm = new DamgSetHelm(50);
        static Armor raidersArmor = new DamgSetArmor(50);
        static Armor raidersGloves = new DamgSetGloves(50);
        static Armor raidersBoots = new DamgSetBoots(50);
        static Armor raidersPants = new DamgSetPants(50);
        static Armor turtleHelm = new DefSetHelm(50);
        static Armor turtleArmor = new DefSetArmor(50);
        static Armor turtleGloves = new DefSetGloves(50);
        static Armor turtleBoots = new DefSetBoots(50);
        static Armor turtlePants = new DefSetPants(50);

        /// <summary>
        /// Booting up game
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            while (true)
            {
            mainMenu:
                Console.WriteLine("Welcome to the game");
                Console.WriteLine("Select one of the following options:");
                Console.WriteLine("New Game");
                Console.WriteLine("Load Game");
                Console.WriteLine("Difficulty");
                input = Console.ReadLine();
                switch (input)
                {
                    case "new game":
                        NewGame();
                        Forest(0);
                        break;
                    case "load game":
                        break;
                    case "difficulty":
                        DifficultyChanging();
                        goto mainMenu;
                    default:
                        Console.WriteLine("You have inputted an incorrect option (all letters should be lowercase)\n");
                        goto mainMenu;
                }
            }
        }

        /// <summary>
        /// Starting up a new game
        /// </summary>
        static void NewGame()
        {
            Console.WriteLine("\nPlease select your character: (input help for information about classes)");
            Console.WriteLine("Knight");
            Console.WriteLine("Mage");
            Console.WriteLine("Archer");
            Console.WriteLine("Bandit");
            Console.WriteLine("Trash");
            Console.WriteLine("Go back");
            input = Console.ReadLine();
            switch (input)
            {
                case "knight":
                    KnightSelect();
                    break;
                case "mage":
                    MageSelect();
                    break;
                case "archer":
                    ArcherSelect();
                    break;
                case "bandit":
                    BanditSelect();
                    break;
                case "trash":
                    TrashSelect();
                    break;
                case "help":
                    Console.WriteLine("Knight: Heavily armored, uses mostly str.");
                    Console.WriteLine("Mage: Fragile, uses mostly mag.");
                    Console.WriteLine("Archer: Relatively fragile, uses mostly dex.");
                    Console.WriteLine("Bandit: Fragile, uses mostly dex.");
                    Console.WriteLine("Trash: Become nothingness.");
                    NewGame();
                    break;
                case "go back":
                    Console.WriteLine("");
                    string[] args = new string[0];
                    Main(args);
                    break;

                // test case for turtle set
                case "turtle":
                    Console.WriteLine("Chose turtle set");
                    player = knight;
                    player.SetShield(shield);
                    player.SetMWeapon(longSword);
                    player.SetMageWeapon(null);
                    player.SetRangedWeapon(null);
                    player.SetArmor(turtleArmor);
                    player.SetGloves(turtleGloves);
                    player.SetBoots(turtleBoots);
                    player.SetHelm(turtleHelm);
                    player.SetPants(turtlePants);
                    player.SetHeals(maxHeals);
                    break;

                // test case for raider set
                case "raider":
                    Console.WriteLine("Chose raider's set");
                    player = knight;
                    player.SetShield(shield);
                    player.SetMWeapon(longSword);
                    player.SetMageWeapon(null);
                    player.SetRangedWeapon(null);
                    player.SetArmor(raidersArmor);
                    player.SetGloves(raidersGloves);
                    player.SetBoots(raidersBoots);
                    player.SetHelm(raidersHelm);
                    player.SetPants(raidersPants);
                    player.SetHeals(maxHeals);
                    break;

                default:
                    Console.WriteLine("You have inputted an incorrect choice");
                    NewGame();
                    break;

            }
        }

        /// <summary>
        /// Selecting a knight menu
        /// </summary>
        static void KnightSelect()
        {
            Console.WriteLine("\nYou have chosen a knight. Are you sure? (yes/no)");
            Console.WriteLine("Weapon: {0}", longSword.ToString());
            Console.WriteLine("Armor: {0}", heavyArmor.ToString());
            Console.WriteLine("Shield: {0}", shield.ToString());
            Console.WriteLine("Staff: {0}", null);
            Console.WriteLine("Bow: {0}", null);
            Console.WriteLine("Stats:");
            Console.WriteLine("health: " + knight.GetHealth() + " str: " + knight.GetStr() + " dex: " + knight.GetDex() + " mag: " + knight.GetMag() +
                " vit: " + knight.GetVit() + " end: " + knight.GetEnd() + " atn: " + knight.GetAttunement());
            input = Console.ReadLine();
            if (input == "yes")
            {
                Console.WriteLine("");
                player = knight;
                player.SetShield(shield);
                player.SetMWeapon(longSword);
                player.SetMageWeapon(null);
                player.SetRangedWeapon(null);
                player.SetArmor(heavyArmor);
                player.SetHeals(maxHeals);
            }
            else if (input == "no")
            {
                NewGame();
            }
            else
            {
                Console.WriteLine("Incorrect input");
                KnightSelect();
            }
        }

        /// <summary>
        /// Selecting a mage menu
        /// </summary>
        static void MageSelect()
        {
            Console.WriteLine("\nYou have chosen a mage. Are you sure? (yes/no)");
            Console.WriteLine("Weapon: {0}", shortSword.ToString());
            Console.WriteLine("Armor: {0}", lightArmor.ToString());
            Console.WriteLine("Shield: {0}", null);
            Console.WriteLine("Staff: {0}", staff.ToString());
            Console.WriteLine("Bow: {0}", null);
            Console.WriteLine("Stats:");
            Console.WriteLine("health: " + mage.GetHealth() + " str: " + mage.GetStr() + " dex: " + mage.GetDex() + " mag: " + mage.GetMag() +
                " vit: " + mage.GetVit() + " end: " + mage.GetEnd() + " atn: " + mage.GetAttunement());
            input = Console.ReadLine();
            if (input == "yes")
            {
                Console.WriteLine("");
                player = mage;
                player.AddSpellsToEquipped(magicMissile);
                player.AddSpellsToEquipped(ironArmor);
                player.SetMageWeapon(staff);
                player.SetRangedWeapon(null);
                player.SetMWeapon(shortSword);
                player.SetArmor(lightArmor);
                player.SetHeals(maxHeals);
            }
            else if (input == "no")
            {
                NewGame();
            }
            else
            {
                Console.WriteLine("Incorrect input");
                MageSelect();
            }
        }

        /// <summary>
        /// Selecting an archer menu
        /// </summary>
        static void ArcherSelect()
        {
            Console.WriteLine("\nYou have chosen an archer. Are you sure? (yes/no)");
            Console.WriteLine("Weapon: {0}", dagger.ToString());
            Console.WriteLine("Armor: {0}", mediumArmor.ToString());
            Console.WriteLine("Shield: {0}", null);
            Console.WriteLine("Staff: {0}", null);
            Console.WriteLine("Bow: {0}", bow.ToString());
            Console.WriteLine("Stats:");
            Console.WriteLine("health: " + archer.GetHealth() + " str: " + archer.GetStr() + " dex: " + archer.GetDex() + " mag: " + archer.GetMag() +
                " vit: " + archer.GetVit() + " end: " + archer.GetEnd() + " atn: " + archer.GetAttunement());
            input = Console.ReadLine();
            if (input == "yes")
            {
                Console.WriteLine("");
                player = archer;
                player.SetMageWeapon(null);
                player.SetRangedWeapon(bow);
                player.SetMWeapon(dagger);
                player.SetArmor(mediumArmor);
                player.SetHeals(maxHeals);
            }
            else if (input == "no")
            {
                NewGame();
            }
            else
            {
                Console.WriteLine("Incorrect input");
                ArcherSelect();
            }
        }

        /// <summary>
        /// Selecting a bandit menu
        /// </summary>
        static void BanditSelect()
        {
            Console.WriteLine("\nYou have chosen a bandit. Are you sure? (yes/no)");
            Console.WriteLine("Weapon: {0}", twinDaggers.ToString());
            Console.WriteLine("Armor: {0}", lightArmor.ToString());
            Console.WriteLine("Shield: {0}", null);
            Console.WriteLine("Staff: {0}", null);
            Console.WriteLine("Bow: {0}", null);
            Console.WriteLine("Stats:");
            Console.WriteLine("health: " + bandit.GetHealth() + " str: " + bandit.GetStr() + " dex: " + bandit.GetDex() + " mag: " + bandit.GetMag() +
                " vit: " + bandit.GetVit() + " end: " + bandit.GetEnd() + " atn: " + bandit.GetAttunement());
            input = Console.ReadLine();
            if (input == "yes")
            {
                Console.WriteLine("");
                player = bandit;
                player.SetMageWeapon(null);
                player.SetRangedWeapon(null);
                player.SetMWeapon(twinDaggers);
                player.SetArmor(lightArmor);
                player.SetHeals(maxHeals);
            }
            else if (input == "no")
            {
                NewGame();
            }
            else
            {
                Console.WriteLine("Incorrect input");
                BanditSelect();
            }
        }

        /// <summary>
        /// Selecting a trash menu
        /// </summary>
        static void TrashSelect()
        {
            Console.WriteLine("\nYou have chosen trash. Are you sure? (yes/no)");
            Console.WriteLine("Weapon: {0}", shortSword.ToString());
            Console.WriteLine("Armor: {0}", null);
            Console.WriteLine("Shield: {0}", null);
            Console.WriteLine("Staff: {0}", null);
            Console.WriteLine("Bow: {0}", null);
            Console.WriteLine("Stats:");
            Console.WriteLine("health: " + trash.GetHealth() + " str: " + trash.GetStr() + " dex: " + trash.GetDex() + " mag: " + trash.GetMag() +
                " vit: " + trash.GetVit() + " end: " + trash.GetEnd() + " atn: " + trash.GetAttunement());
            input = Console.ReadLine();
            if (input == "yes")
            {
                Console.WriteLine("");
                player = trash;
                player.SetMageWeapon(null);
                player.SetRangedWeapon(null);
                player.SetMWeapon(shortSword);
                player.SetArmor(null);
                player.SetHeals(maxHeals);
            }
            else if (input == "no")
            {
                NewGame();
            }
            else
            {
                Console.WriteLine("Incorrect input");
                TrashSelect();
            }
        }

        /// <summary>
        /// Change difficulty
        /// </summary>
        static void DifficultyChanging()
        {
            while (input != "easy" & input != "medium" & input != "hard")
            {
                if (levelMultiplier == 0.7)
                {
                    Console.WriteLine("\nDifficulty currently set to Easy.");
                }
                else if (levelMultiplier == 1)
                {
                    Console.WriteLine("\nDifficulty currently set to Medium.");
                }
                else
                {
                    Console.WriteLine("\nDifficulty currently set to Hard.");
                }
                Console.WriteLine("Choose your difficulty:");
                Console.WriteLine("Easy");
                Console.WriteLine("Medium");
                Console.WriteLine("Hard");
                Console.WriteLine("Go back");
                input = Console.ReadLine();
                if (input == "easy")
                {
                    levelMultiplier = 0.7;
                }
                else if (input == "medium")
                {
                    levelMultiplier = 1;
                }
                else if (input == "hard")
                {
                    levelMultiplier = 1.5;
                }
                else if (input == "go back")
                {
                    Console.WriteLine("");
                    string[] args = new string[0];
                    Main(args);
                }
                else
                {
                    Console.WriteLine("Incorrect Input");
                }
            }
            Console.WriteLine("Difficulty has been set to " + input + "\n");
        }

        /// <summary>
        /// First stage
        /// </summary>
        /// <param name="numberOfKills"></param>
        static void Forest(int numberOfKills)
        {
            MeleeWeapon mWep;
            MageWeapon mageWeapon;
            RangedWeapon rangedWeapon;
            int playerDamg = 0;
            int numForestEnemies = 30;
            int numKills = numberOfKills;
            int var = 0;
            int index = 0;
            Random rnd = new Random();
            Random rng = new Random();
            int monsterHealth = (int)((double)rnd.Next(95, 105) * levelMultiplier);
            int quickAttack = 6, strongAttack = 12;
            int attackHit = 0, monsterAttackHit = 0;
            int monsterAttackType = 0, monsterDamg = 0;
            int vendorNum = 0;
            string spellName = string.Empty;
            while (numKills < numForestEnemies)
            {
                monsterHealth = (int)((double)rnd.Next(95, 105) * levelMultiplier);
                vendorNum = rnd.Next(0, 8);
                if (numKills > 10 && vendorNum == 7)
                {
                    VendorMenu(1, numKills);
                }
                else
                {
                    Monster forestMonster = new Monster(monsterHealth, quickAttack, strongAttack);
                    Console.WriteLine("\nA monster has appeared\n");
                    while (forestMonster.getHealth() > 0)
                    {
                        if (ironArmorTurns != 0)
                        {
                            Console.WriteLine("\nIron Armor is active");
                        }
                        if (swiftEvasionTurns != 0)
                        {
                            Console.WriteLine("Swift Evasion is active");
                        }
                        Console.WriteLine("Player's current health: {0}/{1}", player.GetHealth(), player.GetMaxHealth());
                        Console.WriteLine("Monster's current health: " + forestMonster.getHealth());
                        Console.WriteLine("{0}/{1} monsters killed", numKills, numForestEnemies);
                        Console.WriteLine("Bag Space: {0}/{1}", player.GetEquipmentBag().Count, player.GetBagSpace());
                        Console.WriteLine("Exp: " + player.GetExp());
                        Console.WriteLine("Go rest (r) (this will reset the monsters you have to defeat)");
                        Console.WriteLine("Choose an option:");
                        Console.WriteLine("Attack (a)");
                        Console.WriteLine("Ranged Attack (ra) ({0})", player.GetArrows());
                        Console.WriteLine("Magic Attack (m)");
                        if (lastSpellIndex >= 0)
                        {
                            Console.WriteLine("Last Magic Attack (lm) ({0})", ((Spell)player.GetSpellsEquipped()[lastSpellIndex]).ToString());
                        }
                        Console.WriteLine("Check bag (b)");
                        Console.WriteLine("Check equipment (e)");
                        Console.WriteLine("Heal (h) (heals for {0}) ({1})", potionHealAmount, player.GetHeals());
                        input = Console.ReadLine();

                        switch (input)
                        {
                            // Melee attack sequence
                            case "a":
                                attackHit = rnd.Next(3);
                                monsterAttackType = rnd.Next(4);
                                if (monsterAttackType == 0)
                                {
                                    monsterDamg = (int)(strongAttack * levelMultiplier);
                                }
                                else
                                {
                                    monsterDamg = (int)(quickAttack * levelMultiplier);
                                }

                                if (swiftEvasionTurns == 0)
                                {
                                    monsterAttackHit = rnd.Next(3);
                                }
                                else
                                {
                                    monsterAttackHit = rnd.Next(2);
                                    swiftEvasionTurns--;
                                }

                                if (attackHit == 0)
                                {
                                    Console.WriteLine("\nYour attack failed");
                                    if (monsterAttackHit == 0)
                                    {
                                        Console.WriteLine("The monster has missed you\n");
                                    }
                                    else
                                    {
                                        if (monsterDamg == quickAttack)
                                        {
                                            Console.WriteLine("The monster has landed a quick attack\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("The monster has landed a strong attack\n");
                                        }

                                        // Damage Calculations
                                        PlayerDamagedCalculation(monsterDamg);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("\nYour attack was successful\n");
                                    if (monsterAttackHit == 0)
                                    {
                                        Console.WriteLine("The monster has missed you\n");
                                    }
                                    else
                                    {
                                        if (monsterDamg == quickAttack)
                                        {
                                            Console.WriteLine("The monster has landed a quick attack\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("The monster has landed a strong attack\n");
                                        }

                                        // Damage Calculations
                                        PlayerDamagedCalculation(monsterDamg);
                                    }
                                    playerDamg = PlayerMeleeAttackCalculation();
                                    forestMonster.decreaseHealth(playerDamg);
                                }

                                if (player.GetHealth() <= 0)
                                {
                                    Console.WriteLine("You have died and lost your exp");
                                    ironArmorTurns = 0;
                                    swiftEvasionTurns = 0;
                                    player.SetExp(0);
                                    player.SetHealth(1);
                                    player.SetHeals(maxHeals);
                                    numKills = 0;
                                    RestingArea();
                                }
                                break;

                            // Ranged attack sequence
                            case "ra":
                                // Checks if player has arrows and a bow
                                if (player.GetArrows() != 0 && player.GetRangedWeapon() != null)
                                {
                                    attackHit = rnd.Next(3);
                                    monsterAttackType = rnd.Next(4);
                                    if (monsterAttackType == 0)
                                    {
                                        monsterDamg = (int)(strongAttack * levelMultiplier);
                                    }
                                    else
                                    {
                                        monsterDamg = (int)(quickAttack * levelMultiplier);
                                    }
                                    if (swiftEvasionTurns == 0)
                                    {
                                        monsterAttackHit = rnd.Next(3);
                                    }
                                    else
                                    {
                                        monsterAttackHit = rnd.Next(2);
                                        swiftEvasionTurns--;
                                    }
                                    if (attackHit == 0)
                                    {
                                        Console.WriteLine("\nYour attack failed");
                                        if (monsterAttackHit == 0)
                                        {
                                            Console.WriteLine("The monster has missed you\n");
                                        }
                                        else
                                        {
                                            if (monsterDamg == quickAttack)
                                            {
                                                Console.WriteLine("The monster has landed a quick attack\n");
                                            }
                                            else
                                            {
                                                Console.WriteLine("The monster has landed a strong attack\n");
                                            }

                                            // Damage Calculations
                                            PlayerDamagedCalculation(monsterDamg);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nYour attack was successful\n");
                                        if (monsterAttackHit == 0)
                                        {
                                            Console.WriteLine("The monster has missed you\n");
                                        }
                                        else
                                        {
                                            if (monsterDamg == quickAttack)
                                            {
                                                Console.WriteLine("The monster has landed a quick attack\n");
                                            }
                                            else
                                            {
                                                Console.WriteLine("The monster has landed a strong attack\n");
                                            }

                                            // Damage Calculations
                                            PlayerDamagedCalculation(monsterDamg);
                                        }
                                        rangedWeapon = player.GetRangedWeapon();
                                        forestMonster.decreaseHealth(rangedWeapon.CalcDamg(player.GetDex(), player.GetStr()));
                                    }
                                    player.DecreaseArrows(1);
                                    if (player.GetHealth() <= 0)
                                    {
                                        Console.WriteLine("You have died and lost your exp");
                                        ironArmorTurns = 0;
                                        swiftEvasionTurns = 0;
                                        player.SetExp(0);
                                        RestingArea();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Not enough arrows/No bow equipped");
                                }
                                break;

                            // Magic attack sequence
                            case "m":
                                if (player.GetSpellsEquipped().Count != 0)
                                {
                                    for (int i = 0; i < player.GetSpellsEquipped().Count; i++)
                                    {
                                        Console.WriteLine("(" + i + ")" + ((Spell)(player.GetSpellsEquipped()[i])).ToString());
                                    }
                                    Console.WriteLine("Type in the number of the spell to use");
                                    input = Console.ReadLine();
                                    // Is the input a number
                                    if (int.TryParse(input, out index) == true)
                                    {
                                        // Is the number typed within index
                                        if (index >= 0 && index < player.GetSpellsEquipped().Count)
                                        {
                                            // Does the spell have casts
                                            if (((Spell)player.GetSpellsEquipped()[index]).getCastTimes() > 0)
                                            {
                                                if (((Spell)player.GetSpellsEquipped()[index]).getDamg() != 0)
                                                {
                                                    // Damaging Spells
                                                    attackHit = rnd.Next(5);
                                                    monsterAttackType = rnd.Next(4);
                                                    if (monsterAttackType == 0)
                                                    {
                                                        monsterDamg = (int)(strongAttack * levelMultiplier);
                                                    }
                                                    else
                                                    {
                                                        monsterDamg = (int)(quickAttack * levelMultiplier);
                                                    }
                                                    if (swiftEvasionTurns == 0)
                                                    {
                                                        monsterAttackHit = rnd.Next(3);
                                                    }
                                                    else
                                                    {
                                                        monsterAttackHit = rnd.Next(2);
                                                        swiftEvasionTurns--;
                                                    }
                                                    if (attackHit == 0)
                                                    {
                                                        Console.WriteLine("\nYour attack failed");
                                                        if (monsterAttackHit == 0)
                                                        {
                                                            Console.WriteLine("The monster has missed you\n");
                                                        }
                                                        else
                                                        {
                                                            if (monsterDamg == quickAttack)
                                                            {
                                                                Console.WriteLine("The monster has landed a quick attack\n");
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("The monster has landed a strong attack\n");
                                                            }

                                                            // Damage Calculations
                                                            PlayerDamagedCalculation(monsterDamg);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("\nYour attack was successful\n");
                                                        if (monsterAttackHit == 0)
                                                        {
                                                            Console.WriteLine("The monster has missed you\n");
                                                        }
                                                        else
                                                        {
                                                            if (monsterDamg == quickAttack)
                                                            {
                                                                Console.WriteLine("The monster has landed a quick attack\n");
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("The monster has landed a strong attack\n");
                                                            }

                                                            // Damage Calculations
                                                            PlayerDamagedCalculation(monsterDamg);
                                                        }
                                                        mageWeapon = player.GetMageWeapon();
                                                        if (mageWeapon != null)
                                                        {
                                                            forestMonster.decreaseHealth(mageWeapon.CalcDamg(player.GetMag()) + ((Spell)player.GetSpellsEquipped()[index]).getDamg());
                                                        }
                                                        else
                                                        {
                                                            forestMonster.decreaseHealth(((Spell)player.GetSpellsEquipped()[index]).getDamg() + player.GetMag());
                                                        }
                                                    }
                                                    lastSpellIndex = index;
                                                }
                                                else
                                                {
                                                    // Passive Magic Spells
                                                    spellName = ((Spell)player.GetSpellsEquipped()[index]).GetName();

                                                    switch (spellName)
                                                    {
                                                        case "Iron Armor":
                                                            ironArmorTurns = ((Spell)player.GetSpellsEquipped()[index]).use();
                                                            break;

                                                        case "Swift Evasion":
                                                            swiftEvasionTurns = ((Spell)player.GetSpellsEquipped()[index]).use();
                                                            break;
                                                    }
                                                }
                                                ((Spell)player.GetSpellsEquipped()[index]).decreaseCastTimes();
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough casts on the spell selected");
                                            }


                                        }

                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No spells equipped");
                                }

                                if (player.GetHealth() <= 0)
                                {
                                    Console.WriteLine("You have died and lost your exp");
                                    ironArmorTurns = 0;
                                    swiftEvasionTurns = 0;
                                    player.SetExp(0);
                                    RestingArea();
                                }
                                break;
                            // Last Magic attack sequence
                            case "lm":
                                if (player.GetSpellsEquipped().Count != 0)
                                {
                                    // Is the number typed within index
                                    if (lastSpellIndex >= 0 && lastSpellIndex < player.GetSpellsEquipped().Count)
                                    {
                                        // Does the spell have casts
                                        if (((Spell)player.GetSpellsEquipped()[lastSpellIndex]).getCastTimes() > 0)
                                        {
                                            if (((Spell)player.GetSpellsEquipped()[lastSpellIndex]).getDamg() != 0)
                                            {
                                                // Damaging Spells
                                                attackHit = rnd.Next(5);
                                                monsterAttackType = rnd.Next(4);
                                                if (monsterAttackType == 0)
                                                {
                                                    monsterDamg = (int)(strongAttack * levelMultiplier);
                                                }
                                                else
                                                {
                                                    monsterDamg = (int)(quickAttack * levelMultiplier);
                                                }
                                                if (swiftEvasionTurns == 0)
                                                {
                                                    monsterAttackHit = rnd.Next(3);
                                                }
                                                else
                                                {
                                                    monsterAttackHit = rnd.Next(2);
                                                    swiftEvasionTurns--;
                                                }
                                                if (attackHit == 0)
                                                {
                                                    Console.WriteLine("\nYour attack failed");
                                                    if (monsterAttackHit == 0)
                                                    {
                                                        Console.WriteLine("The monster has missed you\n");
                                                    }
                                                    else
                                                    {
                                                        if (monsterDamg == quickAttack)
                                                        {
                                                            Console.WriteLine("The monster has landed a quick attack\n");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("The monster has landed a strong attack\n");
                                                        }

                                                        // Damage Calculations
                                                        PlayerDamagedCalculation(monsterDamg);
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("\nYour attack was successful\n");
                                                    if (monsterAttackHit == 0)
                                                    {
                                                        Console.WriteLine("The monster has missed you\n");
                                                    }
                                                    else
                                                    {
                                                        if (monsterDamg == quickAttack)
                                                        {
                                                            Console.WriteLine("The monster has landed a quick attack\n");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("The monster has landed a strong attack\n");
                                                        }

                                                        // Damage Calculations
                                                        PlayerDamagedCalculation(monsterDamg);
                                                    }
                                                    mageWeapon = player.GetMageWeapon();
                                                    if (mageWeapon != null)
                                                    {
                                                        forestMonster.decreaseHealth(mageWeapon.CalcDamg(player.GetMag()) + ((Spell)player.GetSpellsEquipped()[index]).getDamg());
                                                    }
                                                    else
                                                    {
                                                        forestMonster.decreaseHealth(((Spell)player.GetSpellsEquipped()[index]).getDamg() + player.GetMag());
                                                    }
                                                }
                                                lastSpellIndex = index;
                                            }
                                            else
                                            {
                                                // Passive Magic Spells
                                                spellName = ((Spell)player.GetSpellsEquipped()[index]).GetName();

                                                switch (spellName)
                                                {
                                                    case "Iron Armor":
                                                        ironArmorTurns = ((Spell)player.GetSpellsEquipped()[index]).use();
                                                        break;

                                                    case "Swift Evasion":
                                                        swiftEvasionTurns = ((Spell)player.GetSpellsEquipped()[index]).use();
                                                        break;
                                                }
                                            }
                                            ((Spell)player.GetSpellsEquipped()[index]).decreaseCastTimes();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Not enough casts on the spell selected");
                                        }


                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No spells equipped");
                                }

                                if (player.GetHealth() <= 0)
                                {
                                    Console.WriteLine("You have died and lost your exp");
                                    ironArmorTurns = 0;
                                    swiftEvasionTurns = 0;
                                    player.SetExp(0);
                                    RestingArea();
                                }
                                break;
                            case "b":
                                player.DisplayEquipmentBag();
                                break;
                            case "e":
                                player.Status();
                                break;
                            case "r":
                                numKills = 0;
                                player.SetHealth(1);
                                player.SetHeals(maxHeals);
                                RestingArea();
                                break;


                            case "h":
                                if (player.GetHeals() > 0)
                                {
                                    player.IncreaseHealth(potionHealAmount);
                                    if (player.GetHealth() > player.GetMaxHealth())
                                    {
                                        player.SetHealth(1);
                                    }
                                    player.SetHeals(player.GetHeals() - 1);
                                    Console.WriteLine("You used a heal");
                                }
                                else
                                {
                                    Console.WriteLine("You are out of heals");
                                }
                                break;
                            default:
                                Console.WriteLine("Incorrect input");
                                break;
                        }
                    }
                    var = rng.Next(0, 3);
                    if (var == 0)
                    {
                        Console.WriteLine("You have found a chest");
                        if (player.GetEquipmentBag().Count < player.GetBagSpace())
                        {
                            player.AddEquipment(0);
                        }
                        else
                        {
                            Console.WriteLine("You are out of bag space.");
                        }

                    }
                    Console.WriteLine("Monster has been slain");
                    numKills++;
                    player.SetExp(player.GetExp() + 300);
                }
            }
            BossFight(1);
        }

        /// <summary>
        /// Second stage
        /// </summary>
        /// <param name="numberOfKills"></param>
        public static void Wasteland(int numberOfKills)
        {
            MeleeWeapon mWep;
            MageWeapon mageWeapon;
            RangedWeapon rangedWeapon;
            int numWastelandEnemies = 60;
            int numKills = numberOfKills;
            int var = 0;
            int index = 0;
            int playerDamg = 0;
            Random rnd = new Random();
            Random rng = new Random();
            int monsterHealth = (int)((double)rnd.Next(500, 550) * levelMultiplier);
            int quickAttack = 12, strongAttack = 24;
            int attackHit = 0, monsterAttackHit = 0;
            int monsterAttackType = 0, monsterDamg = 0;
            int vendorNum = 0;
            string spellName = string.Empty;
            while (numKills < numWastelandEnemies)
            {
                monsterHealth = (int)((double)rnd.Next(500, 550) * levelMultiplier);
                vendorNum = rnd.Next(0, 8);
                if (numKills > 10 && vendorNum == 7)
                {
                    VendorMenu(2, numKills);
                }
                else
                {
                    Monster wastelandMonster = new Monster(monsterHealth, quickAttack, strongAttack);
                    Console.WriteLine("\nA monster has appeared\n");
                    while (wastelandMonster.getHealth() > 0)
                    {
                        if (ironArmorTurns != 0)
                        {
                            Console.WriteLine("\nIron Armor is active");
                        }
                        if (swiftEvasionTurns != 0)
                        {
                            Console.WriteLine("Swift Evasion is active");
                        }
                        Console.WriteLine("Player's current health: {0}/{1}", player.GetHealth(), player.GetMaxHealth());
                        Console.WriteLine("Monster's current health: " + wastelandMonster.getHealth());
                        Console.WriteLine("{0}/{1} monsters killed", numKills, numWastelandEnemies);
                        Console.WriteLine("Bag Space: {0}/{1}", player.GetEquipmentBag().Count, player.GetBagSpace());
                        Console.WriteLine("Exp: " + player.GetExp());
                        Console.WriteLine("Go rest (r) (this will reset the monsters you have to defeat)");
                        Console.WriteLine("Choose an option:");
                        Console.WriteLine("Attack (a)");
                        Console.WriteLine("Ranged Attack (ra) ({0})", player.GetArrows());
                        Console.WriteLine("Magic attack (m)");
                        Console.WriteLine("Check bag (b)");
                        Console.WriteLine("Check equipment (e)");
                        Console.WriteLine("Heal (h) (heals for {0}) ({1})", potionHealAmount, player.GetHeals());
                        input = Console.ReadLine();

                        switch (input)
                        {
                            // Melee attack sequence
                            case "a":
                                attackHit = rnd.Next(3);
                                monsterAttackType = rnd.Next(4);
                                if (monsterAttackType == 0)
                                {
                                    monsterDamg = (int)(strongAttack * levelMultiplier);
                                }
                                else
                                {
                                    monsterDamg = (int)(quickAttack * levelMultiplier);
                                }
                                if (swiftEvasionTurns == 0)
                                {
                                    monsterAttackHit = rnd.Next(3);
                                }
                                else
                                {
                                    monsterAttackHit = rnd.Next(2);
                                    swiftEvasionTurns--;
                                }
                                if (attackHit == 0)
                                {
                                    Console.WriteLine("\nYour attack failed");
                                    if (monsterAttackHit == 0)
                                    {
                                        Console.WriteLine("The monster has missed you\n");
                                    }
                                    else
                                    {
                                        if (monsterDamg == quickAttack)
                                        {
                                            Console.WriteLine("The monster has landed a quick attack\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("The monster has landed a strong attack\n");
                                        }

                                        // Damage Calculations
                                        PlayerDamagedCalculation(monsterDamg);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("\nYour attack was successful\n");
                                    if (monsterAttackHit == 0)
                                    {
                                        Console.WriteLine("The monster has missed you\n");
                                    }
                                    else
                                    {
                                        if (monsterDamg == quickAttack)
                                        {
                                            Console.WriteLine("The monster has landed a quick attack\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("The monster has landed a strong attack\n");
                                        }

                                        // Damage Calculations
                                        PlayerDamagedCalculation(monsterDamg);
                                    }
                                    playerDamg = PlayerMeleeAttackCalculation();
                                    wastelandMonster.decreaseHealth(playerDamg);
                                }
                                if (player.GetHealth() <= 0)
                                {
                                    Console.WriteLine("You have died and lost your exp");
                                    ironArmorTurns = 0;
                                    swiftEvasionTurns = 0;
                                    player.SetExp(0);
                                    RestingArea();
                                }
                                break;

                            // Ranged attack sequence
                            case "ra":
                                // Checks if player has arrows and a bow
                                if (player.GetArrows() != 0 && player.GetRangedWeapon() != null)
                                {
                                    attackHit = rnd.Next(3);
                                    monsterAttackType = rnd.Next(4);
                                    if (monsterAttackType == 0)
                                    {
                                        monsterDamg = (int)(strongAttack * levelMultiplier);
                                    }
                                    else
                                    {
                                        monsterDamg = (int)(quickAttack * levelMultiplier);
                                    }
                                    if (swiftEvasionTurns == 0)
                                    {
                                        monsterAttackHit = rnd.Next(3);
                                    }
                                    else
                                    {
                                        monsterAttackHit = rnd.Next(2);
                                        swiftEvasionTurns--;
                                    }
                                    if (attackHit == 0)
                                    {
                                        Console.WriteLine("\nYour attack failed");
                                        if (monsterAttackHit == 0)
                                        {
                                            Console.WriteLine("The monster has missed you\n");
                                        }
                                        else
                                        {
                                            if (monsterDamg == quickAttack)
                                            {
                                                Console.WriteLine("The monster has landed a quick attack\n");
                                            }
                                            else
                                            {
                                                Console.WriteLine("The monster has landed a strong attack\n");
                                            }

                                            // Damage Calculations
                                            PlayerDamagedCalculation(monsterDamg);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nYour attack was successful\n");
                                        if (monsterAttackHit == 0)
                                        {
                                            Console.WriteLine("The monster has missed you\n");
                                        }
                                        else
                                        {
                                            if (monsterDamg == quickAttack)
                                            {
                                                Console.WriteLine("The monster has landed a quick attack\n");
                                            }
                                            else
                                            {
                                                Console.WriteLine("The monster has landed a strong attack\n");
                                            }

                                            // Damage Calculations
                                            PlayerDamagedCalculation(monsterDamg);
                                        }
                                        rangedWeapon = player.GetRangedWeapon();
                                        wastelandMonster.decreaseHealth(rangedWeapon.CalcDamg(player.GetDex(), player.GetStr()));
                                    }
                                    player.DecreaseArrows(1);
                                    if (player.GetHealth() <= 0)
                                    {
                                        Console.WriteLine("You have died and lost your exp");
                                        ironArmorTurns = 0;
                                        swiftEvasionTurns = 0;
                                        player.SetExp(0);
                                        RestingArea();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Not enough arrows/No bow equipped");
                                }
                                break;

                            // Magic attack sequence
                            case "m":
                                if (player.GetSpellsEquipped().Count != 0)
                                {
                                    for (int i = 0; i < player.GetSpellsEquipped().Count; i++)
                                    {
                                        Console.WriteLine("(" + i + ")" + ((Spell)(player.GetSpellsEquipped()[i])).ToString());
                                    }
                                    Console.WriteLine("Type in the number of the spell to use");
                                    input = Console.ReadLine();
                                    // Is the input a number
                                    if (int.TryParse(input, out index) == true)
                                    {
                                        // Is the number typed within index
                                        if (index >= 0 && index < player.GetSpellsEquipped().Count)
                                        {
                                            // Does the spell have casts
                                            if (((Spell)player.GetSpellsEquipped()[index]).getCastTimes() > 0)
                                            {
                                                if (((Spell)player.GetSpellsEquipped()[index]).getDamg() != 0)
                                                {
                                                    // Damaging Spells
                                                    attackHit = rnd.Next(5);
                                                    monsterAttackType = rnd.Next(4);
                                                    if (monsterAttackType == 0)
                                                    {
                                                        monsterDamg = (int)(strongAttack * levelMultiplier);
                                                    }
                                                    else
                                                    {
                                                        monsterDamg = (int)(quickAttack * levelMultiplier);
                                                    }
                                                    if (swiftEvasionTurns == 0)
                                                    {
                                                        monsterAttackHit = rnd.Next(3);
                                                    }
                                                    else
                                                    {
                                                        monsterAttackHit = rnd.Next(2);
                                                        swiftEvasionTurns--;
                                                    }
                                                    if (attackHit == 0)
                                                    {
                                                        Console.WriteLine("\nYour attack failed");
                                                        if (monsterAttackHit == 0)
                                                        {
                                                            Console.WriteLine("The monster has missed you\n");
                                                        }
                                                        else
                                                        {
                                                            if (monsterDamg == quickAttack)
                                                            {
                                                                Console.WriteLine("The monster has landed a quick attack\n");
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("The monster has landed a strong attack\n");
                                                            }

                                                            // Damage Calculations
                                                            PlayerDamagedCalculation(monsterDamg);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("\nYour attack was successful\n");
                                                        if (monsterAttackHit == 0)
                                                        {
                                                            Console.WriteLine("The monster has missed you\n");
                                                        }
                                                        else
                                                        {
                                                            if (monsterDamg == quickAttack)
                                                            {
                                                                Console.WriteLine("The monster has landed a quick attack\n");
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("The monster has landed a strong attack\n");
                                                            }

                                                            // Damage Calculations
                                                            PlayerDamagedCalculation(monsterDamg);
                                                        }
                                                        mageWeapon = player.GetMageWeapon();
                                                        if (mageWeapon != null)
                                                        {
                                                            wastelandMonster.decreaseHealth(mageWeapon.CalcDamg(player.GetMag()) + ((Spell)player.GetSpellsEquipped()[index]).getDamg());
                                                        }
                                                        else
                                                        {
                                                            wastelandMonster.decreaseHealth(((Spell)player.GetSpellsEquipped()[index]).getDamg() + player.GetMag());
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // Passive Magic Spells
                                                    spellName = ((Spell)player.GetSpellsEquipped()[index]).GetName();

                                                    switch (spellName)
                                                    {
                                                        case "Iron Armor":
                                                            ironArmorTurns = ((Spell)player.GetSpellsEquipped()[index]).use();
                                                            break;

                                                        case "Swift Evasion":
                                                            swiftEvasionTurns = ((Spell)player.GetSpellsEquipped()[index]).use();
                                                            break;
                                                    }
                                                }
                                                ((Spell)player.GetSpellsEquipped()[index]).decreaseCastTimes();
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough casts on the spell selected");
                                            }


                                        }

                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No spells equipped");
                                }

                                if (player.GetHealth() <= 0)
                                {
                                    Console.WriteLine("You have died and lost your exp");
                                    ironArmorTurns = 0;
                                    swiftEvasionTurns = 0;
                                    player.SetExp(0);
                                    RestingArea();
                                }
                                break;
                            case "b":
                                player.DisplayEquipmentBag();
                                break;
                            case "e":
                                player.Status();
                                break;
                            case "r":
                                RestingArea();
                                break;

                            case "h":
                                if (player.GetHeals() > 0)
                                {
                                    player.IncreaseHealth(potionHealAmount);
                                    if (player.GetHealth() > player.GetMaxHealth())
                                    {
                                        player.SetHealth(1);
                                    }
                                    player.SetHeals(player.GetHeals() - 1);
                                    Console.WriteLine("You used a heal");
                                }
                                else
                                {
                                    Console.WriteLine("You are out of heals");
                                }
                                break;
                            default:
                                Console.WriteLine("Incorrect input");
                                break;
                        }
                    }
                    var = rng.Next(0, 5);
                    if (var == 0)
                    {
                        Console.WriteLine("You have found a chest");
                        if (player.GetEquipmentBag().Count < player.GetBagSpace())
                        {
                            player.AddEquipment(1);
                        }
                        else
                        {
                            Console.WriteLine("You are out of bag space.");
                        }

                    }
                    Console.WriteLine("Monster has been slain");
                    numKills++;
                    player.SetExp(player.GetExp() + 1500);
                }
            }
            BossFight(2);
        }

        /// <summary>
        /// Third stage
        /// </summary>
        /// <param name="numberOfKills"></param>
        public static void Graveyard(int numberOfKills)
        {
            MeleeWeapon mWep;
            MageWeapon mageWeapon;
            RangedWeapon rangedWeapon;
            int numGraveyardEnemies = 100;
            int numKills = 0;
            int var = 0;
            int index = 0;
            int playerDamg = 0;
            Random rnd = new Random();
            Random rng = new Random();
            int monsterHealth = (int)((double)rnd.Next(1500, 2000) * levelMultiplier);
            int quickAttack = 30, strongAttack = 40;
            int attackHit = 0, monsterAttackHit = 0;
            int monsterAttackType = 0, monsterDamg = 0;
            int vendorNum = 0;
            string spellName = string.Empty;
            while (numKills < numGraveyardEnemies)
            {
                monsterHealth = (int)((double)rnd.Next(1500, 2000) * levelMultiplier);
                vendorNum = rnd.Next(0, 8);
                if (numKills > 10 && vendorNum == 7)
                {
                    VendorMenu(3, numKills);
                }
                else
                {
                    Monster graveyardMonster = new Monster(monsterHealth, quickAttack, strongAttack);
                    Console.WriteLine("\nA monster has appeared\n");
                    while (graveyardMonster.getHealth() > 0)
                    {
                        if (ironArmorTurns != 0)
                        {
                            Console.WriteLine("\nIron Armor is active");
                        }
                        if (swiftEvasionTurns != 0)
                        {
                            Console.WriteLine("Swift Evasion is active");
                        }
                        Console.WriteLine("Player's current health: {0}/{1}", player.GetHealth(), player.GetMaxHealth());
                        Console.WriteLine("Monster's current health: " + graveyardMonster.getHealth());
                        Console.WriteLine("{0}/{1} monsters killed", numKills, numGraveyardEnemies);
                        Console.WriteLine("Bag Space: {0}/{1}", player.GetEquipmentBag().Count, player.GetBagSpace());
                        Console.WriteLine("Exp: " + player.GetExp());
                        Console.WriteLine("Go rest (r) (this will reset the monsters you have to defeat)");
                        Console.WriteLine("Choose an option:");
                        Console.WriteLine("Attack (a)");
                        Console.WriteLine("Ranged Attack (ra) ({0})", player.GetArrows());
                        Console.WriteLine("Magic attack (m)");
                        Console.WriteLine("Check bag (b)");
                        Console.WriteLine("Check equipment (e)");
                        Console.WriteLine("Heal (h) (heals for {0}) ({1})", potionHealAmount, player.GetHeals());
                        input = Console.ReadLine();

                        switch (input)
                        {
                            // Melee attack sequence
                            case "a":
                                attackHit = rnd.Next(3);
                                monsterAttackType = rnd.Next(4);
                                if (monsterAttackType == 0)
                                {
                                    monsterDamg = (int)(strongAttack * levelMultiplier);
                                }
                                else
                                {
                                    monsterDamg = (int)(quickAttack * levelMultiplier);
                                }
                                if (swiftEvasionTurns == 0)
                                {
                                    monsterAttackHit = rnd.Next(3);
                                }
                                else
                                {
                                    monsterAttackHit = rnd.Next(2);
                                    swiftEvasionTurns--;
                                }
                                if (attackHit == 0)
                                {
                                    Console.WriteLine("\nYour attack failed");
                                    if (monsterAttackHit == 0)
                                    {
                                        Console.WriteLine("The monster has missed you\n");
                                    }
                                    else
                                    {
                                        if (monsterDamg == quickAttack)
                                        {
                                            Console.WriteLine("The monster has landed a quick attack\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("The monster has landed a strong attack\n");
                                        }

                                        // Damage Calculations
                                        PlayerDamagedCalculation(monsterDamg);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("\nYour attack was successful\n");
                                    if (monsterAttackHit == 0)
                                    {
                                        Console.WriteLine("The monster has missed you\n");
                                    }
                                    else
                                    {
                                        if (monsterDamg == quickAttack)
                                        {
                                            Console.WriteLine("The monster has landed a quick attack\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("The monster has landed a strong attack\n");
                                        }

                                        // Damage Calculations
                                        PlayerDamagedCalculation(monsterDamg);
                                    }
                                    playerDamg = PlayerMeleeAttackCalculation();
                                    graveyardMonster.decreaseHealth(playerDamg);
                                }
                                if (player.GetHealth() <= 0)
                                {
                                    Console.WriteLine("You have died and lost your exp");
                                    ironArmorTurns = 0;
                                    swiftEvasionTurns = 0;
                                    player.SetExp(0);
                                    RestingArea();
                                }
                                break;

                            // Ranged attack sequence
                            case "ra":
                                // Checks if player has arrows and a bow
                                if (player.GetArrows() != 0 && player.GetRangedWeapon() != null)
                                {
                                    attackHit = rnd.Next(3);
                                    monsterAttackType = rnd.Next(4);
                                    if (monsterAttackType == 0)
                                    {
                                        monsterDamg = (int)(strongAttack * levelMultiplier);
                                    }
                                    else
                                    {
                                        monsterDamg = (int)(quickAttack * levelMultiplier);
                                    }
                                    if (swiftEvasionTurns == 0)
                                    {
                                        monsterAttackHit = rnd.Next(3);
                                    }
                                    else
                                    {
                                        monsterAttackHit = rnd.Next(2);
                                        swiftEvasionTurns--;
                                    }
                                    if (attackHit == 0)
                                    {
                                        Console.WriteLine("\nYour attack failed");
                                        if (monsterAttackHit == 0)
                                        {
                                            Console.WriteLine("The monster has missed you\n");
                                        }
                                        else
                                        {
                                            if (monsterDamg == quickAttack)
                                            {
                                                Console.WriteLine("The monster has landed a quick attack\n");
                                            }
                                            else
                                            {
                                                Console.WriteLine("The monster has landed a strong attack\n");
                                            }

                                            // Damage Calculations
                                            PlayerDamagedCalculation(monsterDamg);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nYour attack was successful\n");
                                        if (monsterAttackHit == 0)
                                        {
                                            Console.WriteLine("The monster has missed you\n");
                                        }
                                        else
                                        {
                                            if (monsterDamg == quickAttack)
                                            {
                                                Console.WriteLine("The monster has landed a quick attack\n");
                                            }
                                            else
                                            {
                                                Console.WriteLine("The monster has landed a strong attack\n");
                                            }

                                            // Damage Calculations
                                            PlayerDamagedCalculation(monsterDamg);
                                        }
                                        rangedWeapon = player.GetRangedWeapon();
                                        graveyardMonster.decreaseHealth(rangedWeapon.CalcDamg(player.GetDex(), player.GetStr()));
                                    }
                                    player.DecreaseArrows(1);
                                    if (player.GetHealth() <= 0)
                                    {
                                        Console.WriteLine("You have died and lost your exp");
                                        ironArmorTurns = 0;
                                        player.SetExp(0);
                                        RestingArea();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Not enough arrows/No bow equipped");
                                }
                                break;

                            // Magic attack sequence
                            case "m":
                                if (player.GetSpellsEquipped().Count != 0)
                                {
                                    for (int i = 0; i < player.GetSpellsEquipped().Count; i++)
                                    {
                                        Console.WriteLine("(" + i + ")" + ((Spell)(player.GetSpellsEquipped()[i])).ToString());
                                    }
                                    Console.WriteLine("Type in the number of the spell to use");
                                    input = Console.ReadLine();
                                    // Is the input a number
                                    if (int.TryParse(input, out index) == true)
                                    {
                                        // Is the number typed within index
                                        if (index >= 0 && index < player.GetSpellsEquipped().Count)
                                        {
                                            // Does the spell have casts
                                            if (((Spell)player.GetSpellsEquipped()[index]).getCastTimes() > 0)
                                            {
                                                if (((Spell)player.GetSpellsEquipped()[index]).getDamg() != 0)
                                                {
                                                    // Damaging Spells
                                                    attackHit = rnd.Next(5);
                                                    monsterAttackType = rnd.Next(4);
                                                    if (monsterAttackType == 0)
                                                    {
                                                        monsterDamg = (int)(strongAttack * levelMultiplier);
                                                    }
                                                    else
                                                    {
                                                        monsterDamg = (int)(quickAttack * levelMultiplier);
                                                    }
                                                    if (swiftEvasionTurns == 0)
                                                    {
                                                        monsterAttackHit = rnd.Next(3);
                                                    }
                                                    else
                                                    {
                                                        monsterAttackHit = rnd.Next(2);
                                                        swiftEvasionTurns--;
                                                    }
                                                    if (attackHit == 0)
                                                    {
                                                        Console.WriteLine("\nYour attack failed");
                                                        if (monsterAttackHit == 0)
                                                        {
                                                            Console.WriteLine("The monster has missed you\n");
                                                        }
                                                        else
                                                        {
                                                            if (monsterDamg == quickAttack)
                                                            {
                                                                Console.WriteLine("The monster has landed a quick attack\n");
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("The monster has landed a strong attack\n");
                                                            }

                                                            // Damage Calculations
                                                            PlayerDamagedCalculation(monsterDamg);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("\nYour attack was successful\n");
                                                        if (monsterAttackHit == 0)
                                                        {
                                                            Console.WriteLine("The monster has missed you\n");
                                                        }
                                                        else
                                                        {
                                                            if (monsterDamg == quickAttack)
                                                            {
                                                                Console.WriteLine("The monster has landed a quick attack\n");
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("The monster has landed a strong attack\n");
                                                            }

                                                            // Damage Calculations
                                                            PlayerDamagedCalculation(monsterDamg);
                                                        }
                                                        mageWeapon = player.GetMageWeapon();
                                                        if (mageWeapon != null)
                                                        {
                                                            graveyardMonster.decreaseHealth(mageWeapon.CalcDamg(player.GetMag()) + ((Spell)player.GetSpellsEquipped()[index]).getDamg());
                                                        }
                                                        else
                                                        {
                                                            graveyardMonster.decreaseHealth(((Spell)player.GetSpellsEquipped()[index]).getDamg() + player.GetMag());
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // Passive Magic Spells
                                                    spellName = ((Spell)player.GetSpellsEquipped()[index]).GetName();

                                                    switch (spellName)
                                                    {
                                                        case "Iron Armor":
                                                            ironArmorTurns = ((Spell)player.GetSpellsEquipped()[index]).use();
                                                            break;

                                                        case "Swift Evasion":
                                                            swiftEvasionTurns = ((Spell)player.GetSpellsEquipped()[index]).use();
                                                            break;
                                                    }
                                                }
                                                ((Spell)player.GetSpellsEquipped()[index]).decreaseCastTimes();
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough casts on the spell selected");
                                            }


                                        }

                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No spells equipped");
                                }

                                if (player.GetHealth() <= 0)
                                {
                                    Console.WriteLine("You have died and lost your exp");
                                    ironArmorTurns = 0;
                                    swiftEvasionTurns = 0;
                                    player.SetExp(0);
                                    RestingArea();
                                }
                                break;
                            case "b":
                                player.DisplayEquipmentBag();
                                break;
                            case "e":
                                player.Status();
                                break;
                            case "r":
                                RestingArea();
                                break;

                            case "h":
                                if (player.GetHeals() > 0)
                                {
                                    player.IncreaseHealth(potionHealAmount);
                                    if (player.GetHealth() > player.GetMaxHealth())
                                    {
                                        player.SetHealth(1);
                                    }
                                    player.SetHeals(player.GetHeals() - 1);
                                    Console.WriteLine("You used a heal");
                                }
                                else
                                {
                                    Console.WriteLine("You are out of heals");
                                }
                                break;
                            default:
                                Console.WriteLine("Incorrect input");
                                break;
                        }
                    }

                    // Chance for regular drop 
                    var = rng.Next(0, 5);
                    if (var == 0)
                    {
                        Console.WriteLine("You have found a chest");
                        if (player.GetEquipmentBag().Count < player.GetBagSpace())
                        {
                            player.AddEquipment(2);
                        }
                        else
                        {
                            Console.WriteLine("You are out of bag space.");
                        }

                    }

                    // Chance for a set item
                    var = rng.Next(0, 100);
                    if (var == 40)
                    {
                        Console.WriteLine("You have found a set item chest");
                        if (player.GetEquipmentBag().Count < player.GetBagSpace())
                        {
                            player.AddSetItem();
                        }
                        else
                        {
                            Console.WriteLine("You are out of bag space.");
                        }
                    }

                    Console.WriteLine("Monster has been slain");
                    numKills++;
                    player.SetExp(player.GetExp() + 5000);
                }
            }
            BossFight(3);
        }

        /// <summary>
        /// Fourth stage
        /// </summary>
        /// <param name="numberOfKills"></param>
        public static void DepthsOfHell(int numberOfKills)
        {
            Console.WriteLine("Welcome to Hell");
            RestingArea();
        }

        /// <summary>
        /// Boss fight based on stage:
        /// 1. Forest
        /// 2. Wasteland
        /// 3. Graveyard
        /// 4. Depths of Hell (not implemented atm)
        /// 10. Longsword Battlegrounds
        /// 11. Dagger Battlegrounds
        /// 12. Twindaggers Battlegrounds
        /// 13. Shortsword Battlegrounds
        /// 14. Staff Battlegrounds
        /// 15. Bow Battlegrounds
        /// </summary>
        /// <param name="stage"></param>
        public static void BossFight(int stage)
        {
            MeleeWeapon mWep;
            MageWeapon mageWeapon;
            RangedWeapon rangedWeapon;
            int index = 0;
            int bossHealth = 0;
            int quickAttack = 0, strongAttack = 0;
            int playerDamg = 0;
            string spellName = string.Empty;
            Random rnd = new Random();
            Random rng = new Random();
            int rand = 0;
            switch (stage)
            {
                // Forest
                case 1:
                    bossHealth = (int)((double)rnd.Next(500, 600) * levelMultiplier);
                    quickAttack = 12;
                    strongAttack = 20;
                    break;

                // Wasteland
                case 2:
                    bossHealth = (int)((double)rnd.Next(2000, 2500) * levelMultiplier);
                    quickAttack = 30;
                    strongAttack = 45;
                    break;

                // Graveyard
                case 3:
                    bossHealth = (int)((double)rnd.Next(10000, 12500) * levelMultiplier);
                    quickAttack = 60;
                    strongAttack = 75;
                    break;

                // Special stage 1 (Longsword)
                case 10:
                    bossHealth = (int)((double)rnd.Next(100000, 125000) * levelMultiplier);
                    quickAttack = 150;
                    strongAttack = 200;
                    break;

                // Special stage 2 (Dagger)
                case 11:
                    bossHealth = (int)((double)rnd.Next(100000, 125000) * levelMultiplier);
                    quickAttack = 150;
                    strongAttack = 200;
                    break;

                // Special stage 3 (Twindaggers)
                case 12:
                    bossHealth = (int)((double)rnd.Next(100000, 125000) * levelMultiplier);
                    quickAttack = 150;
                    strongAttack = 200;
                    break;

                // Special stage 4 (Shortsword)
                case 13:
                    bossHealth = (int)((double)rnd.Next(100000, 125000) * levelMultiplier);
                    quickAttack = 150;
                    strongAttack = 200;
                    break;

                // Special stage 5 (Staff)
                case 14:
                    bossHealth = (int)((double)rnd.Next(100000, 125000) * levelMultiplier);
                    quickAttack = 150;
                    strongAttack = 200;
                    break;

                // Special stage 6 (Bow)
                case 15:
                    bossHealth = (int)((double)rnd.Next(100000, 125000) * levelMultiplier);
                    quickAttack = 150;
                    strongAttack = 200;
                    break;
            }
            int attackHit = 0, monsterAttackHit = 0;
            int monsterAttackType = 0, monsterDamg = 0;

            Monster bossMonster = new Monster(bossHealth, quickAttack, strongAttack);
            Console.WriteLine("\nA Boss has appeared");
            while (bossMonster.getHealth() > 0)
            {
                if (ironArmorTurns != 0)
                {
                    Console.WriteLine("\nIron Armor is active");
                }
                if (swiftEvasionTurns != 0)
                {
                    Console.WriteLine("Swift Evasion is active");
                }
                Console.WriteLine("Player's current health: {0}/{1}", player.GetHealth(), player.GetMaxHealth());
                Console.WriteLine("Boss's current health: " + bossMonster.getHealth());
                Console.WriteLine("Bag Space: {0}/{1}", player.GetEquipmentBag().Count, player.GetBagSpace());
                Console.WriteLine("Exp: " + player.GetExp());
                Console.WriteLine("Go rest (r) (this will reset the monsters you have to defeat)");
                Console.WriteLine("Choose an option:");
                Console.WriteLine("Attack (a)");
                Console.WriteLine("Ranged Attack (ra) ({0})", player.GetArrows());
                Console.WriteLine("Magic attack (m)");
                Console.WriteLine("Check bag (b)");
                Console.WriteLine("Check equipment (e)");
                Console.WriteLine("Heal (h) (heals for {0}) ({1})", potionHealAmount, player.GetHeals());
                input = Console.ReadLine();

                switch (input)
                {
                    // Melee attack sequence
                    case "a":
                        attackHit = rnd.Next(3);
                        monsterAttackType = rnd.Next(4);
                        if (monsterAttackType == 0)
                        {
                            monsterDamg = (int)(strongAttack * levelMultiplier);
                        }
                        else
                        {
                            monsterDamg = (int)(quickAttack * levelMultiplier);
                        }
                        if (swiftEvasionTurns == 0)
                        {
                            monsterAttackHit = rnd.Next(3);
                        }
                        else
                        {
                            monsterAttackHit = rnd.Next(2);
                            swiftEvasionTurns--;
                        }
                        if (attackHit == 0)
                        {
                            Console.WriteLine("\nYour attack failed");
                            if (monsterAttackHit == 0)
                            {
                                Console.WriteLine("The boss has missed you\n");
                            }
                            else
                            {
                                if (monsterDamg == quickAttack)
                                {
                                    Console.WriteLine("The boss has landed a quick attack\n");
                                }
                                else
                                {
                                    Console.WriteLine("The boss has landed a strong attack\n");
                                }

                                // Damage Calculations
                                PlayerDamagedCalculation(monsterDamg);
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nYour attack was successful\n");
                            if (monsterAttackHit == 0)
                            {
                                Console.WriteLine("The boss has missed you\n");
                            }
                            else
                            {
                                if (monsterDamg == quickAttack)
                                {
                                    Console.WriteLine("The boss has landed a quick attack\n");
                                }
                                else
                                {
                                    Console.WriteLine("The boss has landed a strong attack\n");
                                }

                                // Damage Calculations
                                PlayerDamagedCalculation(monsterDamg);
                            }
                            playerDamg = PlayerMeleeAttackCalculation();
                            bossMonster.decreaseHealth(playerDamg);
                            mWep = player.GetMWeapon();
                        }
                        if (player.GetHealth() <= 0)
                        {
                            Console.WriteLine("You have died and lost your exp");
                            ironArmorTurns = 0;
                            swiftEvasionTurns = 0;
                            player.SetExp(0);
                            RestingArea();
                        }
                        break;

                    // Ranged attack sequence
                    case "ra":
                        // Checks if player has arrows and a bow
                        if (player.GetArrows() != 0 && player.GetRangedWeapon() != null)
                        {
                            attackHit = rnd.Next(3);
                            monsterAttackType = rnd.Next(4);
                            if (monsterAttackType == 0)
                            {
                                monsterDamg = (int)(strongAttack * levelMultiplier);
                            }
                            else
                            {
                                monsterDamg = (int)(quickAttack * levelMultiplier);
                            }
                            if (swiftEvasionTurns == 0)
                            {
                                monsterAttackHit = rnd.Next(3);
                            }
                            else
                            {
                                monsterAttackHit = rnd.Next(2);
                                swiftEvasionTurns--;
                            }
                            if (attackHit == 0)
                            {
                                Console.WriteLine("\nYour attack failed");
                                if (monsterAttackHit == 0)
                                {
                                    Console.WriteLine("The boss has missed you\n");
                                }
                                else
                                {
                                    if (monsterDamg == quickAttack)
                                    {
                                        Console.WriteLine("The boss has landed a quick attack\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("The boss has landed a strong attack\n");
                                    }

                                    // Damage Calculations
                                    PlayerDamagedCalculation(monsterDamg);
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nYour attack was successful\n");
                                if (monsterAttackHit == 0)
                                {
                                    Console.WriteLine("The boss has missed you\n");
                                }
                                else
                                {
                                    if (monsterDamg == quickAttack)
                                    {
                                        Console.WriteLine("The boss has landed a quick attack\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("The boss has landed a strong attack\n");
                                    }

                                    // Damage Calculations
                                    PlayerDamagedCalculation(monsterDamg);
                                }
                                rangedWeapon = player.GetRangedWeapon();
                                bossMonster.decreaseHealth(rangedWeapon.CalcDamg(player.GetDex(), player.GetStr()));
                            }
                            player.DecreaseArrows(1);
                            if (player.GetHealth() <= 0)
                            {
                                Console.WriteLine("You have died and lost your exp");
                                ironArmorTurns = 0;
                                swiftEvasionTurns = 0;
                                player.SetExp(0);
                                RestingArea();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Not enough arrows/No bow equipped");
                        }
                        break;

                    // Magic attack sequence
                    case "m":
                        if (player.GetSpellsEquipped().Count != 0)
                        {
                            for (int i = 0; i < player.GetSpellsEquipped().Count; i++)
                            {
                                Console.WriteLine("(" + i + ")" + ((Spell)(player.GetSpellsEquipped()[i])).ToString());
                            }
                            Console.WriteLine("Type in the number of the spell to use");
                            input = Console.ReadLine();
                            // Is the input a number
                            if (int.TryParse(input, out index) == true)
                            {
                                // Is the number typed within index
                                if (index >= 0 && index < player.GetSpellsEquipped().Count)
                                {
                                    // Does the spell have casts
                                    if (((Spell)player.GetSpellsEquipped()[index]).getCastTimes() > 0)
                                    {
                                        if (((Spell)player.GetSpellsEquipped()[index]).getDamg() != 0)
                                        {
                                            // Damaging Spells
                                            attackHit = rnd.Next(5);
                                            monsterAttackType = rnd.Next(4);
                                            if (monsterAttackType == 0)
                                            {
                                                monsterDamg = (int)(strongAttack * levelMultiplier);
                                            }
                                            else
                                            {
                                                monsterDamg = (int)(quickAttack * levelMultiplier);
                                            }
                                            if (swiftEvasionTurns == 0)
                                            {
                                                monsterAttackHit = rnd.Next(3);
                                            }
                                            else
                                            {
                                                monsterAttackHit = rnd.Next(2);
                                                swiftEvasionTurns--;
                                            }
                                            if (attackHit == 0)
                                            {
                                                Console.WriteLine("\nYour attack failed");
                                                if (monsterAttackHit == 0)
                                                {
                                                    Console.WriteLine("The monster has missed you\n");
                                                }
                                                else
                                                {
                                                    if (monsterDamg == quickAttack)
                                                    {
                                                        Console.WriteLine("The monster has landed a quick attack\n");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("The monster has landed a strong attack\n");
                                                    }

                                                    // Damage Calculations
                                                    PlayerDamagedCalculation(monsterDamg);
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("\nYour attack was successful\n");
                                                if (monsterAttackHit == 0)
                                                {
                                                    Console.WriteLine("The monster has missed you\n");
                                                }
                                                else
                                                {
                                                    if (monsterDamg == quickAttack)
                                                    {
                                                        Console.WriteLine("The monster has landed a quick attack\n");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("The monster has landed a strong attack\n");
                                                    }

                                                    // Damage Calculations
                                                    PlayerDamagedCalculation(monsterDamg);
                                                }
                                                mageWeapon = player.GetMageWeapon();
                                                if (mageWeapon != null)
                                                {
                                                    bossMonster.decreaseHealth(mageWeapon.CalcDamg(player.GetMag()) + ((Spell)player.GetSpellsEquipped()[index]).getDamg());
                                                }
                                                else
                                                {
                                                    bossMonster.decreaseHealth(((Spell)player.GetSpellsEquipped()[index]).getDamg() + player.GetMag());
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // Passive Magic Spells
                                            spellName = ((Spell)player.GetSpellsEquipped()[index]).GetName();

                                            switch (spellName)
                                            {
                                                case "Iron Armor":
                                                    ironArmorTurns = ((Spell)player.GetSpellsEquipped()[index]).use();
                                                    break;

                                                case "Swift Evasion":
                                                    swiftEvasionTurns = ((Spell)player.GetSpellsEquipped()[index]).use();
                                                    break;
                                            }
                                        }
                                        ((Spell)player.GetSpellsEquipped()[index]).decreaseCastTimes();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Not enough casts on the spell selected");
                                    }


                                }

                            }
                        }
                        else
                        {
                            Console.WriteLine("No spells equipped");
                        }

                        if (player.GetHealth() <= 0)
                        {
                            Console.WriteLine("You have died and lost your exp");
                            ironArmorTurns = 0;
                            swiftEvasionTurns = 0;
                            player.SetExp(0);
                            RestingArea();
                        }
                        break;
                    case "b":
                        player.DisplayEquipmentBag();
                        break;
                    case "e":
                        player.Status();
                        break;
                    case "r":
                        RestingArea();
                        break;

                    case "h":
                        if (player.GetHeals() > 0)
                        {
                            player.IncreaseHealth(potionHealAmount);
                            if (player.GetHealth() > player.GetMaxHealth())
                            {
                                player.SetHealth(1);
                            }
                            player.SetHeals(player.GetHeals() - 1);
                            Console.WriteLine("You used a heal");
                        }
                        else
                        {
                            Console.WriteLine("You are out of heals");
                        }
                        break;
                    default:
                        Console.WriteLine("Incorrect input");
                        break;
                }
            }

            // Normal level bosses
            if (stage < 10)
            {
                // Did you find a special stage ticket?
                rand = rng.Next(0, 50);
                if (rand == 30)
                {
                    rand = rng.Next(0, specialStages.Count);
                    if (specialStages.Count != 0)
                    {
                        Console.WriteLine("You found a special stage ticket");
                        specialStagesPlayerList.Add(specialStages[rand]);
                        specialStages.RemoveAt(rand);
                    }
                }
                if (player.GetEquipmentBag().Count < player.GetBagSpace())
                {
                    rand = rng.Next(0, 150);
                    Console.WriteLine(rand);
                    switch (stage)
                    {
                        case 1:
                            player.AddEquipment(1);
                            if (rand == 70)
                            {
                                Console.WriteLine("A Legendary Weapon has been found.");
                                LongSword devastator = new LegendaryLongSword(rng.Next(200, 250));
                                Dagger shadowKnife = new LegendaryDagger(rng.Next(200, 250));
                                TwinDaggers twinAssassins = new LegendaryTwinDaggers(rnd.Next(200, 250));
                                ShortSword shortDevastation = new LegendaryShortSword(rnd.Next(200, 250));
                                Staff wizardsEye = new LegendaryStaff(rnd.Next(200, 250));
                                Bow hawkShot = new LegendaryBow(rnd.Next(200, 250));
                                rand = rng.Next(0, 6);
                                if (player.GetEquipmentBag().Count >= player.GetBagSpace())
                                {
                                    Console.WriteLine("You are out of bag space.");
                                }
                                else
                                {
                                    switch (rand)
                                    {
                                        case 0:
                                            player.GetEquipmentBag().Add(devastator);
                                            break;

                                        case 1:
                                            player.GetEquipmentBag().Add(shadowKnife);
                                            break;

                                        case 2:
                                            player.GetEquipmentBag().Add(twinAssassins);
                                            break;

                                        case 3:
                                            player.GetEquipmentBag().Add(shortDevastation);
                                            break;

                                        case 4:
                                            player.GetEquipmentBag().Add(wizardsEye);
                                            break;

                                        case 5:
                                            break;
                                    }
                                }
                            }
                            break;

                        case 2:
                            player.AddEquipment(2);
                            if (rand == 70)
                            {
                                Console.WriteLine("A Legendary Weapon has been found.");
                                LongSword devastator = new LegendaryLongSword(rng.Next(400, 450));
                                Dagger shadowKnife = new LegendaryDagger(rng.Next(400, 450));
                                TwinDaggers twinAssassins = new LegendaryTwinDaggers(rnd.Next(400, 450));
                                ShortSword shortDevastation = new LegendaryShortSword(rnd.Next(400, 450));
                                Staff wizardsEye = new LegendaryStaff(rnd.Next(400, 450));
                                Bow hawkShot = new LegendaryBow(rnd.Next(400, 450));
                                rand = rng.Next(0, 6);
                                if (player.GetEquipmentBag().Count >= player.GetBagSpace())
                                {
                                    Console.WriteLine("You are out of bag space.");
                                }
                                else
                                {
                                    switch (rand)
                                    {
                                        case 0:
                                            player.GetEquipmentBag().Add(devastator);
                                            break;

                                        case 1:
                                            player.GetEquipmentBag().Add(shadowKnife);
                                            break;

                                        case 2:
                                            player.GetEquipmentBag().Add(twinAssassins);
                                            break;

                                        case 3:
                                            player.GetEquipmentBag().Add(shortDevastation);
                                            break;

                                        case 4:
                                            player.GetEquipmentBag().Add(wizardsEye);
                                            break;

                                        case 5:
                                            player.GetEquipmentBag().Add(hawkShot);
                                            break;
                                    }
                                }
                            }
                            break;

                        case 3:
                            player.AddEquipment(3);
                            if (rand == 70)
                            {
                                Console.WriteLine("A Legendary Weapon has been found.");
                                LongSword devastator = new LegendaryLongSword(rng.Next(600, 650));
                                Dagger shadowKnife = new LegendaryDagger(rng.Next(600, 650));
                                TwinDaggers twinAssassins = new LegendaryTwinDaggers(rnd.Next(600, 650));
                                ShortSword shortDevastation = new LegendaryShortSword(rnd.Next(600, 650));
                                Staff wizardsEye = new LegendaryStaff(rnd.Next(600, 650));
                                Bow hawkShot = new LegendaryBow(rnd.Next(600, 650));
                                rand = rng.Next(0, 6);
                                if (player.GetEquipmentBag().Count >= player.GetBagSpace())
                                {
                                    Console.WriteLine("You are out of bag space.");
                                }
                                else
                                {
                                    switch (rand)
                                    {
                                        case 0:
                                            player.GetEquipmentBag().Add(devastator);
                                            break;

                                        case 1:
                                            player.GetEquipmentBag().Add(shadowKnife);
                                            break;

                                        case 2:
                                            player.GetEquipmentBag().Add(twinAssassins);
                                            break;

                                        case 3:
                                            player.GetEquipmentBag().Add(shortDevastation);
                                            break;

                                        case 4:
                                            player.GetEquipmentBag().Add(wizardsEye);
                                            break;

                                        case 5:
                                            player.GetEquipmentBag().Add(hawkShot);
                                            break;
                                    }
                                }
                            }
                            break;
                    }

                }
                else
                {
                    Console.WriteLine("You are out of bag space.");
                }
            }
            else
            {
                // Dependent on which special stage is active (5% drop rate on all special stages)
                rand = rng.Next(0, 20);
                Console.WriteLine(rand);
                if (rand == 20)
                {
                    Console.WriteLine("A Legendary Weapon has been found.");
                    LongSword devastator = new LegendaryLongSword(rng.Next(4000, 4500));
                    Dagger shadowKnife = new LegendaryDagger(rng.Next(4000, 4500));
                    TwinDaggers twinAssassins = new LegendaryTwinDaggers(rnd.Next(4000, 4500));
                    ShortSword shortDevastation = new LegendaryShortSword(rnd.Next(4000, 4500));
                    Staff wizardsEye = new LegendaryStaff(rnd.Next(4000, 4500));
                    Bow hawkShot = new LegendaryBow(rnd.Next(4000, 4500));
                    if (player.GetEquipmentBag().Count >= player.GetBagSpace())
                    {
                        Console.WriteLine("You are out of bag space.");
                    }
                    else
                    {
                        switch (stage)
                        {
                            // Longsword boss
                            case 10:
                                player.GetEquipmentBag().Add(devastator);
                                break;

                            // Dagger boss
                            case 11:
                                player.GetEquipmentBag().Add(shadowKnife);
                                break;

                            // Twindagger boss
                            case 12:
                                player.GetEquipmentBag().Add(twinAssassins);
                                break;

                            // Shortsword boss
                            case 13:
                                player.GetEquipmentBag().Add(shortDevastation);
                                break;

                            // Staff boss
                            case 14:
                                player.GetEquipmentBag().Add(wizardsEye);
                                break;

                            // Bow boss
                            case 15:
                                player.GetEquipmentBag().Add(hawkShot);
                                break;
                        }
                    }
                }
            }

            switch (stage)
            {
                case 1:
                    player.SetExp(player.GetExp() + 20000);
                    break;

                case 2:
                    player.SetExp(player.GetExp() + 100000);
                    break;

                case 3:
                    player.SetExp(player.GetExp() + 500000);
                    break;

                case 10:
                    player.SetExp(player.GetExp() + 1000000);
                    break;

                case 11:
                    player.SetExp(player.GetExp() + 1000000);
                    break;

                case 12:
                    player.SetExp(player.GetExp() + 1000000);
                    break;

                case 13:
                    player.SetExp(player.GetExp() + 1000000);
                    break;

                case 14:
                    player.SetExp(player.GetExp() + 1000000);
                    break;

                case 15:
                    player.SetExp(player.GetExp() + 1000000);
                    break;
            }

            Console.WriteLine("Boss has been slain");
            Console.WriteLine("Potions will now heal 10 more health.");
            potionHealAmount = potionHealAmount + 10;
            if (numberOfStagesAvailable == stage)
            {
                numberOfStagesAvailable++;
            }
            RestingArea();
        }

        /// <summary>
        /// Resting Area Menu
        /// </summary>
        public static void RestingArea()
        {
            Console.WriteLine("\nSitting at a resting spot");
            Console.WriteLine("Options:");
            Console.WriteLine("Go back to fighting (f)");
            Console.WriteLine("Level up (l)");
            Console.WriteLine("Attune magic (a)");
            Console.WriteLine("Merchant (m)");
            Console.WriteLine("Blacksmith (b)");
            player.SetHealth(1);
            player.SetHeals(maxHeals);
            player.ReplenishSpellCasts();
            input = Console.ReadLine();
            try
            {
                switch (input)
                {
                    case "f":

                        // TODO Change to case statement for different levels later
                        if (numberOfStagesAvailable > 1)
                        {
                            switch (numberOfStagesAvailable)
                            {
                                case 2:
                                    Console.WriteLine("Type in which number stage to go to.");
                                    Console.WriteLine("1. Forest");
                                    Console.WriteLine("2. Wasteland");
                                    Console.WriteLine("3. Special Stages");
                                    input = Console.ReadLine();
                                    switch (input)
                                    {
                                        case "1":
                                            Forest(0);
                                            break;

                                        case "2":
                                            Wasteland(0);
                                            break;

                                        case "3":
                                            if (specialStagesPlayerList.Count != 0)
                                            {
                                                Console.WriteLine("Type in the number of the stage to go to.");
                                                for (int i = 0; i < specialStagesPlayerList.Count; i++)
                                                {
                                                    Console.WriteLine("{0}: {1}", i + 1, specialStagesPlayerList[i]);
                                                }
                                                input = Console.ReadLine();
                                                switch (input)
                                                {
                                                    case "1":
                                                        switch ((string)specialStagesPlayerList[0])
                                                        {
                                                            case "Longsword Battlegrounds":
                                                                BossFight(10);
                                                                break;

                                                            case "Dagger Battlegrounds":
                                                                BossFight(11);
                                                                break;

                                                            case "Twindaggers Battlegrounds":
                                                                BossFight(12);
                                                                break;

                                                            case "Shortsword Battlegrounds":
                                                                BossFight(13);
                                                                break;

                                                            case "Staff Battlegrounds":
                                                                BossFight(14);
                                                                break;

                                                            case "Bow Battlegrounds":
                                                                BossFight(15);
                                                                break;
                                                        }
                                                        break;

                                                    case "2":
                                                        switch ((string)specialStagesPlayerList[1])
                                                        {
                                                            case "Longsword Battlegrounds":
                                                                BossFight(10);
                                                                break;

                                                            case "Dagger Battlegrounds":
                                                                BossFight(11);
                                                                break;

                                                            case "Twindaggers Battlegrounds":
                                                                BossFight(12);
                                                                break;

                                                            case "Shortsword Battlegrounds":
                                                                BossFight(13);
                                                                break;

                                                            case "Staff Battlegrounds":
                                                                BossFight(14);
                                                                break;

                                                            case "Bow Battlegrounds":
                                                                BossFight(15);
                                                                break;
                                                        }
                                                        break;

                                                    case "3":
                                                        switch ((string)specialStagesPlayerList[2])
                                                        {
                                                            case "Longsword Battlegrounds":
                                                                BossFight(10);
                                                                break;

                                                            case "Dagger Battlegrounds":
                                                                BossFight(11);
                                                                break;

                                                            case "Twindaggers Battlegrounds":
                                                                BossFight(12);
                                                                break;

                                                            case "Shortsword Battlegrounds":
                                                                BossFight(13);
                                                                break;

                                                            case "Staff Battlegrounds":
                                                                BossFight(14);
                                                                break;

                                                            case "Bow Battlegrounds":
                                                                BossFight(15);
                                                                break;
                                                        }
                                                        break;

                                                    case "4":
                                                        switch ((string)specialStagesPlayerList[3])
                                                        {
                                                            case "Longsword Battlegrounds":
                                                                BossFight(10);
                                                                break;

                                                            case "Dagger Battlegrounds":
                                                                BossFight(11);
                                                                break;

                                                            case "Twindaggers Battlegrounds":
                                                                BossFight(12);
                                                                break;

                                                            case "Shortsword Battlegrounds":
                                                                BossFight(13);
                                                                break;

                                                            case "Staff Battlegrounds":
                                                                BossFight(14);
                                                                break;

                                                            case "Bow Battlegrounds":
                                                                BossFight(15);
                                                                break;
                                                        }
                                                        break;

                                                    case "5":
                                                        switch ((string)specialStagesPlayerList[4])
                                                        {
                                                            case "Longsword Battlegrounds":
                                                                BossFight(10);
                                                                break;

                                                            case "Dagger Battlegrounds":
                                                                BossFight(11);
                                                                break;

                                                            case "Twindaggers Battlegrounds":
                                                                BossFight(12);
                                                                break;

                                                            case "Shortsword Battlegrounds":
                                                                BossFight(13);
                                                                break;

                                                            case "Staff Battlegrounds":
                                                                BossFight(14);
                                                                break;

                                                            case "Bow Battlegrounds":
                                                                BossFight(15);
                                                                break;
                                                        }
                                                        break;

                                                    case "6":
                                                        switch ((string)specialStagesPlayerList[5])
                                                        {
                                                            case "Longsword Battlegrounds":
                                                                BossFight(10);
                                                                break;

                                                            case "Dagger Battlegrounds":
                                                                BossFight(11);
                                                                break;

                                                            case "Twindaggers Battlegrounds":
                                                                BossFight(12);
                                                                break;

                                                            case "Shortsword Battlegrounds":
                                                                BossFight(13);
                                                                break;

                                                            case "Staff Battlegrounds":
                                                                BossFight(14);
                                                                break;

                                                            case "Bow Battlegrounds":
                                                                BossFight(15);
                                                                break;
                                                        }
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("No special stages available. Defeat bosses for a chance to get a a special stage ticket.");
                                            }
                                            RestingArea();
                                            break;

                                        default:
                                            Console.WriteLine("Incorrect input");
                                            RestingArea();
                                            break;
                                    }
                                    break;

                                case 3:
                                    Console.WriteLine("1. Forest");
                                    Console.WriteLine("2. Wasteland");
                                    Console.WriteLine("3. Graveyard");
                                    Console.WriteLine("4. Special Stages");
                                    input = Console.ReadLine();
                                    switch (input)
                                    {
                                        case "1":
                                            Forest(0);
                                            break;

                                        case "2":
                                            Wasteland(0);
                                            break;

                                        case "3":
                                            Graveyard(0);
                                            break;

                                        case "4":
                                            if (specialStagesPlayerList.Count != 0)
                                            {
                                                Console.WriteLine("Type in the number of the stage to go to.");
                                                for (int i = 0; i < specialStagesPlayerList.Count; i++)
                                                {
                                                    Console.WriteLine("{0}: {1}", i + 1, specialStagesPlayerList[i]);
                                                }
                                                input = Console.ReadLine();
                                                switch (input)
                                                {
                                                    case "1":
                                                        switch ((string)specialStagesPlayerList[0])
                                                        {
                                                            case "Longsword Battlegrounds":
                                                                BossFight(10);
                                                                break;

                                                            case "Dagger Battlegrounds":
                                                                BossFight(11);
                                                                break;

                                                            case "Twindaggers Battlegrounds":
                                                                BossFight(12);
                                                                break;

                                                            case "Shortsword Battlegrounds":
                                                                BossFight(13);
                                                                break;

                                                            case "Staff Battlegrounds":
                                                                BossFight(14);
                                                                break;

                                                            case "Bow Battlegrounds":
                                                                BossFight(15);
                                                                break;
                                                        }
                                                        break;

                                                    case "2":
                                                        switch ((string)specialStagesPlayerList[1])
                                                        {
                                                            case "Longsword Battlegrounds":
                                                                BossFight(10);
                                                                break;

                                                            case "Dagger Battlegrounds":
                                                                BossFight(11);
                                                                break;

                                                            case "Twindaggers Battlegrounds":
                                                                BossFight(12);
                                                                break;

                                                            case "Shortsword Battlegrounds":
                                                                BossFight(13);
                                                                break;

                                                            case "Staff Battlegrounds":
                                                                BossFight(14);
                                                                break;

                                                            case "Bow Battlegrounds":
                                                                BossFight(15);
                                                                break;
                                                        }
                                                        break;

                                                    case "3":
                                                        switch ((string)specialStagesPlayerList[2])
                                                        {
                                                            case "Longsword Battlegrounds":
                                                                BossFight(10);
                                                                break;

                                                            case "Dagger Battlegrounds":
                                                                BossFight(11);
                                                                break;

                                                            case "Twindaggers Battlegrounds":
                                                                BossFight(12);
                                                                break;

                                                            case "Shortsword Battlegrounds":
                                                                BossFight(13);
                                                                break;

                                                            case "Staff Battlegrounds":
                                                                BossFight(14);
                                                                break;

                                                            case "Bow Battlegrounds":
                                                                BossFight(15);
                                                                break;
                                                        }
                                                        break;

                                                    case "4":
                                                        switch ((string)specialStagesPlayerList[3])
                                                        {
                                                            case "Longsword Battlegrounds":
                                                                BossFight(10);
                                                                break;

                                                            case "Dagger Battlegrounds":
                                                                BossFight(11);
                                                                break;

                                                            case "Twindaggers Battlegrounds":
                                                                BossFight(12);
                                                                break;

                                                            case "Shortsword Battlegrounds":
                                                                BossFight(13);
                                                                break;

                                                            case "Staff Battlegrounds":
                                                                BossFight(14);
                                                                break;

                                                            case "Bow Battlegrounds":
                                                                BossFight(15);
                                                                break;
                                                        }
                                                        break;

                                                    case "5":
                                                        switch ((string)specialStagesPlayerList[4])
                                                        {
                                                            case "Longsword Battlegrounds":
                                                                BossFight(10);
                                                                break;

                                                            case "Dagger Battlegrounds":
                                                                BossFight(11);
                                                                break;

                                                            case "Twindaggers Battlegrounds":
                                                                BossFight(12);
                                                                break;

                                                            case "Shortsword Battlegrounds":
                                                                BossFight(13);
                                                                break;

                                                            case "Staff Battlegrounds":
                                                                BossFight(14);
                                                                break;

                                                            case "Bow Battlegrounds":
                                                                BossFight(15);
                                                                break;
                                                        }
                                                        break;

                                                    case "6":
                                                        switch ((string)specialStagesPlayerList[5])
                                                        {
                                                            case "Longsword Battlegrounds":
                                                                BossFight(10);
                                                                break;

                                                            case "Dagger Battlegrounds":
                                                                BossFight(11);
                                                                break;

                                                            case "Twindaggers Battlegrounds":
                                                                BossFight(12);
                                                                break;

                                                            case "Shortsword Battlegrounds":
                                                                BossFight(13);
                                                                break;

                                                            case "Staff Battlegrounds":
                                                                BossFight(14);
                                                                break;

                                                            case "Bow Battlegrounds":
                                                                BossFight(15);
                                                                break;
                                                        }
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("No special stages available. Defeat bosses for a chance to get a a special stage ticket.");
                                            }
                                            RestingArea();
                                            break;

                                        default:
                                            Console.WriteLine("Incorrect input");
                                            RestingArea();
                                            break;
                                    }
                                    break;

                                case 4:
                                    Console.WriteLine("1. Forest");
                                    Console.WriteLine("2. Wasteland");
                                    Console.WriteLine("3. Graveyard");
                                    Console.WriteLine("4. Depths of Hell");
                                    Console.WriteLine("5. Special Stages");
                                    input = Console.ReadLine();
                                    switch (input)
                                    {
                                        case "1":
                                            Forest(0);
                                            break;

                                        case "2":
                                            Wasteland(0);
                                            break;

                                        case "3":
                                            Graveyard(0);
                                            break;

                                        case "4":
                                            DepthsOfHell(0);
                                            break;

                                        case "5":
                                            if (specialStagesPlayerList.Count != 0)
                                            {
                                                Console.WriteLine("Type in the number of the stage to go to.");
                                                for (int i = 0; i < specialStagesPlayerList.Count; i++)
                                                {
                                                    Console.WriteLine("{0}: {1}", i + 1, specialStagesPlayerList[i]);
                                                }
                                                input = Console.ReadLine();
                                                switch (input)
                                                {
                                                    case "1":
                                                        switch ((string)specialStagesPlayerList[0])
                                                        {
                                                            case "Longsword Battlegrounds":
                                                                BossFight(10);
                                                                break;

                                                            case "Dagger Battlegrounds":
                                                                BossFight(11);
                                                                break;

                                                            case "Twindaggers Battlegrounds":
                                                                BossFight(12);
                                                                break;

                                                            case "Shortsword Battlegrounds":
                                                                BossFight(13);
                                                                break;

                                                            case "Staff Battlegrounds":
                                                                BossFight(14);
                                                                break;

                                                            case "Bow Battlegrounds":
                                                                BossFight(15);
                                                                break;
                                                        }
                                                        break;

                                                    case "2":
                                                        switch ((string)specialStagesPlayerList[1])
                                                        {
                                                            case "Longsword Battlegrounds":
                                                                BossFight(10);
                                                                break;

                                                            case "Dagger Battlegrounds":
                                                                BossFight(11);
                                                                break;

                                                            case "Twindaggers Battlegrounds":
                                                                BossFight(12);
                                                                break;

                                                            case "Shortsword Battlegrounds":
                                                                BossFight(13);
                                                                break;

                                                            case "Staff Battlegrounds":
                                                                BossFight(14);
                                                                break;

                                                            case "Bow Battlegrounds":
                                                                BossFight(15);
                                                                break;
                                                        }
                                                        break;

                                                    case "3":
                                                        switch ((string)specialStagesPlayerList[2])
                                                        {
                                                            case "Longsword Battlegrounds":
                                                                BossFight(10);
                                                                break;

                                                            case "Dagger Battlegrounds":
                                                                BossFight(11);
                                                                break;

                                                            case "Twindaggers Battlegrounds":
                                                                BossFight(12);
                                                                break;

                                                            case "Shortsword Battlegrounds":
                                                                BossFight(13);
                                                                break;

                                                            case "Staff Battlegrounds":
                                                                BossFight(14);
                                                                break;

                                                            case "Bow Battlegrounds":
                                                                BossFight(15);
                                                                break;
                                                        }
                                                        break;

                                                    case "4":
                                                        switch ((string)specialStagesPlayerList[3])
                                                        {
                                                            case "Longsword Battlegrounds":
                                                                BossFight(10);
                                                                break;

                                                            case "Dagger Battlegrounds":
                                                                BossFight(11);
                                                                break;

                                                            case "Twindaggers Battlegrounds":
                                                                BossFight(12);
                                                                break;

                                                            case "Shortsword Battlegrounds":
                                                                BossFight(13);
                                                                break;

                                                            case "Staff Battlegrounds":
                                                                BossFight(14);
                                                                break;

                                                            case "Bow Battlegrounds":
                                                                BossFight(15);
                                                                break;
                                                        }
                                                        break;

                                                    case "5":
                                                        switch ((string)specialStagesPlayerList[4])
                                                        {
                                                            case "Longsword Battlegrounds":
                                                                BossFight(10);
                                                                break;

                                                            case "Dagger Battlegrounds":
                                                                BossFight(11);
                                                                break;

                                                            case "Twindaggers Battlegrounds":
                                                                BossFight(12);
                                                                break;

                                                            case "Shortsword Battlegrounds":
                                                                BossFight(13);
                                                                break;

                                                            case "Staff Battlegrounds":
                                                                BossFight(14);
                                                                break;

                                                            case "Bow Battlegrounds":
                                                                BossFight(15);
                                                                break;
                                                        }
                                                        break;

                                                    case "6":
                                                        switch ((string)specialStagesPlayerList[5])
                                                        {
                                                            case "Longsword Battlegrounds":
                                                                BossFight(10);
                                                                break;

                                                            case "Dagger Battlegrounds":
                                                                BossFight(11);
                                                                break;

                                                            case "Twindaggers Battlegrounds":
                                                                BossFight(12);
                                                                break;

                                                            case "Shortsword Battlegrounds":
                                                                BossFight(13);
                                                                break;

                                                            case "Staff Battlegrounds":
                                                                BossFight(14);
                                                                break;

                                                            case "Bow Battlegrounds":
                                                                BossFight(15);
                                                                break;
                                                        }
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("No special stages available. Defeat bosses for a chance to get a a special stage ticket.");
                                            }
                                            RestingArea();
                                            break;


                                        default:
                                            Console.WriteLine("Incorrect input");
                                            RestingArea();
                                            break;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Forest(0);
                        }
                        break;
                    case "l":
                        LevelUp();
                        break;
                    case "a":
                        AttuneMagic();
                        break;
                    case "m":
                        RestingAreaMerchant();
                        break;
                    case "b":
                        BlackSmith();
                        break;
                    default:
                        Console.WriteLine("Incorrect Input");
                        RestingArea();
                        break;
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Incorrect input");
                RestingArea();
            }

        }

        /// <summary>
        /// Resting area merchant menu
        /// </summary>
        private static void RestingAreaMerchant()
        {
            bool found = false;
            bool found2 = false;
            bool found3 = false;
            bool found4 = false;
            Item titaniteShard = new TitaniteShard(1);
            Item largeTitaniteShard = new LargeTitaniteShard(1);
            Item titaniteChunk = new TitaniteChunk(1);
            Item titaniteSlab = new TitaniteSlab(1);

            // Should a heal be available to buy?
            if (healCounter >= 5)
            {
                switch (numberOfStagesAvailable)
                {
                    // Forest available
                    case 1:
                        Console.WriteLine("Merchant Options:");
                        Console.WriteLine("Exp: {0}", player.GetExp());
                        Console.WriteLine("1. Buy 10 arrows (1000)");
                        Console.WriteLine("2. Buy a heal ({0})", potionPrice);
                        Console.WriteLine("3. Buy a titanite shard ({0})", titaniteShardPrice);
                        Console.WriteLine("4. Sell Equipment");
                        Console.WriteLine("5. Leave");
                        input = Console.ReadLine();
                        switch (input)
                        {
                            case "1":
                                if (player.GetExp() >= 1000)
                                {
                                    Console.WriteLine("10 arrows purchased\n");
                                    player.SetExp(player.GetExp() - 1000);
                                    player.IncreaseArrows(10);
                                }
                                else
                                {
                                    Console.Write("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "2":
                                if (player.GetExp() >= potionPrice)
                                {
                                    Console.WriteLine("One more heal is now available\n");
                                    player.SetExp(player.GetExp() - potionPrice);
                                    healCounter -= 5;
                                    potionPrice += 2500;
                                    maxHeals++;
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "3":
                                if (player.GetExp() >= titaniteShardPrice)
                                {
                                    for (int i = 0; i < player.GetItemBag().Count; i++)
                                    {
                                        // Titanite shard searching
                                        if (player.GetItemBag()[i].ToString().Contains("Titanite Shard") && player.GetItemBag()[i].ToString().Length < 20)
                                        {
                                            ((TitaniteShard)player.GetItemBag()[i]).SetQuantity(((TitaniteShard)player.GetItemBag()[i]).GetQuantity() + 1);
                                            found = true;
                                        }
                                    }
                                    if (found == false)
                                    {
                                        player.GetItemBag().Add(titaniteShard);
                                    }
                                    player.SetExp(player.GetExp() - titaniteShardPrice);
                                    Console.WriteLine("You have purchased a titanite shard.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "4":
                                SellEquipment();
                                break;

                            case "5":
                                RestingArea();
                                break;

                            default:
                                Console.WriteLine("Incorrect Input\n");
                                RestingAreaMerchant();
                                break;
                        }
                        break;

                    // Wasteland available
                    case 2:
                        Console.WriteLine("Merchant Options:");
                        Console.WriteLine("Exp: {0}", player.GetExp());
                        Console.WriteLine("1. Buy 10 arrows (1000)");
                        Console.WriteLine("2. Buy a heal ({0})", potionPrice);
                        Console.WriteLine("3. Buy a titanite shard ({0})", titaniteShardPrice);
                        Console.WriteLine("4. Buy a large titanite shard ({0})", largeTitaniteShardPrice);
                        Console.WriteLine("5. Sell Equipment");
                        Console.WriteLine("6. Leave");
                        input = Console.ReadLine();
                        switch (input)
                        {
                            case "1":
                                if (player.GetExp() >= 1000)
                                {
                                    Console.WriteLine("10 arrows purchased\n");
                                    player.SetExp(player.GetExp() - 1000);
                                    player.IncreaseArrows(10);
                                }
                                else
                                {
                                    Console.Write("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "2":
                                if (player.GetExp() >= potionPrice)
                                {
                                    Console.WriteLine("One more heal is now available\n");
                                    player.SetExp(player.GetExp() - potionPrice);
                                    healCounter -= 5;
                                    potionPrice += 2500;
                                    maxHeals++;
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "3":
                                if (player.GetExp() >= titaniteShardPrice)
                                {
                                    for (int i = 0; i < player.GetItemBag().Count; i++)
                                    {
                                        // Titanite shard searching
                                        if (player.GetItemBag()[i].ToString().Contains("Titanite Shard") && player.GetItemBag()[i].ToString().Length < 20)
                                        {
                                            ((TitaniteShard)player.GetItemBag()[i]).SetQuantity(((TitaniteShard)player.GetItemBag()[i]).GetQuantity() + 1);
                                            found = true;
                                        }
                                    }
                                    if (found == false)
                                    {
                                        player.GetItemBag().Add(titaniteShard);
                                    }
                                    player.SetExp(player.GetExp() - titaniteShardPrice);
                                    Console.WriteLine("You have purchased a titanite shard.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "4":
                                if (player.GetExp() >= largeTitaniteShardPrice)
                                {
                                    for (int i = 0; i < player.GetItemBag().Count; i++)
                                    {
                                        // Large titanite shard searching
                                        if (player.GetItemBag()[i].ToString().Contains("Large Titanite Shard"))
                                        {
                                            ((LargeTitaniteShard)player.GetItemBag()[i]).SetQuantity(((LargeTitaniteShard)player.GetItemBag()[i]).GetQuantity() + 1);
                                            found2 = true;
                                        }
                                    }
                                    if (found2 == false)
                                    {
                                        player.GetItemBag().Add(largeTitaniteShard);
                                    }
                                    player.SetExp(player.GetExp() - largeTitaniteShardPrice);
                                    Console.WriteLine("You have purchased a large titanite shard.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "5":
                                SellEquipment();
                                break;

                            case "6":
                                RestingArea();
                                break;

                            default:
                                Console.WriteLine("Incorrect Input\n");
                                RestingAreaMerchant();
                                break;
                        }
                        break;

                    // Graveyard available
                    case 3:
                        Console.WriteLine("Merchant Options:");
                        Console.WriteLine("Exp: {0}", player.GetExp());
                        Console.WriteLine("1. Buy 10 arrows (1000)");
                        Console.WriteLine("2. Buy a heal ({0})", potionPrice);
                        Console.WriteLine("3. Buy a titanite shard ({0})", titaniteShardPrice);
                        Console.WriteLine("4. Buy a large titanite shard ({0})", largeTitaniteShardPrice);
                        Console.WriteLine("5. Buy a titanite chunk ({0})", titaniteChunkPrice);
                        Console.WriteLine("6. Sell Equipment");
                        Console.WriteLine("7. Leave");
                        input = Console.ReadLine();
                        switch (input)
                        {
                            case "1":
                                if (player.GetExp() >= 1000)
                                {
                                    Console.WriteLine("10 arrows purchased\n");
                                    player.SetExp(player.GetExp() - 1000);
                                    player.IncreaseArrows(10);
                                }
                                else
                                {
                                    Console.Write("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "2":
                                if (player.GetExp() >= potionPrice)
                                {
                                    Console.WriteLine("One more heal is now available\n");
                                    player.SetExp(player.GetExp() - potionPrice);
                                    healCounter -= 5;
                                    potionPrice += 2500;
                                    maxHeals++;
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "3":
                                if (player.GetExp() >= titaniteShardPrice)
                                {
                                    for (int i = 0; i < player.GetItemBag().Count; i++)
                                    {
                                        // Titanite shard searching
                                        if (player.GetItemBag()[i].ToString().Contains("Titanite Shard") && player.GetItemBag()[i].ToString().Length < 20)
                                        {
                                            ((TitaniteShard)player.GetItemBag()[i]).SetQuantity(((TitaniteShard)player.GetItemBag()[i]).GetQuantity() + 1);
                                            found = true;
                                        }
                                    }
                                    if (found == false)
                                    {
                                        player.GetItemBag().Add(titaniteShard);
                                    }
                                    player.SetExp(player.GetExp() - titaniteShardPrice);
                                    Console.WriteLine("You have purchased a titanite shard.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "4":
                                if (player.GetExp() >= largeTitaniteShardPrice)
                                {
                                    for (int i = 0; i < player.GetItemBag().Count; i++)
                                    {
                                        // Large titanite shard searching
                                        if (player.GetItemBag()[i].ToString().Contains("Large Titanite Shard"))
                                        {
                                            ((LargeTitaniteShard)player.GetItemBag()[i]).SetQuantity(((LargeTitaniteShard)player.GetItemBag()[i]).GetQuantity() + 1);
                                            found2 = true;
                                        }
                                    }
                                    if (found2 == false)
                                    {
                                        player.GetItemBag().Add(largeTitaniteShard);
                                    }
                                    player.SetExp(player.GetExp() - largeTitaniteShardPrice);
                                    Console.WriteLine("You have purchased a large titanite shard.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "5":
                                if (player.GetExp() >= titaniteChunkPrice)
                                {
                                    for (int i = 0; i < player.GetItemBag().Count; i++)
                                    {
                                        // Titanite chunk searching
                                        if (player.GetItemBag()[i].ToString().Contains("Titanite Chunk"))
                                        {
                                            ((TitaniteChunk)player.GetItemBag()[i]).SetQuantity(((TitaniteChunk)player.GetItemBag()[i]).GetQuantity() + 1);
                                            found3 = true;
                                        }
                                    }
                                    if (found3 == false)
                                    {
                                        player.GetItemBag().Add(titaniteChunk);
                                    }
                                    player.SetExp(player.GetExp() - titaniteChunkPrice);
                                    Console.WriteLine("You have purchased a titanite chunk.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "6":
                                SellEquipment();
                                break;

                            case "7":
                                RestingArea();
                                break;

                            default:
                                Console.WriteLine("Incorrect Input\n");
                                RestingAreaMerchant();
                                break;
                        }
                        break;

                    // Depths of hell available
                    case 4:
                        Console.WriteLine("Merchant Options:");
                        Console.WriteLine("Exp: {0}", player.GetExp());
                        Console.WriteLine("1. Buy 10 arrows (1000)");
                        Console.WriteLine("2. Buy a heal ({0})", potionPrice);
                        Console.WriteLine("3. Buy a titanite shard ({0})", titaniteShardPrice);
                        Console.WriteLine("4. Buy a large titanite shard ({0})", largeTitaniteShardPrice);
                        Console.WriteLine("5. Buy a titanite chunk ({0})", titaniteChunkPrice);
                        Console.WriteLine("6. Buy a titanite slab ({0})", titaniteSlabPrice);
                        Console.WriteLine("7. Sell Equipment");
                        Console.WriteLine("8. Leave");
                        input = Console.ReadLine();
                        switch (input)
                        {
                            case "1":
                                if (player.GetExp() >= 1000)
                                {
                                    Console.WriteLine("10 arrows purchased\n");
                                    player.SetExp(player.GetExp() - 1000);
                                    player.IncreaseArrows(10);
                                }
                                else
                                {
                                    Console.Write("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "2":
                                if (player.GetExp() >= potionPrice)
                                {
                                    Console.WriteLine("One more heal is now available\n");
                                    player.SetExp(player.GetExp() - potionPrice);
                                    healCounter -= 5;
                                    potionPrice += 2500;
                                    maxHeals++;
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "3":
                                if (player.GetExp() >= titaniteShardPrice)
                                {
                                    for (int i = 0; i < player.GetItemBag().Count; i++)
                                    {
                                        // Titanite shard searching
                                        if (player.GetItemBag()[i].ToString().Contains("Titanite Shard") && player.GetItemBag()[i].ToString().Length < 20)
                                        {
                                            ((TitaniteShard)player.GetItemBag()[i]).SetQuantity(((TitaniteShard)player.GetItemBag()[i]).GetQuantity() + 1);
                                            found = true;
                                        }
                                    }
                                    if (found == false)
                                    {
                                        player.GetItemBag().Add(titaniteShard);
                                    }
                                    player.SetExp(player.GetExp() - titaniteShardPrice);
                                    Console.WriteLine("You have purchased a titanite shard.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "4":
                                if (player.GetExp() >= largeTitaniteShardPrice)
                                {
                                    for (int i = 0; i < player.GetItemBag().Count; i++)
                                    {
                                        // Large titanite shard searching
                                        if (player.GetItemBag()[i].ToString().Contains("Large Titanite Shard"))
                                        {
                                            ((LargeTitaniteShard)player.GetItemBag()[i]).SetQuantity(((LargeTitaniteShard)player.GetItemBag()[i]).GetQuantity() + 1);
                                            found2 = true;
                                        }
                                    }
                                    if (found2 == false)
                                    {
                                        player.GetItemBag().Add(largeTitaniteShard);
                                    }
                                    player.SetExp(player.GetExp() - largeTitaniteShardPrice);
                                    Console.WriteLine("You have purchased a large titanite shard.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "5":
                                if (player.GetExp() >= titaniteChunkPrice)
                                {
                                    for (int i = 0; i < player.GetItemBag().Count; i++)
                                    {
                                        // Titanite chunk searching
                                        if (player.GetItemBag()[i].ToString().Contains("Titanite Chunk"))
                                        {
                                            ((TitaniteChunk)player.GetItemBag()[i]).SetQuantity(((TitaniteChunk)player.GetItemBag()[i]).GetQuantity() + 1);
                                            found3 = true;
                                        }
                                    }
                                    if (found3 == false)
                                    {
                                        player.GetItemBag().Add(titaniteChunk);
                                    }
                                    player.SetExp(player.GetExp() - titaniteChunkPrice);
                                    Console.WriteLine("You have purchased a titanite chunk.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "6":
                                if (player.GetExp() >= titaniteSlabPrice)
                                {
                                    for (int i = 0; i < player.GetItemBag().Count; i++)
                                    {
                                        // Titanite slab searching
                                        if (player.GetItemBag()[i].ToString().Contains("Titanite Slab"))
                                        {
                                            ((TitaniteSlab)player.GetItemBag()[i]).SetQuantity(((TitaniteSlab)player.GetItemBag()[i]).GetQuantity() + 1);
                                            found4 = true;
                                        }
                                    }
                                    if (found4 == false)
                                    {
                                        player.GetItemBag().Add(titaniteSlab);
                                    }
                                    player.SetExp(player.GetExp() - titaniteSlabPrice);
                                    Console.WriteLine("You have purchased a titanite slab.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "7":
                                SellEquipment();
                                break;

                            case "8":
                                RestingArea();
                                break;

                            default:
                                Console.WriteLine("Incorrect Input\n");
                                RestingAreaMerchant();
                                break;
                        }
                        break;
                }
            }
            else
            {
                switch (numberOfStagesAvailable)
                {
                    // Forest available
                    case 1:
                        Console.WriteLine("Merchant Options:");
                        Console.WriteLine("Exp: {0}", player.GetExp());
                        Console.WriteLine("1. Buy 10 arrows (1000)");
                        Console.WriteLine("2. Buy a titanite shard ({0})", titaniteShardPrice);
                        Console.WriteLine("3. Sell Equipment");
                        Console.WriteLine("4. Leave");
                        input = Console.ReadLine();
                        switch (input)
                        {
                            case "1":
                                if (player.GetExp() >= 1000)
                                {
                                    Console.WriteLine("10 arrows purchased\n");
                                    player.SetExp(player.GetExp() - 1000);
                                    player.IncreaseArrows(10);
                                }
                                else
                                {
                                    Console.Write("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "2":
                                if (player.GetExp() >= titaniteShardPrice)
                                {
                                    for (int i = 0; i < player.GetItemBag().Count; i++)
                                    {
                                        // Titanite shard searching
                                        if (player.GetItemBag()[i].ToString().Contains("Titanite Shard") && player.GetItemBag()[i].ToString().Length < 20)
                                        {
                                            ((TitaniteShard)player.GetItemBag()[i]).SetQuantity(((TitaniteShard)player.GetItemBag()[i]).GetQuantity() + 1);
                                            found = true;
                                        }
                                    }
                                    if (found == false)
                                    {
                                        player.GetItemBag().Add(titaniteShard);
                                    }
                                    player.SetExp(player.GetExp() - titaniteShardPrice);
                                    Console.WriteLine("You have purchased a titanite shard.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "3":
                                SellEquipment();
                                break;

                            case "4":
                                RestingArea();
                                break;

                            default:
                                Console.WriteLine("Incorrect Input\n");
                                RestingAreaMerchant();
                                break;
                        }
                        break;

                    // Wasteland available
                    case 2:
                        Console.WriteLine("Merchant Options:");
                        Console.WriteLine("Exp: {0}", player.GetExp());
                        Console.WriteLine("1. Buy 10 arrows (1000)");
                        Console.WriteLine("2. Buy a titanite shard ({0})", titaniteShardPrice);
                        Console.WriteLine("3. Buy a large titanite shard ({0})", largeTitaniteShardPrice);
                        Console.WriteLine("4. Sell Equipment");
                        Console.WriteLine("5. Leave");
                        input = Console.ReadLine();
                        switch (input)
                        {
                            case "1":
                                if (player.GetExp() >= 1000)
                                {
                                    Console.WriteLine("10 arrows purchased\n");
                                    player.SetExp(player.GetExp() - 1000);
                                    player.IncreaseArrows(10);
                                }
                                else
                                {
                                    Console.Write("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "2":
                                if (player.GetExp() >= titaniteShardPrice)
                                {
                                    for (int i = 0; i < player.GetItemBag().Count; i++)
                                    {
                                        // Titanite shard searching
                                        if (player.GetItemBag()[i].ToString().Contains("Titanite Shard") && player.GetItemBag()[i].ToString().Length < 20)
                                        {
                                            ((TitaniteShard)player.GetItemBag()[i]).SetQuantity(((TitaniteShard)player.GetItemBag()[i]).GetQuantity() + 1);
                                            found = true;
                                        }
                                    }
                                    if (found == false)
                                    {
                                        player.GetItemBag().Add(titaniteShard);
                                    }
                                    player.SetExp(player.GetExp() - titaniteShardPrice);
                                    Console.WriteLine("You have purchased a titanite shard.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "3":
                                if (player.GetExp() >= largeTitaniteShardPrice)
                                {
                                    for (int i = 0; i < player.GetItemBag().Count; i++)
                                    {
                                        // Large titanite shard searching
                                        if (player.GetItemBag()[i].ToString().Contains("Large Titanite Shard"))
                                        {
                                            ((LargeTitaniteShard)player.GetItemBag()[i]).SetQuantity(((LargeTitaniteShard)player.GetItemBag()[i]).GetQuantity() + 1);
                                            found2 = true;
                                        }
                                    }
                                    if (found2 == false)
                                    {
                                        player.GetItemBag().Add(largeTitaniteShard);
                                    }
                                    player.SetExp(player.GetExp() - largeTitaniteShardPrice);
                                    Console.WriteLine("You have purchased a large titanite shard.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "4":
                                SellEquipment();
                                break;

                            case "5":
                                RestingArea();
                                break;

                            default:
                                Console.WriteLine("Incorrect Input\n");
                                RestingAreaMerchant();
                                break;
                        }
                        break;

                    // Graveyard available
                    case 3:
                        Console.WriteLine("Merchant Options:");
                        Console.WriteLine("Exp: {0}", player.GetExp());
                        Console.WriteLine("1. Buy 10 arrows (1000)");
                        Console.WriteLine("2. Buy a titanite shard ({0})", titaniteShardPrice);
                        Console.WriteLine("3. Buy a large titanite shard ({0})", largeTitaniteShardPrice);
                        Console.WriteLine("4. Buy a titanite chunk ({0})", titaniteChunkPrice);
                        Console.WriteLine("5. Sell Equipment");
                        Console.WriteLine("6. Leave");
                        input = Console.ReadLine();
                        switch (input)
                        {
                            case "1":
                                if (player.GetExp() >= 1000)
                                {
                                    Console.WriteLine("10 arrows purchased\n");
                                    player.SetExp(player.GetExp() - 1000);
                                    player.IncreaseArrows(10);
                                }
                                else
                                {
                                    Console.Write("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "2":
                                if (player.GetExp() >= titaniteShardPrice)
                                {
                                    for (int i = 0; i < player.GetItemBag().Count; i++)
                                    {
                                        // Titanite shard searching
                                        if (player.GetItemBag()[i].ToString().Contains("Titanite Shard") && player.GetItemBag()[i].ToString().Length < 20)
                                        {
                                            ((TitaniteShard)player.GetItemBag()[i]).SetQuantity(((TitaniteShard)player.GetItemBag()[i]).GetQuantity() + 1);
                                            found = true;
                                        }
                                    }
                                    if (found == false)
                                    {
                                        player.GetItemBag().Add(titaniteShard);
                                    }
                                    player.SetExp(player.GetExp() - titaniteShardPrice);
                                    Console.WriteLine("You have purchased a titanite shard.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "3":
                                if (player.GetExp() >= largeTitaniteShardPrice)
                                {
                                    for (int i = 0; i < player.GetItemBag().Count; i++)
                                    {
                                        // Large titanite shard searching
                                        if (player.GetItemBag()[i].ToString().Contains("Large Titanite Shard"))
                                        {
                                            ((LargeTitaniteShard)player.GetItemBag()[i]).SetQuantity(((LargeTitaniteShard)player.GetItemBag()[i]).GetQuantity() + 1);
                                            found2 = true;
                                        }
                                    }
                                    if (found2 == false)
                                    {
                                        player.GetItemBag().Add(largeTitaniteShard);
                                    }
                                    player.SetExp(player.GetExp() - largeTitaniteShardPrice);
                                    Console.WriteLine("You have purchased a large titanite shard.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "4":
                                if (player.GetExp() >= titaniteChunkPrice)
                                {
                                    for (int i = 0; i < player.GetItemBag().Count; i++)
                                    {
                                        // Titanite chunk searching
                                        if (player.GetItemBag()[i].ToString().Contains("Titanite Chunk"))
                                        {
                                            ((TitaniteChunk)player.GetItemBag()[i]).SetQuantity(((TitaniteChunk)player.GetItemBag()[i]).GetQuantity() + 1);
                                            found3 = true;
                                        }
                                    }
                                    if (found3 == false)
                                    {
                                        player.GetItemBag().Add(titaniteChunk);
                                    }
                                    player.SetExp(player.GetExp() - titaniteChunkPrice);
                                    Console.WriteLine("You have purchased a titanite chunk.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "5":
                                SellEquipment();
                                break;

                            case "6":
                                RestingArea();
                                break;

                            default:
                                Console.WriteLine("Incorrect Input\n");
                                RestingAreaMerchant();
                                break;
                        }
                        break;

                    // Depths of hell available
                    case 4:
                        Console.WriteLine("Merchant Options:");
                        Console.WriteLine("Exp: {0}", player.GetExp());
                        Console.WriteLine("1. Buy 10 arrows (1000)");
                        Console.WriteLine("2. Buy a titanite shard ({0})", titaniteShardPrice);
                        Console.WriteLine("3. Buy a large titanite shard ({0})", largeTitaniteShardPrice);
                        Console.WriteLine("4. Buy a titanite chunk ({0})", titaniteChunkPrice);
                        Console.WriteLine("5. Buy a titanite slab ({0})", titaniteSlabPrice);
                        Console.WriteLine("6. Sell Equipment");
                        Console.WriteLine("7. Leave");
                        input = Console.ReadLine();
                        switch (input)
                        {
                            case "1":
                                if (player.GetExp() >= 1000)
                                {
                                    Console.WriteLine("10 arrows purchased\n");
                                    player.SetExp(player.GetExp() - 1000);
                                    player.IncreaseArrows(10);
                                }
                                else
                                {
                                    Console.Write("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "2":
                                if (player.GetExp() >= titaniteShardPrice)
                                {
                                    for (int i = 0; i < player.GetItemBag().Count; i++)
                                    {
                                        // Titanite shard searching
                                        if (player.GetItemBag()[i].ToString().Contains("Titanite Shard") && player.GetItemBag()[i].ToString().Length < 20)
                                        {
                                            ((TitaniteShard)player.GetItemBag()[i]).SetQuantity(((TitaniteShard)player.GetItemBag()[i]).GetQuantity() + 1);
                                            found = true;
                                        }
                                    }
                                    if (found == false)
                                    {
                                        player.GetItemBag().Add(titaniteShard);
                                    }
                                    player.SetExp(player.GetExp() - titaniteShardPrice);
                                    Console.WriteLine("You have purchased a titanite shard.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "3":
                                if (player.GetExp() >= largeTitaniteShardPrice)
                                {
                                    for (int i = 0; i < player.GetItemBag().Count; i++)
                                    {
                                        // Large titanite shard searching
                                        if (player.GetItemBag()[i].ToString().Contains("Large Titanite Shard"))
                                        {
                                            ((LargeTitaniteShard)player.GetItemBag()[i]).SetQuantity(((LargeTitaniteShard)player.GetItemBag()[i]).GetQuantity() + 1);
                                            found2 = true;
                                        }
                                    }
                                    if (found2 == false)
                                    {
                                        player.GetItemBag().Add(largeTitaniteShard);
                                    }
                                    player.SetExp(player.GetExp() - largeTitaniteShardPrice);
                                    Console.WriteLine("You have purchased a large titanite shard.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "4":
                                if (player.GetExp() >= titaniteChunkPrice)
                                {
                                    for (int i = 0; i < player.GetItemBag().Count; i++)
                                    {
                                        // Titanite chunk searching
                                        if (player.GetItemBag()[i].ToString().Contains("Titanite Chunk"))
                                        {
                                            ((TitaniteChunk)player.GetItemBag()[i]).SetQuantity(((TitaniteChunk)player.GetItemBag()[i]).GetQuantity() + 1);
                                            found3 = true;
                                        }
                                    }
                                    if (found3 == false)
                                    {
                                        player.GetItemBag().Add(titaniteChunk);
                                    }
                                    player.SetExp(player.GetExp() - titaniteChunkPrice);
                                    Console.WriteLine("You have purchased a titanite chunk.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "5":
                                if (player.GetExp() >= titaniteSlabPrice)
                                {
                                    for (int i = 0; i < player.GetItemBag().Count; i++)
                                    {
                                        // Titanite slab searching
                                        if (player.GetItemBag()[i].ToString().Contains("Titanite Slab"))
                                        {
                                            ((TitaniteSlab)player.GetItemBag()[i]).SetQuantity(((TitaniteSlab)player.GetItemBag()[i]).GetQuantity() + 1);
                                            found4 = true;
                                        }
                                    }
                                    if (found4 == false)
                                    {
                                        player.GetItemBag().Add(titaniteSlab);
                                    }
                                    player.SetExp(player.GetExp() - titaniteSlabPrice);
                                    Console.WriteLine("You have purchased a titanite slab.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Not enough exp\n");
                                }
                                RestingAreaMerchant();
                                break;

                            case "6":
                                SellEquipment();
                                break;

                            case "7":
                                RestingArea();
                                break;

                            default:
                                Console.WriteLine("Incorrect Input\n");
                                RestingAreaMerchant();
                                break;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Leveling up menu
        /// </summary>
        public static void LevelUp()
        {
            Console.WriteLine("\nExp required to level: " + expRequired);
            Console.WriteLine("Exp: " + player.GetExp());
            Console.WriteLine("Stats to level up:");
            Console.WriteLine("str");
            Console.WriteLine("dex");
            Console.WriteLine("mag");
            Console.WriteLine("vit");
            Console.WriteLine("end");
            Console.WriteLine("atn");
            Console.WriteLine("stats");
            Console.WriteLine("help");
            Console.WriteLine("Go back to resting menu (r)");
            input = Console.ReadLine();
            if (input == "r")
            {
                RestingArea();
            }
            else if (input == "stats")
            {
                Console.WriteLine("health: " + player.GetHealth() + " str: " + player.GetStr() + " dex: " + player.GetDex() + " mag: " + player.GetMag() +
                        " vit: " + player.GetVit() + " end: " + player.GetEnd() + " atn: " + player.GetAttunement());
            }
            else if (input == "help")
            {
                Console.WriteLine("str is used for mostly shortswords and longswords.");
                Console.WriteLine("dex is used for mostly daggers, twindaggers and bows.");
                Console.WriteLine("mag is used for staffs and spells.");
                Console.WriteLine("vit is used to increase maximum health.");
                Console.WriteLine("end is used to increase maximum weight load and bag space.");
                Console.WriteLine("atn is used to increase the amount of attunement slots you can use.");
            }
            else
            {
                if (player.GetExp() >= expRequired)
                {
                    switch (input)
                    {
                        case "str":
                            Console.WriteLine("You are putting a point into str. Are you sure (yes/no)");
                            input = Console.ReadLine();
                            if (input == "yes")
                            {
                                player.IncreaseStr();
                                player.SetLevel(player.GetLevel() + 1);
                                Console.WriteLine("str has increased");
                                healCounter++;
                            }
                            else
                            {
                                LevelUp();
                            }
                            break;
                        case "dex":
                            Console.WriteLine("You are putting a point into dex. Are you sure (yes/no)");
                            input = Console.ReadLine();
                            if (input == "yes")
                            {
                                player.IncreaseDex();
                                player.SetLevel(player.GetLevel() + 1);
                                Console.WriteLine("dex has increased");
                                healCounter++;
                            }
                            else
                            {
                                LevelUp();
                            }
                            break;
                        case "mag":
                            Console.WriteLine("You are putting a point into mag. Are you sure (yes/no)");
                            input = Console.ReadLine();
                            if (input == "yes")
                            {
                                player.IncreaseMag();
                                player.SetLevel(player.GetLevel() + 1);
                                Console.WriteLine("mag has increased");
                                healCounter++;
                            }
                            else
                            {
                                LevelUp();
                            }
                            break;
                        case "vit":
                            Console.WriteLine("You are putting a point into vit. Are you sure (yes/no)");
                            input = Console.ReadLine();
                            if (input == "yes")
                            {
                                player.IncreaseVit();
                                player.SetLevel(player.GetLevel() + 1);
                                player.SetHealth(0);
                                Console.WriteLine("vit has increased");
                                healCounter++;
                            }
                            else
                            {
                                LevelUp();
                            }
                            break;
                        case "end":
                            Console.WriteLine("You are putting a point into end. Are you sure (yes/no)");
                            input = Console.ReadLine();
                            if (input == "yes")
                            {
                                player.IncreaseEnd();
                                player.SetLevel(player.GetLevel() + 1);
                                Console.WriteLine("end has increased");
                                healCounter++;
                            }
                            else
                            {
                                LevelUp();
                            }
                            break;

                        case "atn":
                            Console.WriteLine("You are putting a point into atn. Are you sure (yes/no)");
                            input = Console.ReadLine();
                            if (input == "yes")
                            {
                                player.IncreaseAttunement();
                                player.SetLevel(player.GetLevel() + 1);
                                player.IncreaseAttunementSlots(2);
                                Console.WriteLine("atn has increased");
                                healCounter++;
                            }
                            else
                            {
                                LevelUp();
                            }
                            break;

                        default:
                            Console.WriteLine("Incorrect input");
                            LevelUp();
                            break;
                    }
                    player.SetExp(player.GetExp() - expRequired);
                    expRequired += expIncreasePerLevel;
                }
                else
                {
                    if (input != "str" & input != "dex" & input != "mag" & input != "vit" &
                            input != "end" & input != "atn")
                    {
                        Console.WriteLine("Incorrect Input");
                        LevelUp();
                    }
                    else
                    {
                        Console.WriteLine("Not enough exp to level up");
                    }
                }
            }
            LevelUp();
        }

        // Vendor menu that appears after floor 10
        static void VendorMenu(int level, int numOfKills)
        {
            int rarity = 0;
            int vendorType = 0;
            int itemPrice = 0;
            int index = 0;
            Random rnd = new Random();
            Vendor vendor;

            switch (level)
            {
                case 1:
                    rarity = rnd.Next(0, 101);
                    break;

                case 2:
                    rarity = rnd.Next(101, 201);
                    break;

                case 3:
                    rarity = rnd.Next(201, 301);
                    break;

                case 4:
                    rarity = rnd.Next(301, 401);
                    break;

            }
            vendorType = rnd.Next(0, 4);
            if (vendorType == 0)
            {
                vendor = new ArmorSeller(rarity, 5);
            }
            else if (vendorType == 1)
            {
                vendor = new MeleeWeaponSeller(rarity, 5);
            }
            else if (vendorType == 2)
            {
                vendor = new RangedMageSeller(rarity, 5);
            }
            else
            {
                vendor = new SpellsSeller(rarity, 5);
            }
            Console.WriteLine("A random vendor has appeared");

        vendor:
            Console.WriteLine("Bag Space: {0}/{1}", player.GetEquipmentBag().Count, player.GetBagSpace());
            Console.WriteLine("Exp: " + player.GetExp());
            ArrayList vendorItems = vendor.getItems();
            if (rarity >= 0 && rarity <= 50)
            {
                itemPrice = 1000;
            }
            else if (rarity >= 51 && rarity <= 80)
            {
                itemPrice = 2000;
            }
            else if (rarity >= 81 && rarity <= 95)
            {
                itemPrice = 4000;
            }
            else if (rarity >= 96 && rarity <= 100)
            {
                itemPrice = 8000;
            }
            else if (rarity >= 101 && rarity <= 150)
            {
                itemPrice = 10000;
            }
            else if (rarity >= 151 && rarity <= 180)
            {
                itemPrice = 20000;
            }
            else if (rarity >= 181 && rarity <= 195)
            {
                itemPrice = 40000;
            }
            else if (rarity >= 196 && rarity <= 200)
            {
                itemPrice = 80000;
            }
            else if (rarity >= 201 && rarity <= 250)
            {
                itemPrice = 100000;
            }
            else if (rarity >= 251 && rarity <= 280)
            {
                itemPrice = 150000;
            }
            else if (rarity >= 281 && rarity <= 295)
            {
                itemPrice = 200000;
            }
            else if (rarity >= 296 && rarity <= 300)
            {
                itemPrice = 400000;
            }


            for (int i = 0; i < vendorItems.Count; i++)
            {
                Console.WriteLine(i + ": " + vendorItems[i] + " Exp Required: " + itemPrice);
            }
            Console.WriteLine("Select the number to buy the item (Type in no to exit)");
            input = Console.ReadLine();
            if (int.TryParse(input, out index) == true)
            {
                if (index >= 0 && index < vendorItems.Count)
                {
                    if (player.GetExp() >= itemPrice)
                    {
                        Console.WriteLine("Confirming purchase of " + vendorItems[index] + " for " + itemPrice + "?(yes/no)");
                        input = Console.ReadLine();
                        if (input == "yes")
                        {
                            if (player.GetEquipmentBag().Count < player.GetBagSpace())
                            {
                                player.GetEquipmentBag().Add(vendorItems[index]);
                                vendorItems.RemoveAt(index);
                                player.SetExp(player.GetExp() - itemPrice);
                            }
                            else
                            {
                                Console.WriteLine("You are out of bag space.");
                            }
                        }
                        goto vendor;

                    }
                    else
                    {
                        switch (level)
                        {
                            case 1:
                                Forest(numOfKills);
                                break;

                            case 2:
                                Wasteland(numOfKills);
                                break;

                            case 3:
                                Graveyard(numOfKills);
                                break;

                            case 4:
                                DepthsOfHell(numOfKills);
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("index entered not in range");
                    goto vendor;
                }
            }
            else if (input != "no")
            {
                goto vendor;
            }
        }

        /// <summary>
        /// Menu for selling equipment
        /// </summary>
        public static void SellEquipment()
        {
            string input;
            int index, damg, sellPrice = 0;
            double def = 0;
            for (int i = 0; i < player.GetEquipmentBag().Count; i++)
            {
                Console.WriteLine("(" + i + ") " + player.GetEquipmentBag()[i]);
            }
            if (player.GetEquipmentBag().Count != 0)
            {
                Console.WriteLine("Type in the number of the piece you want to sell or type in no to exit)");
                Console.WriteLine("Type sell all to sell all items in your bag");
                input = Console.ReadLine();
                if (input == "sell all")
                {
                    for (int i = 0; i < player.GetEquipmentBag().Count; i++)
                    {
                        if (player.GetEquipmentBag()[i] is MeleeWeapon)
                        {
                            MeleeWeapon mWeapon = (MeleeWeapon)player.GetEquipmentBag()[i];
                            damg = mWeapon.GetDamg();
                            sellPrice = (int)((damg / 5) * 100);
                        }
                        else if (player.GetEquipmentBag()[i] is Armor)
                        {
                            Armor armor = (Armor)player.GetEquipmentBag()[i];
                            def = armor.GetDef();
                            sellPrice = (int)((def / 5) * 100);
                        }
                        else if (player.GetEquipmentBag()[i] is MageWeapon)
                        {
                            MageWeapon mageWeapon = (MageWeapon)player.GetEquipmentBag()[i];
                            damg = mageWeapon.GetDamg();
                            sellPrice = (int)((damg / 5) * 100);
                        }
                        else if (player.GetEquipmentBag()[i] is RangedWeapon)
                        {
                            RangedWeapon rangedWeapon = (RangedWeapon)player.GetEquipmentBag()[i];
                            damg = rangedWeapon.GetDamg();
                            sellPrice = (int)((damg / 5) * 100);
                        }

                        player.SetExp(player.GetExp() + sellPrice);
                    }

                    for (int i = 0; i < player.GetEquipmentBag().Count; i++)
                    {
                        player.GetEquipmentBag().RemoveRange(0, player.GetEquipmentBag().Count);
                    }
                    RestingArea();
                }
                else
                {
                    if (int.TryParse(input, out index) == true)
                    {
                        if (index >= 0 && index < player.GetEquipmentBag().Count)
                        {
                            if (player.GetEquipmentBag()[index] is MeleeWeapon)
                            {
                                MeleeWeapon mWeapon = (MeleeWeapon)player.GetEquipmentBag()[index];
                                damg = mWeapon.GetDamg();
                                sellPrice = (damg / 5) * 100;
                                Console.WriteLine("Confirming sale of " +
                                    ((MeleeWeapon)player.GetEquipmentBag()[index]).ToString() + " for " + sellPrice + "?(yes/no)");
                                input = Console.ReadLine();
                                if (input == "yes")
                                {
                                    player.GetEquipmentBag().RemoveAt(index);
                                    player.SetExp(player.GetExp() + sellPrice);
                                }
                            }
                            else if (player.GetEquipmentBag()[index] is Armor)
                            {
                                Armor armor = (Armor)player.GetEquipmentBag()[index];
                                def = armor.GetDef();
                                sellPrice = (int)((def / 5) * 100);
                                Console.WriteLine("Confirming sale of " +
                                    ((Armor)player.GetEquipmentBag()[index]).ToString() + " for " + sellPrice + "?(yes/no)");
                                input = Console.ReadLine();
                                if (input == "yes")
                                {
                                    player.GetEquipmentBag().RemoveAt(index);
                                    player.SetExp(player.GetExp() + sellPrice);
                                }
                            }
                            else if (player.GetEquipmentBag()[index] is MageWeapon)
                            {
                                MageWeapon mageWeapon = (MageWeapon)player.GetEquipmentBag()[index];
                                damg = mageWeapon.GetDamg();
                                sellPrice = (damg / 5) * 100;
                                Console.WriteLine("Confirming sale of " +
                                    ((MageWeapon)player.GetEquipmentBag()[index]).ToString() + " for " + sellPrice + "?(yes/no)");
                                input = Console.ReadLine();
                                if (input == "yes")
                                {
                                    player.GetEquipmentBag().RemoveAt(index);
                                    player.SetExp(player.GetExp() + sellPrice);
                                }
                            }
                            else if (player.GetEquipmentBag()[index] is RangedWeapon)
                            {
                                RangedWeapon rangedWeapon = (RangedWeapon)player.GetEquipmentBag()[index];
                                damg = rangedWeapon.GetDamg();
                                sellPrice = (damg / 5) * 100;
                                Console.WriteLine("Confirming sale of " +
                                    ((RangedWeapon)player.GetEquipmentBag()[index]).ToString() + " for " + sellPrice + "?(yes/no)");
                                input = Console.ReadLine();
                                if (input == "yes")
                                {
                                    player.GetEquipmentBag().RemoveAt(index);
                                    player.SetExp(player.GetExp() + sellPrice);
                                }
                            }
                            SellEquipment();
                        }
                        else
                        {
                            Console.WriteLine("index entered not in range");
                            SellEquipment();
                        }
                    }
                }
                RestingArea();
            }
            else
            {
                RestingArea();
            }

        }

        /// <summary>
        /// Menu for attuning magic
        /// </summary>
        public static void AttuneMagic()
        {
            string input;
            int index;
            Console.WriteLine("\nAttunement slots: " + player.GetAttunementSlots());
            Console.WriteLine("Spells equipped:");
            for (int i = 0; i < player.GetSpellsEquipped().Count; i++)
            {
                Console.WriteLine("(" + i + ") " + ((Spell)player.GetSpellsEquipped()[i]).ToString());
            }
            Console.WriteLine("Spells in inventory:");
            for (int i = 0; i < player.GetSpells().Count; i++)
            {
                Console.WriteLine("(" + i + ") " + ((Spell)player.GetSpells()[i]).ToString());
            }
            Console.WriteLine("\nChoose either to Add or Remove spells (type in exit to go back to resting menu)");
            input = Console.ReadLine();
            if (input == "add")
            {
                if (player.GetSpells().Count > 0)
                {
                    Console.WriteLine("Type in the number of the spell to equip");
                    input = Console.ReadLine();
                    if (int.TryParse(input, out index) == true)
                    {
                        if (index >= 0 && index < player.GetSpells().Count)
                        {
                            if (((Spell)player.GetSpells()[index]).getAttunementSlots() <= player.GetAttunementSlots())
                            {
                                player.AddSpellsToEquipped(((Spell)player.GetSpells()[index]));
                                player.DecreaseAttunementSlots(((Spell)player.GetSpells()[index]).getAttunementSlots());
                                player.RemoveSpell(index);
                            }
                            else
                            {
                                Console.WriteLine("Not enough attunement slots");
                                AttuneMagic();
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("There are no spells to be equipped");
                }
                AttuneMagic();
            }
            else if (input == "remove")
            {
                if (player.GetSpellsEquipped().Count > 0)
                {
                    Console.WriteLine("Type in the number of the spell to unequip");
                    input = Console.ReadLine();
                    if (int.TryParse(input, out index) == true)
                    {
                        if (index >= 0 && index < player.GetSpellsEquipped().Count)
                        {
                            player.AddSpell(((Spell)player.GetSpellsEquipped()[index]));
                            player.IncreaseAttunementSlots(((Spell)player.GetSpellsEquipped()[index]).getAttunementSlots());
                            player.RemoveSpellsFromEquipped(index);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("There are no spells to remove");
                }
                AttuneMagic();
            }
            else if (input == "exit")
            {
                RestingArea();
            }
            else
            {
                Console.WriteLine("Incorrect Input");
                AttuneMagic();
            }

        }

        /// <summary>
        /// Blacksmith menu for upgrading
        /// </summary>
        public static void BlackSmith()
        {
            Console.WriteLine("\nType in what you want to do.");
            Console.WriteLine("1. Upgrade armor/weapon.");
            Console.WriteLine("2. Leave");
            input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    UpgradingEquipment();
                    break;

                case "2":
                    RestingArea();
                    break;

                default:
                    Console.WriteLine("Incorrect input.");
                    break;
            }
        }

        public static void UpgradingEquipment()
        {
            int numTitaniteShards = 0;
            int numLargeTitaniteShards = 0;
            int numTitaniteChunks = 0;
            int numTitaniteSlabs = 0;
            int titaniteShardIndex = 0;
            int largeTitaniteShardIndex = 0;
            int titaniteChunkIndex = 0;
            int titaniteSlabIndex = 0;

            for (int i = 0; i < player.GetItemBag().Count; i++)
            {
                if (player.GetItemBag()[i].ToString().Contains("Titanite Shard") && player.GetItemBag()[i].ToString().Length < 20)
                {
                    numTitaniteShards = ((TitaniteShard)player.GetItemBag()[i]).GetQuantity();
                    titaniteShardIndex = i;
                }
                else if (player.GetItemBag()[i].ToString().Contains("Large Titanite Shard"))
                {
                    numLargeTitaniteShards = ((LargeTitaniteShard)player.GetItemBag()[i]).GetQuantity();
                    largeTitaniteShardIndex = i;
                }
                else if (player.GetItemBag()[i].ToString().Contains("Titanite Chunk"))
                {
                    numTitaniteChunks = ((TitaniteChunk)player.GetItemBag()[i]).GetQuantity();
                    titaniteChunkIndex = i;
                }
                else if (player.GetItemBag()[i].ToString().Contains("Titanite Slab"))
                {
                    numTitaniteSlabs = ((TitaniteSlab)player.GetItemBag()[i]).GetQuantity();
                    titaniteSlabIndex = i;
                }
            }

            Console.WriteLine("\nDo you want to upgrade an item equipped or in your bag?");
            Console.WriteLine("1. Equipped");
            Console.WriteLine("2. Bag");
            Console.WriteLine("3. Exit");
            input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    player.Status();
                    UpgradingFromEquipped(numTitaniteShards, numLargeTitaniteShards, numTitaniteChunks, numTitaniteSlabs,
                                                    titaniteShardIndex, largeTitaniteShardIndex, titaniteChunkIndex, titaniteSlabIndex);
                    // NOT IMPLEMENTED
                    break;

                case "2":
                    if (player.GetEquipmentBag().Count != 0)
                    {
                        UpgradingFromEquipmentBag(numTitaniteShards, numLargeTitaniteShards, numTitaniteChunks, numTitaniteSlabs,
                                                    titaniteShardIndex, largeTitaniteShardIndex, titaniteChunkIndex, titaniteSlabIndex);
                    }
                    else
                    {
                        Console.WriteLine("No Equipment in bag to upgrade.");
                    }
                    UpgradingEquipment();
                    break;

                case "3":
                    BlackSmith();
                    break;

                default:
                    Console.WriteLine("Incorrect input.");
                    break;
            }
        }

        public static void UpgradingFromEquipmentBag(int numTitaniteShards, int numLargeTitaniteShards, int numTitaniteChunks, int numTitaniteSlabs,
                                                        int titaniteShardIndex, int largeTitaniteShardIndex, int titaniteChunkIndex, int titaniteSlabIndex)
        {
            int index = 0;
            Armor upgradedEquipmentArmor;
            MeleeWeapon upgradedEquipmentMelee;
            RangedWeapon upgradedEquipmentRanged;
            MageWeapon upgradedEquipmentMage;
            int materialsNeeded = 0;
            int typeOfMaterial = 0;
            int upgradeLevel = 0;
            string equipmentString = String.Empty;

            Console.WriteLine("\nEquipment in Bag:");
            for (int i = 0; i < player.GetEquipmentBag().Count; i++)
            {
                Console.WriteLine("(" + i + ") " + player.GetEquipmentBag()[i]);
            }
            Console.WriteLine("\nItems in inventory:");
            player.DisplayItemBag();
            Console.WriteLine("\nType in the number to upgrade or exit to go back.");
            input = Console.ReadLine();
            if (int.TryParse(input, out index) == true)
            {
                if (index >= 0 && index < player.GetEquipmentBag().Count)
                {
                    if (player.GetEquipmentBag()[index] is Armor)
                    {
                        upgradeLevel = ((Armor)player.GetEquipmentBag()[index]).GetUpgradeLevel();
                        if (upgradeLevel < 10)
                        {
                            equipmentString = player.GetEquipmentBag()[index].ToString();
                            upgradedEquipmentArmor = (Armor)player.GetEquipmentBag()[index];
                            upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() + 1); // This line does the upgrading already. Revert item if needed.  
                            Console.WriteLine("\nDo you want to upgrade {0} to {1}? (Y/N)", equipmentString, upgradedEquipmentArmor);
                            switch (upgradeLevel)
                            {
                                case 0:
                                    materialsNeeded = 2;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 1:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 2:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 3:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 4:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 5:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 6:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 7:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 8:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 9:
                                    materialsNeeded = 1;
                                    Console.WriteLine("This will cost {0} Titanite Slab. You have {1} Titanite Slab.", materialsNeeded, numTitaniteSlabs);
                                    typeOfMaterial = 3;
                                    break;
                            }
                            input = Console.ReadLine();
                            switch (input)
                            {
                                case "y":
                                    switch (typeOfMaterial)
                                    {
                                        case 0:
                                            if (materialsNeeded <= numTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).SetQuantity(((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 1:
                                            if (materialsNeeded <= numLargeTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).SetQuantity(((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).GetQuantity()
                                                                                                                                                                  - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 2:
                                            if (materialsNeeded <= numTitaniteChunks)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).SetQuantity(((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 3:
                                            if (materialsNeeded <= numTitaniteSlabs)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).SetQuantity(((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;
                                    }
                                    UpgradingEquipment();
                                    break;

                                case "n":
                                    upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                    UpgradingEquipment();
                                    break;

                                default:
                                    Console.WriteLine("Incorrect Input");
                                    upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                    UpgradingFromEquipmentBag(numTitaniteShards, numLargeTitaniteShards, numTitaniteChunks, numTitaniteSlabs,
                                                                titaniteShardIndex, largeTitaniteShardIndex, titaniteChunkIndex, titaniteSlabIndex);
                                    break;

                            }
                        }
                        else
                        {
                            Console.WriteLine("Can not upgrade any further.");
                        }
                    }
                    else if (player.GetEquipmentBag()[index] is MeleeWeapon)
                    {
                        upgradeLevel = ((MeleeWeapon)player.GetEquipmentBag()[index]).GetUpgradeLevel();
                        if (upgradeLevel < 10)
                        {
                            equipmentString = player.GetEquipmentBag()[index].ToString();
                            upgradedEquipmentMelee = (MeleeWeapon)player.GetEquipmentBag()[index];
                            upgradedEquipmentMelee.SetUpgradeLevel(upgradedEquipmentMelee.GetUpgradeLevel() + 1); // This line does the upgrading already. Revert item if needed.  
                            Console.WriteLine("\nDo you want to upgrade {0} to {1}? (Y/N)", equipmentString, upgradedEquipmentMelee);
                            switch (upgradeLevel)
                            {
                                case 0:
                                    materialsNeeded = 2;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 1:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 2:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 3:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 4:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 5:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 6:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 7:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 8:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 9:
                                    materialsNeeded = 1;
                                    Console.WriteLine("This will cost {0} Titanite Slab. You have {1} Titanite Slab.", materialsNeeded, numTitaniteSlabs);
                                    typeOfMaterial = 3;
                                    break;
                            }
                            input = Console.ReadLine();
                            switch (input)
                            {
                                case "y":
                                    switch (typeOfMaterial)
                                    {
                                        case 0:
                                            if (materialsNeeded <= numTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).SetQuantity(((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentMelee.SetUpgradeLevel(upgradedEquipmentMelee.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 1:
                                            if (materialsNeeded <= numLargeTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).SetQuantity(((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).GetQuantity()
                                                                                                                                                                  - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentMelee.SetUpgradeLevel(upgradedEquipmentMelee.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 2:
                                            if (materialsNeeded <= numTitaniteChunks)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).SetQuantity(((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentMelee.SetUpgradeLevel(upgradedEquipmentMelee.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 3:
                                            if (materialsNeeded <= numTitaniteSlabs)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).SetQuantity(((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentMelee.SetUpgradeLevel(upgradedEquipmentMelee.GetUpgradeLevel() - 1);
                                            }
                                            break;
                                    }
                                    UpgradingEquipment();
                                    break;

                                case "n":
                                    upgradedEquipmentMelee.SetUpgradeLevel(upgradedEquipmentMelee.GetUpgradeLevel() - 1);
                                    UpgradingEquipment();
                                    break;

                                default:
                                    Console.WriteLine("Incorrect Input");
                                    upgradedEquipmentMelee.SetUpgradeLevel(upgradedEquipmentMelee.GetUpgradeLevel() - 1);
                                    UpgradingFromEquipmentBag(numTitaniteShards, numLargeTitaniteShards, numTitaniteChunks, numTitaniteSlabs,
                                                                titaniteShardIndex, largeTitaniteShardIndex, titaniteChunkIndex, titaniteSlabIndex);
                                    break;

                            }
                        }
                        else
                        {
                            Console.WriteLine("Can not upgrade any further.");
                        }
                    }
                    else if (player.GetEquipmentBag()[index] is RangedWeapon)
                    {
                        upgradeLevel = ((RangedWeapon)player.GetEquipmentBag()[index]).GetUpgradeLevel();
                        if (upgradeLevel < 10)
                        {
                            equipmentString = player.GetEquipmentBag()[index].ToString();
                            upgradedEquipmentRanged = (RangedWeapon)player.GetEquipmentBag()[index];
                            upgradedEquipmentRanged.SetUpgradeLevel(upgradedEquipmentRanged.GetUpgradeLevel() + 1); // This line does the upgrading already. Revert item if needed.  
                            Console.WriteLine("\nDo you want to upgrade {0} to {1}? (Y/N)", equipmentString, upgradedEquipmentRanged);
                            switch (upgradeLevel)
                            {
                                case 0:
                                    materialsNeeded = 2;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 1:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 2:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 3:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 4:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 5:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 6:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 7:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 8:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 9:
                                    materialsNeeded = 1;
                                    Console.WriteLine("This will cost {0} Titanite Slab. You have {1} Titanite Slab.", materialsNeeded, numTitaniteSlabs);
                                    typeOfMaterial = 3;
                                    break;
                            }
                            input = Console.ReadLine();
                            switch (input)
                            {
                                case "y":
                                    switch (typeOfMaterial)
                                    {
                                        case 0:
                                            if (materialsNeeded <= numTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).SetQuantity(((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentRanged.SetUpgradeLevel(upgradedEquipmentRanged.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 1:
                                            if (materialsNeeded <= numLargeTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).SetQuantity(((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).GetQuantity()
                                                                                                                                                                  - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentRanged.SetUpgradeLevel(upgradedEquipmentRanged.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 2:
                                            if (materialsNeeded <= numTitaniteChunks)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).SetQuantity(((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentRanged.SetUpgradeLevel(upgradedEquipmentRanged.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 3:
                                            if (materialsNeeded <= numTitaniteSlabs)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).SetQuantity(((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentRanged.SetUpgradeLevel(upgradedEquipmentRanged.GetUpgradeLevel() - 1);
                                            }
                                            break;
                                    }
                                    UpgradingEquipment();
                                    break;

                                case "n":
                                    upgradedEquipmentRanged.SetUpgradeLevel(upgradedEquipmentRanged.GetUpgradeLevel() - 1);
                                    UpgradingEquipment();
                                    break;

                                default:
                                    Console.WriteLine("Incorrect Input");
                                    upgradedEquipmentRanged.SetUpgradeLevel(upgradedEquipmentRanged.GetUpgradeLevel() - 1);
                                    UpgradingFromEquipmentBag(numTitaniteShards, numLargeTitaniteShards, numTitaniteChunks, numTitaniteSlabs,
                                                                titaniteShardIndex, largeTitaniteShardIndex, titaniteChunkIndex, titaniteSlabIndex);
                                    break;

                            }
                        }
                        else
                        {
                            Console.WriteLine("Can not upgrade any further.");
                        }
                    }
                    else if (player.GetEquipmentBag()[index] is MageWeapon)
                    {
                        upgradeLevel = ((MageWeapon)player.GetEquipmentBag()[index]).GetUpgradeLevel();
                        if (upgradeLevel < 10)
                        {
                            equipmentString = player.GetEquipmentBag()[index].ToString();
                            upgradedEquipmentMage = (MageWeapon)player.GetEquipmentBag()[index];
                            upgradedEquipmentMage.SetUpgradeLevel(upgradedEquipmentMage.GetUpgradeLevel() + 1); // This line does the upgrading already. Revert item if needed.  
                            Console.WriteLine("\nDo you want to upgrade {0} to {1}? (Y/N)", equipmentString, upgradedEquipmentMage);
                            switch (upgradeLevel)
                            {
                                case 0:
                                    materialsNeeded = 2;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 1:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 2:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 3:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 4:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 5:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 6:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 7:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 8:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 9:
                                    materialsNeeded = 1;
                                    Console.WriteLine("This will cost {0} Titanite Slab. You have {1} Titanite Slab.", materialsNeeded, numTitaniteSlabs);
                                    typeOfMaterial = 3;
                                    break;
                            }
                            input = Console.ReadLine();
                            switch (input)
                            {
                                case "y":
                                    switch (typeOfMaterial)
                                    {
                                        case 0:
                                            if (materialsNeeded <= numTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).SetQuantity(((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentMage.SetUpgradeLevel(upgradedEquipmentMage.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 1:
                                            if (materialsNeeded <= numLargeTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).SetQuantity(((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).GetQuantity()
                                                                                                                                                                  - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentMage.SetUpgradeLevel(upgradedEquipmentMage.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 2:
                                            if (materialsNeeded <= numTitaniteChunks)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).SetQuantity(((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentMage.SetUpgradeLevel(upgradedEquipmentMage.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 3:
                                            if (materialsNeeded <= numTitaniteSlabs)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).SetQuantity(((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentMage.SetUpgradeLevel(upgradedEquipmentMage.GetUpgradeLevel() - 1);
                                            }
                                            break;
                                    }
                                    UpgradingEquipment();
                                    break;

                                case "n":
                                    upgradedEquipmentMage.SetUpgradeLevel(upgradedEquipmentMage.GetUpgradeLevel() - 1);
                                    UpgradingEquipment();
                                    break;

                                default:
                                    Console.WriteLine("Incorrect Input");
                                    upgradedEquipmentMage.SetUpgradeLevel(upgradedEquipmentMage.GetUpgradeLevel() - 1);
                                    UpgradingFromEquipmentBag(numTitaniteShards, numLargeTitaniteShards, numTitaniteChunks, numTitaniteSlabs,
                                                                titaniteShardIndex, largeTitaniteShardIndex, titaniteChunkIndex, titaniteSlabIndex);
                                    break;

                            }
                        }
                        else
                        {
                            Console.WriteLine("Can not upgrade any further.");
                        }
                    }
                }

                UpgradingFromEquipmentBag(numTitaniteShards, numLargeTitaniteShards, numTitaniteChunks, numTitaniteSlabs,
                                                                titaniteShardIndex, largeTitaniteShardIndex, titaniteChunkIndex, titaniteSlabIndex);
            }
            else
            {
                UpgradingEquipment();
            }
        }

        public static void UpgradingFromEquipped(int numTitaniteShards, int numLargeTitaniteShards, int numTitaniteChunks, int numTitaniteSlabs,
                                                       int titaniteShardIndex, int largeTitaniteShardIndex, int titaniteChunkIndex, int titaniteSlabIndex)
        {
            int index = 0;
            Armor upgradedEquipmentArmor;
            MeleeWeapon upgradedEquipmentMelee;
            RangedWeapon upgradedEquipmentRanged;
            MageWeapon upgradedEquipmentMage;
            int materialsNeeded = 0;
            int typeOfMaterial = 0;
            int upgradeLevel = 0;
            string equipmentString = String.Empty;

            Console.WriteLine("\nEquipment:");
            player.Status();
            Console.WriteLine("\nType in the piece of equipment you want to upgrade or exit to go back (armor, helm, mage weapon, etc.)");
            input = Console.ReadLine();

            switch (input)
            {
                case "melee weapon":
                    if (player.GetMWeapon() != null)
                    {
                        upgradeLevel = player.GetMWeapon().GetUpgradeLevel();
                        if (upgradeLevel < 10)
                        {
                            equipmentString = player.GetMWeapon().ToString();
                            upgradedEquipmentMelee = player.GetMWeapon();
                            upgradedEquipmentMelee.SetUpgradeLevel(upgradedEquipmentMelee.GetUpgradeLevel() + 1); // This line does the upgrading already. Revert item if needed.  
                            Console.WriteLine("\nDo you want to upgrade {0} to {1}? (Y/N)", equipmentString, upgradedEquipmentMelee);
                            switch (upgradeLevel)
                            {
                                case 0:
                                    materialsNeeded = 2;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 1:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 2:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 3:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 4:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 5:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 6:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 7:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 8:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 9:
                                    materialsNeeded = 1;
                                    Console.WriteLine("This will cost {0} Titanite Slab. You have {1} Titanite Slab.", materialsNeeded, numTitaniteSlabs);
                                    typeOfMaterial = 3;
                                    break;
                            }
                            input = Console.ReadLine();
                            switch (input)
                            {
                                case "y":
                                    switch (typeOfMaterial)
                                    {
                                        case 0:
                                            if (materialsNeeded <= numTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).SetQuantity(((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentMelee.SetUpgradeLevel(upgradedEquipmentMelee.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 1:
                                            if (materialsNeeded <= numLargeTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).SetQuantity(((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).GetQuantity()
                                                                                                                                                                  - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentMelee.SetUpgradeLevel(upgradedEquipmentMelee.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 2:
                                            if (materialsNeeded <= numTitaniteChunks)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).SetQuantity(((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentMelee.SetUpgradeLevel(upgradedEquipmentMelee.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 3:
                                            if (materialsNeeded <= numTitaniteSlabs)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).SetQuantity(((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentMelee.SetUpgradeLevel(upgradedEquipmentMelee.GetUpgradeLevel() - 1);
                                            }
                                            break;
                                    }
                                    UpgradingEquipment();
                                    break;

                                case "n":
                                    upgradedEquipmentMelee.SetUpgradeLevel(upgradedEquipmentMelee.GetUpgradeLevel() - 1);
                                    UpgradingEquipment();
                                    break;

                                default:
                                    Console.WriteLine("Incorrect Input");
                                    upgradedEquipmentMelee.SetUpgradeLevel(upgradedEquipmentMelee.GetUpgradeLevel() - 1);
                                    UpgradingFromEquipped(numTitaniteShards, numLargeTitaniteShards, numTitaniteChunks, numTitaniteSlabs,
                                                                titaniteShardIndex, largeTitaniteShardIndex, titaniteChunkIndex, titaniteSlabIndex);
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("The item is fully upgraded");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no melee weapon equipped.");
                    }
                    break;

                case "armor":
                    if (player.GetArmor() != null)
                    {
                        upgradeLevel = player.GetArmor().GetUpgradeLevel();
                        if (upgradeLevel < 10)
                        {
                            equipmentString = player.GetArmor().ToString();
                            upgradedEquipmentArmor = player.GetArmor();
                            upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() + 1); // This line does the upgrading already. Revert item if needed.  
                            Console.WriteLine("\nDo you want to upgrade {0} to {1}? (Y/N)", equipmentString, upgradedEquipmentArmor);
                            switch (upgradeLevel)
                            {
                                case 0:
                                    materialsNeeded = 2;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 1:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 2:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 3:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 4:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 5:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 6:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 7:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 8:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 9:
                                    materialsNeeded = 1;
                                    Console.WriteLine("This will cost {0} Titanite Slab. You have {1} Titanite Slab.", materialsNeeded, numTitaniteSlabs);
                                    typeOfMaterial = 3;
                                    break;
                            }
                            input = Console.ReadLine();
                            switch (input)
                            {
                                case "y":
                                    switch (typeOfMaterial)
                                    {
                                        case 0:
                                            if (materialsNeeded <= numTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).SetQuantity(((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 1:
                                            if (materialsNeeded <= numLargeTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).SetQuantity(((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).GetQuantity()
                                                                                                                                                                  - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 2:
                                            if (materialsNeeded <= numTitaniteChunks)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).SetQuantity(((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 3:
                                            if (materialsNeeded <= numTitaniteSlabs)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).SetQuantity(((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;
                                    }
                                    UpgradingEquipment();
                                    break;

                                case "n":
                                    upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                    UpgradingEquipment();
                                    break;

                                default:
                                    Console.WriteLine("Incorrect Input");
                                    upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                    UpgradingFromEquipped(numTitaniteShards, numLargeTitaniteShards, numTitaniteChunks, numTitaniteSlabs,
                                                                titaniteShardIndex, largeTitaniteShardIndex, titaniteChunkIndex, titaniteSlabIndex);
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("The item is fully upgraded");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no armor equipped.");
                    }
                    break;

                case "mage weapon":
                    if (player.GetMageWeapon() != null)
                    {
                        upgradeLevel = player.GetMageWeapon().GetUpgradeLevel();
                        if (upgradeLevel < 10)
                        {
                            equipmentString = player.GetMageWeapon().ToString();
                            upgradedEquipmentMage = player.GetMageWeapon();
                            upgradedEquipmentMage.SetUpgradeLevel(upgradedEquipmentMage.GetUpgradeLevel() + 1); // This line does the upgrading already. Revert item if needed.  
                            Console.WriteLine("\nDo you want to upgrade {0} to {1}? (Y/N)", equipmentString, upgradedEquipmentMage);
                            switch (upgradeLevel)
                            {
                                case 0:
                                    materialsNeeded = 2;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 1:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 2:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 3:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 4:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 5:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 6:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 7:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 8:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 9:
                                    materialsNeeded = 1;
                                    Console.WriteLine("This will cost {0} Titanite Slab. You have {1} Titanite Slab.", materialsNeeded, numTitaniteSlabs);
                                    typeOfMaterial = 3;
                                    break;
                            }
                            input = Console.ReadLine();
                            switch (input)
                            {
                                case "y":
                                    switch (typeOfMaterial)
                                    {
                                        case 0:
                                            if (materialsNeeded <= numTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).SetQuantity(((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentMage.SetUpgradeLevel(upgradedEquipmentMage.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 1:
                                            if (materialsNeeded <= numLargeTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).SetQuantity(((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).GetQuantity()
                                                                                                                                                                  - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentMage.SetUpgradeLevel(upgradedEquipmentMage.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 2:
                                            if (materialsNeeded <= numTitaniteChunks)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).SetQuantity(((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentMage.SetUpgradeLevel(upgradedEquipmentMage.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 3:
                                            if (materialsNeeded <= numTitaniteSlabs)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).SetQuantity(((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentMage.SetUpgradeLevel(upgradedEquipmentMage.GetUpgradeLevel() - 1);
                                            }
                                            break;
                                    }
                                    UpgradingEquipment();
                                    break;

                                case "n":
                                    upgradedEquipmentMage.SetUpgradeLevel(upgradedEquipmentMage.GetUpgradeLevel() - 1);
                                    UpgradingEquipment();
                                    break;

                                default:
                                    Console.WriteLine("Incorrect Input");
                                    upgradedEquipmentMage.SetUpgradeLevel(upgradedEquipmentMage.GetUpgradeLevel() - 1);
                                    UpgradingFromEquipped(numTitaniteShards, numLargeTitaniteShards, numTitaniteChunks, numTitaniteSlabs,
                                                                titaniteShardIndex, largeTitaniteShardIndex, titaniteChunkIndex, titaniteSlabIndex);
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("The item is fully upgraded");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no mage weapon equipped.");
                    }
                    break;

                case "ranged weapon":
                    if (player.GetRangedWeapon() != null)
                    {
                        upgradeLevel = player.GetRangedWeapon().GetUpgradeLevel();
                        if (upgradeLevel < 10)
                        {
                            equipmentString = player.GetRangedWeapon().ToString();
                            upgradedEquipmentRanged = player.GetRangedWeapon();
                            upgradedEquipmentRanged.SetUpgradeLevel(upgradedEquipmentRanged.GetUpgradeLevel() + 1); // This line does the upgrading already. Revert item if needed.  
                            Console.WriteLine("\nDo you want to upgrade {0} to {1}? (Y/N)", equipmentString, upgradedEquipmentRanged);
                            switch (upgradeLevel)
                            {
                                case 0:
                                    materialsNeeded = 2;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 1:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 2:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 3:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 4:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 5:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 6:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 7:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 8:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 9:
                                    materialsNeeded = 1;
                                    Console.WriteLine("This will cost {0} Titanite Slab. You have {1} Titanite Slab.", materialsNeeded, numTitaniteSlabs);
                                    typeOfMaterial = 3;
                                    break;
                            }
                            input = Console.ReadLine();
                            switch (input)
                            {
                                case "y":
                                    switch (typeOfMaterial)
                                    {
                                        case 0:
                                            if (materialsNeeded <= numTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).SetQuantity(((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentRanged.SetUpgradeLevel(upgradedEquipmentRanged.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 1:
                                            if (materialsNeeded <= numLargeTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).SetQuantity(((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).GetQuantity()
                                                                                                                                                                  - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentRanged.SetUpgradeLevel(upgradedEquipmentRanged.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 2:
                                            if (materialsNeeded <= numTitaniteChunks)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).SetQuantity(((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentRanged.SetUpgradeLevel(upgradedEquipmentRanged.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 3:
                                            if (materialsNeeded <= numTitaniteSlabs)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).SetQuantity(((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentRanged.SetUpgradeLevel(upgradedEquipmentRanged.GetUpgradeLevel() - 1);
                                            }
                                            break;
                                    }
                                    UpgradingEquipment();
                                    break;

                                case "n":
                                    upgradedEquipmentRanged.SetUpgradeLevel(upgradedEquipmentRanged.GetUpgradeLevel() - 1);
                                    UpgradingEquipment();
                                    break;

                                default:
                                    Console.WriteLine("Incorrect Input");
                                    upgradedEquipmentRanged.SetUpgradeLevel(upgradedEquipmentRanged.GetUpgradeLevel() - 1);
                                    UpgradingFromEquipped(numTitaniteShards, numLargeTitaniteShards, numTitaniteChunks, numTitaniteSlabs,
                                                                titaniteShardIndex, largeTitaniteShardIndex, titaniteChunkIndex, titaniteSlabIndex);
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("The item is fully upgraded");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no ranged weapon equipped.");
                    }
                    break;

                case "shield":
                    if (player.GetShield() != null)
                    {
                        upgradeLevel = player.GetShield().GetUpgradeLevel();
                        if (upgradeLevel < 10)
                        {
                            equipmentString = player.GetShield().ToString();
                            upgradedEquipmentArmor = (Armor)player.GetShield();
                            upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() + 1); // This line does the upgrading already. Revert item if needed.  
                            Console.WriteLine("\nDo you want to upgrade {0} to {1}? (Y/N)", equipmentString, upgradedEquipmentArmor);
                            switch (upgradeLevel)
                            {
                                case 0:
                                    materialsNeeded = 2;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 1:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 2:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 3:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 4:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 5:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 6:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 7:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 8:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 9:
                                    materialsNeeded = 1;
                                    Console.WriteLine("This will cost {0} Titanite Slab. You have {1} Titanite Slab.", materialsNeeded, numTitaniteSlabs);
                                    typeOfMaterial = 3;
                                    break;
                            }
                            input = Console.ReadLine();
                            switch (input)
                            {
                                case "y":
                                    switch (typeOfMaterial)
                                    {
                                        case 0:
                                            if (materialsNeeded <= numTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).SetQuantity(((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 1:
                                            if (materialsNeeded <= numLargeTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).SetQuantity(((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).GetQuantity()
                                                                                                                                                                  - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 2:
                                            if (materialsNeeded <= numTitaniteChunks)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).SetQuantity(((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 3:
                                            if (materialsNeeded <= numTitaniteSlabs)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).SetQuantity(((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;
                                    }
                                    UpgradingEquipment();
                                    break;

                                case "n":
                                    upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                    UpgradingEquipment();
                                    break;

                                default:
                                    Console.WriteLine("Incorrect Input");
                                    upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                    UpgradingFromEquipped(numTitaniteShards, numLargeTitaniteShards, numTitaniteChunks, numTitaniteSlabs,
                                                                titaniteShardIndex, largeTitaniteShardIndex, titaniteChunkIndex, titaniteSlabIndex);
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("The item is fully upgraded");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no shield equipped.");
                    }
                    break;

                case "helm":
                    if (player.GetHelm() != null)
                    {
                        upgradeLevel = player.GetHelm().GetUpgradeLevel();
                        if (upgradeLevel < 10)
                        {
                            equipmentString = player.GetHelm().ToString();
                            upgradedEquipmentArmor = (Armor)player.GetHelm();
                            upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() + 1); // This line does the upgrading already. Revert item if needed.  
                            Console.WriteLine("\nDo you want to upgrade {0} to {1}? (Y/N)", equipmentString, upgradedEquipmentArmor);
                            switch (upgradeLevel)
                            {
                                case 0:
                                    materialsNeeded = 2;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 1:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 2:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 3:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 4:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 5:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 6:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 7:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 8:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 9:
                                    materialsNeeded = 1;
                                    Console.WriteLine("This will cost {0} Titanite Slab. You have {1} Titanite Slab.", materialsNeeded, numTitaniteSlabs);
                                    typeOfMaterial = 3;
                                    break;
                            }
                            input = Console.ReadLine();
                            switch (input)
                            {
                                case "y":
                                    switch (typeOfMaterial)
                                    {
                                        case 0:
                                            if (materialsNeeded <= numTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).SetQuantity(((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 1:
                                            if (materialsNeeded <= numLargeTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).SetQuantity(((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).GetQuantity()
                                                                                                                                                                  - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 2:
                                            if (materialsNeeded <= numTitaniteChunks)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).SetQuantity(((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 3:
                                            if (materialsNeeded <= numTitaniteSlabs)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).SetQuantity(((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;
                                    }
                                    UpgradingEquipment();
                                    break;

                                case "n":
                                    upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                    UpgradingEquipment();
                                    break;

                                default:
                                    Console.WriteLine("Incorrect Input");
                                    upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                    UpgradingFromEquipped(numTitaniteShards, numLargeTitaniteShards, numTitaniteChunks, numTitaniteSlabs,
                                                                titaniteShardIndex, largeTitaniteShardIndex, titaniteChunkIndex, titaniteSlabIndex);
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("The item is fully upgraded");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no helm equipped.");
                    }
                    break;

                case "pants":
                    if (player.GetPants() != null)
                    {
                        upgradeLevel = player.GetPants().GetUpgradeLevel();
                        if (upgradeLevel < 10)
                        {
                            equipmentString = player.GetPants().ToString();
                            upgradedEquipmentArmor = (Armor)player.GetPants();
                            upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() + 1); // This line does the upgrading already. Revert item if needed.  
                            Console.WriteLine("\nDo you want to upgrade {0} to {1}? (Y/N)", equipmentString, upgradedEquipmentArmor);
                            switch (upgradeLevel)
                            {
                                case 0:
                                    materialsNeeded = 2;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 1:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 2:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 3:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 4:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 5:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 6:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 7:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 8:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 9:
                                    materialsNeeded = 1;
                                    Console.WriteLine("This will cost {0} Titanite Slab. You have {1} Titanite Slab.", materialsNeeded, numTitaniteSlabs);
                                    typeOfMaterial = 3;
                                    break;
                            }
                            input = Console.ReadLine();
                            switch (input)
                            {
                                case "y":
                                    switch (typeOfMaterial)
                                    {
                                        case 0:
                                            if (materialsNeeded <= numTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).SetQuantity(((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 1:
                                            if (materialsNeeded <= numLargeTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).SetQuantity(((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).GetQuantity()
                                                                                                                                                                  - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 2:
                                            if (materialsNeeded <= numTitaniteChunks)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).SetQuantity(((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 3:
                                            if (materialsNeeded <= numTitaniteSlabs)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).SetQuantity(((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;
                                    }
                                    UpgradingEquipment();
                                    break;

                                case "n":
                                    upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                    UpgradingEquipment();
                                    break;

                                default:
                                    Console.WriteLine("Incorrect Input");
                                    upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                    UpgradingFromEquipped(numTitaniteShards, numLargeTitaniteShards, numTitaniteChunks, numTitaniteSlabs,
                                                                titaniteShardIndex, largeTitaniteShardIndex, titaniteChunkIndex, titaniteSlabIndex);
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("The item is fully upgraded");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no pants equipped.");
                    }
                    break;

                case "boots":
                    if (player.GetBoots() != null)
                    {
                        upgradeLevel = player.GetBoots().GetUpgradeLevel();
                        if (upgradeLevel < 10)
                        {
                            equipmentString = player.GetBoots().ToString();
                            upgradedEquipmentArmor = (Armor)player.GetBoots();
                            upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() + 1); // This line does the upgrading already. Revert item if needed.  
                            Console.WriteLine("\nDo you want to upgrade {0} to {1}? (Y/N)", equipmentString, upgradedEquipmentArmor);
                            switch (upgradeLevel)
                            {
                                case 0:
                                    materialsNeeded = 2;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 1:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 2:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 3:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 4:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 5:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 6:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 7:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 8:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 9:
                                    materialsNeeded = 1;
                                    Console.WriteLine("This will cost {0} Titanite Slab. You have {1} Titanite Slab.", materialsNeeded, numTitaniteSlabs);
                                    typeOfMaterial = 3;
                                    break;
                            }
                            input = Console.ReadLine();
                            switch (input)
                            {
                                case "y":
                                    switch (typeOfMaterial)
                                    {
                                        case 0:
                                            if (materialsNeeded <= numTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).SetQuantity(((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 1:
                                            if (materialsNeeded <= numLargeTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).SetQuantity(((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).GetQuantity()
                                                                                                                                                                  - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 2:
                                            if (materialsNeeded <= numTitaniteChunks)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).SetQuantity(((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 3:
                                            if (materialsNeeded <= numTitaniteSlabs)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).SetQuantity(((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;
                                    }
                                    UpgradingEquipment();
                                    break;

                                case "n":
                                    upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                    UpgradingEquipment();
                                    break;

                                default:
                                    Console.WriteLine("Incorrect Input");
                                    upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                    UpgradingFromEquipped(numTitaniteShards, numLargeTitaniteShards, numTitaniteChunks, numTitaniteSlabs,
                                                                titaniteShardIndex, largeTitaniteShardIndex, titaniteChunkIndex, titaniteSlabIndex);
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("The item is fully upgraded");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no boots equipped.");
                    }
                    break;

                case "gloves":
                    if (player.GetGloves() != null)
                    {
                        upgradeLevel = player.GetGloves().GetUpgradeLevel();
                        if (upgradeLevel < 10)
                        {
                            equipmentString = player.GetGloves().ToString();
                            upgradedEquipmentArmor = (Armor)player.GetGloves();
                            upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() + 1); // This line does the upgrading already. Revert item if needed.  
                            Console.WriteLine("\nDo you want to upgrade {0} to {1}? (Y/N)", equipmentString, upgradedEquipmentArmor);
                            switch (upgradeLevel)
                            {
                                case 0:
                                    materialsNeeded = 2;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 1:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 2:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 3:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Titanite Shards. You have {1} Titanite Shards.", materialsNeeded, numTitaniteShards);
                                    typeOfMaterial = 0;
                                    break;

                                case 4:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 5:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 6:
                                    materialsNeeded = 8;
                                    Console.WriteLine("This will cost {0} Large Titanite Shards. You have {1} Large Titanite Shards.", materialsNeeded, numLargeTitaniteShards);
                                    typeOfMaterial = 1;
                                    break;

                                case 7:
                                    materialsNeeded = 4;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 8:
                                    materialsNeeded = 6;
                                    Console.WriteLine("This will cost {0} Titanite Chunks. You have {1} Titanite Chunks.", materialsNeeded, numTitaniteChunks);
                                    typeOfMaterial = 2;
                                    break;

                                case 9:
                                    materialsNeeded = 1;
                                    Console.WriteLine("This will cost {0} Titanite Slab. You have {1} Titanite Slab.", materialsNeeded, numTitaniteSlabs);
                                    typeOfMaterial = 3;
                                    break;
                            }
                            input = Console.ReadLine();
                            switch (input)
                            {
                                case "y":
                                    switch (typeOfMaterial)
                                    {
                                        case 0:
                                            if (materialsNeeded <= numTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).SetQuantity(((TitaniteShard)player.GetItemBag()[titaniteShardIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 1:
                                            if (materialsNeeded <= numLargeTitaniteShards)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).SetQuantity(((LargeTitaniteShard)player.GetItemBag()[largeTitaniteShardIndex]).GetQuantity()
                                                                                                                                                                  - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 2:
                                            if (materialsNeeded <= numTitaniteChunks)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).SetQuantity(((TitaniteChunk)player.GetItemBag()[titaniteChunkIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;

                                        case 3:
                                            if (materialsNeeded <= numTitaniteSlabs)
                                            {
                                                Console.WriteLine("Equipment successfully upgraded.");
                                                ((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).SetQuantity(((TitaniteSlab)player.GetItemBag()[titaniteSlabIndex]).GetQuantity() - materialsNeeded);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Not enough materials.");
                                                upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                            }
                                            break;
                                    }
                                    UpgradingEquipment();
                                    break;

                                case "n":
                                    upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                    UpgradingEquipment();
                                    break;

                                default:
                                    Console.WriteLine("Incorrect Input");
                                    upgradedEquipmentArmor.SetUpgradeLevel(upgradedEquipmentArmor.GetUpgradeLevel() - 1);
                                    UpgradingFromEquipped(numTitaniteShards, numLargeTitaniteShards, numTitaniteChunks, numTitaniteSlabs,
                                                                titaniteShardIndex, largeTitaniteShardIndex, titaniteChunkIndex, titaniteSlabIndex);
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("The item is fully upgraded");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no gloves equipped.");
                    }
                    break;

                case "exit":
                    UpgradingEquipment();
                    break;

                default:
                    Console.WriteLine("Incorrect input");
                    UpgradingFromEquipped(numTitaniteShards, numLargeTitaniteShards, numTitaniteChunks, numTitaniteSlabs,
                                    titaniteShardIndex, largeTitaniteShardIndex, titaniteChunkIndex, titaniteSlabIndex);
                    break;
            }
            UpgradingFromEquipped(numTitaniteShards, numLargeTitaniteShards, numTitaniteChunks, numTitaniteSlabs,
                                                            titaniteShardIndex, largeTitaniteShardIndex, titaniteChunkIndex, titaniteSlabIndex);
        }

        /// <summary>
        /// Decreases the health of the player based off of monster damage and armor.
        /// </summary>
        /// <param name="monsterDamg">Base monster damage</param>
        public static void PlayerDamagedCalculation(int monsterDamg)
        {
            double damg = monsterDamg;
            int turtleSetPieces = 0;

            if (player.GetArmor() != null)
            {
                damg = (damg * (1 - player.GetArmor().DamgReduct()));
                if (player.GetArmor().GetSet() == Set.Turtle)
                {
                    turtleSetPieces++;
                }
            }

            if (player.GetHelm() != null)
            {
                damg = (damg * (1 - player.GetHelm().DamgReduct()));
                if (player.GetHelm().GetSet() == Set.Turtle)
                {
                    turtleSetPieces++;
                }
            }

            if (player.GetPants() != null)
            {
                damg = (damg * (1 - player.GetPants().DamgReduct()));
                if (player.GetPants().GetSet() == Set.Turtle)
                {
                    turtleSetPieces++;
                }
            }

            if (player.GetGloves() != null)
            {
                damg = (damg * (1 - player.GetGloves().DamgReduct()));
                if (player.GetGloves().GetSet() == Set.Turtle)
                {
                    turtleSetPieces++;
                }
            }

            if (player.GetBoots() != null)
            {
                damg = (damg * (1 - player.GetBoots().DamgReduct()));
                if (player.GetBoots().GetSet() == Set.Turtle)
                {
                    turtleSetPieces++;
                }
            }

            if (player.GetShield() != null)
            {

                damg = (damg * (1 - player.GetShield().DamgReduct()));
            }

            if (ironArmorTurns != 0)
            {
                damg = .75 * damg;
                ironArmorTurns--;
            }

            switch (turtleSetPieces)
            {
                case 0:
                    break;

                case 1:
                    damg = .95 * damg;
                    break;

                case 2:
                    damg = .9 * damg;
                    break;

                case 3:
                    damg = .85 * damg;
                    break;

                case 4:
                    damg = .75 * damg;
                    break;

                case 5:
                    damg = .6 * damg;
                    break;

                default:
                    break;
            }

            player.DecreaseHealth((int)damg);
        }

        /// <summary>
        /// Decreases the health of the monster based off of player damage and stats for a melee attack
        /// </summary>
        public static int PlayerMeleeAttackCalculation()
        {
            MeleeWeapon mWep;
            int playerDamg = 0;
            int raidersSetPieces = 0;

            mWep = player.GetMWeapon();
            if (mWep is LongSword || mWep is ShortSword)
            {
                playerDamg = mWep.CalcDamg(player.GetStr());
            }
            else if (mWep is Dagger || mWep is TwinDaggers)
            {
                playerDamg = mWep.CalcDamg(player.GetDex());
            }

            if (player.GetArmor() != null)
            {
                if (player.GetArmor().GetSet() == Set.Raider)
                {
                    raidersSetPieces++;
                }
            }

            if (player.GetHelm() != null)
            {
                if (player.GetHelm().GetSet() == Set.Raider)
                {
                    raidersSetPieces++;
                }
            }

            if (player.GetPants() != null)
            {
                if (player.GetPants().GetSet() == Set.Raider)
                {
                    raidersSetPieces++;
                }
            }

            if (player.GetGloves() != null)
            {
                if (player.GetGloves().GetSet() == Set.Raider)
                {
                    raidersSetPieces++;
                }
            }

            if (player.GetBoots() != null)
            {
                if (player.GetBoots().GetSet() == Set.Raider)
                {
                    raidersSetPieces++;
                }
            }

            switch (raidersSetPieces)
            {
                case 0:
                    break;

                case 1:
                    playerDamg = (int)(1.1 * playerDamg);
                    break;

                case 2:
                    playerDamg = (int)(1.2 * playerDamg);
                    break;

                case 3:
                    playerDamg = (int)(1.4 * playerDamg);
                    break;

                case 4:
                    playerDamg = (int)(1.5 * playerDamg);
                    break;

                case 5:
                    playerDamg = (int)(1.7 * playerDamg);
                    break;

                default:
                    break;
            }

            return playerDamg;
        }
    }
}