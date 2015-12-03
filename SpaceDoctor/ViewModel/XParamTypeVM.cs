using SpaceDoctor.Model;
using System;

/***********************************************
    Wrapper for the XParamType class.

    ----------------------------------------
    Autor: Kovalev Alexander
    Email: koalse@gmail.com
    Date: 01.11.2015
************************************************/

namespace SpaceDoctor.ViewModel
{
    /// <summary>
    /// Wrapper for the XParamType class
    /// </summary>
    public class XParamTypeVM 
    {

        readonly XParamType _paramType;

        public XParamTypeVM ()
        {
            _paramType = new XParamType();
        }

        public XParamTypeVM(XParamType paramType)
        {
            if (paramType == null)
                throw new ArgumentNullException("Параметр paramType не может быть null");
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
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("Название параметра не может быть пустым!");
                else
                    ParamType.Name = value;
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
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("Коллега, укажите размерность параметра, пожалуйста!");
                else
                    ParamType.Measure = value;
            }
        }

        /// <summary>
        /// Does this ParamType in the ExamType
        /// </summary>
        public Boolean SelectToNewExam { get; set; }

    }
}
