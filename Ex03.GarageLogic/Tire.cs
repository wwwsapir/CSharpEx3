namespace Ex03.GarageLogic
{
    public sealed class Tire
     {
          private readonly float r_MaxAirPressure;
          private string m_ManufacturerName;
          private float m_CurrAirPressure;

          public string ManufacturerName
          {
               get { return m_ManufacturerName; }
               set { m_ManufacturerName = value; }
          }

          public float CurrAirPressure
          {
              get { return m_CurrAirPressure; }
              set  //The current air pressure is set only if it's a legal value
              {
                  if (value > this.r_MaxAirPressure)
                  {
                      string exceptionStr = string.Format(
                         "The given Air Pressure is not valid. Max Air Pressure is {0}",
                         this.r_MaxAirPressure);
                      throw new ValueOutOfRangeException(exceptionStr, value, 0, this.r_MaxAirPressure);
                  }

                  if (value < 0)
                  {
                      throw new ValueOutOfRangeException("Air Pressure cannot be negative.", value, 0, this.r_MaxAirPressure);
                  }

                  this.m_CurrAirPressure = value;
              }
          }

        public float MaxAirPressure
          {
               get { return r_MaxAirPressure; }
          }

          public Tire(float i_MaxAirPressure)
          {
               r_MaxAirPressure = i_MaxAirPressure;
          }

          public override string ToString()
          {
               return string.Format(
@"Manufacturer: {0} Current Air Pressure: {1} Max Air Pressure: {2}",
                      m_ManufacturerName,
                      m_CurrAirPressure,
                      r_MaxAirPressure);
          }
     }
}
