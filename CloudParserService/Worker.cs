using CloudParserService.Lib;
using CloudParserService.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CloudParserService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IDBWriter _writer;
        private readonly IParser _parser;
        private readonly Settings _settings;

        private readonly IConvert _converter;

        public Worker(ILogger<Worker> logger, IDBWriter writer, IParser parser, IConvert convert, Settings settings)
        {
            _logger = logger;
            _writer = writer;
            _parser = parser;
            _settings = settings;
            _converter = convert;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(LogInfo.ServiceStarted, "Worker started at: {time}", DateTimeOffset.Now);
            while (!stoppingToken.IsCancellationRequested)
            {
                var data = await _parser.GetData(_settings.uri);
                _logger.LogInformation(LogInfo.DataParsed, "Parser get {count} entries", data.Count());

                if (data.Count() > 0)
                {
                    var processedData = data.AsParallel().Select(x => _converter.Convert(x)).ToList();
                    _logger.LogInformation(LogInfo.DataParsed, "Plaintext data converted to objects");

                    await _writer.WriteAll(processedData);
                    _logger.LogInformation(LogInfo.DataWritten, "Worker writed at: {time}", DateTimeOffset.Now);
                }
                else
                {
                    _logger.LogCritical(LogInfo.Error, "No data parsed");
                }
                await Task.Delay(30 * 1000, stoppingToken);
            }

            _logger.LogInformation(LogInfo.ServiceStopped, "Worker stopped at: {time}", DateTimeOffset.Now);
        }
    }
}
