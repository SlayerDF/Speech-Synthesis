using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSpeaker.Extensions
{
	class HotkeyAttribute : DescriptionAttribute
	{
		public HotkeyAttribute(string key, string value) {
			this.Key = key;
			this.Value = value;
		}

		public string Key { get; set; }
		public string Value { get; set; }
	}
}
