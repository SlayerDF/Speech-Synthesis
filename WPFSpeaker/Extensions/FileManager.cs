using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WPFSpeaker.Extensions
{
	public class FileManager
	{
		//Singleton lazy realization
		private static FileManager _instance;
		public static FileManager Instance => _instance ?? (_instance = new FileManager());

		//Use static property 'Instance' to access FileManager instead
		private FileManager() {
			_typeFiles.Add(typeof(ViewModel), "settings.xml");
			_typeFiles.Add(typeof(HotkeysViewModel), "hotkeys.xml");
		}

		private Dictionary<Type, string> _typeFiles = new Dictionary<Type, string>();

		public void Serialize(Type type, object data) {
			if (!_typeFiles.ContainsKey(type)) throw new Exception("Data type is not registered for serialization");
			if (data.GetType() != type) throw new Exception("Serialization type and data does not match");
			var serializer = new XmlSerializer(type);
			using (StreamWriter writer = new StreamWriter(_typeFiles[type]))
				serializer.Serialize(writer, data);
		}

		public object Deserialize(Type type) {
			if (!_typeFiles.ContainsKey(type)) throw new Exception("Data type is not registered for deserialization");
			try {
				var deserializer = new XmlSerializer(type);
				using (StreamReader reader = new StreamReader(_typeFiles[type]))
					return deserializer.Deserialize(reader);
			}
			catch {
				return null;
			}
		}
	}
}
