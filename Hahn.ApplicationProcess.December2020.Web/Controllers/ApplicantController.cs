using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Domain;
using Hahn.ApplicationProcess.December2020.Domain.Model;
using Hahn.ApplicationProcess.December2020.Service;
using Hahn.ApplicationProcess.December2020.Web.Model.CommandDto;
using Hahn.ApplicationProcess.December2020.Web.Model.ResultDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hahn.ApplicationProcess.December2020.Web.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ApplicantController : ControllerBase
    {
        private ILogger<Applicant> _logger; 
        private readonly IApplicantService _applicantService; 
        public ApplicantController(IApplicantService applicantService, ILogger<Applicant> logger)
        {
            _logger = logger;
            _applicantService = applicantService;
        }
        
        /// <summary>
        /// Create a new applicant
        /// </summary>
        /// <param name="applicant"></param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created item Id</response>
        /// <response code="400">If the operation failed</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateApplicantCommandDto applicantDto)
        {
            try
            {
                var result = await _applicantService.CreateApplicant(applicantDto.ToCreateApplicantCommand());
                return Created(Request.Path, ResultDto<int>.CreateSuccessfulResult(result));
            }
            catch (DomainException ex)
            {
                var result = ResultDto<int>.CreateFailedResult(
                    ex.InnerExceptions.Select(x => x.Message));
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error On creating new applicant: data: {@applicantDto}", applicantDto);
                return StatusCode(500, "Something went wrong on server side");
            }

        }
        
        /// <summary>
        ///   Update applicant 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="applicant"></param>
        /// <returns></returns>
        /// /// <response code="201">Returns the newly created item Id</response>
        /// <response code="400">If the operation failed</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateApplicantCommandDto applicantDto)
        {
            try
            {
                
                await _applicantService.UpdateApplicant(applicantDto.ToUpdateApplicantCommand(id));
                return Ok();
            }
            catch (DomainException ex)
            {
                var result = ResultDto<int>.CreateFailedResult(
                    ex.InnerExceptions.Select(x => x.Message));
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error On Updating applicant: data: {@applicantDto} Id: {Id}", applicantDto,id);
                return StatusCode(500, "Something went wrong on server side");
            }
        }
        
        /// <summary>
        ///     Delete an applicant 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                
                await _applicantService.DeleteApplicant(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error On deleting new applicant: id: {id}", id);
                return StatusCode(500, "Something went wrong on server side");
            }
        }
        
        /// <summary>
        /// Get applicant according to given id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ApplicantDto</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicantDto>> Get([FromRoute] int id)
        {
            try
            {
                var applicant = await _applicantService.GetApplicant(id);
                if (applicant != null)
                    return Ok(ResultDto<ApplicantDto>.CreateSuccessfulResult(ApplicantDto.FromApplicant(applicant)));
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error On get single applicant: id: {id}", id);
                return StatusCode(500, "Something went wrong on server side");
            }
        }
        
        /// <summary>
        /// Get all applicant in system
        /// </summary>
        /// <returns>ApplicantDto[]</returns>
        [HttpGet]
        public async Task<ActionResult<ResultDto<IEnumerable<ApplicantDto>>>> GetAll()
        {
            try
            {
                return ResultDto<IEnumerable<ApplicantDto>>.CreateSuccessfulResult(
                    (await _applicantService.GetAllApplicants()).Select(ApplicantDto.FromApplicant));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error On get all applicants");
                return StatusCode(500, "Something went wrong on server side");
            }
        }
    }
}