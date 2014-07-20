//using System.IO;
//using Trigger.CRM.Persistent;
//
//namespace Trigger.Datastore.Persistent
//{
//	[System.ComponentModel.DefaultProperty("FileName")]
//	public class FileData : PersistentModelBase, IFileData
//	{
//		public override string GetRepresentation()
//		{
//			return FileName;
//		}
//
//		public override void Delete()
//		{
//			var path = Path.Combine(StoreConfigurator.DocumentStoreLocation, FileName);
//
//			if (File.Exists(path))
//				File.Delete(path);
//
//			base.Delete();
//		}
//
//		string subject;
//
//		public string Subject
//		{
//			get
//			{
//				return subject;
//			}
//			set
//			{
//				if (Equals(subject, value))
//					return;
//				subject = value;
//
//				OnPropertyChanged();
//			}
//		}
//
//		string fileName;
//
//		public string FileName
//		{
//			get
//			{
//				return fileName;
//			}
//			set
//			{
//				if (Equals(fileName, value))
//					return;
//				fileName = value;
//
//				OnPropertyChanged();
//			}
//		}
//	}
//}
