#pragma once


#include "Object.h"

namespace InfinityOfficialNetwork::Native::Core
{
	class IIterator : virtual public Object
	{
	public:
		virtual constexpr ~IIterator () noexcept
		{}
	};
}

