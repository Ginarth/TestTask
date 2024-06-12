using Microsoft.AspNetCore.Mvc;
using SupportOperatorsSalaryAPI.Data.Database.Entities;
using SupportOperatorsSalaryAPI.Data.Repositories.Interfaces;
using SupportOperatorsSalaryAPI.Data.DTOs;
using SupportOperatorsSalaryAPI.Models;

namespace SupportOperatorsSalaryAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class OperatorsSalaryController(ISupportOperatorRepository operatorRepository, IParameterRepository parameterRepository, IBaseRateRepository baseRateRepository) : Controller
    {
        private readonly ISupportOperatorRepository _supportOperatorRepository = operatorRepository;
        private readonly IParameterRepository _parameterRepository = parameterRepository;
        private readonly IBaseRateRepository _baseRateRepository = baseRateRepository;

        [HttpPost]
        [Route("support_operators")]
        public async Task<IActionResult> CreateSupportOperator(CreatedSupportOperator createdSupportOperator)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            SupportOperator? supportOperator = await _supportOperatorRepository.Create(createdSupportOperator.Convert());

            if (supportOperator is null)
                return Problem("Support operator was not created");

            return CreatedAtAction(nameof(ReadSupportOperator), new { id = supportOperator.Id }, supportOperator);
        }

        [HttpPut]
        [Route("support_operators")]
        public async Task<IActionResult> UpdateSupportOperator(UpdatedSupportOperator updatedSupportOperator)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            SupportOperator? supportOperator = await _supportOperatorRepository.Update(updatedSupportOperator.Convert());

            if (supportOperator is null)
                return NotFound("Support operator not found");

            return CreatedAtAction(nameof(ReadSupportOperator), new { id = supportOperator.Id }, supportOperator);
        }

        [HttpPatch]
        [Route("support_operators")]
        public async Task<IActionResult> PatchSupportOperator(PatchedSupportOperator patchedSupportOperatorContainer)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            SupportOperator? supportOperator = await _supportOperatorRepository.Patch(patchedSupportOperatorContainer.Id, patchedSupportOperatorContainer.IsWorking);

            if (supportOperator is null)
                return NotFound("Support operator not found");

            return CreatedAtAction(nameof(ReadSupportOperator), new { id = supportOperator.Id }, supportOperator);
        }

        [HttpGet]
        [Route("support_operators/{id}")]
        public async Task<IActionResult> ReadSupportOperator(int id)
        {
            SupportOperator? supportOperator = await _supportOperatorRepository.Read(id);

            if (supportOperator is null)
                return NotFound("Support operator not found");

            return Ok(supportOperator);
        }

        [HttpPut]
        [Route("salary/parameters")]
        public async Task<IActionResult> UpdateParameters(UpdatedParametersContainer updatedParametersContainer)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            Parameter[] parameters = new Parameter[Parameter.Names.Length];

            for (int i = 0; i < updatedParametersContainer.UpdatedParameters.Length; i++)
            {
                Parameter? parameter = await _parameterRepository.Update(updatedParametersContainer.UpdatedParameters[i].Convert());

                if (parameter is null)
                    return NotFound($"{updatedParametersContainer.UpdatedParameters[i].Name} parameter not found");

                parameters[i] = parameter;
            }

            return Ok(parameters);
        }

        [HttpGet]
        [Route("support_operators/salaries")]
        public async Task<IActionResult> ReadSalaries()
        {
            List<SupportOperator> supportOperators = new (await _supportOperatorRepository.Read());

            if (supportOperators.Count == 0)
                return NotFound("Support operators not found");

            List<Parameter> parametersList = new (await _parameterRepository.Read());
            Dictionary<string, Parameter> parametersDictionary = parametersList.ToDictionary(p => p.Name);

            string[] missingParameterNames = parametersDictionary.Keys.ToList().Except(Parameter.Names.ToList()).ToArray();
            if (missingParameterNames.Length > 0)
                return NotFound($"Parameters not found:\n{string.Join("\n", missingParameterNames)}");

            List<BaseRate> baseRatesList = new (await _baseRateRepository.Read());
            Dictionary<string, BaseRate> baseRatesDictionary = baseRatesList.ToDictionary(r => r.Position);

            string[] missingBaseRatesPositions = baseRatesDictionary.Keys.ToList().Except(BaseRate.Positions.ToList()).ToArray();
            if (missingBaseRatesPositions.Length > 0)
                return NotFound($"Base rates not found:\n{string.Join("\n", missingBaseRatesPositions)}");

            List<SupportOperatorSalary> salaries = [];
            foreach (SupportOperator supportOperator in supportOperators)
            {
                salaries.Add(new SupportOperatorSalary(supportOperator, parametersDictionary, baseRatesDictionary));
            }

            return Ok(salaries);
        }
    }
}
