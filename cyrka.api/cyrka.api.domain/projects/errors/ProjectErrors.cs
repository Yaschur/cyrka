using cyrka.api.common.errors;

namespace cyrka.api.domain.projects.errors
{
	public static class ProjectErrors
	{
		public static readonly string JobNotFoundErrorCode = "JobNotFound";
		public static readonly CodedException JobNotFoundError = new CodedException(
			ProjectErrors.JobNotFoundErrorCode,
			ProjectErrors.JobNotFoundErrorCode
		);

		public static readonly string JobAlreadySetErrorCode = "JobAlreadySet";
		public static readonly CodedException JobAlreadySetError = new CodedException(
			ProjectErrors.JobAlreadySetErrorCode,
			ProjectErrors.JobAlreadySetErrorCode
		);
	}
}
