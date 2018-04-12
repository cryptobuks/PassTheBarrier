using System;
using System.Threading.Tasks;

namespace PassTheBarier.Core.Logic.Utils
{
	public interface IActionHelper
	{
		Task DoAction(Func<Task> action);

		int DisplayToast(string message);
	}
}