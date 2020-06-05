using OnlineLearning.EntityFramework.Context;
using OnlineLearning.Model;
using OnlineLearning.Shared;
using OnlineLearning.Shared.Enums;
using OnlineLearning.Shared.Interface.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineLearning.EntityFramework.Seed
{
	public class DatabaseSeed
	{
		public static void Seed(ApplicationDatabaseContext context, IPasswordHasher passwordHasher)
		{
			context.Database.EnsureCreated();

			if (context.Users.Count() == 0)
			{
				var users = new List<User>
				{
					new User {
						Email = "superadmin@mail.com",
						Password = passwordHasher.HashPassword("superadmin"),
						UserName = "superadmin",
						FullName = "Super Admin",
						UserRole = Role.SuperAdmin.ToString(),
						//Active = true,
						IsVerified = true
						}
				};
				context.Users.AddRange(users);
				context.SaveChanges();
			}

			if (context.SubmissionStatus.Count() == 0)
			{
				var submission = new List<SubmissionStatus>
				{
					new SubmissionStatus {
						 SubmissionStatusDesc = "Pending"
						},
					new SubmissionStatus {
						 SubmissionStatusDesc = "Submitted"
						},
					new SubmissionStatus {
						 SubmissionStatusDesc = "Reviewed"
						},
					new SubmissionStatus {
						 SubmissionStatusDesc = "Redo"
						},
				};
				context.SubmissionStatus.AddRange(submission);
				context.SaveChanges();
			}

			if (context.ReferenceTypes.Count() == 0)
			{
				var references = new List<ReferenceType>
				{
					new ReferenceType {
						 ReferenceDescription = "Youtube"
						},
					new ReferenceType {
						 ReferenceDescription = "PDF/Word Attachment"
						},
					new ReferenceType {
						 ReferenceDescription = "Other Link"
						},
				};
				context.ReferenceTypes.AddRange(references);
				context.SaveChanges();
			}
		}
	}
}