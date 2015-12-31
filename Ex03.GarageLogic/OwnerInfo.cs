namespace Ex03.GarageLogic
{
    public sealed class OwnerInfo
    {
        private string m_Name;
        private string m_PhoneNumber;

        public string PhoneNumber
        {
            get
            {
                return this.m_PhoneNumber;
            }

            set
            {
                this.m_PhoneNumber = value;
            }
        }

        public string Name
        {
            get
            {
                return this.m_Name;
            }

            set
            {
                this.m_Name = value;
            }
        }

        public OwnerInfo(string i_Name, string i_PhoneNumber)
        {
            m_Name = i_Name;
            m_PhoneNumber = i_PhoneNumber;
        }

        public override string ToString()
        {
            return string.Format(
@"
Owner Name: {0}
Owner Phone Number: {1}",
                        this.m_Name,
                        this.m_PhoneNumber);
        }
    }
}
