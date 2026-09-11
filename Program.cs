namespace TextAdventureTime
{
	using System.Collections.Generic;
	
	class Program
	{
        static Player player = new Player();
		static Enemy currentEnemy;
		static bool potion = false;
		static bool knife = false;
        static bool knifeInLockedRoom = false;
        static bool key = false;
        static bool usedKey = false;
        static bool sword = false;
        static bool swordInKitchenRoom = false;
        static bool shield = false;
        static bool puzzleSolved = false;
        static bool usedPotion = false;
        static void Main(string[] args)
		{
			NewGame(player);
			Console.WriteLine($"Ah, of course, {player.playerName}, how could you forget?");

			while (player.location != "quitroom")
			{
				
				switch (player.location)
				{
					//base functions
					case "newgame":
						NewGame(player);
						break;
                    case "winscreenroom":
                        break;
                    case "deathscreenroom":
                        break;
					case "quitroom":
                        break;

                    //main path
                    case "dimroom":
						DimRoom();
						break;
					case "chainroom":
						break;
					case "invchainroom":
						break;
					case "combatroom":
						break;
					case "rest(ing)room":
						break;
					case "bossroom":
						break;

						//left path
					case "diningroom":
						DiningRoom();
						break;
					case "invdiningroom":
						InvDiningRoom();
						break;
					case "kitchenroom":
						KitchenRoom();
						break;
					case "invkitchenroom":
						InvKitchenRoom();
						break;

						//right path
					case "lockeddoor":
						LockedDoor();
						break;
                    case "lockedroom":
						LockedRoom();
                        break;
					case "invlockedroom":
						InvLockedRoom();
						break;
                }

			}
		}

		static void NewGame(Player player)
		{
			Console.Clear();
            potion = false;
            knife = false;
            key = false;
            sword = false;
            shield = false;
            swordInKitchenRoom = false;
            knifeInLockedRoom = false;
			usedKey = false;
            puzzleSolved = false;
            usedPotion = false;
            Console.WriteLine("You awake, finding yourself alone in a dark, damp chamber.\nThe only light coming from the cinders of a dying torch on the opposite side of the room.");
            string name;
			do
			{
				name = AskQuestion("Your memory is foggy… What was your name?");
			} while (!AskYesOrNo($"{name}, was that it?"));
			player.playerName = name;
			player.location = "dimroom";
		}
		static void DimRoom()
		{
			int response = 0;
            do
			{
				response = int.Parse(AskQuestion("Getting a better look at the room you find that it has 3 wooden doors.\nOne door in front of you, one on the left wall and the last on the right wall.\n\nWhat would you like to do? (answer with the corresponding number)\n1. Investigate the room further.\n2. Attempt to go left.\n3. Attempt to go right.\n4. Attempt to go forward."));
				
			} while (response <= 0 || response > 4);
            switch (response)
            {
                case 1:
					Console.WriteLine("Investigating the room further you don’t find much of note aside from a colony of mold growing in one of the corners.");
                    break;
                case 2:
					player.location = "diningroom";
                    break;
                case 3:
					player.location = "lockeddoor";
                    break;
                case 4:
					player.location = "chainroom";
                    break;
            }
        }

        static void DiningRoom()
		{
            int response = 0;
			Console.WriteLine("You move to the left door and open it.\nThe first thing you’re met with is the stench of rot flooding out of the room like some sickly tidal wave.\nRecovering from the appalling smell you get a look at the room, finding it to be the remnants of an old dining hall.\nThe hall is filled with old stone tables, still covered in plates of rotting food clearly being the source of the foul odor.\nOn the right wall of the room is an opening leading into a makeshift kitchen.");
            do
            {
                response = int.Parse(AskQuestion("\nWhat would you like to do? (answer with the corresponding number)\n1.Investigate the dining hall.\n2.Enter into the kitchen.\n3.Return to the previous room."));

            } while (response <= 0 || response > 3);
            switch (response)
            {
                case 1:
					player.location = "invdiningroom";
                    break;
                case 2:
					player.location = "kitchenroom";
                    break;
                case 3:
					player.location = "dimroom";
                    break;
            }
        }

        static void InvDiningRoom()
        {
			if (potion) 
			{
				Console.WriteLine("You find nothing new. Just the same carving of a 2 on the wall.");
				player.location = "diningroom";
				return;
			}
			bool answer = AskYesOrNo("Searching the dining hall you initially find nothing aside from the rotting food and mold filled cups. However, after thoroughly searching the entire hall you find 2 things.\nFirst is a carving of a 2 on one of the walls, initially hidden by an old drapery.\nSecond is a flask containing a red liquid.\nDo you wish to take the flask with you?");
			switch (answer)
			{
                case true:
                    potion = true;
                    int result = RollD6();
                    if (result <= 3)
                    {
                        Console.WriteLine("A chill runs down your spine as you look at the crimson liquid within.");
                        player.inventory.Add(new Item() { itemName = "chilling potion", value = 20 });
                        player.location = "diningroom";
                        return;
                    }
                    Console.WriteLine("Looking at the warm liquid within the bottle awakens a feeling of vigor in you.");
                    player.inventory.Add(new Item() { itemName = "vigorous potion", value = 30 });
                    player.location = "diningroom";
                    break;
                case false:
                    player.location = "diningroom";
                    break;
            }
		}

        static void KitchenRoom()
        {
            int response = 0;
			Console.WriteLine("Entering the kitchen you find pots filled with more rotten food on various counters and piles of unwashed dishes.");
            do
            {
                response = int.Parse(AskQuestion("What would you like to do? (answer with the corresponding number)\n1.Investigate the kitchen.\n2.Return to the dining hall."));

            } while (response <= 0 || response > 2);
            switch (response)
            {
                case 1:
						player.location = "invkitchenroom";
                    break;
                case 2:
						player.location = "diningroom";
                    break;
            }
        }

		static void InvKitchenRoom()
        {
			if (knife)
			{
				Console.WriteLine("Searching the kitchen once more, you find nothing new.");
                player.location = "kitchenroom";
				return;
            }
            bool answer = (AskYesOrNo("Looking around the kitchen for a while you come across a cabinet holding a somewhat pristine knife.\nDo you wish to take it with you?"));
            switch (answer)
            {
                case true:
					
                    bool hasSword = false;
					
					if (player.inventory.Contains(new Item() { itemName = "sword" }))
                    {
                        hasSword = true;
                    }
                    if (hasSword)
                    {
                        bool answer2 = (AskYesOrNo("As the sword requires both hands to use it would be pointless for you to carry both it and the knife.\nWould you like to leave your sword in favour of taking the knife?"));
                        switch (answer2)
                        {
                            case true:
                                player.inventory.Remove(new Item { itemName = "sword"});
                                swordInKitchenRoom = true;
								break;
                            case false:
                                InvKitchenRoom();
								return;
                        }
                    }
                    player.inventory.Add(new Item() { itemName = "knife", value = 2 });
					knife = true;
                    player.location = "kitchenroom";
                    break;
                case false:
					player.location = "kitchenroom";
                    break;
            }
        }

        static void LockedDoor()
        {
			if (!usedKey)
			{
				Console.WriteLine("Moving to the door on your right and attempting to open it you find it locked.");

            if (key)
                {
                    bool answer = (AskYesOrNo("The key you picked up appears to fit into the door’s lock!\nDo you wish to use it?"));
                    switch (answer)
                    {
                        case true:
                            
								if (player.inventory.Contains(new Item() { itemName = "key" }))
								{
									player.inventory.Remove(new Item() { itemName = "key" });
									usedKey = true;
									Console.WriteLine("As you turn the key and unlock the door you hear a loud snap.\nRemoving the key from the door you find that the flimsy thing has snapped in half.\nHaving no use for a broken key you throw it away as you enter into the newly unlocked room.");
                                    player.location = "lockedroom";
										return;
								}
                            player.location = "dimroom";

                            break;
                        case false:
                            player.location = "dimroom";
                            break;
                    }
                }
            }
			else
			{
                player.location = "lockedroom";
            }
            player.location = "dimroom";
        }

        static void LockedRoom()
        {
            int response = 0;
            Console.WriteLine("");
            do
            {
                response = int.Parse(AskQuestion("Inside you find the remains of an old armory.\nEmpty shelves and tables covered in dust line the walls of the room.\nA large 1 is carved onto the wall in front of you.\nThe only things left here are a shield and long sword.\nWhat would you like to do? (answer with the corresponding number)\n1. Inspect the sword and shield.\n2. Return to the previous room."));

            } while (response <= 0 || response > 2);
            switch (response)
            {
                case 1:
                    player.location = "invlockedroom";
                    break;
                case 2:
                    player.location = "dimroom";
                    break;
            }
        }

        static void InvLockedRoom()
        {
			if (knifeInLockedRoom)
			{
				bool answer;
				answer = AskYesOrNo("You return to find the knife and shield.\nWould you like to leave the sword for the knife and shield?");
				switch (answer)
				{
					case true:
						
							if (player.inventory.Contains(new Item() { itemName = "sword" }))
							{
								player.inventory.Remove(new Item { itemName = "sword" });
							}
						
                        player.inventory.Add(new Item() { itemName = "knife", value = 2 });
                        player.inventory.Add(new Item() { itemName = "shield", value = 10 });
                        sword = false;
                        player.location = "lockedroom";

                        break;
                    case false:
						player.location = "lockedroom";
						break;
				}
			}
			else
			{
                int response = 0;
                do
                {
                    response = int.Parse(AskQuestion("Taking a closer look at the sword and shield you find that the sword would require both your hands to wield.\nYou can only take one of the two. Choose. (answer with the corresponding number)\n1. Take the long sword.\n2. Take the shield.\n3. Take Neither."));

                } while (response <= 0 || response > 3);
                switch (response)
                {
                    case 1:
                        bool hasKnife = false;
                        int knifePos = 0;
                        
                            if (player.inventory.Contains(new Item() { itemName = "knife" }))
                            {
                                hasKnife = true;
                            }
                        
                        if (hasKnife)
                        {
                            bool answer = (AskYesOrNo("As the sword requires both hands to use it would be pointless for you to carry both it and the knife.\nWould you like to leave your knife in favour of taking the sword?"));
                            switch (answer)
                            {
                                case true:
                                    player.inventory.Remove(new Item { itemName = "knife" });
                                    player.inventory.Add(new Item() { itemName = "sword", value = 4 });
                                    knifeInLockedRoom = true;
                                    InvLockedRoom();
                                    break;
                                case false:
                                    InvLockedRoom();
									return;
                            }
                        }
                        else
                        {
                            player.inventory.Add(new Item() { itemName = "sword", value = 4 });
                        }
						break;
                    case 2:
                        break;
                    case 3:
                        break;

                }
            }
						player.location = "lockedroom";

        }

        static void ChainRoom()
        {
            Console.WriteLine("Moving to the door in front of you, you find it unlocked.\nGoing through it you find yourself in yet another room, this one larger and more lit than the previous. The air in here is stale and filled with the faded stench of death.\nChains hang from the ceiling, some binding long dead corpses. The wall opposite of the way you entered has yet another door, this one made of rusted steel with a barred window on it.");
            int response = 0;
            do
            {
                response = int.Parse(AskQuestion("What would you like to do? (answer with the corresponding number)\n1.Investigate the chained corpses.\n2.Investigate the rusted door.\n3.Return to the previous room."));

            } while (response <= 0 || response > 3);
            switch (response)
            {
                case 1:
                    player.location = "invchainroom";
                    break;
                case 2:
                    int response2 = 0;
                    do
                    {
                        response2 = int.Parse(AskQuestion("You approach the rusted door and peek through the bars.\nOn the other side you see yet another room, in its center appears to be a person, hunched over and breathing heavily.\nUpon adjusting to the darkness of the room you get a clearer view of the figure. The clearer view lets you see its ragged clothes and rotting flesh, whatever this creature is it is not human"));

                    } while (response2 <= 0 || response2 > 2);
                    switch (response2)
                    {
                        case 1:
                            Console.WriteLine("As you open the door, the creature turns, its jaw unhinging as it unleashes a raspy screech before rushing at you, clawed hands raised!");
                            Combat(40, 2, 1, "Rotting Husk", false);
                            break;
                        case 2:
                            Console.WriteLine("The creature turns revealing its sunken, bloodshot eyes and peeling skin. It approaches the door and moves its clawed hand to the handle causing the rusty door to swing open.\nAs it does you so deliver a strike to the creature, knocking it back. The surprise momentarily stunning the creature before recovering, unleashing a bloodcurdling screech as it rushes you, clawed hands raised!");
                            Combat(30, 2, 1, "Rotting Husk", false);
                            break;
                    }
                    break;
                case 3:
                    player.location = "dimroom";
                    break;
            }
        }

        static void InvChainRoom()
        {
            Console.WriteLine("Investigating the corpses you find most of them having nothing on them but tattered cloth. But upon investigating further you find one corpse hunched over, as if hiding something.\nMoving it aside you find 3 buttons with a carving above the first and second buttons.\nThe first button is marked with what looks like a table, the second button is marked with a sword.");
            int response = 0;
            do
            {
                response = int.Parse(AskQuestion("What would you like to do? (answer with the corresponding number)\n1.Press the buttons.\n2. Back away from the buttons."));

            } while (response <= 0 || response > 2);
            switch (response)
            {
                case 1:
                    int response2;
                    
                    response2 = int.Parse(AskQuestion("Please enter the order in which you would like to enter them. (example: 123, 321, etc.)"));
                    if (response2 == 213)
                    { 
                        puzzleSolved = true;
                        Console.WriteLine("As you enter the buttons in the right order you hear something shift behind the rusty door.");
                        player.location = "chainroom";
                    }
                    else
                    {
                        Console.WriteLine("As you press the buttons, nothing happens.");
                        player.location = "chainroom";
                    }

                    break;
                case 2:
                    player.location = "chainroom";
                    break;
            }
        }

        static void Room()
        {
        
            int response = 0;
            do
            {
                response = int.Parse(AskQuestion(""));

            } while (response <= 0 || response > 4);
            switch (response)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }

		static void Combat(int enemyHp, int enemyStrength, int enemyDefense, string enemyName, bool isBoss)
		{
			currentEnemy = new Enemy() {playerName = player, enemyName = enemyName, hitPoints = enemyHp, strength = enemyStrength, defense = enemyDefense, isBoss = isBoss};
            int maxActions = 3;
            int result;
            do{
                do {
                    Console.WriteLine("Choose an action to take. (answer with the corresponding number)\n1.Attack the enemy.\n2.Take a defensive stance.\n3.Attempt to flee.");
                    if (potion && !usedPotion)
                    {
                        maxActions = 5;
                        Console.WriteLine("4.Drink your potion.\n5.Throw your potion.");
                    }
                    result = Console.ReadLine();
                } while (result >= 1 && result <= maxActions);

                switch(result)
                {
                    case 1:
                        currentEnemy.TakeDamage(player.strength * (new int= RollD6()))
                        currentEnemy.TakeAction();
                        break;
                    case 2:
                        player.defending = true;
                        enemy.TakeAction();
                        break;
                    case 3:
                        if(!currentEnemy.isBoss)
                        {
                            Console.WriteLine("You flee from the beast, leaving it behind as you return to where you started.");
                            player.location = "dimroom";
                            return;
                        }
                        else
                        {
                            Console.WriteLine("You attempt to flee from the robed figure, but as soon as you turn your back the foul thing has already moved to block your escape!");
                            currentEnemy.TakeAction();
                        }
                        break;
                    case 4:
                        maxActions = 3;
                        usedPotion = true;
                        if (player.inventory.Contains(new Item { itemName = "chilling potion" }))
                        {
                            player.TakeDamage(player.inventory.Find(new Item { itemName "chilling potion"}));
                        }
                        else
                        {

                        }
                            break;
                    case 5:
                        maxActions = 3;
                        usedPotion = true;
                        if (player.inventory.Contains(new Item { itemName = "chilling potion" }))
                        {
                            Enemy.TakeDamage(player.inventory.Find(new Item { itemName "chilling potion" }));

                        }
                        else
                        {

                        }
                        break;
                }
            } while (currentEnemy.hitPoints > 0 && player.hitPoints > 0) ;

        }

        static string AskQuestion(string question)
		{
			string response = "";

			do
			{
				Console.WriteLine(question);
				response = Console.ReadLine().Trim();
			} while (response == "");
			return response;
		}

		static bool AskYesOrNo(string question)
		{
			while(true)
			{
				string response = AskQuestion(question).ToLower();
				switch (response)
				{
					case "yes":
					case "ok":
						return true;
					case "no":
						return false;
				}
			}
		}

		static int RollD6()
		{
            return new Random().Next() % 6 + 1;
        }
	}
	public class Item
	{
		public string itemName;
		public int value;
	
	}
	public class Enemy
	{
		public Player playerName;
		public string enemyName;
		public int hitPoints;
		public int strength;
		public int defense;
		public bool defending = false;
		public bool isBoss;


        public void TakeAction(int action)
		{
			action = new Random().Next() % 2 + 1;

			switch (action)
			{
				case 1: //attack
				{
					Console.WriteLine($"{this.enemyName} attacks! {playerName} takes {this.strength} damage!");
					playerName.TakeDamage(this.strength);
					break;
				}
				case 2: //defend
				{
					Console.WriteLine($"{this.enemyName} raises its guard!");
					defending = true;
					break;
				}
			}
		}

		public void TakeDamage(int damage)
		{
			if (!defending)
			{
				this.hitPoints -= damage;
			}
			else
			{
				Console.WriteLine($"{this.enemyName} guards against your attack, reducing the damage it takes!");
				this.hitPoints -= damage - defense;
			}
			if (this.hitPoints <= 0)
			{
				Console.WriteLine($"{this.enemyName} is slain, {playerName} is victorious!");
			}
		}
	}

	public class Player
	{
		public string playerName;
		public int hitPoints = 100;
		public int hitPointsMax = 100;
		public int defence = 1;
		public int strength = 1;
		public string location;
		public List<Item> inventory = new List<Item>();

		public void TakeDamage(int damage)
		{
			this.hitPoints -= damage;
			
		}
        public void RecoverHealth(int healing)
        {
            this.hitPoints += healing;
            if (this.hitPoints > hitPointsMax)
            {
                this.hitPoints = hitPointsMax;
            }
        }
	} 
}
