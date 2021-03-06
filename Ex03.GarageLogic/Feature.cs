﻿namespace Ex03.GarageLogic
{
     public abstract class Feature  //This is a class that will be used the same for different types of values
     {
          protected string m_Description;
          protected string m_PossibleValues;

          public string Description
          {
               get
               {
                    return string.Format("{0} {1}", m_Description, m_PossibleValues);
               }
          }

          public string PossibleValues
          {
              get
              {
                  return this.m_PossibleValues;
              }
          }

          public override string ToString()     // A function with an injection point
          {
              return string.Format("{0}: {1}", m_Description, Value);
          }

         protected abstract object Value { get; }

         // The function that fills the injection point
          public abstract void SetValue(string i_ValueStr);

         public abstract bool IsValid(string i_InputFeatureValue);
     }
}
