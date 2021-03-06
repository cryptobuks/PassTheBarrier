﻿using MvvmCross.Core.ViewModels;

namespace PassTheBarier.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
    }

    public abstract class BaseViewModel<T> : MvxViewModel<T>
    {

    }

	public abstract class BaseViewModel<TParameter, TResult> : MvxViewModel<TParameter, TResult>
	{

	}
}