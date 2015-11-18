using SpaceDoctor.Model;
using System;

namespace SpaceDoctor.ViewModel
{
    public class XParamTypeVM : XViewModelBase
    {

        readonly XParamType _paramType;

        public XParamTypeVM ()
        {
            _paramType = new XParamType();
        }

        public XParamTypeVM(XParamType paramType)
        {
            _paramType = paramType;
        }

        public XParamType ParamType
        {
            get
            {
                return _paramType;
            }
        }

        public String Name 
        {
            get
            {
                return ParamType.Name;
            }

            set 
            {
                ParamType.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public String Measure 
        {
            get 
            {
            return ParamType.Measure;
            }
            set 
            {
                ParamType.Measure = value;
                RaisePropertyChanged("Measure");
            }
        }


        public Boolean SelectToNewExam { get; set; }

    }
}
