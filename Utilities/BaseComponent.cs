using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BowlingScoreCalculator.Utilities
{
    public class BaseComponent : INotifyPropertyChanged
    {

        #region Enums 
        #endregion

        #region Constants 
        #endregion

        #region Fields 
        protected bool _isDirty;
        #endregion

        #region Properties 

        #region Public Properties 
        #endregion

        #region Protected Properties 
        public virtual bool IsDirty
        {
            get
            {
                return _isDirty;
            }
            set
            {
                _isDirty = value;
                NotifyChanged();
            }
        }
        #endregion

        #region Private Properties 
        #endregion

        #endregion

        #region Constructors 
        #endregion

        #region Events 
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methods 

        #region Public Methods 
        #endregion

        #region Protected Methods 
        protected void NotifyChanged([CallerMemberName] string name = null)
        {
            if (!IsDirty && name != nameof(IsDirty)) //prevent an infinite loop
            {
                IsDirty = true;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected void NotifyChangedKeepClean([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region Private Methods 
        #endregion

        #endregion


    }
}
