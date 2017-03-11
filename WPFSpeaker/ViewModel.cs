using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SpeechSynthesis;

namespace WPFSpeaker
{
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

        public override bool Dub {
	        get { return base.Dub; }
	        set { base.Dub = value; NotifyPropertyChanged(); }
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
