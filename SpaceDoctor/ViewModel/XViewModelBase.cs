using System;
using System.ComponentModel;

namespace SpaceDoctor.ViewModel
{
    /// <summary>
    /// Базовый класс для ViewModels
    /// </summary>
    public class XViewModelBase : INotifyPropertyChanged
    {
        
        /// <summary>
        /// Метод реализует генерацию события изменения свойства
        /// </summary>
        /// <param name="propName">Имя изменившегося свойства</param>
        public void RaisePropertyChanged(String propName)
        {
            if (!String.IsNullOrEmpty(propName))
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }

            else
                throw new ArgumentException("Пустая строка или null");

        }
        public event PropertyChangedEventHandler PropertyChanged = null;
    }
}
