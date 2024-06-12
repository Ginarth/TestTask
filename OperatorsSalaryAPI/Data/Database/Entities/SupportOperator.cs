namespace SupportOperatorsSalaryAPI.Data.Database.Entities
{
    public class SupportOperator
    {
        private int _id = 0;
        public int Id 
        { 
            get => _id; 
            set
            {
                if (value > 0)
                {
                    _id = value;
                }
            }
        }

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

        public bool IsWorking { get; set; } = false;

        private short _firstResponseTime = 0;
        public short FirstResponseTime 
        { 
            get => _firstResponseTime; 
            set
            {
                if (value > -1)
                    _firstResponseTime = value;
            }
        }

        private short _responseTime;
        public short ResponseTime 
        { 
            get => _responseTime; 
            set
            {
                if (value > -1)
                    _responseTime = value;
            }
        }

        private short _competencyAssessment;
        public short CompetencyAssessment 
        { 
            get => _competencyAssessment; 
            set
            {
                if (value > -1)
                    _competencyAssessment = value;
            }
        }

        private short _politenessAssessment;
        public short PolitenessAssessment 
        { 
            get => _politenessAssessment; 
            set
            {
                if (value > -1)
                    _politenessAssessment = value;
            }
        }
    }
}
