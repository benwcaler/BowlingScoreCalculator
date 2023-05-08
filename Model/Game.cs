using BowlingScoreCalculator.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreCalculator.Model
{
    public class Game : BaseComponent 
    {
        private ObservableCollection<Player> _players;
        private DateTime _gameDate;

        public ObservableCollection<Player> Players
        {
            get => _players;
            set
            {
                _players = value;
                NotifyChanged();
            }
        }

        public DateTime GameDate
        {
            get => _gameDate;
            set
            {
                _gameDate = value;
                NotifyChanged();
            }
        }
    }
}
