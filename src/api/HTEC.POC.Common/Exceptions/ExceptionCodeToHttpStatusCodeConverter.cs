using System.Net;

namespace HTEC.POC.Common.Exceptions;

public static class ExceptionCodeToHttpStatusCodeConverter
{
    internal static HttpStatusCode GetHttpStatusCode(int exceptionCode)
    {
        switch ((ExceptionCode)exceptionCode)
        {
            case Exceptions.ExceptionCode.UnauthorizedOperation:
                return HttpStatusCode.Unauthorized;
            case Exceptions.ExceptionCode.ForbiddenOperation:
                return HttpStatusCode.Forbidden;
            case Exceptions.ExceptionCode.BadRequest:
                return HttpStatusCode.BadRequest;
            case Exceptions.ExceptionCode.NotFound:
                return HttpStatusCode.NotFound;
            case Exceptions.ExceptionCode.Conflict:
                return HttpStatusCode.Conflict;
            case Exceptions.ExceptionCode.FeatureDisabled:
                return HttpStatusCode.NotFound;
            case Exceptions.ExceptionCode.CircuitBreakerEnabled:
                return HttpStatusCode.ServiceUnavailable;

            //Business related
            case Exceptions.ExceptionCode.RewardsAlreadyExists:
            case Exceptions.ExceptionCode.CategoryAlreadyExists:
            case Exceptions.ExceptionCode.RewardsItemAlreadyExists:
                return HttpStatusCode.Conflict;

            case Exceptions.ExceptionCode.RewardsDoesNotExist:
            case Exceptions.ExceptionCode.CategoryDoesNotExist:
            case Exceptions.ExceptionCode.RewardsItemDoesNotExist:
                return HttpStatusCode.NotFound;

            case Exceptions.ExceptionCode.RewardsItemPriceMustNotBeZero:
            default:
                return HttpStatusCode.BadRequest;
        }
    }
}
