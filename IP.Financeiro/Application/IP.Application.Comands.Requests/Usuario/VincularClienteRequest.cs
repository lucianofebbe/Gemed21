﻿using IP.Application.Comands.Responses.Usuario;
using IP.BaseDomains;
using MediatR;

namespace IP.Application.Comands.Requests.Usuario
{
    public class VincularClienteRequest : BaseRequest, IRequest<VincularClienteResponse>
    {
        public int ClienteId { get; set; }
    }
}
