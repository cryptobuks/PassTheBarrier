using System;

namespace PassTheBarier.Core.Logic.Exceptions
{
	public class ValidationException : Exception
	{
		public ValidationException(string message)
			: base(message)
		{
		}
	}
}