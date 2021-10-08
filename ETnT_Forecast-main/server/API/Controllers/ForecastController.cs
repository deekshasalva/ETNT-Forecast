using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoWrapper.Wrappers;
using Common;
using Common.Commands;
using Common.Query;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/forecast")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IMediator _mediator;

        public ForecastController(IMediator mediator, IWebHostEnvironment environment)
        {
            _mediator = mediator;
            _environment = environment;
        }

        /// <summary>
        ///     Get All Forecasts by FyYear
        /// </summary>
        /// <returns></returns>
        [HttpGet("{fyYear:int}")]
        public async Task<ApiResponse> GetAllForecastByFyYear(int fyYear)
        {
            var response = await _mediator.Send(new GetForecastsByFyYearQuery(fyYear));
            return response.Any()
                ? new ApiResponse(response.OrderBy(x=>x.Id))
                : new ApiResponse((int) HttpStatusCode.NoContent,
                    $"No Records Found for Year:{fyYear}");
        }

        /// <summary>
        ///     Add and update forecast
        /// </summary>
        /// <param name="forecasts"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> AddForecasts(AddUpdateForecastCommand forecasts)
        {
            var response = await _mediator.Send(forecasts);
            return !response.Any()
                ? new ApiResponse()
                : new ApiResponse((int) HttpStatusCode.BadRequest, response);
        }

        /// <summary>
        ///     Delete forecast by id and fyyear
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}/year/{year:int}")]
        public async Task<ApiResponse> DeleteForecast(Guid id,int year)
        {
            return await _mediator.Send(new DeleteForecastCommand(id,year))
                ? new ApiResponse()
                : new ApiResponse((int) HttpStatusCode.NotFound, $"Forecast {id} not found");
        }

        /// <summary>
        ///     Process forecast file upload
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("upload")]
        public async Task<ApiResponse> UploadFile([FromForm] IFormFile file)
        {
            var uploads = Path.Combine(_environment.ContentRootPath, "uploads");
            if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
            var fileName = $"{DateTime.UtcNow:yyyy-M-d}_{Guid.NewGuid()}.xlsx";
            await using (Stream fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var command = new FileUploadCommand(fileName);
            _mediator.Enqueue(command);

            return new ApiResponse(command.Id);
        }

        /// <summary>
        ///     Get file upload status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("task/{id:guid}")]
        public async Task<ApiResponse> GetTaskStatus(Guid id)
        {
            return new ApiResponse(await _mediator.Send(new GetTaskStatusQuery(id)));
        }

        /// <summary>
        ///     Get Master Data
        /// </summary>
        /// <returns></returns>
        [HttpGet("lookup")]
        public async Task<ApiResponse> GetAllLookupData()
        {
            return new ApiResponse(await _mediator.Send(new GetLookupQuery()));
        }
    }
}