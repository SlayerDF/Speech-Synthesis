using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeechSynthesis;

namespace WPFSpeaker
{
	public class ViewModel : SpeechManager
	{
		//Singleton lazy realization
		private static ViewModel _instance;
		public static ViewModel Instance => _instance ?? (_instance = new ViewModel());

		//Use static property 'Instance' to get FakeRepository instead
		private ViewModel() { }
	}
}
