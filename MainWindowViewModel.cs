using BowlingScoreCalculator.Model;
using BowlingScoreCalculator.UI;
using BowlingScoreCalculator.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BowlingScoreCalculator
{
    public class MainWindowViewModel : BaseComponent
    {
        public enum NavigationItems
        {
            Undefined = 0,
            NewGame = 1,
            GameView = 2
        }
        private Visibility _newGameVisibility = Visibility.Visible;
        private Visibility _gameViewVisibility = Visibility.Collapsed;

        public Visibility NewGameVisibility
        {
            get => _newGameVisibility;
            set
            {
                _newGameVisibility = value;
                NotifyChanged();
            }
        }

        public Visibility GameViewVisibility
        {
            get => _gameViewVisibility;
            set
            {
                _gameViewVisibility = value;
                NotifyChanged();
            }
        }

        public void ShowControl(string page)
        {
            NewGameVisibility = Visibility.Collapsed;
            GameViewVisibility = Visibility.Collapsed;

            NavigationItems navItem = Enum.Parse<NavigationItems>(page);
            switch (navItem)
            {
                case NavigationItems.Undefined:
                    throw new NotImplementedException();
                case NavigationItems.NewGame:
                    NewGameVisibility = Visibility.Visible;
                    break;
                case NavigationItems.GameView:
                    GameViewVisibility = Visibility.Visible;
                    break;
            };
        }
    }
}
