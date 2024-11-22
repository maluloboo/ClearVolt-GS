using ClearVolt.Data.Data;
using ClearVolt.DataIA;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;

namespace ClearVolt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IaController : ControllerBase
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;
        private readonly ClearVoltDbContext _context;
        private readonly ClearVoltIAService _iaService;

        public IaController(ClearVoltIAService iaService, ClearVoltDbContext context)
        {
            _mlContext = new MLContext();
            _iaService = iaService;

            _context = context;

            _model = iaService.LoadModel();
        }

        [HttpPost("IA")]
        public IActionResult GenerateAlert([FromBody] SensorData data)
        {
            var limites = _context.ConfiguracaoColeta.FirstOrDefault(c => c.id_configuracao == data.ConfiguracaoId);

            if (limites == null)
            {
                return BadRequest("Configuração não encontrada para o ID fornecido.");
            }

            var predictionEngine = _mlContext.Model.CreatePredictionEngine<SensorData, SensorPrediction>(_model);

            var filledData = new SensorData
            {
                Temperatura = data.Temperatura,
                Umidade = data.Umidade,
                Label = data.Umidade,
                ConfiguracaoId = data.ConfiguracaoId
            };

            var prediction = predictionEngine.Predict(filledData);

            string alerta = string.Empty;

            if (prediction.TemperaturaPrevista > limites.temp_max)
            {
                alerta = "Alerta: Temperatura acima do limite configurado.";
            }
            else if (prediction.UmidadePrevista < limites.umidade_min)
            {
                alerta = "Alerta: Umidade abaixo do limite configurado.";
            }
            else
            {
                alerta = "Condições dentro dos parâmetros configurados.";
            }

            return Ok(new
            {
                TemperaturaPrevista = prediction.TemperaturaPrevista,
                UmidadePrevista = prediction.UmidadePrevista,
                Alerta = alerta
            });
        }

    }
}
