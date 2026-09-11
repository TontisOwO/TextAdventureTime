namespace TextAdventureTime
{
	using System.Collections.Generic
		
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

	public class Player
	{
	public string playerName;
	public int hitPoints = 10;
	public List<Item> inventory;
	}
}
