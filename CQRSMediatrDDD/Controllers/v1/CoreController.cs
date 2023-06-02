using CQRSMediatrDDD.Domain.Contracts.v1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CQRSMediatrDDD.API.Controllers.v1;

public class CoreController : ControllerBase
{
    private readonly INotificationContext _notificationContext;
    private readonly IMediator _mediator;

    public CoreController(INotificationContext notificationContext, IMediator mediator)
    {
        _notificationContext = notificationContext;
        _mediator = mediator;
    }

    protected IActionResult GetResponse(
        object? response =null, HttpStatusCode successCode = HttpStatusCode.OK, HttpStatusCode failCode = HttpStatusCode.BadRequest)
    {
        if(_notificationContext.HasNotifications) return StatusCode((int)failCode, new {Data = response, _notificationContext.Notifications});

        response = response is null ? null : new {Data = response};
        return StatusCode((int)successCode, response);
    }
}
