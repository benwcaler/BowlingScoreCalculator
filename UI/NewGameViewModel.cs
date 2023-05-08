using BowlingScoreCalculator.Model;
using BowlingScoreCalculator.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreCalculator.UI
{
    public class NewGameViewModel : BaseComponent
    {
        private readonly MainWindowViewModel _mainWindowViewModel = InjectorProvider.Get<MainWindowViewModel>();

        private string _playerName;
        private ObservableCollection<Player> _newPlayers = new();
        private SimpleCommand _addPlayerCommand;
        private SimpleCommand _startGameCommand;
        private GameViewViewModel _gameViewViewModel = InjectorProvider.Get<GameViewViewModel>();

        public GameViewViewModel GameViewViewModel
        {
            get => _gameViewViewModel;
            set
            {
                _gameViewViewModel = value;
                NotifyChanged();
            }
        }

        public string PlayerName
        {
            get => _playerName;
            set
            {
                _playerName = value;
                NotifyChanged();
            }
        }

        public ObservableCollection<Player> NewPlayers
        {
            get => _newPlayers;
            set
            {
                _newPlayers = value;
                NotifyChanged();
            }
        }

        public SimpleCommand AddPlayerCommand
        {
            get => _addPlayerCommand ??= new()
            {
                CanExecuteDelegate = x => PlayerName is not null,
                ExecuteDelegate = x =>
                {
                    Player player = new()
                    {
                        Name = PlayerName
                    };

                    for (int i = 0; i < 9; i++)
                    {
                        player.Frames.Add(new());
                    }

                    player.Frames.Add(new(true));

                    NewPlayers.Add(player);

                    PlayerName = null;
                }
            };
        }

        public SimpleCommand StartGameCommand
        {
            get => _startGameCommand ??= new()
            {
                CanExecuteDelegate = x => NewPlayers is not null && NewPlayers.Any(),
                ExecuteDelegate = x =>
                {
                    GameViewViewModel.Players = NewPlayers;
                    GameViewViewModel.CurrentPlayer = GameViewViewModel.Players[0];
                    _mainWindowViewModel.ShowControl("GameView");
                }
            };
        }

        public NewGameViewModel()
        {

        }
    }
}
