using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using System.Text.Json;

namespace ClearVolt.DataIA
{
    public class ClearVoltIAService
    {
        private readonly MLContext _mlContext;

        public ClearVoltIAService()
        {
            _mlContext = new MLContext();
        }

        public ITransformer LoadModel()
        {
            var modelPath = @"sensor_model.zip"; //Caminho para o zip do modelo treinado
            var model = _mlContext.Model.Load(modelPath, out var modelSchema);

            // Imprimir esquema do modelo
            foreach (var column in modelSchema)
            {
                Console.WriteLine($"{column.Name} ({column.Type})");
            }
            return model;
            //if (File.Exists(modelPath))
            //{

            //    return _mlContext.Model.Load(modelPath, out var modelSchema);
            //}
            //else
            //{
            //    throw new FileNotFoundException($"O modelo não foi encontrado no caminho: {modelPath}");
            //}
        }

        public void TrainModel()
        {
            var trainingData = new List<SensorData>
            {
                new SensorData { Temperatura = 30, Umidade = 60.0f, Label = 32.0f },
                new SensorData { Temperatura = 35, Umidade = 65.0f, Label = 36.0f },
                new SensorData { Temperatura = 40, Umidade = 70.0f, Label = 42.0f },
                new SensorData { Temperatura = 45, Umidade = 75.0f, Label = 48.0f }
            };

            Console.WriteLine($"Número de amostras de treinamento: {trainingData.Count}");

            var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);

            var preview = dataView.Preview(10);

            foreach (var row in preview.RowView)
            {
                Console.WriteLine($"Temperatura: {row.Values[0].Value}, Umidade: {row.Values[1].Value}, Label: {row.Values[2].Value}");
            }

            var pipeline = _mlContext.Transforms.Concatenate("Features", nameof(SensorData.Temperatura), nameof(SensorData.Umidade))
                      .Append(_mlContext.Regression.Trainers.Sdca(labelColumnName: nameof(SensorData.Label), maximumNumberOfIterations: 100));

            var model = pipeline.Fit(dataView);
            _mlContext.Model.Save(model, dataView.Schema, "sensor_model.zip");

            var testData = new SensorData
            {
                Temperatura = 40.0f,
                Umidade = 70.0f
            };

            var predictionEngine = _mlContext.Model.CreatePredictionEngine<SensorData, SensorPrediction>(model);

            var prediction = predictionEngine.Predict(testData);
            Console.WriteLine($"Temperatura Prevista: {prediction.TemperaturaPrevista}, Umidade Prevista: {prediction.UmidadePrevista}");
        }

        private List<SensorData> LoadDataFromJson(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<SensorData>>(json);
        }
    }
}
