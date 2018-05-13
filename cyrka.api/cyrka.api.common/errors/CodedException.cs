using System;

namespace cyrka.api.common.errors
{
	public class CodedException : Exception
	{
		public readonly string ErrorCode;

		public readonly string ErrorMessage;

		public CodedException(string errorCode, string errorMessage = null)
		{
			ErrorCode = errorCode ?? GeneralErrors.UnknownErrorCode;
			ErrorMessage = errorMessage ?? ErrorCode;
		}
	}
}
