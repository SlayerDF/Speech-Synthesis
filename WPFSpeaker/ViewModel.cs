using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;
using FirstFloor.ModernUI.Presentation;
using SpeechSynthesis;
using WPFSpeaker.Extensions;

namespace WPFSpeaker
{
	[XmlRootAttribute("Settings", IsNullable = false)]
	public class ViewModel : SpeechManager, INotifyPropertyChanged
	{
		//Singleton lazy realization
		private static ViewModel _instance;
		public static ViewModel Instance => _instance ?? (_instance = new ViewModel());

		//Use static property 'Instance' to access ViewModel instead
		private ViewModel() { }

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

		//Settings

		public ICommand SaveSettingsCommand { get { return new RelayCommand(param => SaveSettings()); } }
		public ICommand LoadSettingsCommand { get { return new RelayCommand(param => LoadSettings()); } }

		public void SaveSettings() {
			FileManager.Instance.Serialize(typeof(ViewModel), Instance);
		}

		public void LoadSettings() {
			var settings = FileManager.Instance.Deserialize(typeof(ViewModel)) as ViewModel;
			if (settings == null) return;
			Voice = settings.Voice;
			Device = settings.Device;
			Replication = settings.Replication;
			Volume = settings.Volume;
			Rate = settings.Rate;
		}

		//Override properties notifying

		public override int Voice
        {
            get { return base.Voice; }
            set { base.Voice = value; NotifyPropertyChanged(); }
        }

        public override int Device
        {
            get { return base.Device; }
            set { base.Device = value; NotifyPropertyChanged(); }
        }

		public override bool Replication {
	        get { return base.Replication; }
	        set { base.Replication = value; NotifyPropertyChanged(); }
	    }

        public override int Volume
        {
            get { return base.Volume; }
            set { base.Volume = value; NotifyPropertyChanged(); }
        }

        public override int Rate
        {
            get { return base.Rate; }
            set { base.Rate = value; NotifyPropertyChanged(); }
        }


    }
}
