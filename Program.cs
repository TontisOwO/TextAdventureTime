namespace TextAdventure
{
	using System.Collections.Generic;
	
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(
				"You awake, finding yourself alone in a dark, damp chamber.\nThe only light coming from the cinders of a dying torch on the opposite side of the room.\nYour memory is foggy… What was your name?");

			string playerName = Console.ReadLine();
			Console.WriteLine(
				$"Ah, of course, {playerName}, how could you forget?\nGetting a better look at the room you find that it has 3 doors, each made of rotting wood.\nOne door in front of you, one on the left wall and the last on the right wall.\n\nWhat would you like to do? (answer with the corresponding number)\n1. Investigate the room further.\n2. Attempt to go left.\n3. Attempt to go right.\n4. Attempt to go forward."
			);
			int[] action = new int[0];

			int currentAction = 0;
			bool done = false;
			while (true)
			{
				action = AddLength(action);
				
				action[currentAction] = int.Parse(Console.ReadLine());
				switch (action[currentAction])
				{
					case 1:
						break;
					case 2:
						break;
					case 3:
						break;
					case 4:
						done = true;
						break;
				}

				currentAction++;
				if (done) break;
			}
		}

		static int[] AddLength(int[] action)
		{
			int[] newArray = new int[action.Length + 1];
			for (int i = 0; i < action.Length; i++)
			{
				newArray[i] = action[i];
			} 
			return newArray;
		}
	}
	public class Item
	{
		public string itemName;
		public bool inInventory;
	
	}
	class Enemy
	{
		private Player playerName;
		public string enemyName;
		public int hitPoints;
		public int strength;
		public int defense;
		public bool defending = false;

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
		private Enemy enemyName;
		public string playerName;
		public int hitPoints = 10;
		public int defence = 1;
		public int strength = 1;
		public string location;
		public List<Item> inventory = new List<Item>();

		public void TakeDamage(int damage)
		{
			this.hitPoints -= damage;
			if (this.hitPoints <= 0)
			{
				Console.WriteLine($"{this.playerName} was killed by {enemyName}.");
			}
		}
	} 
}
