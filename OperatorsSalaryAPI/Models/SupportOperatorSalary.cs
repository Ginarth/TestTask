using SupportOperatorsSalaryAPI.Data.Database.Entities;

namespace SupportOperatorsSalaryAPI.Models
{
    public class SupportOperatorSalary
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public bool IsWorking { get; private set; }
        public int Amount { get; private set; }
        public int FinalAmount { get; private set; }

        public SupportOperatorSalary(SupportOperator supportOperator, Dictionary<string, Parameter> parametersDictionary, Dictionary<string, BaseRate> baseRatesDictionary)
        {
            Id = supportOperator.Id;
            Name = supportOperator.Name;
            IsWorking = supportOperator.IsWorking;
            Amount = baseRatesDictionary["support_operator"].Amount;
            FinalAmount = 0;

            if (IsWorking)
            {
                string key = string.Empty;
                decimal weight, coefficient = 0, baseAmount = baseRatesDictionary["support_operator"].Amount;
                short currentValue = 0, baseValue, normalValue;

                foreach (string parameter_names in Parameter.Names)
                {
                    switch (parameter_names)
                    {
                        case ("first_response_time"):
                            key = "first_response_time";
                            currentValue = supportOperator.FirstResponseTime;
                            break;
                        case ("response_time"):
                            key = "response_time";
                            currentValue = supportOperator.ResponseTime;
                            break;
                        case ("competency_assessment"):
                            key = "competency_assessment";
                            currentValue = supportOperator.CompetencyAssessment;
                            break;
                        case ("politeness_assessment"):
                            key = "politeness_assessment";
                            currentValue = supportOperator.PolitenessAssessment;
                            break;
                    }

                    baseValue = parametersDictionary[key].BaseValue;
                    normalValue = parametersDictionary[key].NormalValue;
                    weight = parametersDictionary[key].Weight;

                    coefficient += weight * СalculateСoefficient(currentValue, baseValue, normalValue);
                }

                FinalAmount = (int) Math.Truncate(coefficient * baseAmount);
            }

            static decimal СalculateСoefficient(short currentValue, short baseValue, short normalValue, short baseCoef = 1, short normalCoef = 2)
            {
                decimal x0 = baseValue, y0 = baseCoef, x1 = normalValue, y1 = normalCoef,
                    a = (y0 - y1) / (x0 - x1),
                    b = y0 - x0 * a,
                    x = currentValue,
                    y = a * x + b;

                return y > 2M ? 2M : y;
            }
        }
    }
}
