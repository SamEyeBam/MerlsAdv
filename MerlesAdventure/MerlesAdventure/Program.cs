using System;
using System.Collections.Generic;
using System.Linq;

namespace MerlesAdventure
{
	class MainClass
	{
		static int manyMaps = 5; //n, manual set
		static int[,] mapSizes = new int[manyMaps,2]; //set size
		static string[][,] Maps = new string[manyMaps][,];//set size
		static bool[] stageDone = new bool[10];
		static Random rnd = new Random();



		static int currentMap = 2; //


		public class cPlayer:objectParent {
			//public string name;
			public int playerx = 5;
			public int playery = 2;
			public string[] inventory = new string[10];


			public cPlayer(string nm){
				name = nm;
				mhealth = 20; //temp
				health = mhealth;
				weapon = "hands";
				attack = 1;
				defence = 2;
				speed = 2;

				inventory[0] = "Encrypted letter";
				inventory[1] = "";
				inventory[2] = "";
				inventory[3] = "";
				inventory[4] = "";
				inventory[5] = "";
				inventory[6] = "";
				inventory[7] = "";
				inventory[8] = "";
				inventory[9] = "";
			}
			public void moveUp(){
				if (Maps[currentMap][this.playerx, this.playery-1] == ".") {
					Maps[currentMap][this.playerx, this.playery-1] = "P";
					Maps[currentMap][this.playerx, this.playery] = ".";
					this.playery -= 1;
				}
			}
			public void moveDown(){
				if (Maps[currentMap][this.playerx, this.playery+1] == ".") {
					Maps[currentMap][this.playerx, this.playery+1] = "P";
					Maps[currentMap][this.playerx, this.playery] = ".";
					this.playery += 1;
				}
			}
			public void moveLeft(){
				if (Maps[currentMap][this.playerx-1, this.playery] == ".") {
					Maps[currentMap][this.playerx-1, this.playery] = "P";
					Maps[currentMap][this.playerx, this.playery] = ".";
					this.playerx -= 1;
				}
			}
			public void moveRight(){
				if (Maps[currentMap][this.playerx+1, this.playery] == ".") {
					Maps[currentMap][this.playerx+1, this.playery] = "P";
					Maps[currentMap][this.playerx, this.playery] = ".";
					this.playerx += 1;
				}
			}
			public void OpenLetter(){
				string letterText = System.IO.File.ReadAllText("/home/pc/CS/Test/TestCs/Other/Letter");
				Console.Clear();
				Console.WriteLine(letterText);
			}
			public void disInventory(){
				Console.Clear();
				for (int i = 0; i < inventory.Length; i++) {
					Console.WriteLine ("[{0}]{1}", i, inventory [i]);	
				}
			}
			public void SayHi()
			{
				Console.WriteLine("Hi");
			}
		}

		public class objectParent {
			public int x;
			public int y;
			public int map;
			public string name;
			public string type;
			public int mhealth;
			public int health;
			public int attack;
			public int defence;
			public int speed;
			public string weapon;
			public bool battle = false;
			public InventorySlot[] slot;
			public int size;
			//public string description;
			public virtual void Talk(){
			}
			public virtual void Enter(cPlayer p){
			}
			public class InventorySlot{
				public string type = "";
				public bool stackable = false;
				public string name = "";
				public int quantity = 0;
				public string description;

				public InventorySlot(){
					//Console.WriteLine(this.name);
				}
			}

		}

		public class objectCharacter : objectParent {
			public string TalkHello;
			public string TalkOp0;
			public string TalkOp1;
			public string TalkRp0;
			public string TalkRp1;

			public objectCharacter(string nm = "Default Name") {
				name = nm;
				type = "char";
				TalkHello = "Hello Merle";
				TalkOp0 = "Hello " + name + "!";
				TalkOp1 = "Fuck off";
				TalkRp0 = "Hello Merle";
				TalkRp1 = "Well thats fucking rude";
			}
			public override void Talk(){
				Console.WriteLine("[{0}] {1}", name, TalkHello);
				Console.WriteLine("[0] {0}", TalkOp0);
				Console.WriteLine("[1] {0}", TalkOp1);
				Console.WriteLine("[2] Fight");
				Console.WriteLine("");
				Console.Write("> ");
				string input = Console.ReadLine();
				input.ToLower(); //formats input
				switch (input){
				case "0":
					{
						Console.WriteLine("[{0}] {1}", name, TalkRp0);
						break;
					}
				case "1":
					{
						Console.WriteLine("[{0}] {1}", name, TalkRp1);
						break;
					}
				case "2":
					{
						this.battle = true;
						break;
					}
				default:
					{
						Console.WriteLine("Unknown command");
						break;
					}
				}
			}
			public virtual void Speak(){
				Console.WriteLine("Hello");
			}
		}
		public class objectBen : objectCharacter {

			public objectBen(string wep) {
				name = "Ben";
				mhealth = 6;
				health = mhealth;
				attack = 1;
				defence = 1;
				speed = 1;
				weapon = wep;//TODO assign from creation using weaponData. like in this example
				//TODO create debug tool for finding weapon index (that is if thats how i plan to
				//assign weapons/items

				TalkHello = "Oh hello Merle";
				TalkOp0 = "Morning " + name + "!";
				TalkOp1 = "Fuck your birds";
				TalkRp0 = "Ha Stacey wake you up with a little bit of hmm hmm";
				TalkRp1 = "Well thats fucking rude";
				map = 3;
				x = 4;
				y = 1;
			}
			public override void Speak(){
				Console.WriteLine("NOPE");
			}
		}

		public class objectSam : objectCharacter {

			public objectSam() {
				name = "Sam";
				mhealth = 10;
				health = mhealth;
				attack = 2;
				defence = 2;
				speed = 3;
				weapon = "vape";

				TalkHello = "Morning Merle!";
				TalkOp0 = "Morning " + name + "!";
				TalkOp1 = "Fuck you";
				TalkRp0 = "Done the dishes too btw";
				TalkRp1 = "Hahaha fuck you too Merle";
				map = 4;
				x = 12;
				y = 2;
			}
			public override void Speak(){
				Console.WriteLine("NOPE");
			}
		}

		public class objDoor : objectParent{
			public int tox;
			public int toy;
			public int tomap;

			public objDoor(int intx, int inty, int intmap, int inttox, int inttoy, int inttomap){
				type = "door";
				x = intx;
				y = inty;
				map = intmap;
				tox = inttox;
				toy = inttoy;
				tomap = inttomap;
			}
			public override void Enter(cPlayer p){
				Maps[currentMap][p.playerx, p.playery] = ".";
				currentMap = tomap; 
				p.playerx = tox;
				p.playery = toy;
				Maps[currentMap][p.playerx, p.playery] = "P";
			}
		}

		public class Weapon {
			public string name;
			public string type = "wep";
			public int minDmg;
			public int maxDmg;
			//public string description = "";

			public Weapon(string nm, int minD, int maxD){
				name = nm;
				minDmg = minD;
				maxDmg = maxD;
			}
			public int Damage (){
				return rnd.Next (minDmg, maxDmg + 1);
			}
		}

		public class Item {
			public string name = "";
			public string type = "";
			public bool stackable = false;
			public string description = "";
//			public int quantity = 0;

			public Item(string nm, string tpe, bool stck, string desc){
				name = nm;
				type = tpe;
				stackable = stck;
				description = desc;
			}

		}

		public class InventoryContainer : objectParent {

			//public string name;
			//public int size;
			//public InventorySlot[] slot;
			public InventoryContainer(string nm,int sz,int xx,int yy,int intmap) {
				//constructor
				name = nm;
				type = "cont";
				size = sz;
				x = xx;
				y = yy;
				map = intmap;


				slot = new InventorySlot[size]; 
				for (int i=0;i<size;i++){
					slot[i] = new InventorySlot();
				}
				//TODO inventory slots need to be able to contain items plus weapons
			}

			//	public class InventorySlot{
			//		public string name = "";
			//		public string type = "";
			//		public bool stackable = false;
			//		public int quantity = 0;
			//
			//		public InventorySlot(){
			//			//Console.WriteLine(this.name);
			//		}
			//	}

		}


		public static void Main (string[] args)
		{
			//-------------START----------------//
			int objSize = 9; //manual set TODO change to objArray.Count;
			//Random rnd = new Random();
			List<Weapon> weaponData = new List<Weapon>();
			weaponData.Add (new Weapon ("hands", 1, 2));
			weaponData.Add (new Weapon ("keyboard", 2, 4));
			weaponData.Add (new Weapon ("spoon", 1, 3));
			weaponData.Add (new Weapon ("vape", 2, 2));

			List<Item> itemData = new List<Item>();
			itemData.Add (new Item ("hands","wep",false, "These are hands"));
			itemData.Add (new Item ("keyboard", "wep", false, "The keyboard has jizz in it"));
			itemData.Add (new Item ("spoon", "wep", false, "Just a silver spoon"));
			itemData.Add (new Item ("vape", "wep", false, "Mad vape brahh"));
			itemData.Add (new Item ("Paper", "itm", true, "Just blank paper"));
			itemData.Add (new Item ("Feather", "itm", true,"Feather from some dead bird"));

			List<objectParent> objArray = new List<objectParent>();
			//objectBen objBen = new objectBen();
			objectSam objSam = new objectSam();

			objArray.Add(new objectBen(weaponData[1].name));//spoon
			objArray.Add(objSam);
			objArray.Add(new objDoor(6,1,2,1,1,3));
			objArray.Add(new objDoor(0,1,3,5,1,2));
			objArray.Add(new objDoor(4,3,3,7,4,4));
			objArray.Add(new objDoor(5,3,3,7,4,4));
			objArray.Add(new objDoor(7,3,4,5,3,2));
			objArray.Add(new InventoryContainer("Inventory",10,0,0,0));
			objArray.Add(new InventoryContainer("testInv",5,1,3,3));


			cPlayer Player = new cPlayer("Merle"); //creates player
			for (int x = 0; x < stageDone.Length; x++){ //set stages to false
				stageDone[x] = false;
			};
			int tempInt = 0;
			string tempString = "0";
			bool tempBool = false;



			//Load in map files to Maps[][,] array
			void loadInMaps(){
				for(int x = 0;x <= manyMaps-1;x++)
				{
					string loc = "/home/pc/CS/MerleProject/MerlesAdventure/Maps/Map" + System.Convert.ToString (x);
					string fileContents = System.IO.File.ReadAllText(loc);
					fileContents = fileContents.Replace(System.Environment.NewLine, ""); //removes NL

					mapSizes [x,0] = System.Convert.ToInt32(fileContents.Substring (0, 2)); //places in the x
					mapSizes [x,1] = System.Convert.ToInt32(fileContents.Substring (2, 2)); //places in the y

					Maps[x] = new string[mapSizes[x,0],mapSizes[x,1]];//initialze map (sets the array size)
					int tempPosCounter = 4;
					for(int y=0;y< mapSizes[x,1] ;y++){ //y
						for(int j=0;j < mapSizes[x,0];j++){ //x
							Maps[x][j,y] = System.Convert.ToString(fileContents[tempPosCounter]); //places char into map
							tempPosCounter +=1;
						}
						//next loop
					}
				}
			}

			//Display Map
			void displayMap(int x){ 
				Console.Clear();
				for(int y=0;y< mapSizes[x,1] ;y++){ //y
					for(int j=0;j < mapSizes[x,0];j++){ //x
						Console.Write(Maps[x][j,y]);
						/*if ((y == 0) && (j == (mapSizes[x,0]) - 1)){
							Console.Write(" [{0},{1}]", Player.playerx,Player.playery);
						}*/
					}
					Console.WriteLine("");
				}
				Console.SetCursorPosition(mapSizes[x,0],0);
				Console.Write(" Hth:[{0},{1}]", Player.health,Player.mhealth);
				Console.SetCursorPosition(mapSizes[x,0],1);
				Console.Write(" Wep:[{0}]", Player.weapon);
				Console.SetCursorPosition(mapSizes[x,0],2);
				Console.Write(" Pos:[{0},{1}]", Player.playerx,Player.playery);
				Console.SetCursorPosition(0,mapSizes[x,1]);
			}


			void envInteract(int xx, int yy){ // doesnt check for current map
				for (int j = 0; j < objSize;j++){
					if (objArray[j].map == currentMap) {
						if ((Player.playerx + xx == objArray[j].x) && (Player.playery + yy == objArray[j].y)){ //check player x + direction = objects y

							if (objArray[j].type == "char") {
								objArray[j].Talk();
								if (objArray[j].battle == true){
									Battle(j);
									displayMap(currentMap);
								}
							}
							else if (objArray[j].type == "door") {
								objArray[j].Enter(Player);
								displayMap(currentMap);
							}
							else if (objArray[j].type == "cont") {
								ContainerUI(j);
							}
						}
					}
				}
			}

			void Battle(int j){
				objectParent other = objArray[j];
				int disy = 7;
				bool canAttack = false;
				Console.Clear();
				objectParent[] Person = new objectParent[2]{Player,other};

				void clearMid(int iFrom, int iTo){

					for (int n = iFrom;n <= iTo;n++){
						Console.SetCursorPosition(0,n);
						for (int i = 0;i < Console.WindowWidth; i++){
							Console.Write(" ");
						}
					}
				}
				void HealthBar(int xx,int yy, bool bPlayer){
					//Console.WriteLine(Person[0].name); remove
					if (bPlayer == true){
						tempInt = (Person[0].health* 10)/Person[0].mhealth; //never let the number become decimal
					}
					else {
						tempInt = (other.health* 10)/other.mhealth;
					}
					Console.SetCursorPosition(xx,yy);
					Console.Write("[");
					for (int i = 0; i < tempInt; i++) {
						Console.Write("#");//HealthBar!!
					}
					for (int i = 0; i < 10 - tempInt; i++) {
						Console.Write("-");
					}
					Console.Write("]");
					Console.SetCursorPosition(xx,yy+1);

				}
				void draw(){
					int drwx = 13;
					clearMid(0,disy-1);
					tempInt = (Player.health* 10)/Player.mhealth; //never let the number become decimal
					var wepTmp = weaponData.First(item => item.name == Person[0].weapon);
					Console.SetCursorPosition(0,0);
					Console.WriteLine("[{0}]",Player.name);
					HealthBar(0,1,true);
					Console.WriteLine("Hth:{0}/{1}",Player.health,Player.mhealth);
					Console.WriteLine("Wep:{0}",Player.weapon);
					Console.WriteLine("Atk:{0}+[{1}-{2}]",Player.attack,wepTmp.minDmg,wepTmp.maxDmg);
					Console.WriteLine("Def:{0}",Person[0].defence);
					Console.WriteLine("Spd:{0}",Person[0].speed);


					Console.SetCursorPosition(13,0); //enemy draw
					wepTmp = weaponData.First(item => item.name == Person[1].weapon);
					Console.Write("| [{0}]",Person[1].name);
					HealthBar(13,1,false);
					Console.SetCursorPosition(13,2);
					Console.WriteLine("| Hth:{0}/{1}",Person[1].health,Person[1].mhealth);
					Console.SetCursorPosition(13,3);
					Console.WriteLine("| Wep:{0}",Person[1].weapon);
					Console.SetCursorPosition(13,4);
					Console.WriteLine("| Atk:{0}+[{1}-{2}]",Person[1].attack,wepTmp.minDmg,wepTmp.maxDmg);
					Console.SetCursorPosition(drwx,5);
					Console.WriteLine("| Def:{0}",Person[1].defence);
					Console.SetCursorPosition(drwx,6);
					Console.WriteLine("| Spd:{0}",Person[1].speed);
				}

				void atkRound(){
					void personAttack(int l,int t){ //0 for player 1 for other, t for disy placement

						if (rnd.Next(0,100) > 79){
							canAttack = true;
						}

						if (canAttack == true){
							int wepDm = weaponData.First(item => item.name == Person[l].weapon).Damage();
							int totalDamage = Person[0+l].attack + wepDm - Person[1-l].defence; //damage equasion
							Person[1-l].health -= totalDamage; //TODO if defence is high enough, damage adds health

							draw();
							Console.SetCursorPosition(0,disy+1+t);
							Console.WriteLine("[{0}]Hit {1} with [{2}] for {3} damage",
								Person[l].name,Person[1-l].name,Person[l].weapon,totalDamage);
						}
						else {
							Console.SetCursorPosition(0,disy+1+t);
							Console.WriteLine("[{0}]Missed!",Person[l].name);
						}
					}
					//check if Players is dead
					bool ckIfDead(){
						Console.SetCursorPosition(0,disy+3);
						if (Person[0].health <= 0){
							Console.WriteLine("You Died!"); //---Player dies---
							other.battle = false; //TODO remove this
							Console.SetCursorPosition(0,disy+7);
							return true;
						}
						else if (Person[1].health <= 0){
							Console.WriteLine("You killed Him/Her!"); // other dies
							other.battle = false;
							Maps[currentMap][other.x,other.y] = ".";
							other.x = 0;
							other.y = 0;
							Console.SetCursorPosition(0,disy+7);
							Console.Write("Press any ENTER to continue..");
							Console.Read();
							return true;
						}
						else{
							return false;
						}
					}


					//Determine who attacks first
					int firstToAtk;
					if(Player.speed >= other.speed){
						firstToAtk = 0;
					}
					else {
						firstToAtk = 1;
					}

					personAttack(0+firstToAtk,0);// person, disy placement
					tempBool = ckIfDead(); //checks if person killed other, if not then runs method for other person to hit
					if (!tempBool){
						personAttack(1-firstToAtk,1);
						ckIfDead();
					}

				}

				void usrInput(){
					Console.SetCursorPosition(0,disy+4);
					Console.WriteLine("[0]Attack");
					Console.WriteLine("[1]Block"); //TODO
					Console.WriteLine("[2]Potion"); //TODO
					Console.Write("> ");
					string input = Console.ReadLine();
					clearMid(disy,disy+3); //battle narration
					clearMid(disy+7,disy+7); //command line
					switch(input) {
					case "0":
						{
							atkRound();
							break;
						}
					case "1":
						{

							break;
						}
					default:
						{
							Console.WriteLine("Unknown Command");
							break;
						}
					}
				}
				while(other.battle){
					draw();
					usrInput();

				}

			}

			void ContainerItemAdd(int C, string itemNm, int amt = 1){
				//C = container (if 666 then player), itemId reference item database, amt = quantity 1 being default
				for (int i = 0; i < objArray[C].size; i++){
					if (objArray[C].slot[i].name == "") {

						int itemId = itemData.FindIndex(item => item.name == itemNm);

						objArray[C].slot[i].name = itemData[itemId].name;
						objArray[C].slot[i].type = itemData[itemId].type;
						objArray[C].slot[i].stackable = itemData[itemId].stackable;
						objArray[C].slot[i].quantity = amt;
						objArray[C].slot[i].description = itemData[itemId].description;

						break;
					}

				}

			}
			void ContainerItemDestroy(int C, int ind){
				//C = container (666 for player),ind = index of container
				objArray[C].slot[ind].name = "";
				objArray[C].slot[ind].type = "";
				objArray[C].slot[ind].stackable = false;
				objArray[C].slot[ind].quantity = 0;
				objArray[C].slot[ind].description = "";
			}
			void ContainerDisplay(int C){
				//C = container
				//Console.Clear();
				for (int i = 0; i < objArray[C].size; i++) {
					Console.Write("[{0}] {1}",i+1,objArray[C].slot[i].name);

					if (objArray[C].slot[i].stackable == true){
						Console.Write(" ({0})", objArray[C].slot[i].quantity);
					}
					Console.WriteLine("");

				}
			}
			void ContainerSort(int C){
				//C = container
				void Swap(int fst, int snd){
					//name
					tempString = objArray[C].slot[fst].name;
					objArray[C].slot[fst].name = objArray[C].slot[snd].name;
					objArray[C].slot[snd].name = tempString;
					//type
					tempString = objArray[C].slot[fst].type;
					objArray[C].slot[fst].type = objArray[C].slot[snd].type;
					objArray[C].slot[snd].type = tempString;
					//stackable
					tempBool = objArray[C].slot[fst].stackable;
					objArray[C].slot[fst].stackable = objArray[C].slot[snd].stackable;
					objArray[C].slot[snd].stackable = tempBool;
					//quantity
					tempInt = objArray[C].slot[fst].quantity;
					objArray[C].slot[fst].quantity = objArray[C].slot[snd].quantity;
					objArray[C].slot[snd].quantity = tempInt;
					//description
					tempString = objArray[C].slot[fst].description;
					objArray[C].slot[fst].description = objArray[C].slot[snd].description;
					objArray[C].slot[snd].description = tempString;
				}
					
				for (int j = 0;j < objArray[C].size; j++){
					for (int i = 0;i < objArray[C].size - 1; i++){
						if (objArray[C].slot[i].name == ""){
							Swap(i,i+1);
						}
					}
				}
			
			}
			void ContainerItemTransfer(bool T, int C, int fromInd){
				//T = true if from player inv to container, C = container, fromInd = index
				int otherC;
				if (T == true) {
					otherC = C;
					C = objArray.FindIndex(item => item.name == "Inventory");
				}
				else {
					otherC = objArray.FindIndex(item => item.name == "Inventory");
				}

				if ((fromInd >= 0) 
				&& (fromInd < objArray[C].size)
				&& (objArray[C].slot[fromInd].name != "")){
					for (int i = 0;i < objArray[otherC].size; i++){
						if (objArray[otherC].slot[i].name == ""){
							string itemNm = objArray[C].slot[fromInd].name;
							int itemAmt = objArray[C].slot[fromInd].quantity;
							ContainerItemAdd(otherC,itemNm,itemAmt);
							ContainerItemDestroy(C,fromInd);
							ContainerSort(C);
							break;
						}
					}
				}
			
			}

			string ContainerItemInspect(int C,int ind){
				Console.WriteLine("[{0}] {1}", objArray[C].slot[ind].name, objArray[C].slot[ind].description);
				return objArray[C].slot[ind].description;
			}

			void ItemSwitch(){
			}

			void ContainerUI(int Cont){
				bool done = false;
				int userInvInd = objArray.FindIndex(item => item.name == "Inventory");
				void Draw(){
					Console.WriteLine(objArray[Cont].name);
					ContainerDisplay(Cont);
					Console.WriteLine("");

					Console.WriteLine(objArray[userInvInd].name);
					ContainerDisplay(userInvInd);
					Console.WriteLine("");
				}

				void usrInput(){
					Console.WriteLine("[0]Take");
					Console.WriteLine("[1]Put In");
					Console.WriteLine("[2]Destroy");
					Console.WriteLine("[3]Leave");
					Console.WriteLine("[4]Switch (?)");
					Console.Write("> ");
					string input = Console.ReadLine();
					switch(input) {
					case "0":
						{
							Console.Write("slot> ");
							string tempInput = Console.ReadLine();
							tempBool = Int32.TryParse(tempInput, out tempInt);
							if (tempBool == true){
								ContainerItemTransfer(false,Cont,tempInt-1);
							}
							break;
						}
					case "1":
						{
							Console.Write("slot> ");
							string tempInput = Console.ReadLine();
							tempBool = Int32.TryParse(tempInput, out tempInt);
							if (tempBool == true){
								ContainerItemTransfer(true,Cont,tempInt-1);
							}
							break;
						}
					case "2":
						{

							break;
						}
					case "3":
						{
							done = true;
							Console.Clear();
							displayMap(currentMap);
							break;
						}
					default:
						{
							Console.WriteLine("Unknown Command");
							break;
						}
					}
				}

				while (!done){
					Console.Clear();
					Draw();
					usrInput();
				}

			}

			void TestRoom(){
				Console.Clear();
				//				InventoryContainer testInv = new InventoryContainer("testInv",5,0,0,0);
				//				testInv.slot[0].name = "Paper";

				int tempInt9 = objArray.FindIndex(item => item.name == "testInv");
				ContainerItemAdd(tempInt9,"vape");
				ContainerItemAdd(tempInt9,"hands");
				ContainerItemAdd(tempInt9,"Paper", 4);
				ContainerItemAdd(tempInt9,"Feather", 7);
				ContainerItemAdd(tempInt9,"spoon");
				ContainerItemDestroy(tempInt9,0);
				ContainerSort(tempInt9);
				ContainerDisplay(tempInt9);
				ContainerItemInspect(tempInt9,0);
				Console.WriteLine(objArray[tempInt9].slot[0].description);

			}

			void userInput(){ //TODO fix bug for if input less then .length of 2
				Console.WriteLine("");
				Console.Write("> ");
				string input = Console.ReadLine();
				input.ToLower(); //formats input

				switch (input) {
				case "show letter":
				case "display letter":
					{
						Player.OpenLetter();
						break;
					}
				case "   ":
					{
						TestRoom();
						break;
					}
				case "restore health":
					{
						Player.health = Player.mhealth;
						break;
					}
				case "interact left":
				case "il":
					{
						envInteract(-1,0);
						break;
					}
				case "interact right":
				case "ir":
					{
						envInteract(1,0);
						break;
					}
				case "interact up":
				case "iu":
					{
						envInteract(0,-1);
						break;
					}
				case "interact down":
				case "id":
					{
						envInteract(0,1);
						break;
					}
				case "move up":
				case "mu":
					{
						Player.moveUp();
						displayMap(currentMap);
						break;
					}
				case "move down":
				case "md":
					{
						Player.moveDown();
						displayMap(currentMap);
						break;
					}
				case "move left":
				case "ml":
					{
						Player.moveLeft();
						displayMap(currentMap);
						break;
					}
				case "move right":
				case "mr":
					{
						Player.moveRight();
						displayMap(currentMap);
						break;
					}
				case "show map":
				case "display map":
				case "map":

					{
						displayMap(currentMap);
						break;
					}
				case "show inventory":
				case "display inventory":
				case "inventory":
					{
						Console.Clear();
						Console.WriteLine("Inventory");
						ContainerDisplay(objArray.FindIndex(item => item.name == "Inventory"));
						break;
					}
				case "": //change
					{
						displayMap(currentMap);
						break;
					}
				default:
					{

						if ((input.Length > 4) && (input.Substring(0,3) == "map")) { //show different maps
							if (Convert.ToInt32(input.Substring(4)) < manyMaps){
								displayMap(Convert.ToInt32(input.Substring(4)));
							}
							else{
								Console.WriteLine("That map doesnt exist");
							} 
						}
						else if ((input.Substring(0,2) == "ml") //check if substrings equal any of the commands
							|| (input.Substring(0,2) == "mr") //then remove user error by testing if theres a number
							|| (input.Substring(0,2) == "mu") //by trying to convert last char/s to int
							|| (input.Substring(0,2) == "md") //then runs the command if theyre equal to any command
							&& (input.Length > 3)) { //move left
							tempBool = Int32.TryParse(input.Substring(3), out tempInt);
							if (tempBool == true){
								for (int x = 0; x < Convert.ToInt32(input.Substring(3)); x++){
									if (input.Substring(0,2) == "ml") {
										Player.moveLeft();
									}
									else if ((input.Substring(0,2) == "mr")){
										Player.moveRight();
									}
									else if ((input.Substring(0,2) == "mu")){
										Player.moveUp();
									}
									else if ((input.Substring(0,2) == "md")){
										Player.moveDown();
									}
									else {
										Console.WriteLine("Error input.substring if statement");
									}
								}
								displayMap(currentMap);
							}
						}
						else if (input == "help") {
							//help method
						}
						else {
							Console.WriteLine("Unknown command");
						}

						break;
					}
				}

			}
			//--------------------------------------------------------------------//
			loadInMaps();
			Console.WriteLine("Welcome");

			while(true){
				userInput();
			}

			while(stageDone[0] == false){
				//code for stage 0 of the game
			};


			/*C  onsole.WriteLine ("Merle Is Awesome!");
			Console.SetCursorPosition (4, 0);
			System.Threading.Thread.Sleep(2000);
			Console.Write ("Y");
			*/
		}
	}
}
