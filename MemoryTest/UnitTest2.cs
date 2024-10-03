using InfinityOfficialNetwork.Shared.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryTest
{
	[TestClass]
	public unsafe class UnitTest2
	{
		private class TestClass
		{
			public int Value { get; set; } = 42; // Default value for testing
		}

		[TestMethod]
		public void Allocate_SingleInstance_ShouldAllocateMemory()
		{
			// Arrange
			var allocator = new Allocator();
			TestClass testInstance;

			// Act
			ref TestClass allocatedInstance = ref allocator.Allocate<TestClass>();

			// Assert
			Assert.IsNotNull(allocatedInstance);
			Assert.AreEqual(42, allocatedInstance.Value); // Check default value
		}

		[TestMethod]
		public void AllocateArray_ShouldAllocateMemoryAndInitialize()
		{
			// Arrange
			var allocator = new Allocator();
			int count = 5;

			// Act
			TestClass* allocatedArray = allocator.AllocateArray<TestClass>(count);

			// Assert
			Assert.IsTrue((nint)allocatedArray != null);
			for (int i = 0; i < count; i++)
			{
				Assert.AreEqual(42, allocatedArray[i].Value); // Check default value
			}

			// Clean up
			allocator.DeAllocateArray(allocatedArray, count);
		}

		[TestMethod]
		public void Allocate_ShouldThrowOutOfMemoryException_WhenAllocationFails()
		{
			// Arrange
			var allocator = new Allocator();
			// Simulate a failure in memory allocation
			// This could be tricky to simulate. You may need to implement
			// a way to force the allocation to fail, possibly by modifying 
			// the Allocator class for testing purposes.
			// For demonstration, assume a situation where allocation fails.

			// Act & Assert
			Assert.ThrowsException<OutOfMemoryException>(() =>
			{
				allocator.AllocateArray<TestClass>(1_000_000_000);
				// Simulate memory allocation failure
				// You might need to control the allocator behavior in tests
			});
		}

		[TestMethod]
		public void DeAllocate_ShouldFreeMemory()
		{
			// Arrange
			var allocator = new Allocator();
			ref TestClass testInstance = ref allocator.Allocate<TestClass>();

			// Act
			allocator.DeAllocate(ref testInstance);

			// Assert
			// Ensure that the memory has been freed, 
			// but there isn't a direct way to assert this in .NET.
			// You might just check that no exceptions are thrown during deallocation.
		}

		[TestMethod]
		public void DeAllocateArray_ShouldFreeMemoryForAllElements()
		{
			// Arrange
			var allocator = new Allocator();
			int count = 5;
			TestClass* allocatedArray = allocator.AllocateArray<TestClass>(count);

			// Act
			allocator.DeAllocateArray(allocatedArray, count);

			// Assert
			// Ensure that the memory has been freed for all elements
			// Again, asserting memory freeing is complex; check for exceptions instead
		}
	}
}
