using System;
using System.Configuration;
using System.IO;

namespace Trigger.XStorable.DataStore
{
	public static class StoreConfigurator
	{
		public static string DataStoreLocation;

		public static string DocumentStoreLocation;

		public static string StoreMap;

		public static void InitStore()
		{
			DataStoreLocation = SetStoreLocation();

			DocumentStoreLocation = SetDocumentStoreLocation();
		}

		static string SetStoreLocation()
		{
			var value = ConfigurationManager.AppSettings["DataStoreLocation"];

			if (!Directory.Exists(value))
				Directory.CreateDirectory(value);
                
			return value;
		}

		static string SetDocumentStoreLocation()
		{
			var value = ConfigurationManager.AppSettings["DocumentStoreLocation"];

			if (!Directory.Exists(value))
				Directory.CreateDirectory(value);

			return value;
		}
	}
}
