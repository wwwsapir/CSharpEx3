namespace Ex03.GarageLogic
{
     public abstract class Feature
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

          public abstract void SetValue(string i_ValueStr);
          public abstract override string ToString();
     }
}
