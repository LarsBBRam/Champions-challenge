using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Xml.XPath;

namespace Champions_challenge;

class Program
{
    static void Main(string[] args)
    {
        /*
        Plan
        simpelt eventyrspill

        Valg mellom 3 champions. Hver champion har forskjellige våpen.
        bruk terninger for hit og wound, plus rend

        Valg av 3 fiender, og lagre hver gang en blir bekjempet
        Lagre også tap, og gi game over.

        Hver fiende har et slott. Hvis du har bekjempet fienden er slottet tomt.
        Legg til en puzzle (gåte f.eks) ved inngangen til 2 slott.

        Kan ikke gå til siste slottet uten å ha tatt de to andre. 
        En fallen champion står i veien og må bekjempes først.
        med mindre du dukker opp i en lunsjpause, for da er han ikke der.

        Championplaner:
        Roland, a Champion with either a Hammer or Sword. 
            Basic stats
            standard speed, health, good save from the armor.

            both weapons are good
            Hammer has more rend, fewer attacks, better to wound but worse to hit
            Sword has less rend, more attacks, better to hit, but worse to wound.
            Can once per fight make two attacks.

        Sylvia, an assassin with dual daggers and throwing knives. 
            Frailer, but fast.
            higher speed, lower health, decent save from maneuverability

            Daggers have a lot of attacks, some rend, same to wound as to hit.
            throwing knives ALWAYS go first (+100 speed), but are slightly weaker than daggers

        Ser Boldor, a Paladin with a Flail and a shield.
            Slow (negative speed)
            High health, high save
            Flail has good rend, a random amount of attacks (d3+3), better to wound than to hit
            When striking with shield, his save increases. worse rend, decent to hit/wound

            Passively regenerates each turn.


        bossplaner:

        Flesh heap:    
             tåler mye, lav damge, men ruller mange angrep og har lav to hit.
             kan regenerere, som implementeres med en heal mellom hver kamp.
             Ikke så høy save
             treg.
                Angrep:
                    Moving mountain
                        Går alltid sist, massiv damage, kun 1 angrep elns
                    
                    Engulf
                        Forverrer save, masse angrep, lav damage
                    
                    Rotting slam
                        vanlig angrep

        The Vorpal Juggernaut
            Høy save, lav helse. decent speed (litt glass cannon)
            Enorm damage, men få angrep og lav to hit.
            Lage et system for bleed/crits?
                Angrep:
                    Decapitation
                        Få angrep, lav to hit. høy to wound 
                        critical wound gjør double damage

                    Flensing strik
                        vanlig mengde angrep, decent to hit
                        lav to wound
                        critical hits setter på en bleed
                        bleed gjør skade resten av kampen hver tur

                    Juggerslam
                        høyere rend

        Champion of Evil
            svakere enn bosser

        Lord of Hosts
            Siste boss
            tåler mye, slår hardt, er rask
            Bygger opp kraft hver tur basert på både skade han tar
            og skade han gjør.
            maks power gir superangrep
                Angrep:
                    Wing beat:
                        høyere speed og forverrer to hit på neste 
                        angrep hvis det går først

                    Weapon strike:
                        Strong, fewer attacks

                    Flame breath:
                        many weaker attacks

                    Ultimate
                        Very powerful.

        combat system:
        The champion chooses what weapon to use, which dictates its stats.
        the boss chooses at random between its weapons.

        Both fighters rolls a dice and adds their speed to see who goes first.
        They then roll as many dice as they have number of attacks.
        every dice that meets or exceeds the to hit number goes through.
        all dice that went through gets rolled again.
        dice that meet or exceed the wound value goes through.
        the weapons rend increases the save number the fighter needs.
        The opponent then rolls the dice again, and removes all that meets 
        or exceeds their save value.
        any remaining dice do their damage value to the health of the opponent.

        Then the one with the lower speed strikes.
        Combat continues until one fighter reaches 0 health.

        */
        int timesBeaten = 0;

        string? combatStarter = "";
        do
        {

            // Console.Clear();
            Console.WriteLine("How many bosses can you beat? ");
            Console.WriteLine("An enemy approaches! Type escape to shutdown, or anything else to start combat!");

            combatStarter = Console.ReadLine()?.ToLower().Trim().Replace(" ", "");

            if (combatStarter != "escape")
                Combat();
            if (Combat())
                combatStarter = "escape";
            else
            {
                timesBeaten++;
                Console.WriteLine($"Victorious! You've beaten the boss {timesBeaten} times. \nPress enter to continue.");
                Console.ReadLine();
            }


        } while (combatStarter != "escape");
        bool Combat()
        {
            bool gameOver = false;
            Random random = new();

            Champion champion = new();

            champion.ChampHealth = 18;
            champion.ChampSpeed = 2;
            champion.ChampDamage = 2;

            Boss boss = new();

            boss.BossSpeed = 1;
            boss.BossHealth = 18;
            boss.BossDamage = 2;

            while (champion.ChampHealth >= 0 && boss.BossHealth >= 0)
            {
                int damageTaken = 0;

                int champInitiative = random.Next(1, 7) + champion.ChampSpeed;
                int bossInitiative = random.Next(1, 7) + boss.BossSpeed;

                #region ifChampWinsSpeed
                if (champInitiative >= bossInitiative)
                {
                    Console.WriteLine("The Champion was faster! Press any key to start their attack.");
                    Console.Read();

                    ChampionFight();
                    if (ChampionFight().Count != 0)
                    {
                        foreach (int failedSave in ChampionFight())
                        {
                            boss.BossHealth -= champion.ChampDamage;
                            damageTaken += champion.ChampDamage;
                        }
                        Console.WriteLine($"The champion dealt {damageTaken} to the boss, and the boss has {boss.BossHealth} health left.\nPress enter to continue.");
                        Console.ReadLine();

                    }
                    else
                    {
                        Console.WriteLine("All saves made! No damage taken. Press enter to continue.");
                        Console.ReadLine();
                    }

                }
                #endregion

                else
                {
                    Console.WriteLine("The boss strikes first! Press any key to start their attack.");
                    Console.Read();
                    BossFight();

                    if (BossFight().Count != 0)
                    {

                        foreach (int damage in BossFight())
                        {
                            boss.BossHealth -= champion.ChampDamage;
                            damageTaken += champion.ChampDamage;

                        }
                        Console.WriteLine($"{BossFight().Count} saves failed! The boss takes {damageTaken} damage, and is at {boss.BossHealth} left. \n Press enter to continue.");
                        Console.ReadLine();

                    }
                    else
                    {
                        Console.WriteLine("All saves made! No damage taken. Press enter to continue.");
                        Console.ReadLine();

                    }
                }
                #region ifBossWinsSpeed
                if (champInitiative < bossInitiative)
                {
                    Console.WriteLine("The boss strikes back! Press any key to start their attack.");
                    Console.Read();
                    BossFight();

                    if (BossFight().Count != 0)
                    {

                        foreach (int damage in BossFight())
                        {
                            boss.BossHealth -= champion.ChampDamage;
                            damageTaken += champion.ChampDamage;

                        }
                        Console.WriteLine($"{BossFight().Count} saves failed! The boss takes {damageTaken} damage, and is at {boss.BossHealth} left. \n Press enter to continue.");
                        Console.ReadLine();

                    }
                    else
                    {
                        Console.WriteLine("All saves made! No damage taken. Press enter to continue.");
                        Console.ReadLine();

                    }

                }
                else
                {
                    Console.WriteLine("The champion strikes back! Press any key to start their attack.");
                    Console.Read();
                    ChampionFight();
                    if (ChampionFight().Count != 0)
                    {
                        foreach (int failedSave in ChampionFight())
                        {
                            boss.BossHealth -= champion.ChampDamage;
                            damageTaken += champion.ChampDamage;
                        }
                        Console.WriteLine($"The champion dealt {damageTaken} to the boss, and the boss has {boss.BossHealth} health left.\nPress enter to continue.");
                        Console.ReadLine();

                    }
                    else
                    {
                        Console.WriteLine("All saves made! No damage taken. Press enter to continue.");
                        Console.ReadLine();
                    }
                }

                #endregion
            }
            if (champion.ChampHealth <= 0)
            {
                gameOver = true;
                return gameOver;
            }
            else
            {
                gameOver = false;
                return gameOver;
            }
        }

        List<int> BossFight()
        {

            List<int> hitRolls = new();
            List<int> woundRolls = new();
            List<int> failedSaves = new();

            List<int> failure = new();
            Boss bossFight = new();
            bossFight.BossAttackNumber = 12;
            bossFight.BossRend = 0;
            bossFight.BossToHit = 5;
            bossFight.BossToWound = 3;

            Champion championBoss = new();
            championBoss.ChampSave = 4;

            for (int i = 0; i <= bossFight.BossAttackNumber; i++)
            {
                int roll = 0;
                roll = DiceRoll();
                if (roll >= bossFight.BossToHit)
                {
                    hitRolls.Add(roll);
                }
            }

            if (hitRolls.Count != 0)
            {
                Console.WriteLine($"The bossFight rolled {bossFight.BossAttackNumber} attacks, and hit {hitRolls.Count} of them.\n Press enter to roll wounds");
                Console.ReadLine();

                foreach (int hit in hitRolls)
                {
                    int roll = 0;
                    roll = DiceRoll();
                    if (roll >= bossFight.BossToWound)
                    {
                        woundRolls.Add(roll);
                    }
                }
                if (woundRolls.Count != 0)
                {

                    Console.WriteLine($"The boss rolled {hitRolls.Count} and wounded with {woundRolls.Count} of them. \n Press enter to roll champion save");
                    Console.ReadLine();
                    foreach (int wound in woundRolls)
                    {
                        int roll = 0;
                        roll = DiceRoll();
                        if (roll <= championBoss.ChampSave + bossFight.BossRend)
                        {
                            failedSaves.Add(roll);
                        }
                    }
                    return failedSaves;
                }
                else
                {
                    Console.WriteLine($"The boss could not wound. Press enter to continue.");
                    Console.ReadLine();
                    return failure;
                }
            }
            else
            {

                Console.WriteLine($"The boss did not hit any attacks. Press enter to continue");
                Console.ReadLine();
                return failure;
            }
        }

        List<int> ChampionFight()
        {
            List<int> hitRolls = new();
            List<int> woundRolls = new();
            List<int> failedSaves = new();
            List<int> failure = new();

            Champion championFight = new();
            championFight.ChampAttackNumber = 8;
            championFight.ChampToHit = 4;
            championFight.ChampToWound = 4;
            championFight.ChampRend = 1;

            Boss bossChamp = new();

            bossChamp.BossSave = 4;
            for (int i = 0; i <= championFight.ChampAttackNumber; i++)
            {
                int roll = 0;
                roll = DiceRoll();
                if (roll >= championFight.ChampToHit)
                {
                    hitRolls.Add(roll);
                }
            }

            if (hitRolls.Count != 0)
            {
                Console.WriteLine($"The Champion rolled {championFight.ChampAttackNumber} attacks, and hit {hitRolls.Count} of them.\n Press enter to roll wounds");
                Console.Read();

                foreach (int hit in hitRolls)
                {
                    int roll = 0;
                    roll = DiceRoll();
                    if (roll >= championFight.ChampToWound)
                    {
                        woundRolls.Add(roll);
                    }
                }
                if (woundRolls.Count != 0)
                {

                    Console.WriteLine($"The Champion rolled {hitRolls.Count} wounds and wounded with {woundRolls.Count} of them. \n Press enter to roll boss save");
                    Console.ReadLine();


                    foreach (int wound in woundRolls)
                    {
                        int roll = 0;
                        roll = DiceRoll();
                        if (roll <= bossChamp.BossSave + championFight.ChampRend)
                        {
                            failedSaves.Add(roll);
                        }
                    }
                    return failedSaves;

                }
                else
                {
                    Console.WriteLine($"The Champion could not wound. Press enter to continue.");
                    Console.ReadLine();
                    return failure;
                }
            }
            else
            {
                Console.WriteLine($"The Champion did not hit any. Press enter to continue");
                Console.ReadLine();
                return failure;
            }
        }

        int DiceRoll()
        {
            Random dice = new();
            int result = dice.Next(1, 7);
            return result;
        }
    }
    class Champion
    {
        public int ChampAttackNumber { get; set; }
        public int ChampToHit { get; set; }
        public int ChampToWound { get; set; }
        public int ChampRend { get; set; }
        public int ChampDamage { get; set; }
        public int ChampSave { get; set; }
        public int ChampHealth { get; set; }
        public int ChampSpeed { get; set; }
    }

    class Boss
    {
        public int BossAttackNumber { get; set; }
        public int BossToHit { get; set; }
        public int BossToWound { get; set; }
        public int BossRend { get; set; }
        public int BossSave { get; set; }
        public int BossHealth { get; set; }
        public int BossSpeed { get; set; }
        public int BossDamage { get; set; }
    }
}
/*

//Menu class

    function for main menu

    function for champion selection

    function for where to go

    function for choosing what to do at each castle

    function for choosing what weapon to attack with


//function for looking at stats

//Combat class

    get attack dice function, roll

    get and check to hit, roll

    get and check to wound roll

    get and check rend, roll

    get and check damage, subtract from health if failed save

    if health is not 0 repeat


//en Champion class
    //helse
    //save
    //speed
        //standard weapon
            //how many attacks
            //to hit
            //to wound
            //rend
            //damage
    
    //Child champion class
            //new health
            //new save
                //special weapon
                    //how many attacks
                    //to hit
                    //to wound
                    //rend
                    //damage

//boss class
    //health
    //save
    //speed 

//Boss attack class
    //special speed
    //attacks
    //to hit
    //to wound
    //rend
    //damage
    //special effect?
        

//boss attack class child LordOfHostsAttacks
            
    //Wing beat function
        //bonus to speed
        //to hit
        //to wound
        //rend
        //damage
        //special reduce to hit of opponent if faster

    //Weapon strike
        //to hit
        //to wound
        //rend
        //damage

    //flame breath
        //to hit
        //to wound
        //rend
        //damage


    //boss class child LordOfHosts
        //health new
        //save new
        //speed new

            //special ability
        
        //combat




    

*/

