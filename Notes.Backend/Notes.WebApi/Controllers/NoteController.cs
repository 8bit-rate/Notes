﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteCommand;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.WebApi.Models;

namespace Notes.WebApi.Controllers
{
	[Produces("application/json")]
	[Route("api/[controller]")]
	public class NoteController : BaseController
	{
		private readonly IMapper _mapper;

		public NoteController(IMapper mapper) => _mapper = mapper;

		/// <summary>
		/// Get the list of notes 
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// GET /note
		/// </remarks>
		/// <returns>Returns NoteListVm</returns>
		/// <response code="200">Success</response>
		/// <response code="401">If the user is anauthorized</response>
		[HttpGet]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<NoteListVm>> GetALl()
		{
			var query = new GetNoteListQuery
			{
				UserId = UserId
			};

			var vm = await Mediator.Send(query);
			return Ok(vm);
		}

		/// <summary>
		/// Gets the note by id
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// GET /note/66A9B35C-D12E-4641-B9FB-5BBFE890D7A1
		/// </remarks>
		/// <param name="id">Note id (guid)</param>
		/// <returns>Returns NoteDetailsVm</returns>
		/// <response code="200">Success</response>
		/// <response code="401">If the user is anauthorized</response>
		[HttpGet("{id}")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
		{
			var query = new GetNoteDetailsQuery
			{
				UserId = UserId,
				Id = id
			};

			var vm = await Mediator.Send(query);
			return Ok(vm);
		}

		/// <summary>
		/// Creates the note
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// POST /note
		/// {
		///		title: "note title",
		///		details: "note details"
		/// }
		/// </remarks>
		/// <param name="createNoteDto">CreateNoteDto object</param>
		/// <returns>Return id (guid)</returns>
		/// <response code="201">Success</response>
		/// <response code="401">If the user is unauthorized</response>
		[HttpPost]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
		{
			var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
			command.UserId = UserId;
			var noteId = await Mediator.Send(command);
			return Ok(noteId);
		}

		/// <summary>
		/// Updates the note
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// PUT /note
		/// {
		///		title: "updated note title",
		/// }
		/// </remarks>
		/// <param name="updateNoteDto">UpdateNoteDto object</param>
		/// <returns>Returns NoContent</returns>
		/// <response code="204">Success</response>
		/// <response code="401">If the user is unauthorized</response>
		[HttpPut]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Update([FromBody] UpdateNoteDto updateNoteDto)
		{
			var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
			command.UserId = UserId;
			await Mediator.Send(command);
			return NoContent();
		}

		/// <summary>
		/// Delete the note by id
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// DELETE /note/7EA88A6F-F8F3-465E-A42A-A742BBEC5D25
		/// </remarks>
		/// <param name="id">Id of the note (guid)</param>
		/// <returns>Returns NoContent</returns>
		/// <response code="204">Success</response>
		/// <response code="401">If the user is unauthorized</response>
		[HttpDelete("{id}")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Delete(Guid id)
		{
			var command = new DeleteNoteCommand
			{
				Id = id,
				UserId = UserId
			};

			await Mediator.Send(command);
			return NoContent();
		}
	}
}
