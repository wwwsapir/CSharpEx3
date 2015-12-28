using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    using System.Text;

    public class VehicleInfo
    {
        private readonly VehicleCreator.eVehicleType r_VehicleType;
        private string m_ModelName;
        private EnergySource m_EnergySource;
        private List<Tire> m_TiresList;
        private List<Feature> m_FeaturesList;

        public VehicleInfo(VehicleCreator.eVehicleType i_VehicleType, byte i_NumOfTires, byte i_NumOfFeatures)
        {
            this.r_VehicleType = i_VehicleType;     // UI already checked via VehicleCreator function that the vehicle type is valid
            m_TiresList = new List<Tire>(i_NumOfTires);
            m_FeaturesList = new List<Feature>(i_NumOfFeatures);
        }

        public void FillTiresToMax()
        {
            foreach (Tire tire in m_TiresList)
            {
                float airAmountToReachMax = tire.MaxAirPressure - tire.CurrAirPressure;
                tire.InflateTire(airAmountToReachMax);
            }
        }

        public string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        public EnergySource EnergySource
        {
            get { return m_EnergySource; }
            set { m_EnergySource = value; }
        }

        public List<Tire> TiresList
        {
            get { return m_TiresList; }
            set { m_TiresList = value; }
        }

        public List<Feature> FeaturesList
        {
            get { return m_FeaturesList; }
            set { m_FeaturesList = value; }
        }

        public VehicleCreator.eVehicleType VehicleType
        {
            get
            {
                return this.r_VehicleType;
            }
        }

        public override string ToString()
        {
            StringBuilder tiresInfo = new StringBuilder();
            StringBuilder featuresInfo = new StringBuilder();
            foreach (Tire tire in this.m_TiresList)
            {
                tiresInfo.Append(tire.ToString());
            }

            foreach (Feature feature in this.m_FeaturesList)
            {
                featuresInfo.Append(feature.ToString());
            }

            return string.Format(
@"
Vehicle Type: {0}
Model Name: {1}
EnergySource: {2}
Tires Info {3}
Extra Information {4}",
                   VehicleCreator.VehicleTypesDesc[(int)this.r_VehicleType],
                   this.m_ModelName,
                   this.m_EnergySource.ToString(),
                   tiresInfo,
                   featuresInfo);
        }
    }
}
