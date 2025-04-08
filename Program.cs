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

