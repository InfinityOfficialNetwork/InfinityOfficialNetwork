using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InfinityOfficialNetwork.Shared.Memory
{
	public sealed class Allocator : IAllocator
	{
		private class AllocatorHelper<TAlloc>
		{
			public static readonly unsafe int typeSize = Unsafe.SizeOf<TAlloc>();
			public static readonly Type objectType;
			public static readonly ConstructorInfo objectConstructor;
			public static readonly MethodInfo objectDestructor;
			public static nint methodTablePointer;
			static unsafe AllocatorHelper()
			{
				objectType = typeof(TAlloc);
				objectConstructor = objectType.GetConstructor(Type.EmptyTypes);
				objectDestructor = objectType.GetMethod("Finalize", BindingFlags.NonPublic | BindingFlags.Instance) ?? throw new ArgumentNullException();

				object obj = RuntimeHelpers.GetUninitializedObject(typeof(TAlloc));
				nint ptr = ((nint*)(&obj))[0];
				methodTablePointer = ptr;
			}
		}

		public unsafe TAlloc* AllocatePointer<TAlloc>() where TAlloc : new()
		{
			nint o = Marshal.AllocHGlobal(AllocatorHelper<TAlloc>.typeSize);

			if (o == 0)
				throw new OutOfMemoryException();


			((nint*)o)[0] = AllocatorHelper<TAlloc>.methodTablePointer;

			AllocatorHelper<TAlloc>.objectConstructor.Invoke(*(TAlloc*)o, null);


			return (TAlloc*)(o);
		}

		public unsafe TAlloc* AllocateArray<TAlloc>(int Count) where TAlloc : new()
		{
			nint o = Marshal.AllocHGlobal(AllocatorHelper<TAlloc>.typeSize * Count);

			if (o == 0)
				throw new OutOfMemoryException();

			for (int i = 0; i < Count; i++)
			{
				((nint*)(&(((byte*)o)[AllocatorHelper<TAlloc>.typeSize * i])))[0] = AllocatorHelper<TAlloc>.methodTablePointer;

				AllocatorHelper<TAlloc>.objectConstructor.Invoke(((TAlloc*)o)[i], null);
			}


			return (TAlloc*)(o);
		}

		public unsafe ref TAlloc Allocate<TAlloc>() where TAlloc : new() => ref *AllocatePointer<TAlloc>();

		public unsafe void DeAllocatePointer<TAlloc>(TAlloc* o)
		{
			AllocatorHelper<TAlloc>.objectDestructor.Invoke(*o, null);
			Marshal.FreeHGlobal((nint)o);
		}



		public unsafe void DeAllocateArray<TAlloc>(TAlloc* o, int Count)
		{
			for (int i = 0; i < Count; i++)
			{
				AllocatorHelper<TAlloc>.objectDestructor.Invoke((o)[i], null);
			}
			Marshal.FreeHGlobal((nint)o);
		}

		public unsafe void DeAllocate<TAlloc>(ref TAlloc o) => DeAllocatePointer<TAlloc>((TAlloc*)Unsafe.AsPointer(ref o));
	}
}
