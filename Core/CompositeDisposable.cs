using System;
using System.Collections.Generic;

namespace Core
{
	internal class CompositeDisposable : IDisposable
	{
		private readonly ICollection<IDisposable> _disposables = new LinkedList<IDisposable>();
		private bool _disposed;

		public void Add(IDisposable disposable)
		{
			lock(_disposables)
			{
				if(_disposed)
				{
					disposable.Dispose();
				}
				else
				{
					_disposables.Add(disposable);
				}
			}
		}

		public void Remove(IDisposable disposable)
		{
			lock(_disposables)
			{
				if(_disposables.Remove(disposable))
				{
					disposable.Dispose();
				}
			}
		}			

		public void Dispose()
		{
			lock(_disposables)
			{
				if(!_disposed)
				{
					foreach(var disposable in _disposables)
					{
						disposable.Dispose();
					}
					_disposables.Clear();
					_disposed = true;
				}
			}
		}
	}
}
