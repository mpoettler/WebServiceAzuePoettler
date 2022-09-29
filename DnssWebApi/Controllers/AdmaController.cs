using DnssWebApi.Dto;
using DnssWebApi.Interfaces;
using DnssWebApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnssWebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api")]
    public class AdmaController : ControllerBase
    {
        private readonly IInMemModelRepo repository;
        private readonly ILogger<AdmaController> logger;


        public AdmaController(IInMemModelRepo repository, ILogger<AdmaController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns all the current Models</returns>
        [HttpGet]
        public async Task<IEnumerable<AdmaModel>> GetModels()
        {
            //await Properties.StartMessuarementAsync();
            return (await repository.GetModels()).Select(model => model.AsDtoAdmaModel());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdmaModel>> GetModel(int id)
        {
            var model = await repository.GetModel(id);
            if (model is null)
            {
                return NotFound();
            }
            return Ok(model);
        }



        [HttpPost]
        public async Task<ActionResult<AdmaModelDto>> CreateModel(CreateModelDto modelDto)
        {
            if (modelDto == null)
            {
                throw new ArgumentNullException("AdmaModel is null");
            }

            AdmaModel model = new()
            {
                Default = modelDto.Default,
                ID = modelDto.ID,
                Inaktiv = modelDto.Inaktiv,
                Minimum = modelDto.Minimum,
                Maximum = modelDto.Maximum,
                Propertiers = modelDto.Propertiers,
                Type = modelDto.Type,
                Resolution = modelDto.Resolution,
                Unit = modelDto.Unit,
                Value = modelDto.Value
            };

            await repository.CreateModel(model);

            return CreatedAtAction(nameof(GetModel), new { id = model.ID }, model.AsDtoAdmaModel());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AdmaModelDto>> UpdateModel(int id,UpdateModelDto modelDto)
        {
            Task<AdmaModel> doesItemExist = repository.GetModel(id);

            if (doesItemExist is null)
            {
                return NotFound();
            }

            AdmaModel updatedItem = new()
            {
                ID = doesItemExist.Result.ID,
                Default = doesItemExist.Result.Default,
                Inaktiv = modelDto.Inaktiv,
                Minimum = modelDto.Minimum,
                Maximum = modelDto.Maximum,
                Propertiers = modelDto.Propertiers,
                Type = modelDto.Type,
                Resolution = modelDto.Resolution,
                Unit = modelDto.Unit,
                Value = modelDto.Value
            };

            await repository.UpdateModel(updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AdmaModelDto>> DeleteModel(int id)
        {
            Task<AdmaModel> doesItemExist = repository.GetModel(id);

            if (doesItemExist is null)
            {
                return NotFound();
            }

            await repository.DeleteModel(id);

            return NoContent();
        }

    }
}
