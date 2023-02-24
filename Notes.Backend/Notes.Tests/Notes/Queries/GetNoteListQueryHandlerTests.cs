﻿using AutoMapper;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.Persistence;
using Notes.Tests.Common;
using Shouldly;

namespace Notes.Tests.Notes.Queries
{
	[Collection("QueryCollection")]
	public class GetNoteListQueryHandlerTests
	{
		private readonly NotesDbContext Context;
		private readonly IMapper Mapper;

		public GetNoteListQueryHandlerTests(QueryTestFixture fixture) =>
			(Context, Mapper) = (fixture.Context, fixture.Mapper);

		[Fact]
		public async Task GetNoteListQueryHandler_Success()
		{
			// Arrange
			var handler = new GetNoteListQueryHandler(Context, Mapper);

			// Act
			var result = await handler.Handle(
				new GetNoteListQuery
				{
					UserId = NotesContextFactory.UserBId
				},
				CancellationToken.None);

			// Assert
			result.ShouldBeOfType<NoteListVm>();
			result.Notes.Count.ShouldBe(2);
		}
	}
}