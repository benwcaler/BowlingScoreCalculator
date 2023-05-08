using BowlingScoreCalculator.Model;
using BowlingScoreCalculator.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace BowlingScoreCalculator.UI
{
    public class GameViewViewModel : BaseComponent
    {
        private ObservableCollection<Player> _players;
        private SimpleCommand _bowlCommand;
        private Player _currentPlayer;
        private bool _gameOver;

        public ObservableCollection<Player> Players
        {
            get => _players;
            set
            {
                _players = value;
                NotifyChanged();
            }
        }

        public SimpleCommand BowlCommand
        {
            get => _bowlCommand ??= new()
            {
                CanExecuteDelegate = x => !GameOver,
                ExecuteDelegate = x =>
                {
                    //get the player and frame
                    Frame frame = CurrentPlayer.Frames.FirstOrDefault(f => f.FrameStatus == Frame.Status.Open || f.FrameStatus == Frame.Status.DeliveryOne || f.FrameStatus == Frame.Status.DeliveryTwo || f.FrameStatus == Frame.Status.DeliveryThree);
                    int frameIndex = CurrentPlayer.Frames.IndexOf(frame);

                    if (frame.FrameStatus == Frame.Status.Open) frame.FrameStatus = Frame.Status.DeliveryOne;

                    //add score to appropriate roll in frame
                    if (frame.FrameStatus == Frame.Status.DeliveryOne)
                    {
                        frame.DeliveryOne = new Random().Next(11);
                        //frame.DeliveryOne = 10;
                    }
                    else if (frame.FrameStatus == Frame.Status.DeliveryTwo)
                    {
                        frame.DeliveryTwo = new Random().Next(frame.DeliveryOne != 10 ? (10 - frame.DeliveryOne.Value) + 1 : 11);
                        //frame.DeliveryTwo = 10;
                    }
                    else if (frame.FrameStatus == Frame.Status.DeliveryThree)
                    {
                        frame.DeliveryThree = new Random().Next(frame.DeliveryTwo != 10 ? (10 - frame.DeliveryTwo.Value) + 1 : 11);
                        //frame.DeliveryThree = 10;
                    }
                    int indexOfFrame = frameIndex;
                    Frame twoFramesAgo = indexOfFrame > 1 ? CurrentPlayer.Frames[indexOfFrame - 2] : null;
                    Frame oneFrameAgo = indexOfFrame > 0 ? CurrentPlayer.Frames[indexOfFrame - 1] : null;

                    if (frame.FrameStatus == Frame.Status.DeliveryOne && (oneFrameAgo is not null && oneFrameAgo.FrameStatus == Frame.Status.Strike) && (twoFramesAgo is not null && twoFramesAgo.FrameStatus == Frame.Status.Strike))
                    {
                        int previousTotal = CurrentPlayer.Frames.IndexOf(twoFramesAgo) > 0 ? CurrentPlayer.Frames[CurrentPlayer.Frames.IndexOf(twoFramesAgo) - 1].TotalScore : CurrentPlayer.Frames[CurrentPlayer.Frames.IndexOf(twoFramesAgo)].TotalScore;
                        twoFramesAgo.FrameScore += frame.DeliveryOne.Value;
                        twoFramesAgo.TotalScore = previousTotal + twoFramesAgo.FrameScore;
                        twoFramesAgo.FrameStatus = Frame.Status.Closed;

                        oneFrameAgo.FrameScore += frame.DeliveryOne.Value;
                        frame.FrameScore = frame.DeliveryOne.Value;
                    }
                    else if (frame.FrameStatus == Frame.Status.DeliveryOne && (oneFrameAgo is not null && oneFrameAgo.FrameStatus == Frame.Status.Strike))
                    {
                        oneFrameAgo.FrameScore += frame.DeliveryOne.Value;
                        frame.FrameScore = frame.DeliveryOne.Value;
                    }
                    else if (frame.FrameStatus == Frame.Status.DeliveryOne && oneFrameAgo is not null && oneFrameAgo.FrameStatus == Frame.Status.Spare)
                    {
                        oneFrameAgo.FrameScore += frame.DeliveryOne.Value;
                        oneFrameAgo.TotalScore += twoFramesAgo is not null ? oneFrameAgo.FrameScore + twoFramesAgo.TotalScore : oneFrameAgo.FrameScore;
                        oneFrameAgo.FrameStatus = Frame.Status.Closed;

                        frame.FrameScore = frame.DeliveryOne.Value;
                    }
                    else if (frame.FrameStatus == Frame.Status.DeliveryOne)
                    {
                        frame.FrameScore = frame.DeliveryOne.Value;
                    }
                    else if (frame.FrameStatus == Frame.Status.DeliveryTwo && oneFrameAgo is not null && oneFrameAgo.FrameStatus == Frame.Status.Strike)
                    {
                        oneFrameAgo.FrameScore += frame.DeliveryTwo.Value;
                        oneFrameAgo.TotalScore = oneFrameAgo.TotalScore + (twoFramesAgo is not null ? twoFramesAgo.TotalScore : 0) + oneFrameAgo.FrameScore;
                        oneFrameAgo.FrameStatus = Frame.Status.Closed;

                        frame.FrameScore += frame.DeliveryTwo.Value;
                    }
                    else if (frame.FrameStatus == Frame.Status.DeliveryTwo)
                    {
                        frame.FrameScore += frame.DeliveryTwo.Value;
                    }
                    else if (frame.FrameStatus == Frame.Status.DeliveryThree)
                    {
                        frame.FrameScore += frame.DeliveryThree.Value;
                    }

                    if (frame.DeliveryOne == 10 && frameIndex != 9) frame.FrameStatus = Frame.Status.Strike;
                    else if (frame.DeliveryOne is not null && frame.DeliveryTwo is not null && frame.DeliveryThree is not null)
                    {
                        frame.TotalScore = oneFrameAgo.TotalScore + frame.FrameScore;
                        frame.FrameStatus = Frame.Status.Closed;
                    }
                    else if (frame.DeliveryOne == 10 && frame.DeliveryTwo is not null && frameIndex == 9) frame.FrameStatus = Frame.Status.DeliveryThree;
                    else if (frame.DeliveryOne == 10 && frameIndex == 9) frame.FrameStatus = Frame.Status.DeliveryTwo;
                    else if (frame.DeliveryOne + frame.DeliveryTwo == 10 && frameIndex == 9) frame.FrameStatus = Frame.Status.DeliveryThree;
                    else if (frame.DeliveryOne + frame.DeliveryTwo == 10 && frameIndex != 9) frame.FrameStatus = Frame.Status.Spare;
                    else if (frame.DeliveryOne + frame.DeliveryTwo != 10 && frame.DeliveryTwo is not null && frameIndex == 9)
                    {
                        frame.TotalScore = oneFrameAgo.TotalScore + frame.FrameScore;
                        frame.FrameStatus = Frame.Status.Closed;
                    }
                    else if (frame.DeliveryOne is not null && frame.DeliveryTwo is not null && frameIndex != 9)
                    {
                        frame.TotalScore = oneFrameAgo is not null ? oneFrameAgo.TotalScore + frame.FrameScore : frame.FrameScore;
                        frame.FrameStatus = Frame.Status.Closed;
                    }
                    else if (frame.DeliveryOne != 10) frame.FrameStatus = Frame.Status.DeliveryTwo;

                    //change player
                    int playerIndex = Players.IndexOf(CurrentPlayer);
                    if (frameIndex == 9 && playerIndex == Players.Count - 1 && frame.FrameStatus == Frame.Status.Closed)
                    {
                        GameOver = true;
                    }
                    else if ((!frame.Tenth && ((frame.DeliveryOne is not null && frame.DeliveryTwo is not null) || frame.FrameStatus == Frame.Status.Strike)) || (frame.Tenth && frame.FrameStatus == Frame.Status.Closed))
                    {
                        CurrentPlayer = playerIndex == Players.Count - 1 ? Players[0] : Players[playerIndex + 1];
                    }
                }
            };
        }

        public Player CurrentPlayer
        {
            get => _currentPlayer;
            set
            {
                _currentPlayer = value;
                NotifyChanged();
            }
        }

        public bool GameOver
        {
            get => _gameOver;
            set
            {
                _gameOver = value;
                NotifyChanged();
                NotifyChanged(nameof(NewGameVisibility));
            }
        }

        public Visibility NewGameVisibility
        {
            get => GameOver ? Visibility.Visible : Visibility.Collapsed;
        }

        public GameViewViewModel()
        {
        }
    }
}