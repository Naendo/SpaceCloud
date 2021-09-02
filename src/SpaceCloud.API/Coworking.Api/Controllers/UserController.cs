using System.Collections.Generic;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Application.Authentication;
using Coworking.Application.Services;
using Coworking.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coworking.Api.Controllers
{
    [ApiController]
    public class UserController
    {
        private readonly IMediator _mediator;


        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Cacheable(5000)]
        [Authorize(Policy = IdentityPolicies.ADMIN_POLICY)]
        [HttpGet("users/get")]
        public async Task<List<OutputUserDto>> GetUsers()
        {
            return await _mediator.Send(new ReadAllUsersQuery());
        }

        [Authorize(Policy = IdentityPolicies.ADMIN_POLICY)]
        [HttpGet("users/get/{userId}")]
        public async Task<OutputUserDetailDto> GetUserById(int userId)
        {
            return await _mediator.Send(new ReadUserQuery(userId));
        }


        [Authorize(Policy = IdentityPolicies.WORKER_POLICY)]
        [HttpGet("user/get")]
        public async Task<OutputUserDto> GetLoggedUser()
        {
            return await _mediator.Send(new ReadLoggedUserQuery());
        }
    }
}