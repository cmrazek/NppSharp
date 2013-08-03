using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NppSharp
{
	internal static class Util
	{
		public static T TryGetAttribute<T>(this Type type)
		{
			return (from a in type.GetCustomAttributes(typeof(T), false).Cast<T>() select a).FirstOrDefault();
		}
	}
}
