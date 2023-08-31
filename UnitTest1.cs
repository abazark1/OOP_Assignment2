using assign2_HZ4BV8;
using TextFile;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;

namespace UnitTestPet
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestConstructors()
		{
			string name = "Bojack";
			int exhiliration = 40;

			Tarantula t1 = new Tarantula(name, exhiliration);
			Hamster h1 = new Hamster(name, exhiliration);
			Cat c1 = new Cat(name, exhiliration);

			Assert.AreEqual(t1.GetName(), "Bojack");
			Assert.AreEqual(h1.GetName(), "Bojack");
			Assert.AreEqual(c1.GetName(), "Bojack");

			Assert.AreEqual(t1.GetExhiliration(), exhiliration);
			Assert.AreEqual(h1.GetExhiliration(), exhiliration);
			Assert.AreEqual(c1.GetExhiliration(), exhiliration);

			Assert.AreEqual(t1.ExhLevel(1), 41);
			Assert.AreEqual(h1.ExhLevel(2), 42);
			Assert.AreEqual(c1.ExhLevel(3), 43);
		}

		[TestMethod]
		public void Test_ZeroPetMood()
		{
			List<Pet> pets = new();
			List<IMood> moods = new();

			foreach(var p in pets)
			{
				List<(string, int)> result = p.MaxSearch(ref moods, ref pets);
				Assert.AreEqual(("", 0), result);
				break;
			}

			Assert.AreEqual(0, pets.Count);
			Assert.AreEqual(0, moods.Count);
		}

		[TestMethod]
		public void Test_GetMatrix()
		{
			List<Pet> pets = new();
			List<IMood> moods = new();

			pets.Add(new Tarantula("Webster", 10));
			pets.Add(new Hamster("Butterscotch", 10));
			pets.Add(new Cat("Cat-man-do", 10));

			moods.Add(Blue.Instance());
			moods.Add(Usual.Instance());
			moods.Add(Joyful.Instance());


			List<List<(string, int)>> matrix = new();
			
			foreach (var p in pets)
			{

				matrix = p.GetMatrix(ref moods, ref pets);
				Assert.IsTrue(p.AllHappy(ref pets));
				Assert.AreEqual(p.IncreaseMood(ref pets, moods[0]), Usual.Instance());
				Assert.AreEqual(p.IncreaseMood(ref pets, moods[1]), Joyful.Instance());
				Assert.AreEqual(p.IncreaseMood(ref pets, moods[2]), Joyful.Instance());
				break;
			}
			
			Assert.AreEqual(3, matrix[0].Count);
			Assert.AreEqual(("Webster", 8), matrix[0][0]);
			Assert.AreEqual(("Webster", 9), matrix[1][0]);
			Assert.AreEqual(("Webster", 10), matrix[2][0]);

			Assert.AreEqual(3, matrix[1].Count);
			Assert.AreEqual(("Butterscotch", 7), matrix[0][1]);
			Assert.AreEqual(("Butterscotch", 9), matrix[1][1]);
			Assert.AreEqual(("Butterscotch", 11), matrix[2][1]);

			Assert.AreEqual(3, matrix[2].Count);
			Assert.AreEqual(("Cat-man-do", 13), matrix[0][2]);
			Assert.AreEqual(("Cat-man-do", 16), matrix[1][2]);
			Assert.AreEqual(("Cat-man-do", 19), matrix[2][2]);
		}

		[TestMethod]
		public void Test_Blue()
		{
			FileHandle f = new FileHandle();
			List<Pet> pets1 = f.PopulatePets("bluemood.txt");
			List<IMood> moods1 = f.PopulateMood("bluemood.txt");

			Assert.AreEqual(pets1.Count, 3);
			Assert.AreEqual(moods1.Count, 3);

			List<(string, int)> result = new();

			foreach (var p in pets1)
			{
				Assert.IsTrue(p.AllHappy(ref pets1));
				Assert.AreEqual(p.IncreaseMood(ref pets1, moods1[0]), Usual.Instance());
				Assert.AreEqual(p.IncreaseMood(ref pets1, moods1[1]), Usual.Instance());
				result = p.MaxSearch(ref moods1, ref pets1);
				Assert.AreEqual(p.IncreaseMood(ref pets1, moods1[2]), Blue.Instance());
				break;
			}

			Assert.AreEqual(result.Count, 3);
			Assert.AreEqual(("Cat-man-do", 13), result[0]);
			Assert.AreEqual(("Cat-man-do", 16), result[1]);
			Assert.AreEqual(("Cat-man-do", 9), result.Last());
		}

		[TestMethod]
		public void Test_Usual()
		{
			FileHandle f = new FileHandle();
			List<Pet> pets1 = f.PopulatePets("usualmood.txt");
			List<IMood> moods1 = f.PopulateMood("usualmood.txt");

			Assert.AreEqual(pets1.Count, 3);
			Assert.AreEqual(moods1.Count, 3);

			List<(string, int)> result = new();

			foreach (var p in pets1)
			{
				result = p.MaxSearch(ref moods1, ref pets1);
				Assert.IsFalse(p.AllHappy(ref pets1));
				Assert.AreEqual(p.IncreaseMood(ref pets1, moods1[0]), Usual.Instance());
				Assert.AreEqual(p.IncreaseMood(ref pets1, moods1[1]), Usual.Instance());
				Assert.AreEqual(p.IncreaseMood(ref pets1, moods1[2]), Usual.Instance());
				break;
			}

			Assert.AreEqual(result.Count, 3);
			Assert.AreEqual(("Webster", 7), result[0]);
			Assert.AreEqual(("Cat-man-do", 9), result[1]);
			Assert.AreEqual(("Cat-man-do", 12), result.Last());
		}

		[TestMethod]
		public void Test_Joyful()
		{
			FileHandle f = new FileHandle();
			List<Pet> pets1 = f.PopulatePets("joymood.txt");
			List<IMood> moods1 = f.PopulateMood("joymood.txt");

			Assert.AreEqual(pets1.Count, 3);
			Assert.AreEqual(moods1.Count, 3);

			List<(string, int)> result = new();

			foreach (var p in pets1)
			{
				result = p.MaxSearch(ref moods1, ref pets1);
				Assert.IsTrue(p.AllHappy(ref pets1));
				Assert.AreEqual(p.IncreaseMood(ref pets1, moods1[0]), Joyful.Instance());
				Assert.AreEqual(p.IncreaseMood(ref pets1, moods1[1]), Joyful.Instance());
				Assert.AreEqual(p.IncreaseMood(ref pets1, moods1[2]), Joyful.Instance());
				break;
			}

			Assert.AreEqual(result.Count, 3);
			Assert.AreEqual(("Webster", 10), result[0]);
			Assert.AreEqual(("Butterscotch", 12), result[1]);
			Assert.AreEqual(("Cat-man-do", 15), result.Last());
		}


		[TestMethod]
		public void Check_DiffInp()
		{
			//first file
			FileHandle f = new FileHandle();
			List<Pet> pets1 = f.PopulatePets("input.txt");
			List<IMood> moods1 = f.PopulateMood("input.txt");

			Assert.AreEqual(pets1.Count, 3);
			Assert.AreEqual(moods1.Count, 14);

			List<(string, int)> result = new();

			foreach(var p in pets1 )
			{
				result = p.MaxSearch(ref moods1, ref pets1 );
				Console.WriteLine(string.Join(" ", result));
				break;
			}

			Assert.AreEqual(result.Count, 14);
			Assert.AreEqual(("Cat-man-do", 53), result[0]);
			Assert.AreEqual(("Cat-man-do", 70), result.Last());


			//second file 
			FileHandle f1 = new FileHandle();
			List<Pet> pets2 = f1.PopulatePets("input1.txt");
			List<IMood> moods2 = f1.PopulateMood("input1.txt");

			Assert.AreEqual(pets2.Count, 2);
			Assert.AreEqual(moods2.Count, 15);

			List<(string, int)> result1 = new();

			foreach (var p in pets2)
			{
				result1 = p.MaxSearch(ref moods2, ref pets2);
				Console.WriteLine(string.Join(" ", result1));
				break;
			}

			Assert.AreEqual(result1.Count, 15);
			Assert.AreEqual(("Butterscotch", 32), result1[0]);
			Assert.AreEqual(("Cat-man-do", 35), result1[4]);
			Assert.AreEqual(("Cat-man-do", 50), result1[9]);
			Assert.AreEqual(("Cat-man-do", 65), result1.Last());


			//third file
			FileHandle f2 = new FileHandle();
			List<Pet> pets3 = f2.PopulatePets("input2.txt");
			List<IMood> moods3 = f2.PopulateMood("input2.txt");

			Assert.AreEqual(pets3.Count, 5);
			Assert.AreEqual(moods3.Count, 7);

			List<(string, int)> result2 = new();

			foreach (var p in pets3)
			{
				result2 = p.MaxSearch(ref moods3, ref pets3);
				Console.WriteLine(string.Join(" ", result2));
				break;
			}

			Assert.AreEqual(result2.Count, 7);
			Assert.AreEqual(("Webster", 31), result2[0]);
			Assert.AreEqual(("Diane", 27), result2[3]);
			Assert.AreEqual(("Diane", 36), result2.Last());
		}

		[TestMethod]
		public void Test_Exceptions()
		{
			FileHandle f = new FileHandle();
			Assert.ThrowsException<EmptyListException>(() => f.PopulatePets("zeroinp.txt"));
			Assert.ThrowsException<NoMoodException>(() => f.PopulateMood("nomood.txt"));
			Assert.ThrowsException<EmptyFileException>(() => f.PopulatePets("empty.txt"));
			Assert.ThrowsException<BadExhLevelException>(() => f.PopulatePets("badexhil.txt"));
			Assert.ThrowsException<BadMoodException>(() => f.PopulateMood("badmood.txt"));
		}
	}
}
