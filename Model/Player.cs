using BowlingScoreCalculator.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreCalculator.Model
{
    public class Player : BaseComponent
    {
        private string _name;
        private ObservableCollection<Frame> _frames = new();

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyChanged();
            }
        }

        public ObservableCollection<Frame> Frames
        {
            get => _frames;
            set
            {
                _frames = value;
                NotifyChanged();
            }
        }
    }
}
