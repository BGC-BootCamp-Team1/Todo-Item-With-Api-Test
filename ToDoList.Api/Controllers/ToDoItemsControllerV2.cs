﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDoList.Api.Models;
using ToDoList.Api.Services;

namespace ToDoList.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v2/[controller]")]
    [AllowAnonymous]
    public class ToDoItemsV2Controller : ControllerBase
    {

        private readonly IToDoItemService _toDoItemService;
        private readonly ILogger<ToDoItemsController> _logger;


        public ToDoItemsV2Controller(IToDoItemService toDoItemService, ILogger<ToDoItemsController> logger)
        {
            _toDoItemService = toDoItemService;
            _logger = logger;

        }



        [HttpPost]
        [ProducesResponseType(typeof(ToDoItemDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Create New Item",
            Description = "Create a new to-do item"
            )]

        public async Task<ActionResult<ToDoItemDto>> PostAsync([FromBody] ToDoItemCreateRequest toDoItemCreateRequest)
        {
            var toDoItemDto = new ToDoItemDto
            {
                Description = toDoItemCreateRequest.Description,
                Done = toDoItemCreateRequest.Done,
                Favorite = toDoItemCreateRequest.Favorite,
                CreatedTime = DateTimeOffset.UtcNow
            };
            await _toDoItemService.CreateAsync(toDoItemDto);
            return toDoItemDto;
        }


      
    }
}