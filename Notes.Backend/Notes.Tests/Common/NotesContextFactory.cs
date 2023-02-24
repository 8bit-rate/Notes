using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using Notes.Persistence;

namespace Notes.Tests.Common
{
	public class NotesContextFactory
	{
		public static Guid UserAId = Guid.NewGuid();
		public static Guid UserBId = Guid.NewGuid();

		public static Guid NoteIdForDelete = Guid.NewGuid();
		public static Guid NoteIdForUpdate = Guid.NewGuid();

		public static NotesDbContext Create()
		{
			var options = new DbContextOptionsBuilder<NotesDbContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString())
				.Options;

			var context = new NotesDbContext(options);
			context.Database.EnsureCreated();
			context.Notes.AddRange
				(
					new Note
					{
						CreationDate = DateTime.Today,
						Details = "Test details 1",
						EditDate = null,
						Id = Guid.Parse("C771208D-7F57-45C3-B836-45EB1E64E183"),
						Title = "Test title 1",
						UserId = UserAId
					},
					new Note
					{
						CreationDate = DateTime.Today,
						Details = "Test details 2",
						EditDate = null,
						Id = Guid.Parse("E73ADD77-C368-4276-A646-2598D1D009F2"),
						Title = "Test title 2",
						UserId = UserBId
					},
					new Note
					{
						CreationDate = DateTime.Today,
						Details = "Test details 3",
						EditDate = null,
						Id = NoteIdForDelete,
						Title = "Test title 3",
						UserId = UserAId
					},
					new Note
					{
						CreationDate = DateTime.Today,
						Details = "Test details 4",
						EditDate = null,
						Id = NoteIdForUpdate,
						Title = "Test title 4",
						UserId = UserBId
					}
				);

			context.SaveChanges();
			return context;
		}

		public static void Destroy(NotesDbContext context)
		{
			context.Database.EnsureDeleted();
			context.Dispose();
		}
	}
}
