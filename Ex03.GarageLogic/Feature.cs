using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Feature
    {
        protected string m_Description = "";

        public string Description
        {
            get { return m_Description; }
        }

        abstract public void SetValue(string i_ValueStr);
        abstract override public string ToString();
    }
}
