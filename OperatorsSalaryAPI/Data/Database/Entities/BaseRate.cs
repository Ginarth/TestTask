namespace SupportOperatorsSalaryAPI.Data.Database.Entities
{
    public class BaseRate
    {
        public static readonly string[] Positions = ["support_operator"];

        private string _position = "";
        public string Position
        {
            get => _position;
            set
            {
                if (value is not null && value.Length < 51)
                    _position = value;
            }
        }

        private int _amount;
        public int Amount
        {
            get => _amount;
            set
            {
                if (value > 0)
                    _amount = value;
            }
        }
    }
}
