using BowlingScoreCalculator.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BowlingScoreCalculator.Model
{
    public class Frame : BaseComponent
    {
        public enum Status
        {
            Open,
            Strike,
            Spare,
            DeliveryOne,
            DeliveryTwo,
            DeliveryThree,
            Closed
        }

        private Status _frameStatus = Status.Open;
        private int? _deliveryOne;
        private int? _deliveryTwo;
        private int? _deliveryThree;
        private bool _tenth;
        private int _frameScore;
        private int _totalScore;

        public Status FrameStatus
        {
            get => _frameStatus;
            set
            {
                _frameStatus = value;
                NotifyChanged();
                NotifyChanged(nameof(TotalScoreVisibility));
            }
        }

        public int? DeliveryOne
        {
            get => _deliveryOne;
            set
            {
                _deliveryOne = value;
                NotifyChanged();
                NotifyChanged(nameof(DeliveryOneMark));
                NotifyChanged(nameof(DeliveryTwoMark));
            }
        }

        public int? DeliveryTwo
        {
            get => _deliveryTwo;
            set
            {
                _deliveryTwo = value;
                NotifyChanged();
                NotifyChanged(nameof(DeliveryTwoMark));
            }
        }

        public int? DeliveryThree
        {
            get => _deliveryThree;
            set
            {
                _deliveryThree = value;
                NotifyChanged();
                NotifyChanged(nameof(DeliveryThreeMark));
            }
        }

        public bool Tenth
        {
            get => _tenth;
            set
            {
                _tenth = value;
                NotifyChanged();
            }
        }

        public int FrameScore
        {
            get => _frameScore;
            set
            {
                _frameScore = value;
                NotifyChanged();
            }
        }

        public int TotalScore
        {
            get => _totalScore;
            set
            {
                _totalScore = value;
                NotifyChanged();
            }
        }

        public Visibility TenthVisibility
        {
            get => Tenth ? Visibility.Visible : Visibility.Collapsed;
        }

        public string DeliveryOneMark
        {
            get
            {
                if (DeliveryOne == 10 && Tenth) return "X";
                else if (DeliveryOne == 10) return string.Empty;
                else return DeliveryOne.ToString();
            }
        }

        public string DeliveryTwoMark
        {
            get
            {
                if (DeliveryOne + DeliveryTwo == 10) return "/";
                else if (DeliveryOne == 10) return "X";
                else return DeliveryTwo is not null ? DeliveryTwo.ToString() : string.Empty;
            }
        }

        public string DeliveryThreeMark
        {
            get
            {
                if (DeliveryTwo + DeliveryThree == 10) return "/";
                else if (DeliveryThree == 10) return "X";
                else return DeliveryThree.ToString();
            }
        }

        public Visibility TotalScoreVisibility
        {
            get => FrameStatus == Status.Closed ? Visibility.Visible : Visibility.Hidden;
        }

        public Frame() { }
        public Frame(bool isTenth)
        {
            Tenth = isTenth;
        }
    }
}
