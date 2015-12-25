namespace Ex03.GarageLogic
{
     public sealed class Tire
     {
          private string m_ManufacturerName;
          private float m_CurrAirPressure;
          private float m_MaxAirPressure;

          public string ManufacturerName
          {
               get { return m_ManufacturerName; }
               set { m_ManufacturerName = value; }
          }

          public float CurrAirPressure
          {
               get { return m_CurrAirPressure; }
               set { m_CurrAirPressure = value; }
          }

          public float MaxAirPressure
          {
               get { return m_MaxAirPressure; }
               set { m_MaxAirPressure = value; }
          }

          public Tire(string i_ManufacturerName, float i_CurrAirPressure, float i_MaxAirPressure)
          {
               m_ManufacturerName = i_ManufacturerName;
               m_CurrAirPressure = i_CurrAirPressure;
               m_MaxAirPressure = i_MaxAirPressure;
          }

          public void FillAirToMax()
          {
               m_CurrAirPressure = m_MaxAirPressure;
          }

          public override string ToString()
          {
               return string.Format(@"Manufacturer: {0}
Current Air Pressure: {1}
Max Air Pressure: {2}", m_ManufacturerName, m_CurrAirPressure, m_MaxAirPressure);
          }
     }
}
