namespace cyrka.api.common.errors
{
	public static class GeneralErrors
	{
		public static readonly string UnknownErrorCode = "Unknown";
		public static readonly CodedException UnknownError = new CodedException(GeneralErrors.UnknownErrorCode, GeneralErrors.UnknownErrorCode);

		public static readonly string NotFoundCode = "NotFound";
		public static readonly CodedException NotFoundError = new CodedException(GeneralErrors.NotFoundCode, GeneralErrors.NotFoundCode);
	}
}
