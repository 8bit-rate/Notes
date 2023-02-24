using AutoMapper;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Persistence;
using Notes.Tests.Common;
using Shouldly;

namespace Notes.Tests.Notes.Queries
{
	[Collection("QueryCollection")]
	public class GetNoteDetailsQueryHandlerTests
	{
		private readonly NotesDbContext Context;
		private readonly IMapper Mapper;

		public GetNoteDetailsQueryHandlerTests(QueryTestFixture fixture) =>
			(Context, Mapper) = (fixture.Context, fixture.Mapper);

		[Fact]
		public async Task GetNoteDetailsQueryHandler_Success()
		{
			// Arrange
			var handler = new GetNoteDetailsQueryHandler(Context, Mapper);

			// Act
			var result = await handler.Handle(
				new GetNoteDetailsQuery
				{
					UserId = NotesContextFactory.UserBId,
					Id = Guid.Parse("E73ADD77-C368-4276-A646-2598D1D009F2")
				},
				CancellationToken.None);

			// Assert
			result.ShouldBeOfType<NoteDetailsVm>();
			result.Title.ShouldBe("Test title 2");
			result.CreationDate.ShouldBe(DateTime.Today);
		}
	}
}
