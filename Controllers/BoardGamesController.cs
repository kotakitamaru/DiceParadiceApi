using AutoMapper;
using DiceParadiceApi.ModelBinders;
using DiceParadiceApi.Models;
using DiceParadiceApi.Models.DataTransferObjects;
using DiceParadiceApi.Models.RequestFeatures;
using DiceParadiceApi.Repository;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DiceParadiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardGamesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger<BoardGamesController> _logger;
        private readonly IMapper _mapper;

        public BoardGamesController(IRepositoryManager repository, ILogger<BoardGamesController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        [ResponseCache(Duration = 180)]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)] 
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<ActionResult> Get([FromQuery] BoardGamesParameters boardGamesParameters)
        {
            if (!boardGamesParameters.ValidAgeRange)
            {
                return BadRequest("Max price can't be less than price age.");
            }
            var boardGames = await _repository.BoardGame.GetAllAsync(boardGamesParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(boardGames.MetaData));
            var boardGameDtos = _mapper.Map<IEnumerable<BoardGameDto>>(boardGames);
            return Ok(boardGameDtos);
        }
        
        [HttpGet("{id}", Name = "BoardGameById")]
        [ResponseCache(Duration = 180)]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)] 
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<ActionResult> Get(Guid id)
        {
            var boardGame = await _repository.BoardGame.GetByIdAsync(id);
            if (boardGame.IsNull())
            {
                _logger.LogInformation("BoardGame with id: {Id} doesn't exist in the database", id);
                return NotFound();
            }
            var boardGameDto = _mapper.Map<BoardGameDto>(boardGame);
            return Ok(boardGameDto);
        }

        [HttpGet("collection/{ids}", Name = "BoardGameCollection")]
        public async Task<ActionResult> Get([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            var boardGames = await _repository.BoardGame.GetByIdsAsync(ids);
            if(ids.Count() != boardGames.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<BoardGameDto>>(boardGames));
        }
        
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] BoardGameForCreationDto boardGame)
        {
            if (boardGame == null)
            {
                _logger.LogError("BoardGameDto object sent from client for creation is null");
                return BadRequest("BoardGameDto object is null");
            }
            if(!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the BoardGameForCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var boardGameEntity = _mapper.Map<BoardGame>(boardGame);
            _repository.BoardGame.Add(boardGameEntity);
            await _repository.SaveAsync();
            return CreatedAtRoute("BoardGameById", new { id = boardGameEntity.Id }, boardGame);
        }
        [HttpPost("collection")]
        public async Task<ActionResult> Post([FromBody] IEnumerable<BoardGameForCreationDto> boardGames)
        {
            if (boardGames == null)
            {
                _logger.LogError("BoardGames collection sent from client is null");
                return BadRequest("BoardGame collection is null");
            }
            if(!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the BoardGameForCreationDto object");
                return UnprocessableEntity(ModelState);
            }
            
            var boardGameEntities = _mapper.Map<IEnumerable<BoardGame>>(boardGames).ToArray();
            foreach (var boardGame in boardGameEntities)
            {
                _repository.BoardGame.Add(boardGame);
            }
            await _repository.SaveAsync();
            
            var ids = string.Join(",", boardGameEntities.Select(c => c.Id));
            return CreatedAtRoute("BoardGameCollection", new { ids }, boardGames);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var boardGame = await _repository.BoardGame.GetByIdAsync(id);
            if (boardGame == null)
            {
                _logger.LogInformation("BoardGame with Id: {Id} doesn't exist", id);
                return NotFound();
            }
            _repository.BoardGame.Delete(boardGame);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] BoardGameForUpdateDto boardGame)
        {
            if (boardGame == null)
            {
                _logger.LogError("BoardGameForUpdateDto object sent from client is null");
                return BadRequest("BoardGameForUpdateDto object is null");
            }
            if(!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the BoardGameForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }
            var boardGameEntity = await _repository.BoardGame.GetByIdAsync(id,true);
            if (boardGameEntity == null)
            {
                _logger.LogInformation("Employee with id: {Id} doesn\'t exist in the database", id);
                return NotFound();
            }

            _mapper.Map(boardGame, boardGameEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
