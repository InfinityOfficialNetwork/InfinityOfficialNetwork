using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfinityOfficialNetwork.Shared.Memory
{
	public unsafe interface IAllocator
	{
		abstract ref TAlloc Allocate<TAlloc>() where TAlloc : new();
		abstract TAlloc* AllocatePointer<TAlloc>() where TAlloc : new();
		abstract TAlloc* AllocateArray<TAlloc>(int Count) where TAlloc : new();
		//abstract TAlloc* Allocate<TAlloc, T1>(T1 t1);

		abstract unsafe void DeAllocate<TAlloc>(ref TAlloc o);
		abstract unsafe void DeAllocatePointer<TAlloc>(TAlloc* o);
		abstract unsafe void DeAllocateArray<TAlloc>(TAlloc* o, int Count);
	}
}
