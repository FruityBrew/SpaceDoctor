using SpaceDoctor.Model;
using System;

/***********************************************
    Wrapper for the XParam class.

    ----------------------------------------
    Autor: Kovalev Alexander
    Email: koalse@gmail.com
    Date: 01.11.2015
************************************************/

namespace SpaceDoctor.ViewModel
{
     class XParamVM : XViewModelBase
    {

        #region fields

        readonly XParam _param;
        readonly XParamTypeVM _paramType;

        #endregion

        #region ctors
        public XParamVM() : this(new XParam())
        {

        }

        public XParamVM(XParamTypeVM paramType) 
        {
            _param = new XParam();
            _param.Type = paramType.ParamType;
            _paramType = paramType;
        }

        public XParamVM(XParam param)
        {
            _param = param;
            _paramType = new XParamTypeVM(param.Type);
        }

        #endregion


        #region properties


        public XParam Param
        {
            get
            {
                return _param;
            }
        }

        public Double Value 
        {
            get 
            {
                return Param.Value;
             }
             set
             {
                if (value < 0)
                    throw new ArgumentException("Ребята, значение не может быть меньше нуля");
                else
                {
                    Param.Value = value;
                    RaisePropertyChanged("Value");
                }
             }
        }


        public XParamTypeVM ParamType
        {
            get
            {
                return _paramType;
            }
        }

        #endregion
    }
}
