using SpaceDoctor.Model;
using System;
using System.ComponentModel;

/***********************************************
    Wrapper for the XDrag class.

    ----------------------------------------
    Autor: Kovalev Alexander
    Email: koalse@gmail.com
    Date: 01.11.2015
************************************************/

namespace SpaceDoctor.ViewModel
{
    /// <summary>
    ///  Wrapper for the XDrag class
    /// </summary>
    class XDragVM : XViewModelBase
    {

        #region fields

        readonly XDrag _drag;

        private Boolean _addToKit;


        #endregion

        #region ctors

        public XDragVM () : this (new XDrag())
        {

        }

        public XDragVM(XDrag drag)
        {
            if (drag == null)
                throw new ArgumentNullException(" Параметр drag не может быть null");
            _drag = drag;            
        }
        #endregion

        #region properties

        public XDrag Drag
        {
            get
            {
                return _drag;
            }
        }

        public String Name
        {
            get
            {
                return Drag.Name;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("Название препарата не может быть пустым!");
                else
                Drag.Name = value;
            }
        }

        public String Form
        {
            get
            {
                return Drag.Form;
            }
            set
            {
                Drag.Form = value;
            }
        }
               


        public Boolean SelectedToNewKit
        {
            get
            {
                return _addToKit;
            }
            set
            {
                _addToKit = value;
                RaisePropertyChanged("SelectedToNewKit");
            }
        }

        #endregion

    }
}
