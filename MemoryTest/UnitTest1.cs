using InfinityOfficialNetwork.Shared.Memory;
using System.Diagnostics;

namespace MemoryTest
{
	[TestClass]
	public class UnitTest1
	{
		private class Test
		{
			public int a, b, c;

			public Test()
			{
				a =0; b = 0; c = 0;
			}
		}

		[TestMethod]
		public unsafe void TestMethod1()
		{
			Allocator allocator = new Allocator();

			Test* test = allocator.AllocatePointer<Test>();

			test->a = 1;
			test->b = 2;
			test->c = 3;

			Assert.AreEqual(test->a, 1);
			Assert.AreEqual(test->b, 2);
			Assert.AreEqual(test->c, 3);

			allocator.DeAllocatePointer(test);

		}

		[TestMethod]
		public unsafe void TestMethod2()
		{
			Allocator allocator = new Allocator();

			Test* test = allocator.AllocateArray<Test>(3);

			test[0].a = 1;
			test[0].b = 2;
			test[0].c = 3;

			test[1].a = 1;
			test[1].b = 2;
			test[1].c = 3;

			test[2].a = 1;
			test[2].b = 2;
			test[2].c = 3;

			Assert.AreEqual(test[0].a, 1);
			Assert.AreEqual(test[0].b, 2);
			Assert.AreEqual(test[0].c, 3);

			Assert.AreEqual(test[1].a, 1);
			Assert.AreEqual(test[1].b, 2);
			Assert.AreEqual(test[1].c, 3);

			Assert.AreEqual(test[2].a, 1);
			Assert.AreEqual(test[2].b, 2);
			Assert.AreEqual(test[2].c, 3);

			allocator.DeAllocateArray(test, 3);

		}


		[TestMethod]
		public unsafe void TestMethod3()
		{
			Allocator allocator = new Allocator();

			ref Test test = ref allocator.Allocate<Test>();

			

			test.a = 1;
			test.b = 2;
			test.c = 3;


			Assert.AreEqual(test.a, 1);
			Assert.AreEqual(test.b, 2);
			Assert.AreEqual(test.c, 3);

			allocator.DeAllocate(ref test);

		}

		[TestMethod]
		public unsafe void TestMethod4()
		{
			Allocator allocator = new Allocator();

			ref Process test = ref allocator.Allocate<Process>();

			test.StartInfo.FileName = "cmd";
			test.StartInfo.ArgumentList.Add("/C");
			test.StartInfo.ArgumentList.Add("ECHO");
			test.StartInfo.ArgumentList.Add("123");
			test.StartInfo.RedirectStandardOutput = true;

			string output = "";

			test.OutputDataReceived += (sender, args) =>
			{
				output += args.Data;
			};


			test.Start();
			test.BeginOutputReadLine();
			test.WaitForExit();

			Assert.IsTrue(output.Contains("123"));

			allocator.DeAllocate(ref test);

		}
	}
}