namespace SupportOperatorsSalaryAPI.Data.Database.Entities
{
    public class Parameter
    {
        public static readonly string[] Names = ["first_response_time", "response_time", "competency_assessment", "politeness_assessment"];

        private string _name = "";
        public string Name 
        {
            get => _name;
            set
            {
                if (value is not null && value.Length < 51)
                    _name = value;
            }
        }

        private decimal _weight;
        public decimal Weight 
        { 
            get => _weight;
            set
            {
                if (_weight > 0.00M || _weight < 1.00M || BitConverter.GetBytes(decimal.GetBits(value)[3])[2] < 3)
                    _weight = value;   
            }
        }

        private short _baseValue;
        public short BaseValue 
        { 
            get => _baseValue;
            set
            {
                if (value > 0)
                    _baseValue = value;
            }
        }

        private short _normalValue;
        public short NormalValue 
        { 
            get => _normalValue;
            set
            {
                if (value > 0)
                    _normalValue = value;
            }
        }
    }
}
