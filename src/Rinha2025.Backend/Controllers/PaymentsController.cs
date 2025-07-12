using Microsoft.AspNetCore.Mvc;
using Rinha2025.Backend.MessageBus;
using Rinha2025.Backend.Models;
using System.Text.Json;

namespace Rinha2025.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly ILogger<PaymentsController> _logger;
        private readonly IMessageBus _messageBus;

        public PaymentsController(ILogger<PaymentsController> logger, IMessageBus messageBus)
        {
            _logger = logger;
            _messageBus = messageBus;
        }

        [HttpPost]
        public async Task<IActionResult> Post(PaymentsViewModel viewModel)
        {
            _logger.LogInformation("Received POST /payments: CorrelationId={CorrelationId}, Amount={Amount}", viewModel.CorrelationId, viewModel.Amount);
            
            viewModel.RequestedAt = DateTime.UtcNow;
            await _messageBus.Publish(JsonSerializer.Serialize(viewModel));
            
            return Accepted();
        }
    }
}
