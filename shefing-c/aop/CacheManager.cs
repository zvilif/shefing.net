using System.Collections.Generic;
using System.Threading;
using Castle.DynamicProxy;
using shefing_c.Entities;

namespace com.zvil.shefing.aop
{

	/// <summary>
	/// The class that encapsulates the cache management aspect
	/// 
	/// @author Zvi Lifshitz
	/// </summary>

	public class CacheManager : IInterceptor
	{
		private readonly Dictionary<CalcModel, string> cache;
		private readonly ReaderWriterLock mapLock; // We use it to synchronize the cache

		/// <summary>
		/// Cache shall not exceed a predefined size, in real life this value should be configurable through the
		/// application's properties file. Another option is to set it through an API, which will give us an easy
		/// way to test this mechanism. But this is too much for this little test...
		/// </summary>
		private const int CACHE_LIMIT = 1000;

		/// <summary>
		/// Instantiate the cache in the constructor
		/// </summary>
		public CacheManager()
		{
			cache = new Dictionary<CalcModel, string>();
			mapLock = new ReaderWriterLock ();
		}

		/// <summary>
		/// Do the work. Try to locate the input object in the cash. If found then return the value from the
		/// cash otherwise calculate the result and store it in the cash. </summary>
		/// <param name="joinPoint">     Contains information about the execution point, including the arguments of the method </param>
		/// <returns>      the result either from the cash or by calculation. </returns>
		/// <exception cref="Throwable"> </exception>
		public void Intercept(IInvocation invocation)
		{
			// Get the argument and locate it in the cache
			CalcModel key = (CalcModel)(invocation.Arguments[0]);
			if (key == null)
			{
				invocation.Proceed();
				return;
			}
			string result = getFromCache(key);

			// If the object is found in the cache return the cached value. Otherwise proceed with the original
			// method and store the result in the cash
			if (result == null)
			{
				invocation.Proceed();
				result = (string)invocation.ReturnValue;;
				storeInCache(key, result);
			}
			invocation.ReturnValue = result;
		}

		/// <summary>
		/// Get an object from the map while synchronizing the map </summary>
		/// <param name="key">      the key to look for </param>
		/// <returns>          the found object </returns>
		private string getFromCache(CalcModel key)
		{
			mapLock.AcquireReaderLock(Timeout.Infinite);
			try
			{
				return cache[key];
			}
			catch(KeyNotFoundException) {
				return null;
			}
			finally
			{
				mapLock.ReleaseReaderLock();
			}
		}

		/// <summary>
		/// Store an object in the map while synchronizing the map. If the cache size exceeds the configured limit
		/// clear it before adding a new entry. </summary>
		/// <param name="key">       the key </param>
		/// <param name="value">     the value </param>
		private void storeInCache(CalcModel key, string value)
		{
			mapLock.AcquireWriterLock(Timeout.Infinite);
			try
			{
				if (cache.Count >= CACHE_LIMIT)
				{
					cache.Clear();
				}
				cache[key] = value;
			}
			finally
			{
				mapLock.ReleaseWriterLock();
			}
		}
   }

}